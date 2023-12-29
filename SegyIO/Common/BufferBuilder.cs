namespace SegyIO.Common
{
    /// <summary>
    /// 将类的变量转换为字节数组
    /// </summary>
    internal class BufferBuilder
    {
        private readonly List<byte> listBytes;

        /// <summary>
        /// 返回构建的字节数组
        /// </summary>
        public byte[] bytes => listBytes.ToArray();

        public BufferBuilder()
        {
            listBytes = new List<byte>();
        }

        public int Length { get { return listBytes.Count; } }

        #region Append

        public void Append(byte[] val)
        {
            listBytes.AddRange(val);
        }

        public void Append(byte val)
        {
            listBytes.Add(val);
        }

        public void Append(short val)
        {
            byte[] bs = BitConverter.GetBytes(val);
            listBytes.AddRange(bs);
        }

        public void Append(ushort val)
        {
            byte[] bs = BitConverter.GetBytes(val);
            listBytes.AddRange(bs);
        }
        public void Append(int val)
        {
            byte[] bs = BitConverter.GetBytes(val);
            listBytes.AddRange(bs);
        }
        public void Append(uint val)
        {
            byte[] bs = BitConverter.GetBytes(val);
            listBytes.AddRange(bs);
        }

        public void Append(float val)
        {
            byte[] bs = BitConverter.GetBytes(val);
            listBytes.AddRange(bs);
        }

        public void Append(double val)
        {
            byte[] bs = BitConverter.GetBytes(val);
            listBytes.AddRange(bs);
        }

        /// <summary>
        /// 将string类型转换为byte数组
        /// </summary>
        /// <param name="val"></param>
        /// <param name="len">要转换为的byte数组长度,如果字符串中的字符数大于该值则截取,反之则补足</param>
        public void Append(string val, int len)
        {
            if (string.IsNullOrEmpty(val))
                val = "  ";
            byte[] bs = String2Bytes(val, len);
            listBytes.AddRange(bs);
        }

        public void Append(DateTime val)
        {
            byte[] bs = BitConverter.GetBytes(val.ToBinary());
            listBytes.AddRange(bs);
        }

        #endregion

        #region Insert

        public void Insert(byte[] val, int index)
        {
            listBytes.InsertRange(index, val);
        }

        public void Insert(byte val, int index)
        {
            listBytes.Insert(index, val);
        }

        public void Insert(short val, int index)
        {
            byte[] bs = BitConverter.GetBytes(val);
            listBytes.InsertRange(index, bs);
        }

        public void Insert(ushort val, int index)
        {
            byte[] bs = BitConverter.GetBytes(val);
            listBytes.InsertRange(index, bs);
        }
        public void Insert(int val, int index)
        {
            byte[] bs = BitConverter.GetBytes(val);
            listBytes.InsertRange(index, bs);
        }
        public void Insert(uint val, int index)
        {
            byte[] bs = BitConverter.GetBytes(val);
            listBytes.InsertRange(index, bs);
        }

        public void Insert(float val, int index)
        {
            byte[] bs = BitConverter.GetBytes(val);
            listBytes.InsertRange(index, bs);
        }

        public void Insert(double val, int index)
        {
            byte[] bs = BitConverter.GetBytes(val);
            listBytes.InsertRange(index, bs);
        }

        /// <summary>
        /// 将string类型转换为byte数组
        /// </summary>
        /// <param name="val"></param>
        /// <param name="len">要转换为的byte数组长度,如果字符串中的字符数大于该值则截取,反之则补足</param>
        /// <param name="index"></param>
        public void Insert(string val, int len, int index)
        {
            if (string.IsNullOrEmpty(val))
                val = "  ";
            byte[] bs = String2Bytes(val, len);
            listBytes.InsertRange(index, bs);
        }

        #endregion

        /// <summary>
        /// 将string类型转换为byte数组
        /// </summary>
        /// <param name="val"></param>
        /// <param name="len">要转换为的byte数组长度,如果字符串中的字节数大于该值则截取,反之则补足</param>
        public static byte[] String2Bytes(string val, int len)
        {
            List<byte> bytes = new List<byte>();
            byte[] bsTemp = System.Text.Encoding.Default.GetBytes(val);
            if (bsTemp.Length >= len)
            {
                for (int i = 0; i < len; i++)
                {
                    bytes.Add(bsTemp[i]);
                }
            }
            else
            {
                bytes.AddRange(bsTemp);
                for (int i = bsTemp.Length; i < len; i++)
                {
                    const byte b = 32; //补空格
                    bytes.Add(b);
                }
            }
            return bytes.ToArray();
        }

        /// <summary>
        /// 将byte数组转换为string
        /// </summary>
        /// <param name="bytes"></param>
        public static string Bytes2String(byte[] bytes)
        {
            return System.Text.Encoding.Default.GetString(bytes);
        }

        public void Append(bool visible)
        {
            byte[] bs = BitConverter.GetBytes(visible);
            listBytes.AddRange(bs);
        }
    }
}

