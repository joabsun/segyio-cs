using SegyIO.Common;
using SegyIO.Model;
using System.Text;

namespace SegyIO.IO
{
    internal class SegyReaderHelper
    {
        /// <summary>
        /// 判断文件的字节序
        /// </summary>
        /// <param name="segyfilename"></param>
        /// <returns></returns>
        internal static ByteOrder GetByteOrder(string segyfilename)
        {
            FileStream fs = new FileStream(segyfilename, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryReader br = new BinaryReader(fs);

            ByteOrder byteOrder;

            br.ReadBytes(3200 + 24);

            var DataFormatCode = br.ReadUInt16();
            if (DataFormatCode >= 1 && DataFormatCode <= 8)
            {
                byteOrder = ByteOrder.LittleEndian;
            }
            else
            {
                byteOrder = ByteOrder.BigEndian;
            }

            br.Close();
            fs.Close();

            return byteOrder;
        }

        /// <summary>
        /// 根据数据格式码得到每个数据的字节数
        /// </summary>
        /// <param name="DataFormatCode"></param>
        /// <returns></returns>
        internal static int GetBytesPerSample(ushort DataFormatCode)
        {
            var bytesPerSample = 4;  //表示每样点字节数,用于计算道数
            switch (DataFormatCode)
            {
                case 1:
                    bytesPerSample = 4;
                    break;
                case 2:
                    bytesPerSample = 4;
                    break;
                case 3:
                    bytesPerSample = 2;
                    break;
                case 4:
                    bytesPerSample = 4;
                    break;
                case 5:
                    bytesPerSample = 4;
                    break;
                case 8:
                    bytesPerSample = 1;
                    break;
            }
            return bytesPerSample;
        }

        /// <summary>
        /// 根据字节序和SEGY数据存储格式,在将字节数据转换为地震数据
        /// </summary>
        /// <param name="trcBytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="isSwap"></param>
        /// <param name="DataFormatCode"></param>
        /// <returns></returns>
        internal static float ToTrcAmp(byte[] trcBytes, int startIndex, bool isSwap, ushort DataFormatCode)
        {
            float temp = 0;
            switch (DataFormatCode)
            {
                case 1:
                    temp = BufferStreamReader.ReadIbmFloat(trcBytes, startIndex);
                    break;
                case 2:
                    temp = BufferStreamReader.ReadInt32(trcBytes, startIndex, isSwap);
                    break;
                case 3:
                    temp = BufferStreamReader.ReadInt16(trcBytes, startIndex, isSwap);
                    break;
                case 4:
                    temp = BufferStreamReader.ReadInt32(trcBytes, startIndex, isSwap);
                    break;
                case 5:
                    temp = BufferStreamReader.ReadFloat(trcBytes, startIndex, isSwap);
                    break;
                case 8:
                    temp = BufferStreamReader.ReadByte(trcBytes, startIndex);
                    break;
            }
            return temp;
        }

        /// <summary>
        /// Read TextHeader
        /// </summary>
        /// <returns></returns>
        internal static string ReadTextHeader(byte[] bytes)
        {
            string EBCDICHeader;
            try
            {
                StringBuilder sb = new StringBuilder();
                Ebcdic2Ascii e2a = new Ebcdic2Ascii();
                for (int i = 0; i < 3200; i++)
                {
                    sb.Append(e2a.ToAscii(bytes[i]));
                }
                EBCDICHeader = sb.ToString();
            }
            catch (Exception e)
            {
                string errMsg = "Can not read the text header from the file!\n Error message is " + e.Message;
                throw new Exception(errMsg);
            }

            return EBCDICHeader;
        }

        /// <summary>
        /// 从字节数组中读取卷头
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="byteOrder"></param>
        /// <returns></returns>
        internal static SegyReelHeader ReadReelHeader(byte[] bytes, ByteOrder byteOrder)
        {
            var isSwap = byteOrder == ByteOrder.BigEndian;
            var index = 0;

            SegyReelHeader reelheader = new SegyReelHeader
            {
                JobID = BufferStreamReader.ReadInt32(bytes, ref index, isSwap),
                LineNumber = BufferStreamReader.ReadInt32(bytes, ref index, isSwap),
                ReelNumber = BufferStreamReader.ReadInt32(bytes, ref index, isSwap),
                TracesPerRecord = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                AuxTracesPerRec = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                SmpInt = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                OrigSampIntervl = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                SmpPerTrc = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                OrigSampsPerTrc = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                DataFormatCode = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                CMPfold = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                TraceSortCode = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                VerticalSumCode = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                SweepStartFreq = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                SweepEndFreq = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                SweepLength = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                SweepTypeCode = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                SweepTraceNum = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                SweepTaperLen = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                SweepTaperEnd = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                TaperType = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                DataTraceCorr = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                BinaryGainRecov = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                AmplitudeRecov = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                Units = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                Polarity = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                VibratoryPol = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                Unassigned = BufferStreamReader.ReadBytes(bytes, ref index, 240),
                RevisionNumber = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                FixLenTraceFlag = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                ExtTextFileHeader = BufferStreamReader.ReadUInt16(bytes, ref index, isSwap),
                Unassigned1 = BufferStreamReader.ReadBytes(bytes, ref index, 94)
            };

            return reelheader;
        }

        /// <summary>
        /// 从道头字节数组中读取道头
        /// </summary>
        /// <param name="trcHdrBytes"></param>
        /// <param name="isSwap"></param>
        /// <returns></returns>
        internal static SegyTraceHeader ReadTraceHeader(byte[] trcHdrBytes, bool isSwap)
        {
            if (trcHdrBytes.Length < 240)
                return null;

            BufferReader bufferReader = new BufferReader(trcHdrBytes, isSwap);

            SegyTraceHeader header = new SegyTraceHeader
            {
                TrcNoInLn = bufferReader.ReadUInt32(),
                TrcNoInTp = bufferReader.ReadUInt32(),
                FFID = bufferReader.ReadUInt32(),
                TrcNoInFld = bufferReader.ReadUInt32(),
                EnergySrcPt = bufferReader.ReadUInt32(),
                CdpNo = bufferReader.ReadUInt32(),
                TrcNoInCmp = bufferReader.ReadUInt32(),
                TrcIdCd = bufferReader.ReadUInt16(),
                NumVertSum = bufferReader.ReadUInt16(),
                NumHorznSum = bufferReader.ReadUInt16(),
                DataUse = bufferReader.ReadUInt16(),
                RecvOff = bufferReader.ReadInt32(),
                RecvElev = bufferReader.ReadInt32(),
                SrcElev = bufferReader.ReadInt32(),
                SrcDepth = bufferReader.ReadInt32(),
                RecDatumElev = bufferReader.ReadInt32(),
                SrcDatumElev = bufferReader.ReadInt32(),
                WaterDepthSrc = bufferReader.ReadInt32(),
                WaterDepthRec = bufferReader.ReadInt32(),

                SrvyScal = bufferReader.ReadInt16(),
                MapScale = bufferReader.ReadInt16(),
                SrcX = bufferReader.ReadInt32(),
                SrcY = bufferReader.ReadInt32(),
                RecvX = bufferReader.ReadInt32(),
                RecvY = bufferReader.ReadInt32(),
                CoordUnit = bufferReader.ReadUInt16(),
                WeatherV = bufferReader.ReadInt16(),
                SubweatherV = bufferReader.ReadInt16(),
                SrcUpholeT = bufferReader.ReadInt16(),
                RecUpholeT = bufferReader.ReadInt16(),
                SrcStaticCor = bufferReader.ReadInt16(),
                RecStaticCor = bufferReader.ReadInt16(),
                TotalStatic = bufferReader.ReadInt16(),
                LagTimeA = bufferReader.ReadInt16(),
                LagTimeB = bufferReader.ReadInt16(),
                DelayRec = bufferReader.ReadInt16(),
                MuteTimeStart = bufferReader.ReadInt16(),
                MuteTimeEnd = bufferReader.ReadInt16(),
                SamplesThisTrace = bufferReader.ReadUInt16(),
                SmplIntvl = bufferReader.ReadUInt16(),
                GainType = bufferReader.ReadUInt16(),
                GainConst = bufferReader.ReadInt16(),
                IniGain = bufferReader.ReadInt16(),
                Correlated = bufferReader.ReadUInt16(),
                SweepStartFreq = bufferReader.ReadInt16(),
                SweepEndFreq = bufferReader.ReadInt16(),
                SweepTime = bufferReader.ReadInt16(),
                SweepType = bufferReader.ReadUInt16(),
                SweepTaperStart = bufferReader.ReadInt16(),
                SweepTaperEnd = bufferReader.ReadInt16(),
                TaperType = bufferReader.ReadInt16(),
                AliasFilter = bufferReader.ReadInt16(),
                AliasSlope = bufferReader.ReadInt16(),
                NotchFilter = bufferReader.ReadInt16(),
                NotchSlope = bufferReader.ReadInt16(),
                LowCut = bufferReader.ReadInt16(),
                HighCut = bufferReader.ReadInt16(),
                LowCutSlope = bufferReader.ReadInt16(),
                HighCutSlope = bufferReader.ReadInt16(),
                Year = bufferReader.ReadUInt16(),
                JulianDay = bufferReader.ReadUInt16(),
                Hour = bufferReader.ReadUInt16(),
                Minute = bufferReader.ReadUInt16(),
                Second = bufferReader.ReadUInt16(),
                TimeBasisCode = bufferReader.ReadUInt16(),
                TraceWeightFactor = bufferReader.ReadInt16(),
                GeophnNoRoll = bufferReader.ReadInt16(),
                GeophnNoFldStart = bufferReader.ReadInt16(),
                GeophnNoFldEnd = bufferReader.ReadInt16(),
                GapSize = bufferReader.ReadInt16(),
                OverTravelCode = bufferReader.ReadUInt16(),

                //新增属性
                CdpX = bufferReader.ReadInt32(),
                CdpY = bufferReader.ReadInt32(),
                Inline = bufferReader.ReadInt32(),
                Crossline = bufferReader.ReadInt32(),
                ShotpointNumber = bufferReader.ReadInt32(),
                ShotpointNumberScale = bufferReader.ReadInt16(),
                TraceValueUnit = bufferReader.ReadInt16(),
                TransductConstant = bufferReader.ReadInt32(),
                TransductConstantExp = bufferReader.ReadInt16(),
                TransductUnit = bufferReader.ReadInt16(),
                DeviceTraceIdentifier = bufferReader.ReadInt16(),
                TimeScalar = bufferReader.ReadInt16(),
                SourceOrientationType = bufferReader.ReadInt16(),
                SourceEnergyDirection1 = bufferReader.ReadInt16(),
                SourceEnergyDirection2 = bufferReader.ReadInt32(),
                SourceMeasurement1 = bufferReader.ReadInt32(),
                SourceMeasurement2 = bufferReader.ReadInt16(),
                SourceMeasurementUnit = bufferReader.ReadInt16(),

                Spare = bufferReader.ReadBytes(8)
            };

            return header;
        }

    }
}
