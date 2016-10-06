﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Network
{
    public class NetMessageDisconnection : NetMessage
    {
        public override NetMessageType MessageType
        {
            get
            {
                return NetMessageType.Disconnection;
            }
        }
    }
}