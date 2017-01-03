using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.General.OS
{
    public class OSWindows : OS
    {
        /// <summary>
        /// Constructor
        /// </summary>
        internal OSWindows()
        {

        }

        /// <summary>
        /// Get Windows version
        /// </summary>
        /// <returns></returns>
        public OSWindowsVersion GetVersion()
        {
            int major = this.GetVersionMajor();
            int minor = this.GetVersionMinor();
            switch (this.GetPlatform())
            {
                case PlatformID.Win32NT:
                    switch (this.GetVersionMajor())
                    {
                        case 4:
                            if (this.GetVersionMinor() == 0)
                                return OSWindowsVersion.WindowsNT;
                            break;
                        case 5:
                            if (this.GetVersionMinor() == 0)
                                return OSWindowsVersion.Windows2000;
                            if (this.GetVersionMinor() == 1)
                                return OSWindowsVersion.WindowsXP;
                            if (this.GetVersionMinor() == 2)
                                return OSWindowsVersion.Windows2003;
                            break;
                        case 6:
                            if (this.GetVersionMinor() == 0)
                                return OSWindowsVersion.WindowsVista;
                            if (this.GetVersionMinor() == 1)
                                return OSWindowsVersion.Windows7;
                            if (this.GetVersionMinor() == 2)
                                return OSWindowsVersion.Windows8;
                            if (this.GetVersionMinor() == 3)
                                return OSWindowsVersion.Windows8_1;
                            break;
                        case 10:
                            if (this.GetVersionMinor() == 0)
                                return OSWindowsVersion.Windows10;
                            break;
                    }
                    break;
                case PlatformID.Win32Windows:
                    switch (this.GetVersionMajor())
                    {
                        case 4:
                            if (this.GetVersionMinor() == 0)
                                return OSWindowsVersion.Windows95;
                            if (this.GetVersionMinor() == 10)
                                return OSWindowsVersion.Windows98;
                            if (this.GetVersionMinor() == 90)
                                return OSWindowsVersion.WindowsMe;
                            break;
                    }
                    break;
            }
            return OSWindowsVersion.Undefine;
        }
    }
}
