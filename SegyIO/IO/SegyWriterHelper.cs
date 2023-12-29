using SegyIO.Common;
using SegyIO.Model;

namespace SegyIO.IO
{
    internal class SegyWriterHelper
    {
        /// <summary>
        /// Write Text Header
        /// </summary>
        internal static void WriteTextHeader(BinaryWriter bw, string EBCDICHeader)
        {
            Ebcdic2Ascii e2a = new Ebcdic2Ascii();
            byte[] bytes = e2a.ToEbcdic(EBCDICHeader);
            bw.Write(bytes);
        }

        /// <summary>
        /// Write Reel Header
        /// </summary>
        internal static void WriteReelHeader(BinaryWriter bw, SegyReelHeader Reelheader, ByteOrder byteOrder)
        {
            if (byteOrder == ByteOrder.BigEndian)
            {
                bw.Write(ByteSwapper.Swap(Reelheader.JobID));
                bw.Write(ByteSwapper.Swap(Reelheader.LineNumber));
                bw.Write(ByteSwapper.Swap(Reelheader.ReelNumber));
                bw.Write(ByteSwapper.Swap(Reelheader.TracesPerRecord));
                bw.Write(ByteSwapper.Swap(Reelheader.AuxTracesPerRec));
                bw.Write(ByteSwapper.Swap(Reelheader.SmpInt));
                bw.Write(ByteSwapper.Swap(Reelheader.OrigSampIntervl));
                bw.Write(ByteSwapper.Swap(Reelheader.SmpPerTrc));
                bw.Write(ByteSwapper.Swap(Reelheader.OrigSampsPerTrc));
                bw.Write(ByteSwapper.Swap(Reelheader.DataFormatCode));
                bw.Write(ByteSwapper.Swap(Reelheader.CMPfold));
                bw.Write(ByteSwapper.Swap(Reelheader.TraceSortCode));
                bw.Write(ByteSwapper.Swap(Reelheader.VerticalSumCode));
                bw.Write(ByteSwapper.Swap(Reelheader.SweepStartFreq));
                bw.Write(ByteSwapper.Swap(Reelheader.SweepEndFreq));
                bw.Write(ByteSwapper.Swap(Reelheader.SweepLength));
                bw.Write(ByteSwapper.Swap(Reelheader.SweepTypeCode));
                bw.Write(ByteSwapper.Swap(Reelheader.SweepTraceNum));
                bw.Write(ByteSwapper.Swap(Reelheader.SweepTaperLen));
                bw.Write(ByteSwapper.Swap(Reelheader.SweepTaperEnd));
                bw.Write(ByteSwapper.Swap(Reelheader.TaperType));
                bw.Write(ByteSwapper.Swap(Reelheader.DataTraceCorr));
                bw.Write(ByteSwapper.Swap(Reelheader.BinaryGainRecov));
                bw.Write(ByteSwapper.Swap(Reelheader.AmplitudeRecov));
                bw.Write(ByteSwapper.Swap(Reelheader.Units));
                bw.Write(ByteSwapper.Swap(Reelheader.Polarity));
                bw.Write(ByteSwapper.Swap(Reelheader.VibratoryPol));

                bw.Write(Reelheader.Unassigned, 0, 240);
                bw.Write(ByteSwapper.Swap(Reelheader.RevisionNumber));
                bw.Write(ByteSwapper.Swap(Reelheader.FixLenTraceFlag));
                bw.Write(ByteSwapper.Swap(Reelheader.ExtTextFileHeader));
                bw.Write(Reelheader.Unassigned1, 0, 94);
            }
            else
            {
                bw.Write(Reelheader.JobID);
                bw.Write(Reelheader.LineNumber);
                bw.Write(Reelheader.ReelNumber);
                bw.Write(Reelheader.TracesPerRecord);
                bw.Write(Reelheader.AuxTracesPerRec);
                bw.Write(Reelheader.SmpInt);
                bw.Write(Reelheader.OrigSampIntervl);
                bw.Write(Reelheader.SmpPerTrc);
                bw.Write(Reelheader.OrigSampsPerTrc);
                bw.Write(Reelheader.DataFormatCode);
                bw.Write(Reelheader.CMPfold);
                bw.Write(Reelheader.TraceSortCode);
                bw.Write(Reelheader.VerticalSumCode);
                bw.Write(Reelheader.SweepStartFreq);
                bw.Write(Reelheader.SweepEndFreq);
                bw.Write(Reelheader.SweepLength);
                bw.Write(Reelheader.SweepTypeCode);
                bw.Write(Reelheader.SweepTraceNum);
                bw.Write(Reelheader.SweepTaperLen);
                bw.Write(Reelheader.SweepTaperEnd);
                bw.Write(Reelheader.TaperType);
                bw.Write(Reelheader.DataTraceCorr);
                bw.Write(Reelheader.BinaryGainRecov);
                bw.Write(Reelheader.AmplitudeRecov);
                bw.Write(Reelheader.Units);
                bw.Write(Reelheader.Polarity);
                bw.Write(Reelheader.VibratoryPol);

                bw.Write(Reelheader.Unassigned, 0, 240);
                bw.Write(Reelheader.RevisionNumber);
                bw.Write(Reelheader.FixLenTraceFlag);
                bw.Write(Reelheader.ExtTextFileHeader);
                bw.Write(Reelheader.Unassigned1, 0, 94);
            }
        }

        /// <summary>
        /// Write Trace Header
        /// </summary>
        internal static void WriteTraceHeader(BinaryWriter bw, SegyTraceHeader Traceheader, ByteOrder byteOrder)
        {
            if (byteOrder == ByteOrder.BigEndian)
            {
                bw.Write(ByteSwapper.Swap(Traceheader.TrcNoInLn));
                bw.Write(ByteSwapper.Swap(Traceheader.TrcNoInTp));
                bw.Write(ByteSwapper.Swap(Traceheader.FFID));
                bw.Write(ByteSwapper.Swap(Traceheader.TrcNoInFld));
                bw.Write(ByteSwapper.Swap(Traceheader.EnergySrcPt));
                bw.Write(ByteSwapper.Swap(Traceheader.CdpNo));
                bw.Write(ByteSwapper.Swap(Traceheader.TrcNoInCmp));
                bw.Write(ByteSwapper.Swap(Traceheader.TrcIdCd));
                bw.Write(ByteSwapper.Swap(Traceheader.NumVertSum));
                bw.Write(ByteSwapper.Swap(Traceheader.NumHorznSum));
                bw.Write(ByteSwapper.Swap(Traceheader.DataUse));
                bw.Write(ByteSwapper.Swap(Traceheader.RecvOff));
                bw.Write(ByteSwapper.Swap(Traceheader.RecvElev));
                bw.Write(ByteSwapper.Swap(Traceheader.SrcElev));
                bw.Write(ByteSwapper.Swap(Traceheader.SrcDepth));
                bw.Write(ByteSwapper.Swap(Traceheader.RecDatumElev));
                bw.Write(ByteSwapper.Swap(Traceheader.SrcDatumElev));
                bw.Write(ByteSwapper.Swap(Traceheader.WaterDepthSrc));
                bw.Write(ByteSwapper.Swap(Traceheader.WaterDepthRec));
                bw.Write(ByteSwapper.Swap(Traceheader.SrvyScal));
                bw.Write(ByteSwapper.Swap(Traceheader.MapScale));
                bw.Write(ByteSwapper.Swap(Traceheader.SrcX));
                bw.Write(ByteSwapper.Swap(Traceheader.SrcY));
                bw.Write(ByteSwapper.Swap(Traceheader.RecvX));
                bw.Write(ByteSwapper.Swap(Traceheader.RecvY));
                bw.Write(ByteSwapper.Swap(Traceheader.CoordUnit));
                bw.Write(ByteSwapper.Swap(Traceheader.WeatherV));
                bw.Write(ByteSwapper.Swap(Traceheader.SubweatherV));
                bw.Write(ByteSwapper.Swap(Traceheader.SrcUpholeT));
                bw.Write(ByteSwapper.Swap(Traceheader.RecUpholeT));
                bw.Write(ByteSwapper.Swap(Traceheader.SrcStaticCor));
                bw.Write(ByteSwapper.Swap(Traceheader.RecStaticCor));
                bw.Write(ByteSwapper.Swap(Traceheader.TotalStatic));
                bw.Write(ByteSwapper.Swap(Traceheader.LagTimeA));
                bw.Write(ByteSwapper.Swap(Traceheader.LagTimeB));
                bw.Write(ByteSwapper.Swap(Traceheader.DelayRec));
                bw.Write(ByteSwapper.Swap(Traceheader.MuteTimeStart));
                bw.Write(ByteSwapper.Swap(Traceheader.MuteTimeEnd));
                bw.Write(ByteSwapper.Swap(Traceheader.SamplesThisTrace));
                bw.Write(ByteSwapper.Swap(Traceheader.SmplIntvl));
                bw.Write(ByteSwapper.Swap(Traceheader.GainType));
                bw.Write(ByteSwapper.Swap(Traceheader.GainConst));
                bw.Write(ByteSwapper.Swap(Traceheader.IniGain));
                bw.Write(ByteSwapper.Swap(Traceheader.Correlated));
                bw.Write(ByteSwapper.Swap(Traceheader.SweepStartFreq));
                bw.Write(ByteSwapper.Swap(Traceheader.SweepEndFreq));
                bw.Write(ByteSwapper.Swap(Traceheader.SweepTime));
                bw.Write(ByteSwapper.Swap(Traceheader.SweepType));
                bw.Write(ByteSwapper.Swap(Traceheader.SweepTaperStart));
                bw.Write(ByteSwapper.Swap(Traceheader.SweepTaperEnd));
                bw.Write(ByteSwapper.Swap(Traceheader.TaperType));
                bw.Write(ByteSwapper.Swap(Traceheader.AliasFilter));
                bw.Write(ByteSwapper.Swap(Traceheader.AliasSlope));
                bw.Write(ByteSwapper.Swap(Traceheader.NotchFilter));
                bw.Write(ByteSwapper.Swap(Traceheader.NotchSlope));
                bw.Write(ByteSwapper.Swap(Traceheader.LowCut));
                bw.Write(ByteSwapper.Swap(Traceheader.HighCut));
                bw.Write(ByteSwapper.Swap(Traceheader.LowCutSlope));
                bw.Write(ByteSwapper.Swap(Traceheader.HighCutSlope));
                bw.Write(ByteSwapper.Swap(Traceheader.Year));
                bw.Write(ByteSwapper.Swap(Traceheader.JulianDay));
                bw.Write(ByteSwapper.Swap(Traceheader.Hour));
                bw.Write(ByteSwapper.Swap(Traceheader.Minute));
                bw.Write(ByteSwapper.Swap(Traceheader.Second));
                bw.Write(ByteSwapper.Swap(Traceheader.TimeBasisCode));
                bw.Write(ByteSwapper.Swap(Traceheader.TraceWeightFactor));
                bw.Write(ByteSwapper.Swap(Traceheader.GeophnNoRoll));
                bw.Write(ByteSwapper.Swap(Traceheader.GeophnNoFldStart));
                bw.Write(ByteSwapper.Swap(Traceheader.GeophnNoFldEnd));
                bw.Write(ByteSwapper.Swap(Traceheader.GapSize));
                bw.Write(ByteSwapper.Swap(Traceheader.OverTravelCode));

                //新增属性
                bw.Write(ByteSwapper.Swap(Traceheader.CdpX));
                bw.Write(ByteSwapper.Swap(Traceheader.CdpY));
                bw.Write(ByteSwapper.Swap(Traceheader.Inline));
                bw.Write(ByteSwapper.Swap(Traceheader.Crossline));
                bw.Write(ByteSwapper.Swap(Traceheader.ShotpointNumber));
                bw.Write(ByteSwapper.Swap(Traceheader.ShotpointNumberScale));
                bw.Write(ByteSwapper.Swap(Traceheader.TraceValueUnit));
                bw.Write(ByteSwapper.Swap(Traceheader.TransductConstant));
                bw.Write(ByteSwapper.Swap(Traceheader.TransductConstantExp));
                bw.Write(ByteSwapper.Swap(Traceheader.TransductUnit));
                bw.Write(ByteSwapper.Swap(Traceheader.DeviceTraceIdentifier));
                bw.Write(ByteSwapper.Swap(Traceheader.TimeScalar));
                bw.Write(ByteSwapper.Swap(Traceheader.SourceOrientationType));
                bw.Write(ByteSwapper.Swap(Traceheader.SourceEnergyDirection1));
                bw.Write(ByteSwapper.Swap(Traceheader.SourceEnergyDirection2));
                bw.Write(ByteSwapper.Swap(Traceheader.SourceMeasurement1));
                bw.Write(ByteSwapper.Swap(Traceheader.SourceMeasurement2));
                bw.Write(ByteSwapper.Swap(Traceheader.SourceMeasurementUnit));

                bw.Write(Traceheader.Spare, 0, 8);
            }
            else
            {
                bw.Write(Traceheader.TrcNoInLn);
                bw.Write(Traceheader.TrcNoInTp);
                bw.Write(Traceheader.FFID);
                bw.Write(Traceheader.TrcNoInFld);
                bw.Write(Traceheader.EnergySrcPt);
                bw.Write(Traceheader.CdpNo);
                bw.Write(Traceheader.TrcNoInCmp);
                bw.Write(Traceheader.TrcIdCd);
                bw.Write(Traceheader.NumVertSum);
                bw.Write(Traceheader.NumHorznSum);
                bw.Write(Traceheader.DataUse);
                bw.Write(Traceheader.RecvOff);
                bw.Write(Traceheader.RecvElev);
                bw.Write(Traceheader.SrcElev);
                bw.Write(Traceheader.SrcDepth);
                bw.Write(Traceheader.RecDatumElev);
                bw.Write(Traceheader.SrcDatumElev);
                bw.Write(Traceheader.WaterDepthSrc);
                bw.Write(Traceheader.WaterDepthRec);
                bw.Write(Traceheader.SrvyScal);
                bw.Write(Traceheader.MapScale);
                bw.Write(Traceheader.SrcX);
                bw.Write(Traceheader.SrcY);
                bw.Write(Traceheader.RecvX);
                bw.Write(Traceheader.RecvY);
                bw.Write(Traceheader.CoordUnit);
                bw.Write(Traceheader.WeatherV);
                bw.Write(Traceheader.SubweatherV);
                bw.Write(Traceheader.SrcUpholeT);
                bw.Write(Traceheader.RecUpholeT);
                bw.Write(Traceheader.SrcStaticCor);
                bw.Write(Traceheader.RecStaticCor);
                bw.Write(Traceheader.TotalStatic);
                bw.Write(Traceheader.LagTimeA);
                bw.Write(Traceheader.LagTimeB);
                bw.Write(Traceheader.DelayRec);
                bw.Write(Traceheader.MuteTimeStart);
                bw.Write(Traceheader.MuteTimeEnd);
                bw.Write(Traceheader.SamplesThisTrace);
                bw.Write(Traceheader.SmplIntvl);
                bw.Write(Traceheader.GainType);
                bw.Write(Traceheader.GainConst);
                bw.Write(Traceheader.IniGain);
                bw.Write(Traceheader.Correlated);
                bw.Write(Traceheader.SweepStartFreq);
                bw.Write(Traceheader.SweepEndFreq);
                bw.Write(Traceheader.SweepTime);
                bw.Write(Traceheader.SweepType);
                bw.Write(Traceheader.SweepTaperStart);
                bw.Write(Traceheader.SweepTaperEnd);
                bw.Write(Traceheader.TaperType);
                bw.Write(Traceheader.AliasFilter);
                bw.Write(Traceheader.AliasSlope);
                bw.Write(Traceheader.NotchFilter);
                bw.Write(Traceheader.NotchSlope);
                bw.Write(Traceheader.LowCut);
                bw.Write(Traceheader.HighCut);
                bw.Write(Traceheader.LowCutSlope);
                bw.Write(Traceheader.HighCutSlope);
                bw.Write(Traceheader.Year);
                bw.Write(Traceheader.JulianDay);
                bw.Write(Traceheader.Hour);
                bw.Write(Traceheader.Minute);
                bw.Write(Traceheader.Second);
                bw.Write(Traceheader.TimeBasisCode);
                bw.Write(Traceheader.TraceWeightFactor);
                bw.Write(Traceheader.GeophnNoRoll);
                bw.Write(Traceheader.GeophnNoFldStart);
                bw.Write(Traceheader.GeophnNoFldEnd);
                bw.Write(Traceheader.GapSize);
                bw.Write(Traceheader.OverTravelCode);

                //新增属性
                bw.Write(Traceheader.CdpX);
                bw.Write(Traceheader.CdpY);
                bw.Write(Traceheader.Inline);
                bw.Write(Traceheader.Crossline);
                bw.Write(Traceheader.ShotpointNumber);
                bw.Write(Traceheader.ShotpointNumberScale);
                bw.Write(Traceheader.TraceValueUnit);
                bw.Write(Traceheader.TransductConstant);
                bw.Write(Traceheader.TransductConstantExp);
                bw.Write(Traceheader.TransductUnit);
                bw.Write(Traceheader.DeviceTraceIdentifier);
                bw.Write(Traceheader.TimeScalar);
                bw.Write(Traceheader.SourceOrientationType);
                bw.Write(Traceheader.SourceEnergyDirection1);
                bw.Write(Traceheader.SourceEnergyDirection2);
                bw.Write(Traceheader.SourceMeasurement1);
                bw.Write(Traceheader.SourceMeasurement2);
                bw.Write(Traceheader.SourceMeasurementUnit);

                bw.Write(Traceheader.Spare, 0, 8);
            }
        }

        /// <summary>
        /// 转换振幅数据为字节数组
        /// </summary>
        internal static byte[] TrcAmp2Bytes(float amp, ByteOrder byteOrder, ushort DataFormatCode)
        {
            byte[] bytes = null;
            if (byteOrder == ByteOrder.BigEndian)
            {
                switch (DataFormatCode)
                {
                    case 1:
                        bytes = FloatConverter.Ieee2IbmB(amp);
                        break;
                    case 2:
                        bytes = BitConverter.GetBytes(ByteSwapper.Swap((int)amp));
                        break;
                    case 3:
                        bytes = BitConverter.GetBytes(ByteSwapper.Swap((short)amp));
                        break;
                    case 5:
                        bytes = BitConverter.GetBytes(ByteSwapper.Swap(amp));
                        break;
                    case 8:
                        bytes = BitConverter.GetBytes(ByteSwapper.Swap((byte)amp));
                        break;
                }
            }
            else
            {
                switch (DataFormatCode)
                {
                    case 1:
                        bytes = BitConverter.GetBytes(amp);
                        break;
                    case 2:
                        bytes = BitConverter.GetBytes((int)amp);
                        break;
                    case 3:
                        bytes = BitConverter.GetBytes((short)amp);
                        break;
                    case 5:
                        bytes = BitConverter.GetBytes(amp);
                        break;
                    case 8:
                        bytes = BitConverter.GetBytes((byte)amp);
                        break;
                }
            }
            return bytes;
        }

        /// <summary>
        /// 写单个振幅数据
        /// </summary>
        internal static void WriteTrcAmp(BinaryWriter bw, float amp, ByteOrder byteOrder, ushort DataFormatCode)
        {
            if (byteOrder == ByteOrder.BigEndian)
            {
                switch (DataFormatCode)
                {
                    case 1:
                        bw.Write(FloatConverter.Ieee2IbmB(amp));
                        break;
                    case 2:
                        bw.Write(ByteSwapper.Swap((int)amp));
                        break;
                    case 3:
                        bw.Write(ByteSwapper.Swap((short)amp));
                        break;
                    case 4:
                        bw.Write(ByteSwapper.Swap((int)amp));
                        break;
                    case 5:
                        bw.Write(ByteSwapper.Swap(amp));
                        break;
                    case 8:
                        bw.Write(ByteSwapper.Swap((byte)amp));
                        break;
                }
            }
            else
            {
                switch (DataFormatCode)
                {
                    case 1:
                        bw.Write(FloatConverter.Ieee2IbmB(amp));
                        break;
                    case 2:
                        bw.Write((int)amp);
                        break;
                    case 3:
                        bw.Write((short)amp);
                        break;
                    case 4:
                        bw.Write((int)amp);
                        break;
                    case 5:
                        bw.Write(amp);
                        break;
                    case 8:
                        bw.Write((byte)amp);
                        break;
                }
            }
        }
    }
}
