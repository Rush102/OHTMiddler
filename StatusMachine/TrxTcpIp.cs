using com.mirle.iibg3k0.ttc.Common.TCPIP;
using com.mirle.iibg3k0.ttc.Data.TcpIpData;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.mirle.iibg3k0.ttc.Common
{
    public class TrxTcpIp
    {
        private int monitorIntervalMilliseconds = 100;
        Boolean hasRtn = false;
        Boolean hasSystemError = false;
        Boolean hasReplyTimeout = false;

        private TcpIpAgent tcpipAgent = null;
        object recvStr = null;

        /// <summary>
        /// TrxTcpIp建構式
        /// </summary>
        /// <param name="agent"></param>
        public TrxTcpIp(TcpIpAgent agent)
        {
            tcpipAgent = agent;
        }

        /// <summary>
        /// 傳送TcpIp Message (Primary)
        /// </summary>
        /// <param name="raw_data"></param>
        /// <returns></returns>
        public bool SendRawData(byte[] raw_data)
        {
            bool isSuccess = true;
            try
            {
                tcpipAgent.SendRawData(raw_data);
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        /// <summary>
        /// 傳送TcpIp Message (Primary)
        /// </summary>
        /// <param name="msgStructure"></param>
        /// <returns></returns>
        public bool SendPrimary<T>(T msgStructure)
        {
            bool isSuccess = true;
            try
            {
                dynamic d_msgStr = msgStructure;
                d_msgStr.SeqNum = tcpipAgent.getSendSeqNum();
                tcpipAgent.SendMessage(d_msgStr);
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
        }
        /// <summary>
        /// 傳送TcpIp Message (Secondary)
        /// </summary>
        /// <param name="msgStructure"></param>
        /// <returns></returns>
        public bool SendSecondary<T>(T msgStructure)
        {
            bool isSuccess = true;
            try
            {
                tcpipAgent.SendMessage(msgStructure);
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
        }






        public ReturnCode sendRecv<TSource, TSource2>(TSource inStr, out TSource2 outStr,
            out string rtnMsg)
        {
            return sendRecv<TSource, TSource2>(inStr, out outStr, out rtnMsg, tcpipAgent.RecvTimeout, tcpipAgent.RetryCount);
        }

        const int NOT_COMPLETE = 0;
        const int COMPLETED = 1;
        public ReturnCode sendRecv<TSource>(List<TSource> inStrs, out string rtnMsg)
        {
            rtnMsg = "";
            COMM_DATA_REPORT_RESP stRecv;
            for (int i = 0; i < inStrs.Count; i++)
            {
                TrxTcpIp.ReturnCode result = sendRecv(inStrs[i], out stRecv, out rtnMsg);
                if (result == ReturnCode.Normal)
                {
                    if (isLastData<TSource>(inStrs, i))
                    {
                        if (stRecv.CompFlag != COMPLETED)
                        {
                            rtnMsg = string.Format("Complete flag is not match.Record:[{0}]", i + 1);
                            return ReturnCode.DataCheckFail;
                        }
                    }
                    else
                    {
                        if (stRecv.CompFlag != NOT_COMPLETE)
                        {
                            rtnMsg = string.Format("Complete flag is not match.Record:[{0}]", i + 1);
                            return ReturnCode.DataCheckFail;
                        }
                    }
                    Console.WriteLine("Send complete,Record:[{0}]", i + 1);
                }
                else
                {
                    rtnMsg = string.Format("Send array message fail.Record:[{0}]", i + 1);
                    return result;
                }
            }

            return ReturnCode.Normal;
        }

        private static bool isLastData<TSource>(List<TSource> inStrs, int i)
        {
            return i == inStrs.Count - 1;
        }

        
        public ReturnCode sendRecv<TSource, TSource2>(TSource inStr, out TSource2 outStr,
        out string rtnMsg,
        int timeoutMSec, int retryCnt)
        {
            byte[] bytPacketDatas = TCPUtility._Str2Packet(inStr);
            int iPacketID = TCPUtility.PrcGetPacketID(bytPacketDatas);
            ushort iSeqNum = 0;
            if (!tcpipAgent.ImsgStateMachine.IsInState(TcpIpAgent.E_Msg_STS.Idle))
            {
                throw new TcpIpStateException(string.Format("The status is incorrect when sent.TcpIp name:{0} ,Pack ID:{1} ,crt state:{2}"
                                                             , tcpipAgent.Name
                                                             , iPacketID
                                                             , tcpipAgent.ImsgStateMachine.State));
            }

            int threadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
            tcpipAgent.onMessageGenerated();
            hasRtn = false;

            outStr = (TSource2)TCPUtility.GetDefault(typeof(TSource2));


            EventHandler<TcpIpEventArgs> handler = null;

            rtnMsg = string.Empty;
            int iReplyPacketID = iPacketID + 100;
            try
            {
                handler = delegate(Object _sender, TcpIpEventArgs _e)
                {
                    replyHandler<TSource, TSource2>(_sender, _e, ref inStr);
                };

                tcpipAgent.addTcpIpReceivedHandler(iReplyPacketID, handler);

                int count = 0;
                do
                {
                    Console.WriteLine(string.Format("Send begin ID:{0}", iPacketID));
                    tcpipAgent.onMessageSent();
                    bool is_success = this.SendPrimary(inStr);
                    if (!is_success)
                    {
                        rtnMsg = "Send message fail.";
                        TcpIpExceptionEventArgs e = new TcpIpExceptionEventArgs(iPacketID, iSeqNum, bytPacketDatas, rtnMsg);
                        tcpipAgent.onErrorHappend(e);
                        return ReturnCode.SendDataFail;
                    }
                    Console.WriteLine(string.Format("Send success ID:{0}", iPacketID));

                    tcpipAgent.onWaitForReply();

                    SpinWait.SpinUntil(() => hasRtn, timeoutMSec);

                    if (!hasRtn)
                    {
                        if (count++ >= retryCnt)
                        {
                            Console.WriteLine(string.Format("Replay timeout ID:{0}", iPacketID));
                            rtnMsg = string.Format("A response timeout occurred. retry count:{0}", count);
                            TcpIpExceptionEventArgs e = new TcpIpExceptionEventArgs(iPacketID, iSeqNum, bytPacketDatas, rtnMsg);
                            tcpipAgent.onReplyTimeout(e);
                            return ReturnCode.Timeout;
                        }
                        Console.WriteLine(string.Format("Send aging ID:{0}, count:{1}", iPacketID, count));
                    }
                } while (!hasRtn);
                outStr = (TSource2)recvStr;
                tcpipAgent.onMessageSentCmp();
            }
            catch (Exception ex)
            {
                rtnMsg = ex.ToString();
                TcpIpExceptionEventArgs e = new TcpIpExceptionEventArgs(iPacketID, iSeqNum, bytPacketDatas, rtnMsg);
                tcpipAgent.onErrorHappend(e);
                return ReturnCode.SendDataFail;
            }
            finally
            {
                tcpipAgent.removeTcpIpReceivedHandler(iReplyPacketID, handler);
                tcpipAgent.onMessageSentFinish();
                Console.WriteLine(string.Format("Send finish ID:{0}", iPacketID));
            }
            return ReturnCode.Normal;
        }


        #region Google ProtoBuf
        public bool SendGoogleMsg<TSource>(TSource msg, bool isReply = false)
            where TSource : IMessage<TSource>
        {
            bool isSuccess = true;
            try
            {
                if (!isReply)
                {
                    int message_num = tcpipAgent.getSendSeqNum();
                    setMsgNumByGoogleMessage(msg, message_num);

                    //recodeLog<TSource>(msg);
                }
                byte[] bytPacketDatas = GoogleMessage2ByteArray(msg);
                this.SendRawData(bytPacketDatas);
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        private static void recodeLog<TSource>(TSource msg)
            where TSource : IMessage<TSource>
        {

            var descriptor = msg.Descriptor;
            var oneof = descriptor.Oneofs[0];
            var value = oneof.Accessor.GetCaseFieldDescriptor(msg);
            foreach (var field in value.MessageType.Fields.InDeclarationOrder())
            {
                //object obj = field.Accessor.GetValue(value);
            }

            StringBuilder sb = new StringBuilder();
            OneofDescriptor specifyOne = msg.Descriptor.Oneofs[0];
            FieldDescriptor specifyField = null;

            foreach (FieldDescriptor field in specifyOne.Fields)
            {
                if (field != null)
                {
                    specifyField = field;
                    break;
                }
            }
            sb.Append("Title:");
            sb.AppendLine(specifyOne.Name);
            foreach (var field in specifyField.MessageType.Fields.InDeclarationOrder())
            {
                field.Accessor.GetValue(msg);
                sb.Append(field.Name);
                sb.Append(":");
                sb.AppendLine("");
            }
        }

        public ReturnCode sendRecv_Google<TSource, TSource2>(TSource inStr, out TSource2 outStr,
          out string rtnMsg)
            where TSource : IMessage<TSource>
            where TSource2 : IMessage<TSource2>, new()
        {
            return sendRecv_Google(inStr, out outStr, out rtnMsg, tcpipAgent.RecvTimeout, tcpipAgent.RetryCount);
        }
        public ReturnCode sendRecv_Google<TSource, TSource2>(TSource inStr, out TSource2 outStr,
          out string rtnMsg,
          int timeoutMSec, int retryCnt)
            where TSource : IMessage<TSource>
            where TSource2 : IMessage<TSource2>, new()
        {
            int iPacketID = getPacketIDByGoogleMessage(inStr);
            ushort iSeqNum = 0;
            if (!tcpipAgent.ImsgStateMachine.IsInState(TcpIpAgent.E_Msg_STS.Idle))
            {
                throw new TcpIpStateException(string.Format("The status is incorrect when sent.TcpIp name:{0} ,Pack ID:{1} ,crt state:{2}"
                                                             , tcpipAgent.Name
                                                             , iPacketID
                                                             , tcpipAgent.ImsgStateMachine.State));
            }

            int threadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
            tcpipAgent.onMessageGenerated();
            hasRtn = false;

            outStr = (TSource2)TCPUtility.GetDefault(typeof(TSource2));


            EventHandler<TcpIpEventArgs> handler = null;

            rtnMsg = string.Empty;
            int iReplyPacketID = 0;
            switch (tcpipAgent.ConnectMode)
            {
                case TcpIpAgent.TCPIP_AGENT_COMM_MODE.SERVER_MODE:
                    iReplyPacketID = iPacketID + 100;
                    break;
                case TcpIpAgent.TCPIP_AGENT_COMM_MODE.CLINET_MODE:
                    iReplyPacketID = iPacketID - 100;
                    break;
            }
            try
            {
                handler = delegate(Object _sender, TcpIpEventArgs _e)
                {
                    replyHandler_Google<TSource>(_sender, _e, ref inStr);
                };

                tcpipAgent.addTcpIpReceivedHandler(iReplyPacketID, handler);

                int count = 0;
                do
                {
                    Console.WriteLine(string.Format("Send begin ID:{0}", iPacketID));
                    tcpipAgent.onMessageSent();
                    bool is_success = this.SendGoogleMsg(inStr);
                    if (!is_success)
                    {
                        rtnMsg = "Send message fail.";
                        TcpIpExceptionEventArgs e = new TcpIpExceptionEventArgs(iPacketID, iSeqNum, inStr, rtnMsg);
                        tcpipAgent.onErrorHappend(e);
                        return ReturnCode.SendDataFail;
                    }
                    Console.WriteLine(string.Format("Send success ID:{0}", iPacketID));

                    tcpipAgent.onWaitForReply();

                    SpinWait.SpinUntil(() => hasRtn, timeoutMSec);

                    if (!hasRtn)
                    {
                        if (count++ >= retryCnt)
                        {
                            Console.WriteLine(string.Format("Replay timeout ID:{0}", iPacketID));
                            rtnMsg = string.Format("A response timeout occurred. retry count:{0}", count);
                            TcpIpExceptionEventArgs e = new TcpIpExceptionEventArgs(iPacketID, iSeqNum, inStr, rtnMsg);
                            tcpipAgent.onReplyTimeout(e);
                            return ReturnCode.Timeout;
                        }
                        Console.WriteLine(string.Format("Send aging ID:{0}, count:{1}", iPacketID, count));
                    }
                } while (!hasRtn);
                outStr = (TSource2)recvStr;
                tcpipAgent.onMessageSentCmp();
            }
            catch (Exception ex)
            {
                rtnMsg = ex.ToString();
                TcpIpExceptionEventArgs e = new TcpIpExceptionEventArgs(iPacketID, iSeqNum, inStr, rtnMsg);
                tcpipAgent.onErrorHappend(e);
                return ReturnCode.SendDataFail;
            }
            finally
            {
                tcpipAgent.removeTcpIpReceivedHandler(iReplyPacketID, handler);
                tcpipAgent.onMessageSentFinish();
                Console.WriteLine(string.Format("Send finish ID:{0}", iPacketID));
            }
            return ReturnCode.Normal;
        }

        public byte[] GoogleMessage2ByteArray(IMessage message)
        {
            byte[] arrayByte = new byte[message.CalculateSize()];
            message.WriteTo(new Google.Protobuf.CodedOutputStream(arrayByte));

            return arrayByte;
        }


        string PACKET_ID_NAME = "ID";
        string MESSAGE_NUM_NAME = "SeqNum";
        public int getPacketIDByGoogleMessage(IMessage message)
        {
            int packet_id = 0;
            var descriptor = message.Descriptor;
            foreach (var field in descriptor.Fields.InDeclarationOrder())
            {
                if (field.Name == PACKET_ID_NAME)
                {
                    packet_id = (int)field.Accessor.GetValue(message);
                    break;
                }
            }

            return packet_id;
        }
        public int getMsgNumByGoogleMessage(IMessage message)
        {
            int packet_id = 0;
            var descriptor = message.Descriptor;
            foreach (var field in descriptor.Fields.InDeclarationOrder())
            {
                if (field.Name == MESSAGE_NUM_NAME)
                {
                    packet_id = (int)field.Accessor.GetValue(message);
                    break;
                }
            }

            return packet_id;
        }
        public bool setMsgNumByGoogleMessage(IMessage message, int num)
        {
            bool isSuccess = true;
            try
            {
                var descriptor = message.Descriptor;
                foreach (var field in descriptor.Fields.InDeclarationOrder())
                {
                    if (field.Name == MESSAGE_NUM_NAME)
                    {
                        field.Accessor.SetValue(message, num);
                        break;
                    }
                }
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        #endregion Google ProtoBuf

        

        private object handlerLock = new object();
        protected virtual void replyHandler<TSource, TSource2>(object sender, TcpIpEventArgs e, ref TSource inStr)
        {
            int threadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
            lock (handlerLock)
            {
                try
                {
                    if (hasRtn)
                    {
                        return;
                    }
                    byte[] bytPacketDatas = TCPUtility._Str2Packet(inStr);
                    ushort iSeqNum = TCPUtility.PrcGetPacketSeqNum(bytPacketDatas);

                    if (iSeqNum != e.iSeqNum)
                    {
                        return;
                    }
                    TSource2 tmpSXFY = (TSource2)TCPUtility._Packet2Str<TSource2>((byte[])e.objPacket, tcpipAgent.Name);

                    recvStr = (Object)tmpSXFY;
                    hasRtn = true;
                }
                catch (Exception ex)
                {
                    //todo recode log
                }
            }
        }

        protected virtual void replyHandler_Google<TSource>(object sender, TcpIpEventArgs e, ref TSource inStr)
            where TSource : IMessage<TSource>
        {
            int threadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
            lock (handlerLock)
            {
                try
                {
                    if (hasRtn)
                    {
                        return;
                    }

                    int iSeqNum = getMsgNumByGoogleMessage(inStr);

                    if (iSeqNum != e.iSeqNum)
                    {
                        return;
                    }

                    recvStr = e.objPacket;
                    hasRtn = true;
                }
                catch (Exception ex)
                {
                    //todo recode log
                }
            }
        }




        /// <summary>
        /// 取得目前當下系統時間
        /// </summary>
        /// <returns></returns>
        protected long getNow()
        {
            return System.DateTime.Now.Ticks / 10000;
        }

        /// <summary>
        /// SECS通訊結果代碼
        /// </summary>
        public enum ReturnCode
        {
            /// <summary>
            /// 成功
            /// </summary>
            Normal = 0,
            /// <summary>
            /// T3 Timeout
            /// </summary>
            Timeout = 1000,
            /// <summary>
            /// 訊息傳送失敗
            /// </summary>
            SendDataFail = 2000,

            DataCheckFail = 3000
        }

    }

    //A0.02 Begin
    public class TriggerEventQueue
    {
        private readonly object _eventSyncRoot = new object();
        private readonly Queue<WaitCallback> _eventDelegateQueue = new Queue<WaitCallback>();
        private String name = string.Empty;
        private WorkKeyFlag flag = new WorkKeyFlag();
        private Object _lock = new Object();
        public TriggerEventQueue(string name)
        {
            this.name = name;
            handlerEvent += handler;
        }

        public void onNotify(ChangeEventStruct _eventStruct)
        {
            WaitCallback eventDelegate = delegate(object obj)
            {

            };
            lock (_eventDelegateQueue)
            {
                _eventDelegateQueue.Enqueue(eventDelegate);
            }
            if (flag.Compare(WorkKeyFlag.UNSET))
            {
                ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(doWork), null);
            }
        }

        private void doWork(Object wKey)
        {
            if (flag.CompareAndSet(WorkKeyFlag.UNSET, WorkKeyFlag.SET))
            {
                while (true)
                {
                    try
                    {
                        WaitCallback eventDelegate = null;
                        lock (_eventDelegateQueue)
                        {
                            if (_eventDelegateQueue.Count <= 0)
                            {
                                break;
                            }
                            eventDelegate = _eventDelegateQueue.Dequeue();
                        }
                        HandlerDelgate tmpHandlerEvent = handlerEvent;
                        if (tmpHandlerEvent != null && eventDelegate != null)
                        {

                            tmpHandlerEvent(eventDelegate);

                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                flag.CompareAndSet(WorkKeyFlag.SET, WorkKeyFlag.UNSET);
            }
            else
            {

            }
        }

        public delegate void HandlerDelgate(WaitCallback eventDelegate);
        public event HandlerDelgate handlerEvent;
        private void handler(WaitCallback eventDelegate)
        {
            eventDelegate(null);
        }
    }

    public class WorkKeyFlag
    {
        public static readonly int SET = 1;
        public static readonly int UNSET = 0;

        private int eventPoint;
        public WorkKeyFlag()
        {
            eventPoint = UNSET;
        }

        public int Exchange(int update)
        {
            return Interlocked.Exchange(ref eventPoint, update);
        }

        public bool Compare(int expect)
        {
            return Interlocked.CompareExchange(ref eventPoint, eventPoint, expect) == expect;
        }

        public bool CompareAndSet(int expect, int update)
        {
            return Interlocked.CompareExchange(ref eventPoint, update, expect) == expect;
        }
    }

    public class TcpIpStateException : Exception
    {
        public TcpIpStateException()
            : base()
        {

        }

        public TcpIpStateException(string message) : base(message) { }


    }

}
