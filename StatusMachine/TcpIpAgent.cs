using Cowboy.Sockets;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.mirle.iibg3k0.ttc.Common.TCPIP
{
    public class TcpIpAgent
    {
        private Dictionary<int, EventHandler<TcpIpEventArgs>> RealTcpIpReceivedDic = new Dictionary<int, EventHandler<TcpIpEventArgs>>();
        private Dictionary<string, EventHandler<TcpIpEventArgs>> RealTcpIpDisconnetedDic = new Dictionary<string, EventHandler<TcpIpEventArgs>>();
        private Dictionary<string, EventHandler<TcpIpEventArgs>> RealTcpIpConnetedDic = new Dictionary<string, EventHandler<TcpIpEventArgs>>();
        public event EventHandler<TcpIpEventArgs> TcpIpReplyTimeOut;
        public event EventHandler<TcpIpEventArgs> TcpIpSendError;
        public event EventHandler<E_Msg_STS> TcpIpSendRecvStateChange;
        public enum TCPIP_AGENT_COMM_MODE
        {
            SERVER_MODE,
            CLINET_MODE
        }


        //Connection connection = null;

        private int Num;
        public string Name { get; private set; }

        private ushort m_iSendSeqNum = 0;
        private const ushort SENDSEQNUM_MAX = 999;
        private const ushort SENDSEQNUM_MIN = 1;

        public string LocalIPAddress { get; private set; }
        public int LocalIPPort { get; private set; }
        public string RemoteIPAddress { get; private set; }
        public int RemoteIPPort { get; private set; }
        public TCPIP_AGENT_COMM_MODE ConnectMode { get; private set; }


        public int RecvTimeout { get; private set; }
        public int SendTimeout { get; private set; }
        public int MaxReadSize { get; private set; }
        public int ReconnectInterval { get; private set; }
        public int RetryCount { get; private set; }
        public string IpAndPort { get { return string.Concat(RemoteIPAddress, ":", RemoteIPPort.ToString()); } }

        public int preReciveSeqNo { get; private set; }
        private int[] NeedToBoConfirmReceivePacketID;

        public E_Msg_STS messagestate = E_Msg_STS.Idle;
        public E_Msg_STS MessageState
        {
            get
            {
                return messagestate;
            }
            set
            {
                messagestate = value;
                onSendRecvStateChangeHappend(MessageState);
            }
        }

        public TcpSocketSaeaSession session { get; private set; }
        public TcpSocketSaeaClient ClientSession { get; private set; }

        private TcpIpClient tcpipClient = null;

        internal IStateMachine<E_Msg_STS, E_Msg_EVENT> ImsgStateMachine = null;

        public Boolean IsConnection { get; private set; }

        public TrxTcpIp TrxSECS
        {
            get { return new TrxTcpIp(this); }
        }

        public TcpIpAgent(int num, string name, string loacl_ip, int local_port, string remote_ip, int remote_port, TCPIP_AGENT_COMM_MODE mode,
                               int recv_timeout, int send_timeout, int max_readsize, int reconnection_interval, int retry_count)
        {
            Num = num;
            Name = name;
            LocalIPAddress = loacl_ip;
            LocalIPPort = local_port;
            RemoteIPAddress = remote_ip;
            RemoteIPPort = remote_port;
            ConnectMode = mode;
            RecvTimeout = recv_timeout;
            SendTimeout = send_timeout;
            MaxReadSize = max_readsize;
            ReconnectInterval = reconnection_interval;
            RetryCount = retry_count;

            if (ConnectMode == TCPIP_AGENT_COMM_MODE.CLINET_MODE)
                tcpipClient = new TcpIpClient(remote_ip, remote_port, loacl_ip, local_port, this);

            ImsgStateMachine = new StateMachineStateless<E_Msg_STS, E_Msg_EVENT>
                (name, StateMachineFactory.creatVHStateMachine(() => MessageState, (state) => MessageState = state));
        }

        public void start()
        {

        }

        public void stop()
        {
            switch (ConnectMode)
            {
                case TCPIP_AGENT_COMM_MODE.SERVER_MODE:
                    session.Close();
                    break;
                case TCPIP_AGENT_COMM_MODE.CLINET_MODE:
                    ClientSession.Close();
                    break;
            }
        }


        #region todo 註冊監聽收到的ID
        /// <summary>
        /// 註冊監聽收到的StreamFunction
        /// </summary>
        /// <param name="function_id">註冊的Function ID。</param>
        /// <param name="handler"></param>
        public void addTcpIpReceivedHandler(int packet_id, EventHandler<TcpIpEventArgs> handler)
        {
            lock (RealTcpIpReceivedDic)
            {
                if (RealTcpIpReceivedDic.ContainsKey(packet_id))
                {
                    RealTcpIpReceivedDic[packet_id] += handler;
                }
                else
                {
                    RealTcpIpReceivedDic.Add(packet_id, handler);
                }
            }
        }

        /// <summary>
        /// 移除StreamFunction的監聽
        /// </summary>
        /// <param name="packet_id"></param>
        /// <param name="handler"></param>
        public void removeTcpIpReceivedHandler(int packet_id, EventHandler<TcpIpEventArgs> handler)
        {
            lock (RealTcpIpReceivedDic)
            {
                if (RealTcpIpReceivedDic.ContainsKey(packet_id))
                {
                    RealTcpIpReceivedDic[packet_id] -= handler;
                }
            }
        }

        private static readonly string DisconnectedEventName = "DisconnectedEventName";
        private static readonly string ConnectedEventName = "ConnectedEventName";

        /// <summary>
        /// 註冊監聽TcpIp Disconnected
        /// </summary>
        /// <param name="handler"></param>
        public void addTcpIpDisconnectedHandler(EventHandler<TcpIpEventArgs> handler)
        {
            lock (RealTcpIpDisconnetedDic)
            {
                if (RealTcpIpDisconnetedDic.ContainsKey(DisconnectedEventName.Trim()))
                {
                    RealTcpIpDisconnetedDic[DisconnectedEventName.Trim()] += handler;
                }
                else
                {
                    RealTcpIpDisconnetedDic.Add(DisconnectedEventName.Trim(), handler);
                }
            }
        }
        /// <summary>
        /// 移除監聽TcpIp Disconnected
        /// </summary>
        /// <param name="handler"></param>
        public void removeTcpIpDisconnectedHandler(EventHandler<TcpIpEventArgs> handler)
        {
            lock (RealTcpIpDisconnetedDic)
            {
                if (RealTcpIpDisconnetedDic.ContainsKey(DisconnectedEventName.Trim()))
                {
                    RealTcpIpDisconnetedDic[DisconnectedEventName.Trim()] -= handler;
                }
            }
        }
        /// <summary>
        /// 註冊監聽TcpIp Connected
        /// </summary>
        /// <param name="handler"></param>
        public void addTcpIpConnectedHandler(EventHandler<TcpIpEventArgs> handler)
        {
            lock (RealTcpIpConnetedDic)
            {
                if (RealTcpIpConnetedDic.ContainsKey(ConnectedEventName.Trim()))
                {
                    RealTcpIpConnetedDic[ConnectedEventName.Trim()] += handler;
                }
                else
                {
                    RealTcpIpConnetedDic.Add(ConnectedEventName.Trim(), handler);
                }
            }
        }
        /// <summary>
        /// 移除監聽TcpIp Connected
        /// </summary>
        /// <param name="handler"></param>
        public void removeTcpIpConnectedHandler(EventHandler<TcpIpEventArgs> handler)
        {
            lock (RealTcpIpConnetedDic)
            {
                if (RealTcpIpConnetedDic.ContainsKey(ConnectedEventName.Trim()))
                {
                    RealTcpIpConnetedDic[ConnectedEventName.Trim()] -= handler;
                }
            }
        }

        protected static Object ReplyTimeoutLock = new object();
        /// <summary>
        /// 監聽ReplyTimeOut
        /// </summary>
        /// <param name="handler"></param>
        public void addReplyTimeOutHandler(EventHandler<TcpIpEventArgs> handler)
        {
            lock (ReplyTimeoutLock)
            {
                TcpIpReplyTimeOut += handler;
            }
        }
        /// <summary>
        /// 移除監聽ReplyTimeOut
        /// </summary>
        /// <param name="handler"></param>
        public void removeReplyTimeOutHandler(EventHandler<TcpIpEventArgs> handler)
        {
            lock (ReplyTimeoutLock)
            {
                TcpIpReplyTimeOut -= handler;
            }
        }

        protected static Object SendErrorLock = new object();
        /// <summary>
        /// 監聽SendError
        /// </summary>
        /// <param name="handler"></param>
        public void addSendErrorHandler(EventHandler<TcpIpEventArgs> handler)
        {
            lock (SendErrorLock)
            {
                TcpIpSendError += handler;
            }
        }
        /// <summary>
        /// 移除監聽SendError
        /// </summary>
        /// <param name="handler"></param>
        public void removeSendErrorHandler(EventHandler<TcpIpEventArgs> handler)
        {
            lock (SendErrorLock)
            {
                TcpIpSendError -= handler;
            }
        }

        protected static Object SendRecvStateChange = new object();
        /// <summary>
        /// 監聽SendError
        /// </summary>
        /// <param name="handler"></param>
        public void addSendRecvStateChangeHandler(EventHandler<E_Msg_STS> handler)
        {
            lock (SendRecvStateChange)
            {
                TcpIpSendRecvStateChange += handler;
            }
        }
        /// <summary>
        /// 移除監聽SendError
        /// </summary>
        /// <param name="handler"></param>
        public void removeSendRecvStateChangeHandler(EventHandler<E_Msg_STS> handler)
        {
            lock (SendRecvStateChange)
            {
                TcpIpSendRecvStateChange -= handler;
            }
        }
        #endregion todo 註冊/反註冊監聽收到的ID

        #region 通知事件的發生

        /// <summary>
        /// 通知SECS Disconnected
        /// </summary>
        /// <param name="e"></param>
        public void onTcpIpDisconnected(TcpIpEventArgs e)
        {
            IsConnection = false;
            EventHandler<TcpIpEventArgs> tmpEventHandler = null;
            lock (RealTcpIpDisconnetedDic)
            {
                if (RealTcpIpDisconnetedDic.ContainsKey(DisconnectedEventName.Trim()))
                {
                    tmpEventHandler = RealTcpIpDisconnetedDic[DisconnectedEventName];
                }
            }
            if (tmpEventHandler != null)
            {
                ChangeEventStruct eventStruct = new ChangeEventStruct()
                {
                    EventHandler = tmpEventHandler,
                    EventArgs = e
                };
                triggerEvent(eventStruct);
            }
        }
        /// <summary>
        /// 通知SECS Connected
        /// </summary>
        /// <param name="e"></param>
        public void onTcpIpConnected(TcpIpEventArgs e)
        {
            IsConnection = true;
            EventHandler<TcpIpEventArgs> tmpEventHandler = null;
            lock (RealTcpIpConnetedDic)
            {
                if (RealTcpIpConnetedDic.ContainsKey(ConnectedEventName.Trim()))
                {
                    tmpEventHandler = RealTcpIpConnetedDic[ConnectedEventName];
                }
            }
            if (tmpEventHandler != null)
            {
                ChangeEventStruct eventStruct = new ChangeEventStruct()
                {
                    EventHandler = tmpEventHandler,
                    EventArgs = e
                };
                triggerEvent(eventStruct);
            }
        }
        int reciveCount = 0;

        public void DecodReciveRawData(byte[] raw_data)
        {
            byte[] bytPacketDatas = null;
            int data_coune = raw_data.Length;
            int PacketID = 0;
            UInt16 SeqNum = 0;
            object objPacket = null;

            //while (data_coune >= LEN_MESSAGE_SIZE)
            //{
            string result = TCPUtility.PrcConvBytes2HexStr(raw_data, raw_data.Length, " ");
            Console.WriteLine(reciveCount++);
            Console.WriteLine(result);

            // Check Message Header
            if ((raw_data[0] == '@') && (raw_data[1] == 'P')
                && (raw_data[2] == 'K') && (raw_data[3] == 'T'))
            {
                //bytPacketDatas = TCPUtility.PrcGetPacketFromMessage(raw_data);
                bytPacketDatas = TCPUtility.PrcGetPacketFromMessage_New(raw_data);
                PacketID = TCPUtility.PrcGetPacketID(bytPacketDatas);
                SeqNum = TCPUtility.PrcGetPacketSeqNum(bytPacketDatas);
                objPacket = bytPacketDatas;
                TcpIpEventArgs arg = new TcpIpEventArgs(PacketID, SeqNum, objPacket);
                onTcpIpReceive(PacketID, arg);
                dataSeqNoOrderToComfirm(PacketID, SeqNum);
            }
            //data_coune -= LEN_MESSAGE_SIZE;
            //Array.Copy(raw_data, LEN_MESSAGE_SIZE, raw_data, 0, data_coune);
            //}

        }

        public delegate void UnPackWrapperMessage(byte[] raw_data, out UInt16 packet_no, out UInt16 seq_num, out object message_obj);
        private UnPackWrapperMessage unPackWrapperMsg;
        public void DecodReciveRawData_Google(byte[] raw_data)
        {
            byte[] bytPacketDatas = raw_data;
            UInt16 PacketID = 0;
            UInt16 SeqNum = 0;
            object objPacket = null;

            string result = TCPUtility.PrcConvBytes2HexStr(bytPacketDatas, bytPacketDatas.Length, " ");
            Console.WriteLine(reciveCount++);
            Console.WriteLine(result);

            unPackWrapperMsg(bytPacketDatas, out PacketID, out SeqNum, out objPacket);

            TcpIpEventArgs arg = new TcpIpEventArgs(PacketID, SeqNum, objPacket);
            onTcpIpReceive(PacketID, arg);
            dataSeqNoOrderToComfirm(PacketID, SeqNum);
        }
        public void injectUnPackWrapperMsg(UnPackWrapperMessage _unPackWrapperMsg)
        {
            this.unPackWrapperMsg = _unPackWrapperMsg;
        }





        private void dataSeqNoOrderToComfirm(int packet_id, int seq_no)
        {
            if (NeedToBoConfirmReceivePacketID != null
                && !NeedToBoConfirmReceivePacketID.Contains(packet_id))
                return;
            if (seq_no < SENDSEQNUM_MIN || seq_no > SENDSEQNUM_MAX)
            {
                LogCollection.TrxMsgWarn(string.Format("Packet ID:{0} seq no over range, Seq No:{1}"
                                                       , packet_id
                                                       , seq_no));
                return;
            }
            if (seq_no == preReciveSeqNo)
            {
                LogCollection.TrxMsgWarn(string.Format("Packet ID:{0} of number has not changed, the same as the last seq no:{1}"
                                       , packet_id
                                       , seq_no));
                return;
            }

            if (preReciveSeqNo != 0)
            {
                if (preReciveSeqNo == SENDSEQNUM_MAX)
                {
                    if (seq_no != SENDSEQNUM_MIN)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("Num of leaks:");
                        for (int i = SENDSEQNUM_MIN; i < seq_no; i++)
                        {
                            sb.Append(i).Append(' ');
                        }
                        LogCollection.TrxMsgWarn(sb.ToString());
                    }
                }
                else if (seq_no - 1 != preReciveSeqNo)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Num of leaks:");

                    int NumOfLeaks = (seq_no - 1) - preReciveSeqNo;
                    if (NumOfLeaks > 0)
                    {
                        for (int i = preReciveSeqNo + 1; i < seq_no; i++)
                        {
                            sb.Append(i).Append(' ');
                        }
                    }
                    else
                    {
                        for (int i = preReciveSeqNo + 1; i <= SENDSEQNUM_MAX; i++)
                        {
                            sb.Append(i).Append(' ');
                        }
                        for (int i = SENDSEQNUM_MIN; i < seq_no; i++)
                        {
                            sb.Append(i).Append(' ');
                        }
                    }
                    LogCollection.TrxMsgWarn(sb.ToString());
                }
            }
            preReciveSeqNo = seq_no;
        }


        /// <summary>
        /// 通知Receive stream function
        /// </summary>
        /// <param name="PacketID">通知的Stream Function</param>
        /// <param name="e"></param>
        public void onTcpIpReceive(int PacketID, TcpIpEventArgs e)
        {
            EventHandler<TcpIpEventArgs> tmpEventHandler = null;
            lock (RealTcpIpReceivedDic)
            {
                if (RealTcpIpReceivedDic.ContainsKey(PacketID))
                {
                    tmpEventHandler = RealTcpIpReceivedDic[PacketID];
                }
            }
            if (tmpEventHandler != null)
            {
                ChangeEventStruct eventStruct = new ChangeEventStruct()
                {
                    EventHandler = tmpEventHandler,
                    EventArgs = e
                };
                triggerEvent(eventStruct);
            }
        }



        public void onMessageGenerated()
        {
            ImsgStateMachine.Fire(E_Msg_EVENT.HasBeenGenerated);
        }
        public void onMessageSent()
        {
            ImsgStateMachine.Fire(E_Msg_EVENT.HasBeenSent);
        }
        public void onWaitForReply()
        {
            ImsgStateMachine.Fire(E_Msg_EVENT.WaitingForReply);
        }
        public void onMessageSentCmp()
        {
            ImsgStateMachine.Fire(E_Msg_EVENT.HasBeenCompleted);
        }
        public void onMessageSentFinish()
        {
            ImsgStateMachine.Fire(E_Msg_EVENT.Finish);
        }
        /// <summary>
        /// 通知Reply TimeOut
        /// </summary>
        /// <param name="e"></param>
        public void onReplyTimeout(TcpIpEventArgs e)
        {
            ImsgStateMachine.Fire(E_Msg_EVENT.TimeOutHappend);
            if (TcpIpReplyTimeOut != null)
            {
                ChangeEventStruct eventStruct = new ChangeEventStruct()
                {
                    EventHandler = TcpIpReplyTimeOut,
                    EventArgs = e
                };
                triggerEvent(eventStruct);
            }
        }
        public void onErrorHappend(TcpIpEventArgs e)
        {
            ImsgStateMachine.Fire(E_Msg_EVENT.ErrorHappend);
            if (TcpIpSendError != null)
            {
                ChangeEventStruct eventStruct = new ChangeEventStruct()
                {
                    EventHandler = TcpIpSendError,
                    EventArgs = e
                };
                triggerEvent(eventStruct);
            }
        }

        public void onSendRecvStateChangeHappend(E_Msg_STS e_msg_satet)
        {
            if (TcpIpSendRecvStateChange != null)
            {
                Task.Run(() => TcpIpSendRecvStateChange(this, e_msg_satet));
            }
        }

        private void triggerEvent(ChangeEventStruct eventStruct)
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(triggerEventHandler), eventStruct);
            }
            catch (Exception ex)
            {
                //todo recode log
            }

        }

        private void triggerEventHandler(Object obj)
        {
            //            licenseKey.checkValidation();
            ChangeEventStruct eventStruct = obj as ChangeEventStruct;
            if (eventStruct.EventHandler != null)
            {
                DateTime startTime = DateTime.Now;
                eventStruct.EventHandler(this, eventStruct.EventArgs);
                double totalMS = DateTime.Now.Subtract(startTime).TotalMilliseconds;
            }
        }
        #endregion 通知事件的發生

        #region 發送訊息
        //const UInt16 SeqNoBeginByte = 8;
        public void SendMessage<T>(T msgStructure)
        {
            byte[] bytPacketDatas;
            byte[] bytSendMsgDatas;
            //byte[] bytSeqNoDatas;
            //bytSeqNoDatas = BitConverter.GetBytes(getSendSeqNum());
            bytPacketDatas = TCPUtility._Str2Packet(msgStructure);
            bytSendMsgDatas = TCPUtility.PrcCreateSendMessage_New(bytPacketDatas);

            LogCollection.TrxMsgInfo(TCPUtility.toStuctureString(msgStructure, Name, true));
            Console.WriteLine("Send Data:{0}", TCPUtility.PrcConvBytes2HexStr(bytSendMsgDatas, bytSendMsgDatas.Length, " "));

            //Array.Copy(bytSeqNoDatas, 0, bytSendMsgDatas, SeqNoBeginByte, bytSeqNoDatas.Length);
            Task taskSendMsg = null;
            switch (ConnectMode)
            {
                case TCPIP_AGENT_COMM_MODE.SERVER_MODE:
                    taskSendMsg = session.SendAsync(bytSendMsgDatas);
                    break;
                case TCPIP_AGENT_COMM_MODE.CLINET_MODE:
                    taskSendMsg = ClientSession.SendAsync(bytSendMsgDatas);
                    break;
            }
            taskSendMsg.Wait();
        }

        public void SendRawData(byte[] bytSendMsgDatas)
        {
            Task taskSendMsg = null;
            switch (ConnectMode)
            {
                case TCPIP_AGENT_COMM_MODE.SERVER_MODE:
                    taskSendMsg = session.SendAsync(bytSendMsgDatas);
                    break;
                case TCPIP_AGENT_COMM_MODE.CLINET_MODE:
                    taskSendMsg = ClientSession.SendAsync(bytSendMsgDatas);
                    break;
            }
            taskSendMsg.Wait();
        }

        #endregion 發送訊息

        public void setNeedToBoConfirmReceivePacketID(int[] packets_id)
        {
            NeedToBoConfirmReceivePacketID = packets_id;
        }


        public void refreshConnection()
        {

        }

        public UInt16 getSendSeqNum()
        {
            m_iSendSeqNum++;
            if (m_iSendSeqNum > SENDSEQNUM_MAX)
            {
                m_iSendSeqNum = SENDSEQNUM_MIN;
            }
            return (m_iSendSeqNum);
        }

        #region 設置參數
        //public void setRecvTimeout(int recv_timeout)
        //{
        //    RecvTimeout = recv_timeout;
        //    connection.setRecvTimeout(recv_timeout);
        //}
        //public void setSendTimeout(int send_timeout)
        //{
        //    SendTimeout = send_timeout;
        //    connection.setSendTimeout(send_timeout);
        //}
        //public void setMaxReadSize(int max_readsize)
        //{
        //    MaxReadSize = max_readsize;
        //    connection.setMaxReadSize(max_readsize);
        //}
        //public void setReconnectInterval(int reconnection_interval)
        //{
        //    ReconnectInterval = reconnection_interval;
        //    connection.setReconnectInterval(reconnection_interval);
        //}
        //public void setRetryCount(int retry_count)
        //{
        //    RetryCount = retry_count;
        //    connection.setRetryCount(retry_count);
        //}
        #endregion 設置參數

        public void NotifySetTcpSocketSaeaSession(TcpSocketSaeaSession _session)
        {
            session = _session;
            TcpIpEventArgs arg = new TcpIpEventArgs(0, 0, null);
            onTcpIpConnected(arg);
        }
        public void NotifySetTcpSocketSaeaSession(TcpSocketSaeaClient _session)
        {
            ClientSession = _session;
            TcpIpEventArgs arg = new TcpIpEventArgs(0, 0, null);
            onTcpIpConnected(arg);
        }
        public async void NotifyCloseTcpSocketSaeaSession(TcpSocketSaeaSession _session)
        {
            await session.Close();
            session = null;
            TcpIpEventArgs arg = new TcpIpEventArgs(0, 0, null);
            onTcpIpDisconnected(arg);
        }
        public async void NotifyCloseTcpSocketSaeaSession(TcpSocketSaeaClient _session)
        {
            await ClientSession.Close();
            ClientSession = null;
            TcpIpEventArgs arg = new TcpIpEventArgs(0, 0, null);
            onTcpIpDisconnected(arg);
        }
        #region enum
        public enum E_Msg_STS
        {
            Idle,
            Generated,
            Sent,
            WaitingForReply,
            Completed,

            TimeOut,
            Error
        }
        public enum E_Msg_EVENT
        {
            HasBeenGenerated,
            HasBeenSent,
            WaitingForReply,
            HasBeenCompleted,

            ErrorHappend,
            TimeOutHappend,

            Finish
        }
        #endregion enum
    }


    public class ChangeEventStruct
    {
        public EventHandler<TcpIpEventArgs> EventHandler { get; set; }
        public TcpIpEventArgs EventArgs { get; set; }
    }
}
