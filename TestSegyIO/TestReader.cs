using System.Diagnostics;
using SegyIO.IO;
using SegyIO.Model;

namespace TestSegyIO
{
    internal class TestReader
    {
        /// <summary>
        /// Read some information in the SEGY file, including: number of traces, number of sampling points per trace,
        /// sampling rate, starting time
        /// </summary>
        public static void ReadSegyFileInfo()
        {
            string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName);
            string segyFilename = Path.Combine(folder, "test1.segy");

            ByteOrder byteOrder = SegyReader.GetByteOrder(segyFilename);

            FileStream fs = new FileStream(segyFilename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            SegyReader.ReadTextHeader(br); //读文本头
            SegyReelHeader reelHeader = SegyReader.ReadReelHeader(br, byteOrder);

            var DataFormatCode = reelHeader.DataFormatCode;
            int samplesPerTrace = reelHeader.SmpPerTrc;
            int sampleInt = reelHeader.SmpInt;
            var bytesPerSample = SegyReader.GetBytesPerSample(DataFormatCode);
            var traceNumber = (int)((br.BaseStream.Length - 3600) / (240 + samplesPerTrace * bytesPerSample));
            SegyTraceHeader Traceheader = SegyReader.ReadTraceHeader(br, byteOrder);
            int delayTime = Traceheader.DelayRec;

            br.Close();

            string s = "Trace number: " + traceNumber + Environment.NewLine;
            s += "Sample number per trace: " + samplesPerTrace + Environment.NewLine;
            s += "Sample rate(us): " + sampleInt + Environment.NewLine;
            s += "Delay time: " + delayTime;

            Console.WriteLine(s);
        }

        /// <summary>
        /// 读取segy文件中的文本卷头
        /// </summary>
        /// <param name="segyFile"></param>
        public static string ReadTextHeader(string segyFile)
        {
            FileStream fs = new FileStream(segyFile, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            //读文本卷头3200字节
            string EBCDICHeader = SegyReader.ReadTextHeader(br);

            br.Close();

            return EBCDICHeader;
        }

        /// <summary>
        /// 读取segy文件中的二进制卷头信息
        /// </summary>
        /// <param name="segyfile"></param>
        /// <returns>二进制卷头</returns>
        public static SegyReelHeader ReadSegyReelHeader(string segyfile)
        {
            ByteOrder byteOrder = SegyReader.GetByteOrder(segyfile);

            FileStream fs = new FileStream(segyfile, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            //跳过文本卷头3200字节
            br.BaseStream.Seek(3200, SeekOrigin.Begin);
            SegyReelHeader Reelheader = SegyReader.ReadReelHeader(br, byteOrder);

            br.Close();

            return Reelheader;
        }

        /// <summary>
        /// 读取道数据, 大文件不建议这种方式读取，可能导致内存溢出。2G内问题不大
        /// </summary>
        /// <param name="segyfile">源文件</param>
        /// <param name="textHdr">返回的文本卷头</param>
        /// <param name="reelHdr">返回的卷头</param>
        /// <param name="trcHdrs">返回的道头数组</param>
        /// <returns>道数据,形如Trc[smp,trc]</returns>
        public static float[,] ReadSegy(string segyfile, out string textHdr, out SegyReelHeader reelHdr, out SegyTraceHeader[] trcHdrs)
        {
            ByteOrder byteOrder = SegyReader.GetByteOrder(segyfile);

            FileStream fs = new FileStream(segyfile, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            textHdr = SegyReader.ReadTextHeader(br);
            reelHdr = SegyReader.ReadReelHeader(br, byteOrder);

            var dataFormat = reelHdr.DataFormatCode;
            int smpPerTrc = reelHdr.SmpPerTrc;
            var bytesPerSmp = SegyReader.GetBytesPerSample(dataFormat);  //表示每样点字节数,用于计算每道字节数
            var bytesPerTrc = 240 + smpPerTrc * bytesPerSmp;  //每道字节数

            var trcs = (int)((br.BaseStream.Length - 3600) / bytesPerTrc);

            trcHdrs = new SegyTraceHeader[trcs];
            float[,] trcData = new float[smpPerTrc, trcs];
            for (int i = 0; i < trcs; i++)
            {
                long skip = 3600 + bytesPerTrc * i; //跳过的字节数
                br.BaseStream.Seek(skip, SeekOrigin.Begin);

                trcHdrs[i] = SegyReader.ReadTraceHeader(br, byteOrder);
                float[] trc = SegyReader.ReadTrcData(br, smpPerTrc, bytesPerSmp, byteOrder, dataFormat);
                for (int j = 0; j < smpPerTrc; j++)
                {
                    trcData[j, i] = trc[j];
                }
            }

            br.Close();
            return trcData;
        }

        /// <summary>
        /// 读取无卷头的SEGY道数据,形如Trc[smp,trc]
        /// </summary>
        /// <param name="segyfile">源文件</param>
        /// <param name="trcHdrs">返回的道头数组</param>
        /// <param name="byteOrder"></param>
        /// <param name="dataFormat"></param>
        /// <returns>道数据,形如Trc[smp,trc]</returns>
        public static float[,] ReadSegyNoReel(string segyfile, ByteOrder byteOrder, ushort dataFormat, out SegyTraceHeader[] trcHdrs)
        {
            FileStream fs = new FileStream(segyfile, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            SegyTraceHeader trcHdr = SegyReader.ReadTraceHeader(br, byteOrder);

            int smpPerTrc = trcHdr.SamplesThisTrace;
            var bytesPerSmp = SegyReader.GetBytesPerSample(dataFormat);  //表示每样点字节数,用于计算每道字节数
            var bytesPerTrc = 240 + smpPerTrc * bytesPerSmp;  //每道字节数

            var trcs = (int)(br.BaseStream.Length / bytesPerTrc);

            trcHdrs = new SegyTraceHeader[trcs];
            float[,] trcData = new float[smpPerTrc, trcs];
            br.BaseStream.Seek(0, SeekOrigin.Begin); //重置读取位置为开始
            for (int i = 0; i < trcs; i++)
            {
                trcHdrs[i] = SegyReader.ReadTraceHeader(br, byteOrder);
                float[] trc = SegyReader.ReadTrcData(br, smpPerTrc, bytesPerSmp, byteOrder, dataFormat);
                for (int j = 0; j < smpPerTrc; j++)
                {
                    trcData[j, i] = trc[j];
                }
            }

            br.Close();
            return trcData;
        }

        /// <summary>
        /// 读取SEGY单道数据
        /// </summary>
        /// <param name="segyfile">源文件</param>
        /// <param name="trcNo">道序号</param>
        /// <returns>道数据</returns>
        public static float[] ReadATrcData(string segyfile, int trcNo)
        {
            ByteOrder byteOrder = SegyReader.GetByteOrder(segyfile);

            FileStream fs = new FileStream(segyfile, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            SegyReader.ReadTextHeader(br);
            SegyReelHeader reelHdr = SegyReader.ReadReelHeader(br, byteOrder);

            var dataFormat = reelHdr.DataFormatCode;
            int smpPerTrc = reelHdr.SmpPerTrc;
            var bytesPerSmp = SegyReader.GetBytesPerSample(dataFormat); //表示每样点字节数,用于计算每道字节数
            var bytesPerTrc = 240 + smpPerTrc * bytesPerSmp; //每道字节数

            var skipBytes = 3600 + bytesPerTrc * trcNo; //读取当前道要跳过的字节数,从文件开始计算
            br.BaseStream.Seek(skipBytes, SeekOrigin.Begin);

            float[] trc = SegyReader.ReadTrcData(br, smpPerTrc, bytesPerSmp, byteOrder, dataFormat);

            br.Close();
            return trc;
        }

    }
}
