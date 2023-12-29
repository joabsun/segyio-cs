namespace SegyIO.Common
{
    internal class FloatConverter
    {
        /// <summary>
        /// Convert Ibm float number(Big-Endian) to float number，Verified
        /// </summary>
        /// <param name="fData"></param>
        /// <returns></returns>
        public static float Ibm2Float(float fData)
        {
            byte[] bytes = BitConverter.GetBytes(fData);
            return Ibm2Float(bytes);
        }

        /// <summary>
        /// Convert Ibm float(Big-Endian) bytes to IEEE float Bytes
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] IbmBytes2IeeeBytes(byte[] bytes)
        {
            var temp = Ibm2Float(bytes);
            return BitConverter.GetBytes(temp);
        }

        /// <summary>
        /// Convert Ibm float(Big-Endian) bytes to float number，Verified
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static float Ibm2Float(byte[] bytes)
        {
            //sign
            var sign = (bytes[0] & 128) >> 7;
            // exponent (base is 16)
            var exp = (bytes[0] & 127) - 64;

            // fraction from the last 3 bytes
            var frac = (bytes[1] << 16) + (bytes[2] << 8) + bytes[3];
            // compute mantissa
            //double m = frac / (Math.Pow(2, 24));
            const double e24 = 16777216.0; //Math.Pow(2, 24)
            var m = frac / e24;
            var temp = (float)((1 - 2 * sign) * Math.Pow(16, exp) * m);
            if (Math.Abs(temp) <= 5.877472E-39)
                temp = 0;
            return temp;
        }

        /// <summary>
        /// Convert a ieee float number(Little-Endian) to the byte array of a ibm float number(Big-Endian)，Verified
        /// </summary>
        /// <param name="ieee">little endian ieee float</param>
        /// <returns></returns>
        public static byte[] Ieee2IbmB(float ieee)
        {
            byte[] bytes = BitConverter.GetBytes(ieee);

            Array.Reverse(bytes);

            //get the sign,exp,frac of the ieee float 
            var sign = (bytes[0] & 128) >> 7;
            var exp = ((bytes[0] & 127) << 1) + ((bytes[1] & 128) >> 7);
            var frac = ((bytes[1] & 127) << 16) + (bytes[2] << 8) + bytes[3];

            exp = exp - 127 + 1;
            var remainExp = exp & 0x03;
            var exp1 = exp >> 2 + 64;

            frac = (frac & 0x007FFFFF) | 0x00800000;

            if (remainExp != 0)
            {
                exp1 += 1;
                frac = frac >> (4 - remainExp);
            }

            exp1 = exp1 + 64;
            sign = sign << 7;
            bytes[0] = (byte)(sign + exp1);
            bytes[1] = (byte)((frac & 0x00ff0000) >> 16);
            bytes[2] = (byte)((frac & 0x0000ff00) >> 8);
            bytes[3] = (byte)(frac & 0x000000ff);

            return bytes;
        }

        /// <summary>
        /// Convert Ibm float(Big-Endian) bytes to ieee double number
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static double Ibm2IeeeD(byte[] bytes)
        {
            //sign
            var sign = (bytes[0] & 128) >> 7;
            // exponent (base is 16)
            var exp = (bytes[0] & 127) - 64;

            // fraction from the last 3 bytes
            var frac = (bytes[1] << 16) + (bytes[2] << 8) + bytes[3];
            // compute mantissa
            var m = frac / Math.Pow(2, 56);
            return (1 - 2 * sign) * Math.Pow(16, exp) * m;
        }

        /// <summary>
        /// Convert a ieee float number to the byte array of a ibm float number
        /// </summary>
        /// <param name="ieeeDb">little endian ieee double value</param>
        /// <returns></returns>
        public static byte[] Ieee2IbmB(double ieeeDb)
        {
            byte[] bytes = BitConverter.GetBytes(ieeeDb);
            Array.Reverse(bytes);
            //for (int i = 0; i < 8; i++)
            //    Console.Write(bytes[i] + "\t");
            //Console.Write("\n");

            //get the sign,exp,frac of the ieee float 
            var sign = (bytes[0] & 128) >> 7;
            var exp = ((bytes[0] & 127) << 4) + ((bytes[1] & 240) >> 4);
            long frac;
            frac = ((long)(bytes[1] & 15) << 48) + ((long)bytes[2] << 40) + ((long)bytes[3] << 32) + ((long)bytes[4] << 24)
                + ((long)bytes[5] << 16) + ((long)bytes[6] << 8) + bytes[7];

            //Console.Write("S:" + sign + ";E:" + exp + ";F:" + frac + "\n");
            //// compute mantissa
            //double m = frac / (Math.Pow(2, 52));
            //double temp= (1 - 2 * sign) * (Math.Pow(2, exp-1023)) *(1+m);
            //Console.WriteLine("ieee:" + temp);

            //frac = (int) (Math.Pow(2, 23) *frac / (Math.Pow(2, 52)));
            //temp = (1 - 2 * sign) * (Math.Pow(2, exp - 1023)) * (1 + frac / Math.Pow(2, 23));
            //Console.WriteLine("ieee1:" + temp);

            exp = exp - 1023 + 1;
            var remainExp = exp & 0x03;
            var exp1 = exp >> 2 + 64;

            frac = (frac & 0x007FFFFF) | 0x00800000;

            if (remainExp != 0)
            {
                exp1 += 1;
                frac = frac >> (4 - remainExp);
            }

            exp1 = exp1 + 64;
            sign = sign << 7;
            byte[] bytes1 = new byte[4];
            bytes1[0] = (byte)(sign + exp1);
            bytes1[1] = (byte)((frac & 0x00ff0000) >> 16);
            bytes1[2] = (byte)((frac & 0x0000ff00) >> 8);
            bytes1[3] = (byte)(frac & 0x000000ff);

            // compute mantissa
            //double m1 = frac / (Math.Pow(2, 24));
            //temp = ((1 - 2 * sign) * (Math.Pow(16, exp1-64)) * m1);
            //Console.WriteLine("ibm:" + temp);

            return bytes1;
        }

        /// <summary>
        /// Convert a ieee float number to a ibm float number
        /// </summary>
        /// <param name="ieee">little endian ieee float</param>
        /// <returns></returns>
        public static float Ieee2IbmF(float ieee)
        {
            byte[] bytes = BitConverter.GetBytes(ieee);

            Array.Reverse(bytes);

            //get the sign,exp,frac of the ieee float 
            var sign = (bytes[0] & 128) >> 7;
            var exp = ((bytes[0] & 127) << 1) + ((bytes[1] & 128) >> 7);
            var frac = ((bytes[1] & 127) << 16) + (bytes[2] << 8) + bytes[3];

            //float temp = SEF2Ieee(sign, exp, frac);
            //Console.WriteLine(temp);

            exp = exp - 127 + 1;
            var remainExp = exp & 0x03;
            var exp1 = exp >> 2 + 64;

            frac = (frac & 0x007FFFFF) | 0x00800000;

            if (remainExp != 0)
            {
                exp1 += 1;
                frac = frac >> (4 - remainExp);
            }
            var mantissa = frac / Math.Pow(16, exp1);

            return (float)(Math.Pow(-1, sign) * Math.Pow(16, exp1) * mantissa);
        }

    }
}
