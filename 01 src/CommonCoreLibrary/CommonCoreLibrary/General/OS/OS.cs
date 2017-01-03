using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.General.OS
{
    public abstract class OS
    {
        public OS GetOS()
        {
            switch (this.GetPlatform())
            {
                case PlatformID.MacOSX:
                    return new OSMac();
                    break;
                case PlatformID.Unix:
                    return new OSUnix();
                    break;
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    return new OSWindows();
                    break;
            }
            return null;
        }

        /// <summary>
        /// Get major windows version
        /// </summary>
        /// <returns></returns>
        public int GetVersionMajor()
        {
            return Environment.OSVersion.Version.Major;
        }

        /// <summary>
        /// Get minor windows version
        /// </summary>
        /// <returns></returns>
        public int GetVersionMinor()
        {
            return Environment.OSVersion.Version.Minor;
        }

        /// <summary>
        /// Get windows platform id
        /// </summary>
        /// <returns></returns>
        public PlatformID GetPlatform()
        {
            return Environment.OSVersion.Platform;
        }
    }
}
