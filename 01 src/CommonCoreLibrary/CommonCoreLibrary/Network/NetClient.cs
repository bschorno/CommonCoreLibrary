using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace CommonCoreLibrary.Network
{
    public class NetClient : INet
    {
        private Socket              _tcpSocket;
        private Socket              _udpSocket;
        private EndPoint            _tcpLocalEndPoint;
        private EndPoint            _udpLocalEndPoint;
        private EndPoint            _tcpRemoteEndPoint;
        private EndPoint            _udpRemoteEndPoint;
        private ManualResetEvent    _tcpMre;
        private ManualResetEvent    _udpMre;
        private NetInfoLog          _infoLog;
        private NetChunkCollector   _chunkCollector;
        private int                 _bufferSize            = Net.DEFAULT_BUFFER_SIZE;

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
        /// remote endpoint of tcp connection
        /// </summary>
        public EndPoint TcpRemoteEndPoint
        {
            get
            {
                return this._tcpRemoteEndPoint;
            }
        }

        /// <summary>
        /// remote endpoint of udp connection
        /// </summary>
        public EndPoint UdpRemoteEndPoint
        {
            get
            {
                return this._udpRemoteEndPoint;
            }
        }

        /// <summary>
        /// is tcp socket connected?
        /// </summary>
        public bool IsTcpConnected
        {
            get
            {
                if (this._tcpSocket == null)
                    return false;
                return this._tcpSocket.Connected;
            }
        }

        /// <summary>
        /// is udp socket connected?
        /// </summary>
        public bool IsUdpConnected
        {
            get
            {
                if (this._udpSocket == null)
                    return false;
                return this._udpLocalEndPoint != null;
            }
        }

        /// <summary>
        ///     
        /// </summary>
        public NetClient()
        {
            this._tcpLocalEndPoint = new IPEndPoint(Net.LocalIP(), 0);
            this._udpLocalEndPoint = new IPEndPoint(Net.LocalIP(), 0);
            this._tcpRemoteEndPoint = null;
            this._udpRemoteEndPoint = null;
            this._infoLog = NetInfoLog.GetInstance();
            this._chunkCollector = new NetChunkCollector();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tcpSocket">tcp socket (has to be bound)</param>
        /// <param name="udpSocket">udp socket (has to be bound)</param>
        /// <exception cref="ArgumentNullException"></exception>
        public NetClient(Socket tcpSocket, Socket udpSocket)
            : this()
        {
            if (tcpSocket == null)
                throw new ArgumentNullException("Is null", "tcpSocket");
            if (udpSocket == null)
                throw new ArgumentNullException("Is null", "udpSocket");

            this._tcpSocket = tcpSocket;
            this._udpSocket = udpSocket;

            this._tcpLocalEndPoint = this._tcpSocket.LocalEndPoint;
            this._udpLocalEndPoint = this._udpSocket.LocalEndPoint;
            this._tcpRemoteEndPoint = this._tcpSocket.RemoteEndPoint;
            this._udpRemoteEndPoint = null;
        }

        /// <summary>
        /// connect socket to entpoint
        /// </summary>
        /// <param name="ip">ip-address of remote endpoint</param>
        /// <param name="port">port of remote endpoint</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NetException"></exception>
        public void Connect(string ip, ushort port)
        {
            if (ip == string.Empty)
                throw new ArgumentNullException("Is empty", "ip");
            if (this._tcpLocalEndPoint == null)
                throw new NetException("TCP local endpoint is not bound");
            if (this._udpLocalEndPoint == null)
                throw new NetException("UDP local endpoint is not bound");

            try
            {
                this._tcpRemoteEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            }
            catch (FormatException ex)
            {
                throw new NetException("IP address is invalid");
            }

            this._tcpSocket.BeginConnect(this._tcpRemoteEndPoint, new AsyncCallback(TcpConnectionCallback), this._tcpSocket);
        }

        /// <summary>
        /// disconnect from remote socket
        /// </summary>
        public void Disconnect()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// send message to server by tcp
        /// </summary>
        /// <param name="message">message</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NetException"></exception>
        public void TcpSend(NetMessage message)
        {
            if (message == null)
                throw new ArgumentNullException("Is null", "message");
            if (!this.IsTcpConnected)
                throw new NetException("TCP socket isn't bound!");

            lock (this._tcpSocket)
            {
                this._tcpMre.Reset();
                NetBuffer var1 = new NetBuffer();
                //var1.Data = 
                NetChunk[] var2 = this._chunkCollector.GetChunk(var1);
                for (int var3 = 0; var3 > var2.Length; var3++)
                {
                    this._tcpSocket.BeginSend(var2[var3].Buffer.Data, 0, var2[var3].Buffer.Length, 0, new AsyncCallback(TcpSendCallback), var2[var3].Buffer);
                    this._tcpMre.WaitOne();
                }
            }
        }

        /// <summary>
        /// send message to server by udp
        /// </summary>
        /// <param name="message">message</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NetException"></exception>
        public void UdpSend(NetMessage message)
        {
            if (message == null)
                throw new ArgumentNullException("Is null", "message");
            if (!this.IsUdpConnected)
                throw new NetException("UDP socket isn't bound!");

            lock (this._udpSocket)
            {
                this._udpMre.Reset();
                NetBuffer var1 = new NetBuffer();
                //var1.Data =
                NetChunk[] var2 = this._chunkCollector.GetChunk(var1);
                for (int var3 = 0; var3 > var2.Length; var3++)
                {
                    this._udpSocket.BeginSendTo(var2[var3].Buffer.Data, 0, var2[var3].Buffer.Length, 0, this._udpRemoteEndPoint, new AsyncCallback(UdpSendCallback), var2[3].Buffer);
                    this._udpMre.WaitOne();
                }
            }
        }

        /// <summary>
        /// tcp begin receive from socket
        /// </summary>
        public void TcpReceive()
        {
            NetBuffer var1 = new NetBuffer(this._bufferSize);
            this._tcpSocket.BeginReceive(var1.Data, 0, this._bufferSize, 0, new AsyncCallback(TcpReceiveCallback), var1);
        }

        /// <summary>
        /// udp begin receive from socket
        /// </summary>
        private void UdpReceive()
        {
            NetBuffer var1 = new NetBuffer(this._bufferSize);
            this._udpSocket.BeginReceiveFrom(var1.Data, 0, this._bufferSize, 0, ref this._udpRemoteEndPoint, new AsyncCallback(UdpReceiveCallback), var1);
        }

        /// <summary>
        /// tcp connection callback
        /// </summary>
        /// <param name="ar">async result</param>
        private void TcpConnectionCallback(IAsyncResult ar)
        {
            this._tcpSocket.EndConnect(ar);

        }

        /// <summary>
        /// tcp data receive callback
        /// </summary>
        /// <param name="ar">async result</param>
        private void TcpReceiveCallback(IAsyncResult ar)
        {
            NetBuffer var1 = (NetBuffer)ar.AsyncState;
            int var2 = this._tcpSocket.EndReceive(ar);

        }

        /// <summary>
        /// udp data receive callback
        /// </summary>
        /// <param name="ar">async result</param>
        private void UdpReceiveCallback(IAsyncResult ar)
        {
            NetBuffer var1 = (NetBuffer)ar.AsyncState;
            int var2 = this._udpSocket.EndReceiveFrom(ar, ref this._udpRemoteEndPoint);

        }

        /// <summary>
        /// tcp data send callback
        /// </summary>
        /// <param name="ar">async result</param>
        private void TcpSendCallback(IAsyncResult ar)
        {
            NetBuffer var1 = (NetBuffer)ar.AsyncState;
            int var2 = this._tcpSocket.EndSend(ar);
            this._tcpMre.Set();
        }

        /// <summary>
        /// udp data send callback
        /// </summary>
        /// <param name="ar">async result</param>
        private void UdpSendCallback(IAsyncResult ar)
        {
            NetBuffer var1 = (NetBuffer)ar.AsyncState;
            int var2 = this._udpSocket.EndSend(ar);
            this._udpMre.Set();
        }
    }
}