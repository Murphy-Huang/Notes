using UnityEngine;
using System;
using System.IO;
using System.Collections;
// ��GC������ʽ



// ������д���� �����ʽ��Լ��Ϊutf-8
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
    /// ����
    /// </summary>
    public void Clear()
    {
        _origin = 0;
        _position = 0;
        _length = 0;
    }

    // д��һ���޷��Ŷ�����
    public void WriteUInt16(ushort num)
    {
        byte[] buf = BufRWUtil.GetBytes(num);
        WriteBuf(buf, 2);
    }

    // д��һ��������
    public void WriteInt16(short num)
    {
        byte[] buf = BufRWUtil.GetBytes(num);
        WriteBuf(buf, 2);
    }

    // д��һ������
    public void WriteInt32(int num)
    {
        byte[] buf = BufRWUtil.GetBytes(num);
        WriteBuf(buf, 4);
    }

    // д��һ��������
    public void WriteInt64(long num)
    {
        byte[] buf = BufRWUtil.GetBytes(num);
        WriteBuf(buf, 8);
    }

    // д��һ��������
    public void WriteFloat(float num)
    {
        byte[] buf = BufRWUtil.GetBytes(num);
        WriteBuf(buf, 4);
    }

    // д��һ��˫���ȸ�����
    public void WriteDouble(double num)
    {
        byte[] buf = BufRWUtil.GetBytes(num);
        WriteBuf(buf, 8);
    }

    // д��һ���ַ���<[�ִ�����][�ַ�������]>
    public void WriteString(string str)
    {
        int len = 0;
        byte[] buf = BufRWUtil.GetBytes(str, ref len);
        WriteInt32(len);
        WriteBuf(buf, len);
    }

    // д��һ���ַ�
    public void WriteChar(char a)
    {
        WriteByte((byte)a);
    }

    // д��һ���ֽ�
    public void WriteByte(byte b)
    {
        byte[] buf = BufRWUtil.GetBytes(b);
        WriteBuf(buf, 1);
    }

    // д��һ���ֽ�����
    public void WriteBytes(byte[] bs)
    {
        WriteInt32(bs.Length);
        WriteBuf(bs, bs.Length);
    }

    // д��һ��������
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

    // ���һ���ֽ�����
    public void AddBytes(byte[] bs)
    {
        WriteBuf(bs, bs.Length);
    }

    // ������д����ֽ�����
    public byte[] GetBuf()
    {
        byte[] buf = new byte[this._length];
        Buffer.BlockCopy(m_buffer, 0, buf, 0, this._length);
        return buf;
    }

    /// <summary>
    /// д��һ���ֽ�����
    /// </summary>
    /// <param name="buffer">��д����ֽ���</param>
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

