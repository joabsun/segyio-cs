namespace SegyIO.Common
{
    /// <summary>
    /// 封装从字节数组中读取数据的类
    /// </summary>
    internal class BufferStreamReader
    {

        #region 读4字节整型

        /// <summary>
        /// 从源数据中读取4字节整型
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="isSwap"></param>
        /// <returns></returns>
        public static int ReadInt32(byte[] srcBytes, int startIndex, bool isSwap)
        {
            if (isSwap)
            {
                byte[] bytes = GetBytes(srcBytes, startIndex, 4);
                Array.Reverse(bytes);
                return BitConverter.ToInt32(bytes, 0);
            }

            return BitConverter.ToInt32(srcBytes, startIndex);
        }

        /// <summary>
        /// 从源数据中读取4字节整型, 提升索引
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="isSwap"></param>
        /// <returns></returns>
        public static int ReadInt32(byte[] srcBytes, ref int startIndex, bool isSwap)
        {
            if (isSwap)
            {
                byte[] bytes = GetBytes(srcBytes, startIndex, 4);
                startIndex += 4;
                Array.Reverse(bytes);
                return BitConverter.ToInt32(bytes, 0);
            }

            var index = startIndex;
            startIndex += 4;
            return BitConverter.ToInt32(srcBytes, index);
        }

        /// <summary>
        /// 从源数据中读取4字节无符号整型
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="isSwap"></param>
        /// <returns></returns>
        public static uint ReadUInt32(byte[] srcBytes, int startIndex, bool isSwap)
        {
            if (isSwap)
            {
                byte[] bytes = GetBytes(srcBytes, startIndex, 4);
                Array.Reverse(bytes);
                return BitConverter.ToUInt32(bytes, 0);
            }
            return BitConverter.ToUInt32(srcBytes, startIndex);
        }

        /// <summary>
        /// 从源数据中读取4字节无符号整型, 提升索引
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="isSwap"></param>
        /// <returns></returns>
        public static uint ReadUInt32(byte[] srcBytes, ref int startIndex, bool isSwap)
        {
            if (isSwap)
            {
                byte[] bytes = GetBytes(srcBytes, startIndex, 4);
                startIndex += 4;
                Array.Reverse(bytes);
                return BitConverter.ToUInt32(bytes, 0);
            }

            var index = startIndex;
            startIndex += 4;
            return BitConverter.ToUInt32(srcBytes, index);
        }

        #endregion

        #region 读2字节整型

        /// <summary>
        /// 从源数据中读取2字节整型
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="isSwap"></param>
        /// <returns></returns>
        public static short ReadInt16(byte[] srcBytes, int startIndex, bool isSwap)
        {
            if (isSwap)
            {
                byte[] bytes = GetBytes(srcBytes, startIndex, 2);
                Array.Reverse(bytes);
                return BitConverter.ToInt16(bytes, 0);
            }
            return BitConverter.ToInt16(srcBytes, startIndex);
        }

        /// <summary>
        /// 从源数据中读取2字节整型, 提升索引
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="isSwap"></param>
        /// <returns></returns>
        public static short ReadInt16(byte[] srcBytes, ref int startIndex, bool isSwap)
        {
            if (isSwap)
            {
                byte[] bytes = GetBytes(srcBytes, startIndex, 2);
                startIndex += 2;
                Array.Reverse(bytes);
                return BitConverter.ToInt16(bytes, 0);
            }

            var i = BitConverter.ToInt16(srcBytes, startIndex);
            startIndex += 2;
            return i;
        }

        /// <summary>
        /// 从源数据中读取2字节无符号整型
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="isSwap"></param>
        /// <returns></returns>
        public static ushort ReadUInt16(byte[] srcBytes, int startIndex, bool isSwap)
        {
            if (isSwap)
            {
                byte[] bytes = GetBytes(srcBytes, startIndex, 2);
                Array.Reverse(bytes);
                return BitConverter.ToUInt16(bytes, 0);
            }
            return BitConverter.ToUInt16(srcBytes, startIndex);
        }

        /// <summary>
        /// 从源数据中读取2字节整型, 提升索引
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="isSwap"></param>
        /// <returns></returns>
        public static ushort ReadUInt16(byte[] srcBytes, ref int startIndex, bool isSwap)
        {
            if (isSwap)
            {
                byte[] bytes = GetBytes(srcBytes, startIndex, 2);
                startIndex += 2;
                Array.Reverse(bytes);
                return BitConverter.ToUInt16(bytes, 0);
            }

            var i = BitConverter.ToUInt16(srcBytes, startIndex);
            startIndex += 2;
            return i;
        }

        #endregion

        #region 读1字节整型

        /// <summary>
        /// 从源数据中读取1字节整型
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static byte ReadByte(byte[] srcBytes, int startIndex)
        {
            return srcBytes[startIndex];
        }

        /// <summary>
        /// 从源数据中读取1字节整型
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static byte ReadByte(byte[] srcBytes, ref int startIndex)
        {
            var index = startIndex;
            startIndex++;
            return srcBytes[index];
        }

        #endregion

        #region 读字节数组

        /// <summary>
        /// 从源数据中读取字节数组
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] ReadBytes(byte[] srcBytes, int startIndex, int length)
        {
            byte[] bytes = GetBytes(srcBytes, startIndex, length);
            return bytes;
        }

        /// <summary>
        /// 从源数据中读取字节数组
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] ReadBytes(byte[] srcBytes, ref int startIndex, int length)
        {
            var index = startIndex;
            startIndex += length;
            byte[] bytes = GetBytes(srcBytes, index, length);
            return bytes;
        }

        #endregion

        #region 读浮点数

        /// <summary>
        /// 从源数据中读取 float
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="isSwap"></param>
        /// <returns></returns>
        public static float ReadFloat(byte[] srcBytes, int startIndex, bool isSwap)
        {
            if (isSwap)
            {
                byte[] bytes = GetBytes(srcBytes, startIndex, 4);
                Array.Reverse(bytes);
                return BitConverter.ToSingle(bytes, 0);
            }
            return BitConverter.ToSingle(srcBytes, startIndex);
        }

        /// <summary>
        /// 从源数据中读取 float
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="isSwap"></param>
        /// <returns></returns>
        public static float ReadFloat(byte[] srcBytes, ref int startIndex, bool isSwap)
        {
            var index = startIndex;
            startIndex += 4;
            if (isSwap)
            {
                byte[] bytes = GetBytes(srcBytes, index, 4);
                Array.Reverse(bytes);
                return BitConverter.ToSingle(bytes, 0);
            }
            return BitConverter.ToSingle(srcBytes, index);
        }

        /// <summary>
        /// 从源数据中读取double
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="isSwap"></param>
        /// <returns></returns>
        public static double ReadDouble(byte[] srcBytes, int startIndex, bool isSwap)
        {
            if (isSwap)
            {
                byte[] bytes = GetBytes(srcBytes, startIndex, 8);
                Array.Reverse(bytes);
                return BitConverter.ToDouble(bytes, 0);
            }
            return BitConverter.ToDouble(srcBytes, startIndex);
        }

        /// <summary>
        /// 从源数据中读取double
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="isSwap"></param>
        /// <returns></returns>
        public static double ReadDouble(byte[] srcBytes, ref int startIndex, bool isSwap)
        {
            var index = startIndex;
            startIndex += 8;
            if (isSwap)
            {
                byte[] bytes = GetBytes(srcBytes, index, 8);
                Array.Reverse(bytes);
                return BitConverter.ToDouble(bytes, 0);
            }
            return BitConverter.ToDouble(srcBytes, index);
        }

        /// <summary>
        /// 从源数据中读取IBM float
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static float ReadIbmFloat(byte[] srcBytes, int startIndex)
        {
            byte[] bytes = GetBytes(srcBytes, startIndex, 4);
            //return IBMSingle_Back.ToSingle(bytes);
            return FloatConverter.Ibm2Float(bytes);
        }

        /// <summary>
        /// 从源数据中读取IBM float
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static float ReadIbmFloat(byte[] srcBytes, ref int startIndex)
        {
            var index = startIndex;
            startIndex += 4;
            byte[] bytes = GetBytes(srcBytes, index, 4);
            return FloatConverter.Ibm2Float(bytes);
        }

        #endregion

        /// <summary>
        /// 从字节数组提取数据
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static byte[] GetBytes(byte[] srcBytes, int startIndex, int length)
        {
            byte[] bytes = new byte[length];
            for (int i = 0; i < length; i++)
            {
                bytes[i] = srcBytes[i + startIndex];
            }
            return bytes;
        }

    }
}
