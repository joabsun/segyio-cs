using SegyIO.Model;

namespace SegyIO.IO
{
    /// <summary>
    /// 封装SEGY道头数据的读取
    /// </summary>
    public class TraceHeaderReader
    {
        /// <summary>
        /// 读取segy文件中的道头信息数组
        /// </summary>
        /// <param name="segyfilename"></param>
        /// <returns></returns>
        public static SegyTraceHeader[] ReadSegyTraceHeader(string segyfilename)
        {
            ByteOrder byteOrder = SegyReader.GetByteOrder(segyfilename);

            FileStream fs = new FileStream(segyfilename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            //路过文本卷头3200字节
            br.BaseStream.Seek(3200, SeekOrigin.Current);
            SegyReelHeader Reelheader = SegyReader.ReadReelHeader(br, byteOrder);

            var DataFormatCode = Reelheader.DataFormatCode;
            int samplesPerTrace = Reelheader.SmpPerTrc;
            var bytesPerSample = SegyReaderHelper.GetBytesPerSample(DataFormatCode);  //表示每样点字节数,用于计算道数
            var bytesPerTrcData = bytesPerSample * samplesPerTrace;
            var traceNumber = (int)((br.BaseStream.Length - 3600) / (240 + bytesPerTrcData));
            SegyTraceHeader[] traceHeaders = new SegyTraceHeader[traceNumber];

            List<byte[]> trcHdrBytes = new List<byte[]>(traceNumber);
            for (long i = 0; i < traceNumber; i++)
            {
                var seek = 3600 + i * (240 + bytesPerTrcData);
                br.BaseStream.Seek(seek, SeekOrigin.Begin);
                byte[] bytes = br.ReadBytes(240);
                trcHdrBytes.Add(bytes);
            }

            br.Close();

            //并行
            Parallel.For(0, traceNumber, i =>
            {
                traceHeaders[i] = SegyReader.ReadTraceHeader(trcHdrBytes[i], byteOrder);
            });

            return traceHeaders;
        }

        /// <summary>
        /// 读取segy文件中的第一个道头
        /// </summary>
        /// <param name="segyfilename"></param>
        /// <returns></returns>
        public static SegyTraceHeader GetFirstSegyTraceHeader(string segyfilename)
        {
            ByteOrder byteOrder = SegyReader.GetByteOrder(segyfilename);

            FileStream fs = new FileStream(segyfilename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            //路过卷头3600字节
            br.BaseStream.Seek(3600, SeekOrigin.Current);

            byte[] bytes = br.ReadBytes(240);
            SegyTraceHeader traceHeader = SegyReader.ReadTraceHeader(bytes, byteOrder);

            br.Close();

            return traceHeader;
        }

        /// <summary>
        /// 读取segy文件中的指定前trcCount道的道头信息数组
        /// </summary>
        /// <param name="segyfilename"></param>
        /// <param name="trcCount"> </param>
        /// <returns></returns>
        public static SegyTraceHeader[] ReadSegyTraceHeader(string segyfilename, int trcCount)
        {
            ByteOrder byteOrder = SegyReader.GetByteOrder(segyfilename);

            FileStream fs = new FileStream(segyfilename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            //路过文本卷头3200字节
            br.BaseStream.Seek(3200, SeekOrigin.Current);
            SegyReelHeader Reelheader = SegyReader.ReadReelHeader(br, byteOrder);

            var DataFormatCode = Reelheader.DataFormatCode;
            int samplesPerTrace = Reelheader.SmpPerTrc;
            var bytesPerSample = SegyReaderHelper.GetBytesPerSample(DataFormatCode);  //表示每样点字节数,用于计算道数
            var traceNumber = (int)((br.BaseStream.Length - 3600) / (240 + samplesPerTrace * bytesPerSample));

            if (trcCount > traceNumber)
                trcCount = traceNumber;
            SegyTraceHeader[] traceHeaders = new SegyTraceHeader[trcCount];

            //读道头,跳过道数据
            for (int i = 0; i < trcCount; i++)
            {
                byte[] bytes = br.ReadBytes(240);
                traceHeaders[i] = SegyReader.ReadTraceHeader(bytes, byteOrder);

                br.BaseStream.Seek(bytesPerSample * samplesPerTrace, SeekOrigin.Current);
            }

            br.Close();

            return traceHeaders;
        }


        /// <summary>
        /// 根据变量名及值从道头列表中查询满足条件的道序号
        /// </summary>
        /// <param name="trcHdrs"></param>
        /// <param name="varName"></param>
        /// <param name="varValue"></param>
        /// <returns></returns>
        public static List<int> ExtractTrcNo(SegyTraceHeader[] trcHdrs, string varName, int varValue)
        {
            List<int> listTrcNos = new List<int>();
            for (int i = 0; i < trcHdrs.Length; i++)
            {
                SegyTraceHeader trcHdr = trcHdrs[i];
                Type trType = trcHdr.GetType();//获得道头类的Type
                object temp1 = trType.GetProperty(varName).GetValue(trcHdr, null); //得到varName变量的值
                var temp = Convert.ToInt32(temp1);
                if (varValue == temp)
                    listTrcNos.Add(i);
            }
            return listTrcNos;
        }

        /// <summary>
        /// 根据变量名从道头列表中查询变量的非重复值
        /// </summary>
        /// <param name="trcHdrs"></param>
        /// <param name="varName">变量名</param>
        /// <param name="isDistinct">是否剔除重复值</param>
        /// <returns></returns>
        public static List<int> ExtractValuesFromTrcHdr(SegyTraceHeader[] trcHdrs, string varName, bool isDistinct = false)
        {
            List<int> listValues = new List<int>();
            for (int i = 0; i < trcHdrs.Length; i++)
            {
                SegyTraceHeader trcHdr = trcHdrs[i];
                Type trType = trcHdr.GetType();//获得道头类的Type
                object temp1 = trType.GetProperty(varName).GetValue(trcHdr, null); //得到varName变量的值
                var temp = Convert.ToInt32(temp1);
                listValues.Add(temp);
            }

            if (!isDistinct)
                return listValues;

            IEnumerable<int> distinctValues = from item in listValues select item;
            distinctValues = distinctValues.Distinct();

            return distinctValues.ToList();
        }

    }
}
