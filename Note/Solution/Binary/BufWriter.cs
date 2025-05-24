using UnityEngine;
using System;
using System.IO;
using System.Collections;
// 无GC操作方式



// 二进制写操作 编码格式需约定为utf-8
public class BufWriter
{
    private byte[] m_buffer;

    private int _capacity;
    private int _length;
    private int _origin;
    private int _position;

    public byte[] MBuf { 
        get { return m_buffer; } 
    }

    public int Length
    {
        get { return _length; }
    }

    public int Position
    {
        get { return _position; }
    }

    public BufWriter(int capacity = 512)
    {
        _capacity = capacity;
        m_buffer = new byte[_capacity];
        _origin = 0;
        _position = 0;
        _length = 0;
    }

    /// <summary>
    /// 清理
    /// </summary>
    public void Clear()
    {
        _origin = 0;
        _position = 0;
        _length = 0;
    }

    // 写入一个无符号短整型
    public void WriteUInt16(ushort num)
    {
        byte[] buf = BufRWUtil.GetBytes(num);
        WriteBuf(buf, 2);
    }

    // 写入一个短整型
    public void WriteInt16(short num)
    {
        byte[] buf = BufRWUtil.GetBytes(num);
        WriteBuf(buf, 2);
    }

    // 写入一个整型
    public void WriteInt32(int num)
    {
        byte[] buf = BufRWUtil.GetBytes(num);
        WriteBuf(buf, 4);
    }

    // 写入一个长整型
    public void WriteInt64(long num)
    {
        byte[] buf = BufRWUtil.GetBytes(num);
        WriteBuf(buf, 8);
    }

    // 写入一个浮点型
    public void WriteFloat(float num)
    {
        byte[] buf = BufRWUtil.GetBytes(num);
        WriteBuf(buf, 4);
    }

    // 写入一个双精度浮点型
    public void WriteDouble(double num)
    {
        byte[] buf = BufRWUtil.GetBytes(num);
        WriteBuf(buf, 8);
    }

    // 写入一个字符串<[字串长度][字符串数据]>
    public void WriteString(string str)
    {
        int len = 0;
        byte[] buf = BufRWUtil.GetBytes(str, ref len);
        WriteInt32(len);
        WriteBuf(buf, len);
    }

    // 写入一个字符
    public void WriteChar(char a)
    {
        WriteByte((byte)a);
    }

    // 写入一个字节
    public void WriteByte(byte b)
    {
        byte[] buf = BufRWUtil.GetBytes(b);
        WriteBuf(buf, 1);
    }

    // 写入一个字节数组
    public void WriteBytes(byte[] bs)
    {
        WriteInt32(bs.Length);
        WriteBuf(bs, bs.Length);
    }

    // 写入一个布尔型
    public void WriteBool(bool b)
    {
        if (b)
        {
            WriteByte((byte)1);
        }
        else
        {
            WriteByte((byte)0);
        }
    }

    // 添加一个字节数组
    public void AddBytes(byte[] bs)
    {
        WriteBuf(bs, bs.Length);
    }

    // 返回已写入的字节数组
    public byte[] GetBuf()
    {
        byte[] buf = new byte[this._length];
        Buffer.BlockCopy(m_buffer, 0, buf, 0, this._length);
        return buf;
    }

    /// <summary>
    /// 写入一个字节数组
    /// </summary>
    /// <param name="buffer">待写入的字节组</param>
    public void WriteBuf(byte[] buffer, int buffer_len)
    {
        int count = buffer_len;
        if (buffer == null)
        {
            throw new ArgumentNullException("buffer", Environment.GetEnvironmentVariable("ArgumentNull_Buffer"));
        }
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException("count", Environment.GetEnvironmentVariable("ArgumentOutOfRange_NeedNonNegNum"));
        }
        if (buffer_len < count)
        {
            throw new ArgumentException(Environment.GetEnvironmentVariable("Argument_InvalidOffLen"));
        }
        int num = this._position + count;
        if (num < 0)
        {
            throw new IOException(Environment.GetEnvironmentVariable("IO.IO_StreamTooLong"));
        }

        if (num > this._capacity)
        {
            int cb = num / 256;
            int len = (cb + 1) * 256;
            byte[] newbuf = new byte[len];
            Buffer.BlockCopy(this.m_buffer, 0, newbuf, 0, this._length);
            this.m_buffer = newbuf;
            _capacity = len;
        }

        if (count <= 8)
        {
            int num2 = count;
            while (--num2 >= 0)
            {
                this.m_buffer[this._position + num2] = buffer[num2];
            }
        }
        else
        {
            Buffer.BlockCopy(buffer, 0, this.m_buffer, this._position, count);
        }

        this._position = num;
        this._length = num;
    }
}

