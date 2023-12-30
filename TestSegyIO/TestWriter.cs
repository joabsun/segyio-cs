using SegyIO.IO;
using SegyIO.Model;

namespace TestSegyIO
{
    internal class TestWriter
    {
        /// <summary>
        /// 写SEGY数据
        /// </summary>
        /// <param name="outfile">输出文件</param>
        /// <param name="txtHdr">文本卷头</param>
        /// <param name="reelHdr">二进制卷头</param>
        /// <param name="trcHdrs">道头数组</param>
        /// <param name="trcData">道数据,形如Trc[smp,trc]</param>
        /// <param name="byteOrder">字节序</param>
        /// <param name="dataFormat">数据格式码</param>
        public static void WriteSegy(string outfile, string txtHdr, SegyReelHeader reelHdr, SegyTraceHeader[] trcHdrs,
            float[,] trcData, ByteOrder byteOrder, ushort dataFormat)
        {
            FileStream fs = new FileStream(outfile, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);

            SegyWriter.WriteTextHeader(bw, txtHdr);
            SegyWriter.WriteReelHeader(bw, reelHdr, byteOrder);

            var trcCount = trcData.GetLength(1);
            var smpCount = trcData.GetLength(0);

            for (int i = 0; i < trcCount; i++)
            {
                SegyWriter.WriteTraceHeader(bw, trcHdrs[i], byteOrder);

                float[] ampls = new float[smpCount];
                for (int j = 0; j < smpCount; j++)
                {
                    ampls[j] = trcData[j, i];
                }
                SegyWriter.WriteTrcData(bw, ampls, byteOrder, dataFormat);
            }

            bw.Close();
        }

        /// <summary>
        /// 根据抽取的道序号列表从源文件抽取道数据存储于新文件
        /// </summary>
        /// <param name="srcSegy">源文件</param>
        /// <param name="outSegy">新文件</param>
        /// <param name="trcNos">要抽取的道序号列表</param>
        /// <param name="minTime">ms</param>
        /// <param name="maxTime">ms </param>
        /// <param name="stepTime">ms</param>
        public static void ExtractSegy(string srcSegy, string outSegy, IList<int> trcNos, float minTime, float maxTime, float stepTime)
        {
            ByteOrder byteOrder = SegyReader.GetByteOrder(srcSegy);

            FileStream fs = new FileStream(srcSegy, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            FileStream fs1 = new FileStream(outSegy, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs1);

            //读卷头
            string EBCDICHeader = SegyReader.ReadTextHeader(br);
            SegyReelHeader reelheader = SegyReader.ReadReelHeader(br, byteOrder);

            var dataFormat = reelheader.DataFormatCode;
            int srcSmpInt = reelheader.SmpInt;
            int srcSmpPerTrc = reelheader.SmpPerTrc;
            var bytesPerSmp = SegyReader.GetBytesPerSample(dataFormat);  //表示每样点字节数,用于计算每道字节数
            var bytesPerTrcData = srcSmpPerTrc * bytesPerSmp;  //每道数据字节数
            var bytesPerTrc = 240 + bytesPerTrcData;  //每道字节数

            var trcs = trcNos.Count;

            //读写道头及数据
            SegyTraceHeader trcHdr1 = SegyReader.ReadTraceHeader(br, byteOrder); //源数据第一道道头
            int srcMinTime = trcHdr1.DelayRec;
            var outSmpPerTrc = (int)((maxTime - minTime) / stepTime) + 1; //输出数据的每道样点数
            var outBytesPerTrc = outSmpPerTrc * bytesPerSmp; //输出数据的每道字节数
            var outMinSmpNo = (int)((minTime - srcMinTime) * 1000 / srcSmpInt);//输出数据的起始样点数
            var outMaxSmpNo = (int)((maxTime - srcMinTime) * 1000 / srcSmpInt);//输出数据的结束样点数
            var skipSmpNo = (int)(stepTime * 1000 / srcSmpInt) - 1;

            //写卷头
            SegyWriter.WriteTextHeader(bw, EBCDICHeader);
            reelheader.SmpInt = (ushort)(stepTime * 1000);
            reelheader.SmpPerTrc = (ushort)outSmpPerTrc;
            SegyWriter.WriteReelHeader(bw, reelheader, byteOrder);
            for (int i = 0; i < trcs; i++)
            {
                long trcNo = trcNos[i];

                var skipBytes = 3600 + bytesPerTrc * trcNo; //读取当前道要跳过的字节数,从文件开始计算
                br.BaseStream.Seek(skipBytes, SeekOrigin.Begin);

                SegyTraceHeader trcHdr = SegyReader.ReadTraceHeader(br, byteOrder);
                trcHdr.DelayRec = (short)minTime;
                trcHdr.SmplIntvl = reelheader.SmpInt;
                trcHdr.SamplesThisTrace = reelheader.SmpPerTrc;

                if (skipSmpNo == 0) //未改变输出的采样间隔
                {
                    var skipBytesInTrc = bytesPerSmp * outMinSmpNo;
                    br.BaseStream.Seek(skipBytesInTrc, SeekOrigin.Current);
                    byte[] trcData = br.ReadBytes(outBytesPerTrc);

                    SegyWriter.WriteTraceHeader(bw, trcHdr, byteOrder);
                    bw.Write(trcData);
                }
                else
                {
                    var skipBytesInTrc = bytesPerSmp * outMinSmpNo;
                    br.BaseStream.Seek(skipBytesInTrc, SeekOrigin.Current);
                    List<byte> trcData = new List<byte>();
                    var inc = skipSmpNo + 1;//输出样点的增量
                    var skipBytes11 = skipSmpNo * bytesPerSmp;//每两个样点间读取时要跳过的字节数
                    for (int j = outMinSmpNo; j <= outMaxSmpNo; j += inc)
                    {
                        byte[] bytesSmp = br.ReadBytes(bytesPerSmp);
                        trcData.AddRange(bytesSmp);
                        br.BaseStream.Seek(skipBytes11, SeekOrigin.Current);
                    }
                    SegyWriter.WriteTraceHeader(bw, trcHdr, byteOrder);
                    bw.Write(trcData.ToArray());
                }
            }

            br.Close();
            bw.Close();
        }

    }
}
