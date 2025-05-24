using System;

public static unsafe class BufRWUtil
{
    private static byte[] gbuf = new byte[1024];

    public static byte[] GetBytes(byte value)
    {
        gbuf[0] = value;
        return gbuf;
    }

    public static byte[] GetBytes(ushort value)
    {
        return GetBytes((short)value);
    }

    public static byte[] GetBytes(short value) // 2
    {
        fixed (byte* ptr = gbuf)
        {
            *(short*)ptr = value;
        }

        return gbuf;
    }

    public unsafe static byte[] GetBytes(int value) // 4
    {
        fixed (byte* ptr = gbuf)
        {
            *(int*)ptr = value;
        }

        return gbuf;
    }

    public unsafe static byte[] GetBytes(long value) // 8
    {
        fixed (byte* ptr = gbuf)
        {
            *(long*)ptr = value;
        }

        return gbuf;
    }

    public unsafe static byte[] GetBytes(float value)
    {
        return GetBytes(*(int*)(&value));
    }

    public unsafe static byte[] GetBytes(double value)
    {
        return GetBytes(*(long*)(&value));
    }

    public unsafe static byte[] GetBytes(string s, ref int l)
    {
        if (s == null)
        {
            l = 0;
            return gbuf;
        }
        if(s.Length > 1024)
        {
            throw new ArgumentException(Environment.GetEnvironmentVariable("BufRWUtil > s out the range 1024")); // Òì³£,×Ö·û´®¹ý³¤
        }
        int bytes = System.Text.Encoding.UTF8.GetBytes(s, 0, s.Length, gbuf, 0);
        l = bytes;
        return gbuf;
    }
}
