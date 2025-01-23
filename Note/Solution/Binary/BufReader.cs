using UnityEngine;
using System;
using System.IO;
using System.Collections;
// 无GC操作方式

// 二进制读操作 编码格式需约定为utf-8
public class BufReader
{

    private byte[] m_buffer;

    private int _length;
    private int _origin;
    private int _position;

    public int Length
    {
        get { return _length; }
    }

    public int Position
    {
        get { return _position; }
    }

    public BufReader()
    {

    }

    public BufReader(byte[] buffer)
    {
        Reload(buffer);
    }

    /// <summary>
    /// 重新装载数据
    /// </summary>
    /// <param name="buffer"></param>
    public void Reload(byte[] buffer)
    {
        m_buffer = buffer;
        _origin = 0;
        _position = 0;
        _length = buffer.Length;
    }

    // 截取一定的字节
    public byte[] Read(int offset, int count)
    {
        if ((offset + count) > _length)
        {
            CCLog.LogError("Data out of range " + (offset + count).ToString() + " offset=" + offset + " count=" + count);
            return null;
        }

        byte[] buf = new byte[count];
        Array.Copy(m_buffer, offset, buf, 0, count);
        return buf;
    }

    private void OutErr(int len)
    {
        if((_position + len) > _length)
        {
            throw new ArgumentException(Environment.GetEnvironmentVariable("BufReader > _position more than _length")); // 超出数据范围
        }
    }


    // 读取一个字节
    public byte ReadByte()
    {
        int len = 1;
        OutErr(len);
        byte buf = m_buffer[_position];
        _position += len;
        return buf;
    }

    // 读取一个无符号短整型
    public ushort ReadUInt16()
    {
        int len = 2;
        OutErr(len);
        ushort val = BitConverter.ToUInt16(m_buffer, _position);
        _position += len;
        return val;
    }

    // 读取一个短整型
    public short ReadInt16()
    {
        int len = 2;
        OutErr(len);
        short val = BitConverter.ToInt16(m_buffer, _position);
        _position += len;
        return val;
    }

    // 读取一个整型数据
    public int ReadInt32()
    {
        int len = 4;
        OutErr(len);
        int val = BitConverter.ToInt32(m_buffer, _position);
        _position += len;
        return val;
    }

    // 读取一个长整型数据
    public long ReadInt64()
    {
        int len = 8;
        OutErr(len);
        long val = BitConverter.ToInt64(m_buffer, _position);
        _position += len;
        return val;
    }

    // 读取一个浮点型
    public float ReadFloat()
    {
        int len = 4;
        OutErr(len);
        float val = BitConverter.ToSingle(m_buffer, _position);
        _position += len;
        return val;
    }

    // 读取一个双精度浮点型数据
    public double ReadDouble()
    {
        int len = 8;
        OutErr(len);
        double val = BitConverter.ToDouble(m_buffer, _position);
        _position += len;
        return val;
    }

    // 读取一个字符串<[字串长度][字符串数据]>
    public string ReadString()
    {
        int len = ReadInt32();
        OutErr(len);
        string s = System.Text.Encoding.UTF8.GetString(m_buffer, _position, len);
        _position += len;
        return s;
    }

    // 读取一个字符
    public char ReadChar()
    {
        return (char)ReadByte();
    }

    // 拷贝一段数据到目标字节数组
    public void ReadBytesCopy(byte[] dstbuf, int startIndex)
    {
        int len = ReadInt32();
        OutErr(len);
        int dstbuf_count = dstbuf.Length - startIndex;
        if (dstbuf_count < len)
        {
            throw new ArgumentException(Environment.GetEnvironmentVariable("ReadBytesCopy > dstbuf less than len")); // 异常,容量不足
        }
        for (int i = 0; i < len; i++)
        {
            dstbuf[startIndex + i] = m_buffer[_position + i];
        }
        _position += len;
    }


    // 获取一段字节数组
    public byte[] ReadBytes()
    {
        int len = ReadInt32();
        OutErr(len);
        byte[] buf = Read(_position, len);
        _position += len;
        return buf;
    }

    /// <summary>
    /// 读取一段字节数据到dstbuf
    /// </summary>
    /// <param name="dstbuf"></param>
    /// <param name="startIndex"></param>
    /// <param name="read_count"></param>
    public void ReadBytesCopy(byte[] dstbuf, int startIndex, int read_count)
    {
        OutErr(read_count);
        if(read_count > dstbuf.Length)
        {
            throw new ArgumentOutOfRangeException("read_count", Environment.GetEnvironmentVariable("ArgumentOutOfRange_ReadBytesCopy"));
        }
        for (int i = 0; i < read_count; i++)
        {
            dstbuf[startIndex + i] = m_buffer[_position + i];
        }
        _position += read_count;
    }

    /// <summary>
    /// 读取一段字节数组
    /// </summary>
    /// <param name="len">字节长度</param>
    /// <returns></returns>
    public byte[] ReadBytes(int len)
    {
        OutErr(len);
        byte[] buf = Read(_position, len);
        _position += len;
        return buf;
    }

    // 读取一个布尔型
    public bool ReadBool()
    {
        byte bnum = ReadByte();
        if (bnum == 1)
        {
            return true;
        }
        else if (bnum == 0)
        {
            return false;
        }
        else
        {
            CCLog.LogError("ReadBoolean Error");
            return false;
        }
    }
}

