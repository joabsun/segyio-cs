using SegyIO.Model;

namespace SegyIO.IO
{
    /// <summary>
    /// Segy Writer
    /// </summary>
    public class SegyWriter
    {
        #region Write Text Header, Reel Header, Trace Header

        /// <summary>
        /// Write Text Header
        /// </summary>
        public static void WriteTextHeader(BinaryWriter bw, string EBCDICHeader)
        {
            if (string.IsNullOrEmpty(EBCDICHeader))
                EBCDICHeader = "  ".PadRight(3200);
            if (EBCDICHeader.Length > 3200)
                EBCDICHeader = EBCDICHeader.Substring(0, 3200);
            else if (EBCDICHeader.Length < 3200)
                EBCDICHeader = EBCDICHeader.PadRight(3200);
            SegyWriterHelper.WriteTextHeader(bw, EBCDICHeader);
        }

        /// <summary>
        /// Write ReelHeader
        /// </summary>
        public static void WriteReelHeader(BinaryWriter bw, SegyReelHeader reelHeader, ByteOrder byteOrder)
        {
            SegyWriterHelper.WriteReelHeader(bw, reelHeader, byteOrder);
        }

        /// <summary>
        /// Write TraceHeader
        /// </summary>
        public static void WriteTraceHeader(BinaryWriter bw, SegyTraceHeader Traceheader, ByteOrder byteOrder)
        {
            SegyWriterHelper.WriteTraceHeader(bw, Traceheader, byteOrder);
        }

        #endregion

        #region Write Trace Data

        /// <summary>
        /// Write Trace Data
        /// </summary>
        public static void WriteTrcData(BinaryWriter bw, float[] amps, ByteOrder byteOrder, ushort DataFormatCode)
        {
            bw.Write(TrcData2Bytes(amps, byteOrder, DataFormatCode));
        }

        /// <summary>
        /// Convert trace data to bytes
        /// </summary>
        public static byte[] TrcData2Bytes(float[] amps, ByteOrder byteOrder, ushort DataFormatCode)
        {
            List<byte> buffer = new List<byte>();
            foreach (float amp in amps)
            {
                buffer.AddRange(SegyWriterHelper.TrcAmp2Bytes(amp, byteOrder, DataFormatCode));
            }
            return buffer.ToArray();
        }

        #endregion

    }
}
