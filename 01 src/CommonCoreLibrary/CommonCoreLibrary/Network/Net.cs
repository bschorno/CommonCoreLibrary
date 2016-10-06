using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace CommonCoreLibrary.Network
{
    public static class Net
    {
        public const byte CHUNK_HEADER_SIZE = 3;
        public const int  DEFAULT_BUFFER_SIZE = 4096;

        public static IPAddress LocalIP()
        {
            IPAddress var1 = null;
            foreach (IPAddress var2 in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                if (var2.AddressFamily == AddressFamily.InterNetwork)
                {
                    var1 = var2;
                    break;
                }
            return var1;
        }
    }
}