using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.iibg3k0.ttc.Common
{
    public class TcpIpEventArgs
    {
        public int iPt { get; private set; }
        public int iPacketID { get; private set; }
        public UInt16 iSeqNum { get; private set; }
        public object objPacket { get; private set; }
        public DateTime EventTime { get; private set; }

        public TcpIpEventArgs(int _iPacketID, UInt16 _iSeqNum, object _objPacket)
        {
            iPacketID = _iPacketID;
            iSeqNum = _iSeqNum;
            objPacket = _objPacket;
            EventTime = DateTime.Now;
        }

        public string getEventTime()
        {
            return EventTime.ToString("yyyy-MM-dd HH:mm:ss.fffff");
        }
    }

    public class TcpIpExceptionEventArgs : TcpIpEventArgs
    {
        public string sMessage { get; private set; }
        public TcpIpExceptionEventArgs(int _iPacketID, UInt16 _iSeqNum, object _objPacket, string msg) :
            base(_iPacketID, _iSeqNum, _objPacket)
        {
            sMessage = msg;
        }
    }

}
