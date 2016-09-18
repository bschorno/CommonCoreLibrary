using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary
{
    public delegate void NetInfoEvent(object sender, NetInfoEventArgs e);

    public class NetInfoEventArgs : EventArgs
    {
        private string      _message;
        private NetInfoType _infoType;
        private Exception   _exception;

        public NetInfoEventArgs(string message, NetInfoType infoType)
        {
            this._message = message;
            this._infoType = infoType;
        }

        public NetInfoEventArgs(string message)
                    : this(message, NetInfoType.Info)
        {

        }

        public NetInfoEventArgs(Exception exception)
                    : this(exception.Message, NetInfoType.Error)
        {
            this._exception = exception;
        }

        /// <summary>
        /// message
        /// </summary>
        public string Message
        {
            get
            {
                return this._message;
            }
        }

        /// <summary>
        /// info type
        /// </summary>
        public NetInfoType InfoType
        {
            get
            {
                return this._infoType;
            }
        }

        /// <summary>
        /// exception
        /// </summary>
        public Exception Exception
        {
            get
            {
                return this._exception;
            }
        }
    }
}