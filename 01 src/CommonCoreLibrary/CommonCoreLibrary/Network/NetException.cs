using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Network
{
    public class NetException : Exception
    {
        public NetException(string message)
            : base(message)
        {

        }
    }
}
