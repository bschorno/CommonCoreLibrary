using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace CommonCoreLibrary.Network
{
    internal interface INet
    {
        /// <summary>
        /// buffer size
        /// </summary>
        int BufferSize
        {
            get;
            set;
        }
        /// <summary>
        /// local endpoint for tcp connection
        /// </summary>
        EndPoint TcpLocalEndPoint
        {
            get;
            set;
        }
        /// <summary>
        /// local endpoint for udp connection
        /// </summary>
        EndPoint UdpLocalEndPoint
        {
            get;
            set;
        }
    }
}