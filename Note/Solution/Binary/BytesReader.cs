using UnityEngine;
using System;
using System.IO;
using System.Collections;


// 二进制读操作 编码格式需约定为utf-8
public class BytesReader
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

    public BytesReader(byte[] buffer)
    {
        m_buffer = buffer;
        _origin = 0;
        _position = 0;
        _length = buffer.Length;
    }

    // 截取一定的字节
    public byte[] Read(int offset,int count)
    {
        if ((offset + count) > _length)
        {
            CCLog.LogError("Data out of range " + (offset + count).ToString()+ " offset="+ offset+ " count="+ count);
            return null;
        }

        byte[] buf = new byte[count];
        Array.Copy(m_buffer, offset, buf, 0, count);
        return buf;
    }


    // 读取一个字节
    public byte ReadByte()
    {
        int len = 1;
        byte buf = m_buffer[_position];
        _position += len;
        return buf;
    }

    // 读取一个无符号短整型
    public ushort ReadUInt16()
    {
        int len = 2;
        byte[] buf = Read(_position, len);
        _position += len;
        return System.BitConverter.ToUInt16(buf, 0);
    }

    // 读取一个短整型
    public short ReadInt16()
    {
        int len = 2;
        byte[] buf = Read(_position, len);
        _position += len;
        return System.BitConverter.ToInt16(buf, 0); 
    }

    // 读取一个整型数据
    public int ReadInt32()
    {
        int len = 4;
        byte[] buf = Read(_position, len);
        _position += len;
        return System.BitConverter.ToInt32(buf, 0);
    }

    // 读取一个长整型数据
    public long ReadInt64()
    {
        int len = 8;
        byte[] buf = Read(_position, len);
        _position += len;
        return System.BitConverter.ToInt64(buf, 0);
    }

    // 读取一个浮点型
    public float ReadFloat()
    {
        int len = 4;
        byte[] buf = Read(_position, len);
        _position += len;
        return System.BitConverter.ToSingle(buf, 0);
    }

    // 读取一个双精度浮点型数据
    public double ReadDouble()
    {
        int len = 8;
        byte[] buf = Read(_position, len);
        _position += len;
        return System.BitConverter.ToDouble(buf, 0);
    }
	
    // 读取一个字符串<[字串长度][字符串数据]>
    public string ReadString()
    {
        int len = ReadInt32();
        byte[] buf = Read(_position, len);
        _position += len;
        return System.Text.Encoding.UTF8.GetString(buf);
    }

    // 读取一个字符
    public char ReadChar()
    {
        return (char)ReadByte();
    }

    // 获取一段字节数组
    public byte[] ReadBytes()
    {
        int len = ReadInt32();
        byte[] buf = Read(_position, len);
        _position += len;
        return buf;
    }


    /// <summary>
    /// 读取一段字节数组
    /// </summary>
    /// <param name="len">字节长度</param>
    /// <returns></returns>
    public byte[] ReadBytes(int len)
    {
        byte[] buf = Read(_position, len);
        _position += len;
        //for(int i=0; i< buf.Length; i++)
        //{
        //    Debug.LogError(string.Format("{0}", buf[i]));
        //}
        return buf;
    }

    // 读取一个无符号短整型
    public int ReadNumber(byte bytedata)
    {
        byte bufdata = (byte)(bytedata & 0x7f);
        byte[] buf = new byte[2];
        buf[0] = 0x00;
        buf[1] = bufdata;
        return System.BitConverter.ToUInt16(buf, 0);
    }

    // 读取一个布尔型
    public bool ReadBool()
    {
        byte bnum = ReadByte();
        if (bnum==1)
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
