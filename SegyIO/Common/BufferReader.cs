namespace SegyIO.Common
{
    /// <summary>
    /// 封装从缓冲区(即字节数组)中读取数据的类
    /// </summary>
    internal class BufferReader : IDisposable
    {
        #region 构造函数及属性

        public BufferReader(byte[] buffer, bool isSwap)
        {
            Buffer = buffer;
            IsSwap = isSwap;
        }

        /// <summary>
        /// 缓冲区(即字节数组)
        /// </summary>
        private byte[] Buffer { get; set; }

        /// <summary>
        /// 是否交换字节序
        /// </summary>
        private bool IsSwap { get; set; }

        /// <summary>
        /// 当前索引,下次读数据将从当前索引开始
        /// </summary>
        private int CurrentIndex { get; set; }

        #endregion

        /// <summary>
        /// 改变读取位置
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="seekLocation"></param>
        internal void Seek(int offset, SeekLocation seekLocation)
        {
            if (seekLocation == SeekLocation.Begin)
                CurrentIndex = offset;
            else
                CurrentIndex += offset;
        }

        /// <summary>
        /// 读取Boolean,提升索引位置1
        /// </summary>
        public bool ReadBoolean()
        {
            byte[] bytes = GetBytes(Buffer, CurrentIndex, 1);
            CurrentIndex += 1;

            return BitConverter.ToBoolean(bytes, 0);
        }

        /// <summary>
        /// 读取4字节整型,提升索引位置4
        /// </summary>
        public int ReadInt32()
        {
            byte[] bytes = GetBytes(Buffer, CurrentIndex, 4);
            CurrentIndex += 4;

            if (IsSwap)
                Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// 读取4字节无符号整型,提升索引位置4
        /// </summary>
        public uint ReadUInt32()
        {
            byte[] bytes = GetBytes(Buffer, CurrentIndex, 4);
            CurrentIndex += 4;

            if (IsSwap)
                Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }

        /// <summary>
        /// 读取2字节整型,提升索引位置2
        /// </summary>
        public short ReadInt16()
        {
            byte[] bytes = GetBytes(Buffer, CurrentIndex, 2);
            CurrentIndex += 2;

            if (IsSwap)
                Array.Reverse(bytes);
            return BitConverter.ToInt16(bytes, 0);
        }

        /// <summary>
        /// 读取2字节无符号整型,提升索引位置2
        /// </summary>
        public ushort ReadUInt16()
        {
            byte[] bytes = GetBytes(Buffer, CurrentIndex, 2);
            CurrentIndex += 2;

            if (IsSwap)
                Array.Reverse(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }

        /// <summary>
        /// 从源数据中读取1字节整型,提升索引位置1
        /// </summary>
        public byte ReadByte()
        {
            var b = Buffer[CurrentIndex];
            CurrentIndex++;

            return b;
        }

        /// <summary>
        /// 从源数据中读取Char,提升索引位置2
        /// </summary>
        public char ReadChar()
        {
            byte[] bytes = GetBytes(Buffer, CurrentIndex, 2);
            CurrentIndex += 2;

            return BitConverter.ToChar(bytes, 0);
        }

        /// <summary>
        /// 从源数据中读取字节数组,提升索引位置length
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] ReadBytes(int length)
        {
            byte[] bytes = GetBytes(Buffer, CurrentIndex, length);
            CurrentIndex += length;

            return bytes;
        }

        /// <summary>
        /// 从源数据中读取IEEE float,提升索引位置4
        /// </summary>
        public float ReadFloat()
        {
            byte[] bytes = GetBytes(Buffer, CurrentIndex, 4);
            CurrentIndex += 4;

            if (IsSwap)
                Array.Reverse(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }

        /// <summary>
        /// 从源数据中读取IBM float,提升索引位置4
        /// </summary>
        public float ReadIbmFloat()
        {
            byte[] bytes = GetBytes(Buffer, CurrentIndex, 4);
            CurrentIndex += 4;

            return FloatConverter.Ibm2Float(bytes);
        }

        /// <summary>
        /// 读取IEEE double型,提升索引位置8
        /// </summary>
        public double ReadDouble()
        {
            byte[] bytes = GetBytes(Buffer, CurrentIndex, 8);
            CurrentIndex += 8;

            if (IsSwap)
                Array.Reverse(bytes);
            return BitConverter.ToDouble(bytes, 0);
        }

        /// <summary>
        /// 读取DateTime型,提升索引位置8
        /// </summary>
        public DateTime ReadDateTime()
        {
            byte[] bytes = GetBytes(Buffer, CurrentIndex, 8);
            CurrentIndex += 8;

            long l = BitConverter.ToInt64(bytes, 0);
            return DateTime.FromBinary(l);
        }

        /// <summary>
        /// 读取string,提升索引位置length
        /// </summary>
        public string ReadString(int length)
        {
            byte[] bytes = GetBytes(Buffer, CurrentIndex, length);
            CurrentIndex += length;

            return Bytes2String(bytes);
        }

        /// <summary>
        /// 从字节数组提取数据
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private byte[] GetBytes(byte[] srcBytes, int startIndex, int length)
        {
            byte[] bytes = new byte[length];
            for (int i = 0; i < length; i++)
            {
                bytes[i] = srcBytes[i + startIndex];
            }
            return bytes;
        }

        public void Dispose()
        {
            Buffer = null;
        }

        /// <summary>
        /// 将byte数组转换为string
        /// </summary>
        /// <param name="bytes"></param>
        private static string Bytes2String(byte[] bytes)
        {
            return System.Text.Encoding.Default.GetString(bytes);
        }
    }

    internal enum SeekLocation
    {
        Begin,
        Current
    }
}
