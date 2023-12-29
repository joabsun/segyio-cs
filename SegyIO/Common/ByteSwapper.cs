namespace SegyIO.Common
{
    /// <summary>
    /// Swap byte order
    /// </summary>
    internal class ByteSwapper
    {
        public static int Swap(int int32)
        {
            return (int32 & 0xFF) << 24 | (int32 >> 8 & 0xFF) << 16 | (int32 >> 16 & 0xFF) << 8 | (int32 >> 24 & 0xFF);
        }

        public static uint Swap(uint int32)
        {
            return (int32 & 0xFF) << 24 | (int32 >> 8 & 0xFF) << 16 | (int32 >> 16 & 0xFF) << 8 | (int32 >> 24 & 0xFF);
        }

        public static short Swap(short int16)
        {
            return (short)((int16 & 0xFF) << 8 | ((int16 >> 8) & 0xFF) << 0);
        }

        public static ushort Swap(ushort int16)
        {
            return (ushort)((int16 & 0xFF) << 8 | ((int16 >> 8) & 0xFF) << 0);
        }

        public static long Swap(long value)
        {
            var b1 = (value >> 0) & 0xff;
            var b2 = (value >> 8) & 0xff;
            var b3 = (value >> 16) & 0xff;
            var b4 = (value >> 24) & 0xff;
            var b5 = (value >> 32) & 0xff;
            var b6 = (value >> 40) & 0xff;
            var b7 = (value >> 48) & 0xff;
            var b8 = (value >> 56) & 0xff;

            return b1 << 56 | b2 << 48 | b3 << 40 | b4 << 32 |
                   b5 << 24 | b6 << 16 | b7 << 8 | b8 << 0;
        }

        public static double Swap(double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return BitConverter.ToDouble(bytes,0);
        }

        public static float Swap(float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }

    }
}
