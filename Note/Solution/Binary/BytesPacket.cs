using System.Collections.Generic;

public class BytesPacket
{
    private ushort _packetid = 0;
    private List<byte> _buffer = null;
    private ushort _offset = 0;

    public BytesPacket()
    {
        this._buffer = new List<byte>();
        this._offset = 0;
    }

    public BytesPacket(byte[] bytes)
    {
        List<byte> buffer = new List<byte>(bytes);

        this._buffer = buffer;
        this._offset = 0;
    }

    /// <summary>
    /// 获取数据内容
    /// </summary>
    /// <returns></returns>
    public byte[] GetBuf()
    {
        return this._buffer.ToArray();
    }


    public void Encode(ushort packetId)
    {
        this._packetid = packetId;
        List<byte> all = new List<byte>();

        // 包体总长度 = (协议内容长度 + 协议号长度)
        //PacketUtil.WriteUshort(all, (ushort)(this._buffer.Count + 2));
        //PacketUtil.WriteUshort(all, packetId);

        PacketUtil.WriteUshort(all, (ushort)(this._buffer.Count));
        PacketUtil.WriteUshort(all, packetId);
        all.AddRange(this._buffer);

        this._buffer = all;
    }

    public ushort ReadPacketid()
    {
        if (this._packetid == 0)
        {
            this._packetid = ReadUshort();
        }
        return this._packetid;
    }

    public List<byte> GetBuffer()
    {
        return this._buffer;
    }

    public byte[] GetBufferBytes()
    {
        return this._buffer.ToArray();
    }


    public void WriteByte(byte v)
    {
        PacketUtil.WriteByte(this._buffer, v);
    }

    public void WriteBytes(byte[] v)
    {
        PacketUtil.WriteBytes(this._buffer, v);
    }

    public void WriteSbyte(sbyte v)
    {
        PacketUtil.WriteSbyte(this._buffer, v);
    }

    public void WriteUshort(ushort v)
    {
        PacketUtil.WriteUshort(this._buffer, v);
    }

    public void WriteShort(short v)
    {
        PacketUtil.WriteShort(this._buffer, v);
    }

    public void WriteUint(uint v)
    {
        PacketUtil.WriteUint(this._buffer, v);
    }

    public void WriteInt(int v)
    {
        PacketUtil.WriteInt(this._buffer, v);
    }

    public void WriteUlong(ulong v)
    {
        PacketUtil.WriteUlong(this._buffer, v);
    }

    public void WriteLong(long v)
    {
        PacketUtil.WriteLong(this._buffer, v);
    }

    public void WriteFloat(float v)
    {
        PacketUtil.WriteFloat(this._buffer, v);
    }

    public void WriteDouble(double v)
    {
        PacketUtil.WriteDouble(this._buffer, v);
    }

    public void WriteString(string v)
    {
        PacketUtil.WriteString(this._buffer, v);
    }

    public void WriteStringNoLength(string v)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(v);
        this._buffer.AddRange(bytes);
    }

    public void WriteBuffer(List<byte> buffer)
    {
        this._buffer.AddRange(buffer);
    }


    public byte ReadByte()
    {
        byte[] data = this._buffer.GetRange(this._offset, 1).ToArray();
        this._offset += 1;

        return PacketUtil.ReadByte(data);
    }

    public sbyte ReadSbyte()
    {
        byte[] data = this._buffer.GetRange(this._offset, 1).ToArray();
        this._offset += 1;

        return PacketUtil.ReadSbyte(data);
    }

    public ushort ReadUshort()
    {
        byte[] data = this._buffer.GetRange(this._offset, 2).ToArray();
        this._offset += 2;

        return PacketUtil.ReadUshort(data);
    }

    public short ReadShort()
    {
        byte[] data = this._buffer.GetRange(this._offset, 2).ToArray();
        this._offset += 2;

        return PacketUtil.ReadShort(data);
    }

    public uint ReadUint()
    {
        byte[] data = this._buffer.GetRange(this._offset, 4).ToArray();
        this._offset += 4;
        return PacketUtil.ReadUint(data);
    }

    public int ReadInt()
    {
        byte[] data = this._buffer.GetRange(this._offset, 4).ToArray();
        this._offset += 4;
        return PacketUtil.ReadInt(data);
    }

    public ulong ReadUlong()
    {
        byte[] data = this._buffer.GetRange(this._offset, 8).ToArray();
        this._offset += 8;
        return PacketUtil.ReadUlong(data);
    }

    public long ReadLong()
    {
        CCLog.LogError(this._buffer.Count+" "+this._offset);
        byte[] data = this._buffer.GetRange(this._offset, 8).ToArray();
        this._offset += 8;
        return PacketUtil.ReadLong(data);
    }

    public float ReadFloat()
    {
        byte[] data = this._buffer.GetRange(this._offset, 4).ToArray();
        this._offset += 4;
        return PacketUtil.ReadFloat(data);
    }

    public double ReadDouble()
    {
        byte[] data = this._buffer.GetRange(this._offset, 8).ToArray();
        this._offset += 8;
        return PacketUtil.ReadDouble(data);
    }

    public string ReadString()
    {
        ushort len = this.ReadUshort();

        byte[] data = this._buffer.GetRange(this._offset, len).ToArray();
        this._offset += len;

        return PacketUtil.ReadString(data);
    }

}
