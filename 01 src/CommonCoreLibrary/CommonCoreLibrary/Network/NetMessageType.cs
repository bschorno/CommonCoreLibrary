using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary
{
    public enum NetMessageType
    {
        /// <summary>
        /// undefined message
        /// </summary>
        Undefine = 0x0001,
        /// <summary>
        /// new client has connected to the server
        /// </summary>
        Connection = 0x0002,
        /// <summary>
        /// server callback after connection
        /// </summary>
        ConnectionCallback = 0x0003,
        /// <summary>
        /// client want's to disconnect from the server
        /// </summary>
        Disconnection = 0x0004,
        /// <summary>
        /// generally info message
        /// </summary>
        Info = 0x0005,
        /// <summary>
        /// binary data
        /// </summary>
        Data = 0x0006
    }
}