using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.General.OS
{
    public class OSUnix : OS
    {
        /// <summary>
        /// Constructor
        /// </summary>
        internal OSUnix()
        {

        }

        /// <summary>
        /// Get Windows version
        /// </summary>
        /// <returns></returns>
        public OSUnixVersion GetVersion()
        {
            return OSUnixVersion.Undefine;
        }
    }
}
