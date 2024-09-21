using System;
using System.Collections.Generic;

namespace Solution.NetManager
{
    public class NetSocket
    {
        private NetClient _netClient;
        private NetReceiveBuf _netRecbuf; // 接收到的数据缓存
        private IParseProtocol _iParse; // 数据协议压包和解析接口
        private string _ip;
        private int _port;

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="iParse"></param>
        /// <param name="_connectedCall"></param>
        public void Connect(string ip, int port, IParseProtocol iParse, Action<bool> _connectedCall = null)
        {
            _ip = ip;
            _port = port;
            _netClient = new NetClient();
            _netRecbuf = new NetReceiveBuf(256);
            _iParse = iParse;
            _netClient.Connect(ip, port, _netRecbuf, _connectedCall);
        }

        // 判断连接是否有效
        public bool IsConnected
        {
            get
            {
                if (this._netClient == null)
                {
                    return false;
                }
                else
                {
                    return this._netClient.IsConnected;
                }
            }
        }

        // 以二进制的方式发送消息
        public void SendMsg(byte[] data)
        {
            _netClient.SendWrite(data);
        }
        public void SendMsg(byte[] data, int data_len)
        {
            _netClient.SendWrite(data, data_len);
        }

        private int _netRecbuf_pre_len = 0;
        public void Update()
        {

            if (_netClient != null) _netClient.Update();

            if (!this.IsConnected) return;

            if (_netRecbuf.Length == _netRecbuf_pre_len) return; // 数据有变化才执行

            while (this._netRecbuf.Length > 0)
            {
                // 处理接收到的数据
                lock (this._netRecbuf)
                {
                    int type = _iParse.DataUnpack(this._netRecbuf.MBuf, this._netRecbuf.Length);
                    if (_netClient == null || _netRecbuf == null || _iParse == null) break;

                    // 数据解析成功[删除已解析的数据]
                    if (type > 0) // 不用break，一帧内处理完
                    {
                        this._netRecbuf.DelBytesForReaded(type);
                        //this._netRecbuf.Clear();
                    }
                    // 该协议号不存在，bug错误[丢弃整个缓存包]
                    else if (type == 0)
                    {
                        this._netRecbuf.Clear();
                        break;
                    }
                    // 不合法的数据[丢弃整个缓存包]
                    else if (type == -1)
                    {
                        this._netRecbuf.Clear();
                        Debug.LogWarning("NetManager: Not legitimate data");
                        break;
                    }
                    // 数据不完整
                    else if (type == -2)
                    {
                        //this._netRecbuf.Clear();
                        Debug.LogWarning("NetManager: Big Data miss");
                        break;
                    }
                }
            }
            _netRecbuf_pre_len = this._netRecbuf.Length;
        }

        /// <summary>
        /// 直接断开连接
        /// </summary>
        public void DisConnect()
        {
            if (this._netClient != null)
            {
                this._netClient.Dispose();
                this._netClient = null;
                this._netRecbuf = null;
            }

            if (_iParse != null)
            {
                _iParse.Destory();
                _iParse = null;
            }
        }
    }
}