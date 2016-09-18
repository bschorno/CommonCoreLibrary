using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary
{
    public abstract class NetMessage
    {
        public NetMessage()
        {
            throw new System.NotImplementedException();
        }

        public abstract NetMessageType MessageType
        {
            get;
        }
    }
}