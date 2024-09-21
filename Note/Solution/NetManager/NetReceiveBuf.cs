using System;

namespace Note.Solution.NetManager
{
    public class NetReceiveBuf
    {
        // STATE

        private byte[] _buf;  // 数据缓存
        private int _origin;   // 原始位置
        private int _capacity; // 缓存容量
        private int _position; // 数据已读位置
        private int _length;   // 缓存的数据长度

        // 长度
        public int Length { get { return _length; } }

        // 数据内容
        public byte[] MBuf { get { return _buf; } }


        public NetReceiveBuf(int size = 1024)
        {
            _buf = new byte[size];
            _capacity = size;
            _origin = 0;
            _position = 0;
            _length = 0;
        }

        // PUBLIC

        /// <summary>
        /// 将socket缓存数据读进来
        /// </summary>
        /// <param name="socketbuf">socket缓存</param>
        /// <param name="readCount">已读取到的数据长度</param>
        public void AddBytesFromSocketBuff(byte[] socketbuf, int readCount)
        {
            int num = _position + readCount;
            if (num > this._capacity)
            {
                // 数据缓存不足，需要拓展缓存
                byte[] newbuf = new byte[num];
                Buffer.BlockCopy(this._buf, 0, newbuf, 0, this._length);
                this._buf = newbuf;
                _capacity = num;
            }

            // 复制新数据
            Buffer.BlockCopy(socketbuf, 0, this._buf, _position, readCount);
            _position = num;
            _length = num;
        }

        /// <summary>
        /// 移除已解析的数据
        /// </summary>
        /// <param name="length"></param>
        public void DelBytesForReaded(int length)
        {
            int num = this._position - length;
            if(num<0)
            {
                Debug.LogError("ERROR: the msg length is out of range");
                return;
            }
            Buffer.BlockCopy(this._buf, length, this._buf, 0, num);
            _position = num;
            _length = num;
        }


        /// <summary>
        /// 返回已写入的字节数组
        /// </summary>
        public byte[] GetBuf()
        {
            byte[] buf = new byte[this._length];
            Buffer.BlockCopy(this._buf, 0, buf, 0, this._length);
            return buf;
        }

        /// <summary>
        /// 清空数据缓存
        /// </summary>
        public void Clear()
        {
            Array.Clear(this._buf, 0, this._buf.Length);
            _position = 0;
            _length = 0;
        }
    }
}