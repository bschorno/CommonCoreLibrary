using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Serialisation
{
    public class SrlException : Exception
    {
        private object _sender;

        /// <summary>
        /// Sender of this exception
        /// </summary>
        public object Sender
        {
            get
            {
                return this._sender;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="msg">Exception message</param>
        public SrlException(string msg)
            : base(msg)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="msg">Exception message</param>
        /// <param name="sender">Exception sender</param>
        public SrlException(string msg, object sender)
            : this(msg)
        {
            this._sender = sender;
        }
    }
}
