using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Network
{
    public class NetMessageData : NetMessage
    {
        public override NetMessageType MessageType
        {
            get
            {
                return NetMessageType.Data;
            }
        }
    }
}