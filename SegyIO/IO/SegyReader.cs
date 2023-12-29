using SegyIO.Model;

namespace SegyIO.IO
{
    /// <summary>
    /// 对标准SEGY文件的读取
    /// </summary>
    public class SegyReader
    {
        #region 判断文件的字节序, 根据数据格式码得到每个数据的字节数, 获取SEGY的总道数

        /// <summary>
        /// 判断文件的字节序
        /// </summary>
        /// <param name="segyfile"></param>
        /// <returns></returns>
        public static ByteOrder GetByteOrder(string segyfile)
        {
            return SegyReaderHelper.GetByteOrder(segyfile);
        }

        /// <summary>
        /// 根据数据格式码得到每个数据的字节数
        /// </summary>
        /// <param name="dataFormatCode"></param>
        /// <returns></returns>
        public static int GetBytesPerSample(ushort dataFormatCode)
        {
            return SegyReaderHelper.GetBytesPerSample(dataFormatCode);
        }

        /// <summary>
        /// 获取SEGY的总道数
        /// </summary>
        /// <param name="file">SEGY file</param>
        /// <returns></returns>
        public static int GetTotalTraces(string file)
        {
            SegyReelHeader reel = ReadReelHeader(file);
            int smpPerTrc = reel.SmpPerTrc;
            var bytesPerSample = SegyReaderHelper.GetBytesPerSample(reel.DataFormatCode);
            var bytesPerTrc = 240 + bytesPerSample * smpPerTrc;
            FileStream fs = new FileStream(file, FileMode.Open);
            var totalTrc = (int)((fs.Length - 3600) / bytesPerTrc);
            fs.Close();
            return totalTrc;
        }

        #endregion


        #region Read: Text Header, Reel Header, Trace Header

        /// <summary>
        /// Read TextHeader
        /// </summary>
        /// <returns></returns>
        public static string ReadTextHeader(BinaryReader br)
        {
            byte[] bytes = br.ReadBytes(3200);
            return SegyReaderHelper.ReadTextHeader(bytes);
        }

        /// <summary>
        /// Read ReelHeader
        /// </summary>
        /// <returns></returns>
        public static SegyReelHeader ReadReelHeader(BinaryReader br, ByteOrder byteOrder)
        {
            byte[] bytes = br.ReadBytes(400);

            return SegyReaderHelper.ReadReelHeader(bytes, byteOrder);
        }

        /// <summary>
        /// Read ReelHeader from a segy file
        /// </summary>
        public static SegyReelHeader ReadReelHeader(string segyfile)
        {
            ByteOrder byteOrder = GetByteOrder(segyfile);

            FileStream fs = new FileStream(segyfile, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            br.BaseStream.Seek(3200, SeekOrigin.Begin); 
            SegyReelHeader Reelheader = ReadReelHeader(br, byteOrder);
            br.Close();

            return Reelheader;
        }

        /// <summary>
        /// Read TraceHeader
        /// </summary>
        /// <returns></returns>
        public static SegyTraceHeader ReadTraceHeader(BinaryReader br, ByteOrder byteOrder)
        {
            var isSwap = byteOrder == ByteOrder.BigEndian;
            byte[] trcHdrBytes = br.ReadBytes(240);
            return SegyReaderHelper.ReadTraceHeader(trcHdrBytes, isSwap);
        }

        /// <summary>
        /// Read TraceHeader from a buffer
        /// </summary>
        /// <param name="trcHdrBytes"></param>
        /// <param name="byteOrder"></param>
        /// <returns></returns>
        public static SegyTraceHeader ReadTraceHeader(byte[] trcHdrBytes, ByteOrder byteOrder)
        {
            var isSwap = byteOrder == ByteOrder.BigEndian;
            return SegyReaderHelper.ReadTraceHeader(trcHdrBytes, isSwap);
        }


        #endregion


        #region Read: TraceData


        /// <summary>
        /// 从BinaryReader的当前位置开始"连续"读取一道数据
        /// </summary>
        /// <param name="br">BinaryReader</param>
        /// <param name="smpPerTrc"></param>
        /// <param name="bytesPerSmp"></param>
        /// <param name="byteOrder"></param>
        /// <param name="dataFormat"></param>
        /// <returns></returns>
        public static float[] ReadTrcData(BinaryReader br, int smpPerTrc, int bytesPerSmp, ByteOrder byteOrder, ushort dataFormat)
        {
            var isSwap = byteOrder == ByteOrder.BigEndian;
            byte[] trcBytes = br.ReadBytes(smpPerTrc * bytesPerSmp);
            float[] trcData = new float[smpPerTrc];
            for (int i = 0; i < smpPerTrc; i++)
            {
                trcData[i] = SegyReaderHelper.ToTrcAmp(trcBytes, i * bytesPerSmp, isSwap, dataFormat);
            }
            return trcData;
        }

        /// <summary>
        /// 从道字节数组中并行读单道数据
        /// </summary>
        /// <param name="trcBytes">道字节数组</param>
        /// <param name="trcData">输出的道数据</param>
        /// <param name="smpPerTrc">输出的道样点数</param>
        /// <param name="bytesPerSmp"></param>
        /// <param name="isSwap"></param>
        /// <param name="dataFormat"></param>
        public static void ReadTrcData(byte[] trcBytes, float[] trcData, int smpPerTrc, int bytesPerSmp, bool isSwap, ushort dataFormat)
        {
            Parallel.For(0, smpPerTrc, i =>
            {
                trcData[i] = SegyReaderHelper.ToTrcAmp(trcBytes, i * bytesPerSmp, isSwap, dataFormat);
            });
        }

        /// <summary>
        /// 从byte[]"连续"读取一道数据
        /// </summary>
        /// <param name="trcBytes">BinaryReader</param>
        /// <param name="smpPerTrc"></param>
        /// <param name="bytesPerSmp"></param>
        /// <param name="byteOrder"></param>
        /// <param name="dataFormat"></param>
        /// <returns></returns>
        public static float[] ReadTrcData(byte[] trcBytes, int smpPerTrc, int bytesPerSmp, ByteOrder byteOrder, ushort dataFormat)
        {
            var isSwap = byteOrder == ByteOrder.BigEndian;
            float[] trcData = new float[smpPerTrc];
            for (int i = 0; i < smpPerTrc; i++)
            {
                trcData[i] = SegyReaderHelper.ToTrcAmp(trcBytes, i * bytesPerSmp, isSwap, dataFormat);
            }
            return trcData;
        }

        /// <summary>
        /// 从BinaryReader的指定位置开始"连续"读取一道数据
        /// </summary>
        /// <param name="br">BinaryReader</param>
        /// <param name="skipBytes"></param>
        /// <param name="seekOrigin"></param>
        /// <param name="smpPerTrc"></param>
        /// <param name="bytesPerSmp"></param>
        /// <param name="byteOrder"></param>
        /// <param name="dataFormat"></param>
        /// <returns></returns>
        public static float[] ReadTrcData(BinaryReader br, long skipBytes, SeekOrigin seekOrigin, int smpPerTrc, int bytesPerSmp, ByteOrder byteOrder, ushort dataFormat)
        {
            var isSwap = byteOrder == ByteOrder.BigEndian;
            br.BaseStream.Seek(skipBytes, seekOrigin);
            byte[] trcBytes = br.ReadBytes(smpPerTrc * bytesPerSmp);
            float[] trcData = new float[smpPerTrc];
            for (int i = 0; i < smpPerTrc; i++)
            {
                trcData[i] = SegyReaderHelper.ToTrcAmp(trcBytes, i * bytesPerSmp, isSwap, dataFormat);
            }
            return trcData;
        }

        #endregion 

    }
}

