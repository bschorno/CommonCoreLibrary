using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Security;

namespace CommonCoreLibrary
{
    public class NetServer : INet
    {
        private Socket          _tcpListener;
        private Socket          _udpListener;
        private EndPoint        _tcpLocalEndPoint;
        private EndPoint        _udpLocalEndPoint;
        private NetInfoLog      _infoLog;
        private int             _bufferSize         = Net.DEFAULT_BUFFER_SIZE;
        private List<NetClient> _clients            = new List<NetClient>();

        /// <summary>
        /// buffer size
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public int BufferSize
        {
            get
            {
                return this._bufferSize;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentNullException("Buffer can't less/equal 0", "value");
                this._bufferSize = value;
            }
        }

        /// <summary>
        /// local endpoint for tcp connection
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public EndPoint TcpLocalEndPoint
        {
            get
            {
                return this._tcpLocalEndPoint;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("No reference", "value");
                this._tcpLocalEndPoint = value;
            }
        }

        /// <summary>
        /// local endpoint for udp connection
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public EndPoint UdpLocalEndPoint
        {
            get
            {
                return this._udpLocalEndPoint;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("No reference", "value");
                this._udpLocalEndPoint = value;
            }
        }

        /// <summary>
        /// count of connected sockets
        /// </summary>
        public int ConnectionCount
        {
            get
            {
                return this._clients.Count;
            }
        }

        public bool IsRunning
        {
            get
            {
                if (this._tcpListener == null ||
                    this._udpListener == null)
                    return false;
                if (!this._tcpListener.IsBound ||
                    !this._udpListener.IsBound)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public NetServer()
        {
            this._tcpLocalEndPoint = new IPEndPoint(Net.LocalIP(), 0);
            this._udpLocalEndPoint = new IPEndPoint(Net.LocalIP(), 0);
            this._infoLog = NetInfoLog.GetInstance();
        }

        /// <summary>
        /// start listening
        /// </summary>
        /// <exception cref="NetException"></exception>
        public void Start()
        {
            if (this._tcpLocalEndPoint == null)
                throw new NetException("TCP local endpoint is not bound");
            if (this._udpLocalEndPoint == null)
                throw new NetException("UDP local endpoint is not bound");
            if (this._tcpListener != null)
                if (this._tcpListener.IsBound)
                    throw new NetException("TCP socket is already bound");
            if (this._udpListener != null)
                if (this._udpListener.IsBound)
                    throw new NetException("UDP socket is already bound");

            this._tcpListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this._udpListener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            try
            {
                this._tcpListener.Bind(this._tcpLocalEndPoint);
                this._udpListener.Bind(this._udpLocalEndPoint);

                if (!this._tcpListener.IsBound)
                    this._infoLog.Log(this, "TCP socket couldn't be bound!", NetInfoType.Error);
                else
                {
                    this._infoLog.Log(this, string.Format("TCP socket is bound at {0}", this._tcpLocalEndPoint.ToString()));
                    this._tcpListener.Listen(1000);
                    this._tcpListener.BeginAccept(new AsyncCallback(AcceptCallback), this._tcpListener);
                }

                if (!this._udpListener.IsBound)
                    this._infoLog.Log(this, "UDP socket couldn't be bound!", NetInfoType.Error);
                else
                {
                    this._infoLog.Log(this, string.Format("UDP socket is bound at {0}", this._udpLocalEndPoint.ToString()));
                }
            }
            catch (Exception ex)
            {
                this._infoLog.Log(this, ex);
                this._tcpListener.Dispose();
                this._udpListener.Dispose();
                this._tcpListener = null;
                this._udpListener = null;
            }
        }

        /// <summary>
        /// disconnect all connection and stop listening
        /// </summary>
        public void Stop()
        {
            if (this._tcpListener != null)
                if (!this._tcpListener.IsBound)
                    return;
            if (this._udpListener != null)
                if (!this._udpListener.IsBound)
                    return;

            this._clients.ForEach(delegate (NetClient var1)
            {
                var1.Disconnect();
            });
            this._tcpListener.Dispose();
            this._udpListener.Dispose();
            this._clients.Clear();
            this._tcpListener = null;
            this._udpListener = null;
        }

        /// <summary>
        /// tcp connection callback
        /// </summary>
        /// <param name="ar">async result</param>
        private void AcceptCallback(IAsyncResult ar)
        {
            Socket var1 = this._tcpListener.EndAccept(ar);
            NetClient var2 = new NetClient(var1, this._udpListener);
            this._clients.Add(var2);
            this._infoLog.Log(this, string.Format("Connection attempt from {0}", var2.TcpLocalEndPoint.ToString()));
            this._tcpListener.BeginAccept(new AsyncCallback(AcceptCallback), this._tcpListener);
        }
    }
}