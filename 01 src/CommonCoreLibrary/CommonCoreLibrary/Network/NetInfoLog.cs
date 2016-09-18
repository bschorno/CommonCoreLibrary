using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary
{
    public sealed class NetInfoLog
    {
        private static NetInfoLog _netInfoLog;

        public event NetInfoEvent InfoEvent;

        private NetInfoLog()
        {

        }

        public static NetInfoLog GetInstance()
        {
            if (_netInfoLog == null)
                _netInfoLog = new NetInfoLog();
            return _netInfoLog;
        }

        public void Log(object sender, string message, NetInfoType infoType)
        {
            this.OnInfoEvent(sender, new NetInfoEventArgs(message, infoType));
        }

        public void Log(object sender, string message)
        {
            this.OnInfoEvent(sender, new NetInfoEventArgs(message));
        }

        public void Log(object sender, Exception exception)
        {
            this.OnInfoEvent(sender, new NetInfoEventArgs(exception));
        }

        private void OnInfoEvent(object sender, NetInfoEventArgs e)
        {
            if (this.InfoEvent != null)
                this.InfoEvent(sender, e);
        }
    }
}
