using System;
using System.Net;
using System.Net.Sockets;

namespace Solution.NetManager
{
    /// <summary>
    /// 基础状态类
    /// </summary>
    public class StateObject
    {
        public TcpClient Client;
        public NetworkStream Stream;
        public NetReceiveBuf NetRecbuf;
        public int ReceiveBufferSize;
        public byte[] Buffer;
    }

    public class NetClient
    {
        // STATE
        private TcpClient _client;
        private NetworkStream _ns;

        // CACHED REFERENCE
        private NetReceiveBuf _netRecbuf; // 缓存接收到的字节数据

        /// <summary>
        /// 连接成功或失败回调函数
        /// </summary>
        public Action<bool> CallbackConnected;
        private int _connectState; // 连接回调状态 0正常 1连接成功 2连接失败

        // PUBLIC 

        public void Update()
        {
            if (_connectState != 0)
            {
                if (_connectState == 1)
                {
                    InitRead();       // 初始化接收消息接口
                    CallbackConnected(true);
                }
                else
                {
                    CallbackConnected(false);
                }
                _connectState = 0;
            }
        }

#region 连接服务器
        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="receiveQueue"></param>
        /// <returns></returns>
        public void Connect(string ip, int port, NetReceiveBuf netRecbuf, Action<bool> resfunc)
        {
            this._connectState = 0;
            this.CallbackConnected = resfunc;
            if (_client != null)
            {
                Debug.Log("网络还存在，先断开之前的网络");
                Dispose(); // 断开网络
                resfunc(false);
            }
            Debug.Log("开始新的Connect");
            _client = new TcpClient();

            this._netRecbuf = netRecbuf;

            IPAddress ipdrr;
            try
            {
                if (IPAddress.TryParse(ip, out ipdrr))
                {
                    //Console.WriterLine("合法IP");
                }
                else
                {
                    //Console.WriterLine("非法IP"); // 可能是域名
                    IPHostEntry host = Dns.GetHostEntry(ip);
                    ipdrr = host.AddressList[0];
                }
                _client.BeginConnect(ipdrr, Convert.ToInt32(port), new AsyncCallback(BeginConnectBack), _client);   //根据服务器的IP地址和端口号 异步连接服务器
            }
            catch (Exception exception)
            {
                this._connectState = 2;
                Debug.LogError("ip解析失败 "+ exception.Message);
            }
        }


        /// <summary>
        /// 连接回调
        /// </summary>
        /// <param name="ar"></param>
        private void BeginConnectBack(IAsyncResult ar)
        {
            TcpClient t = _client; // (TcpClient)ar.AsyncState;
            if (t == null)
            {
                Debug.LogWarning("_client为空，可能该链接已被析构!");
                return;
            }
            try
            {
                if (t.Connected) // 连接成功
                {
                    t.EndConnect(ar); // 函数运行到这里就说明BeginConnect执行完成，异步连接完成
                    this._connectState = 1;
                }
                else // socket连接失败
                {
                    this._connectState = 2;
                }
            }
            catch (Exception exception)
            {
                this._connectState = 2;
                Debug.LogError(exception.Message + "错误异常：" + exception.StackTrace);
            }
        }

        // 判断网络是否已连接
        private bool _isConnected;
        public bool IsConnected
        {
            get
            {
                if (_client == null)
                {
                    return false;
                }
                return _client.Connected;
            }
        }

#endregion
        
#region  接收消息
        // 接收消息
        private void InitRead()
        {
            if (_client!=null && _client.Connected)
            {
                NetworkStream stream = _client.GetStream();
                _ns = stream;
                if (stream.CanRead)
                {
                    StateObject state = new StateObject();
                    state.Client            = _client;
                    state.ReceiveBufferSize = 4096;
                    state.Buffer            = new byte[state.ReceiveBufferSize];
                    state.NetRecbuf         = _netRecbuf;
                    stream.BeginRead(state.Buffer, 0, state.ReceiveBufferSize, new AsyncCallback(ReadAsync), state);
                }
            }
        }

        // 接收消息异步回调函数
        private void ReadAsync(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            if (state.Client == null || !state.Client.Connected) // 连接是否正常
            {
                return;
            }
            NetworkStream stream   = state.Client.GetStream();
            int readCount = stream.EndRead(ar);
            if (readCount > 0)
            {
                lock (state.NetRecbuf)
                {
                    state.NetRecbuf.AddBytesFromSocketBuff(state.Buffer, readCount);
                }
                stream.BeginRead(state.Buffer, 0, state.ReceiveBufferSize, new AsyncCallback(ReadAsync), state);
            }
            else
            {
                stream.Close();
                state.Client.Close();
                stream.Dispose();
                state.Client.Dispose();
                stream = null;
                state = null;
            }
        }
#endregion

#region 发送数据
        // 发送消息------------------------------
        public void SendWrite(byte[] data)
        {
            SendWrite(data, data.Length);
        }
        public void SendWrite(byte[] data, int data_len)
        {
            if (_client != null && _client.Connected)
            {
                NetworkStream stream = _client.GetStream();
                if (stream.CanWrite)
                {
                    stream.BeginWrite(data, 0, data_len, new AsyncCallback(WriteAsync), stream);
                }
            }
        }

        // 异步写数据
        private void WriteAsync(IAsyncResult iar)
        {
            NetworkStream stream = (NetworkStream)iar.AsyncState;
            stream.EndWrite(iar);
        }
#endregion

#region 关闭网络
        public void Dispose()
        {
            Debug.Log("析构Dispose Net");
            if (_ns != null)
            {
                _ns.Close();
                _ns.Dispose();
                _ns = null;
            }
            if (_client != null)
            {
                _client.Close();
                _client.Dispose();
                _client = null;
                _netRecbuf = null;
                Debug.Log("析构网络");
            }
        }
#endregion

    }
}