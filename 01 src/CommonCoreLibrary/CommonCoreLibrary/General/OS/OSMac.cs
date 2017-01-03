using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.General.OS
{
    public class OSMac : OS
    {
        /// <summary>
        /// Constructor
        /// </summary>
        internal OSMac()
        {

        }

        /// <summary>
        /// Get Windows version
        /// </summary>
        /// <returns></returns>
        public OSMacVersion GetVersion()
        {
            return OSMacVersion.Undefine;
        }
    }
}
