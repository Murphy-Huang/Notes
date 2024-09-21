using System;
using System.Collections.Generic;
using Solution.Util;

namespace Solution.NetManager
{
    public class NetManager : Manager
    {
        // SINGLETON
        private static NetManager instance;
        public static NetManager ins
        {
            get
            {
                if (instance == null)
                {
                    Debug.LogError("NetManager not create");
                }
                return instance;
            }
        }

        // STATE
        private Dictionary<int, NetSocket> _nets = new Dictionary<int, NetSocket>();
        private int _keyIndex = 0;

        // LIFECYCLE METHODS
        void Awake()
        {
            instance = this;
        }
        
        void Update ()
        {
            // 此种遍历方式可以避免每帧GC
            var enumerator = _nets.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var element = enumerator.Current;
                element.Value.Update();
            }
        }

        void OnDestroy()
        {
            ClearNet();
            instance = null;
        }

        // PRIVATE

        /// <summary>
        /// 获取一个键值
        /// </summary>
        /// <returns></returns>
        private int GetKey()
        {
            if(_keyIndex >= int.MaxValue)
            {
                _keyIndex = 0;
            }
            do 
            {
                _keyIndex++;
            } 
            while (_nets.ContainsKey(_keyIndex));
            return _keyIndex;
        }

        // PUBLIC

        /// <summary>
        /// 创建一个新的socket连接
        /// </summary>
        /// <param name="net_id">联网id,-1表示会随机一个联网id</param>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="iParse"></param>
        /// <param name="_connectedCall"></param>
        /// 返回键值标识
        public int Connect(int net_id, string ip, int port, IParseProtocol iParse, Action<bool> _connectedCall = null)
        {
            int key;
            if (net_id > 0) 
            {
                key = net_id;
            }
            else
            {
                key = GetKey();
            }
            if (_nets.ContainsKey(key))
            {
                CCLog.LogError("联网id已存在 key=" + key);
                return -1;
            }
            NetSocket socket = new NetSocket();
            socket.Connect(ip, port, iParse, _connectedCall);
            _nets[key] = socket;
            iParse.SetNetId(key);
            return key;
        }


        /// <summary>
        /// 获取一个网络连接
        /// </summary>
        /// <param name="key"></param>
        /// <returns>NetSocket</returns> 
        public NetSocket GetNet(int key)
        {
            if (_nets.ContainsKey(key))
            {
                return _nets[key];
            }
            return null;
        }

        /// <summary>
        /// 获取当前网络连接数量
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _nets.Count;
        }

        /// <summary>
        /// 是否在连接中
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsConnected(int key)
        {
            if (_nets.ContainsKey(key))
            {
                return _nets[key].IsConnected;
            }
            return false;
        }

        /// <summary>
        /// 关闭某个网络
        /// </summary>
        /// <param name="key"></param>
        public void CloseNet(int key)
        {
            if (_nets.ContainsKey(key))
            {
                NetSocket net = _nets[key];
                net.DisConnect();
                net = null;
                _nets.Remove(key);
            }
        }

        /// <summary>
        /// 清理存储的所有网络
        /// </summary>
        public void ClearNet()
        {
            if (_nets != null)
            {
                foreach (KeyValuePair<int, NetSocket> item in _nets)
                {
                    item.Value.DisConnect();
                }
                _nets.Clear();
                _nets = null;
            }
        }
    }
}