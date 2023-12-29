using System.Runtime.Serialization;

namespace SegyIO.Common
{
    public class SectionPoint
    {
        /// <summary>
        /// 在地震数据文件中的道序号,从0开始
        /// </summary>
        public int TrcNo { set; get; }
        public int InLine { set; get; }
        public int CrossLine { set; get; }
        public double X { set; get; }
        public double Y { set; get; }
        public int StartTime { get; set; }

        /// <summary>
        /// 炮点坐标X
        /// </summary>
        [OptionalField] public double SrcX;

        /// <summary>
        /// 炮点坐标Y
        /// </summary>
        [OptionalField] public double SrcY;

        /// <summary>
        /// 从炮点到接收点的绝对距离
        /// </summary>
        [OptionalField] public double RecvOff;

        public SectionPoint()
        {

        }

        public SectionPoint(SectionPoint pt)
        {
            TrcNo = pt.TrcNo;
            InLine = pt.InLine;
            CrossLine = pt.CrossLine;
            X = pt.X;
            Y = pt.Y;
            StartTime = pt.StartTime;

            SrcX = pt.SrcX;
            SrcY = pt.SrcY;
            RecvOff = pt.RecvOff;
        }

        public override string ToString()
        {
            string ret = TrcNo + ",";
            ret += InLine + ",";
            ret += CrossLine + ",";
            ret += X + ",";
            ret += Y + ",";
            ret += StartTime.ToString();
            return ret;
        }

        /// <summary>
        /// 将字符串转换为类实例
        /// </summary>
        public static SectionPoint FromString(string line)
        {
            if (string.IsNullOrEmpty(line))
                return null;


            char[] trimChars = { ' ', ',', ';', '\t' };
            line = line.Trim(trimChars);
            string[] strs = line.Split(trimChars);
            if (strs.Length < 6)
                return null;
            SectionPoint sp;
            try
            {
                sp = new SectionPoint
                {
                    TrcNo = Convert.ToInt32(strs[0]),
                    InLine = Convert.ToInt32(strs[1]),
                    CrossLine = Convert.ToInt32(strs[2]),
                    X = Convert.ToDouble(strs[3]),
                    Y = Convert.ToDouble(strs[4]),
                    StartTime = Convert.ToInt32(strs[5])
                };
            }
            catch (Exception)
            {
                sp = null;
            }
            return sp;
        }

        /// <summary>
        /// 把该结构的成员按顺序转换为字节数组
        /// </summary>
        public static int ToBytes(SectionPoint sp, out byte[] bytes)
        {
            if (sp == null)
            {
                bytes = null;
                return 0;
            }
            BufferBuilder builder = new BufferBuilder();

            builder.Append(sp.TrcNo);
            builder.Append(sp.InLine);
            builder.Append(sp.CrossLine);
            builder.Append(sp.X);
            builder.Append(sp.Y);
            builder.Append(sp.StartTime);

            bytes = builder.bytes;
            return builder.Length;
        }

        /// <summary>
        /// 将字节数组转换为类实例
        /// </summary>
        public static SectionPoint FromBytes(byte[] bytes)
        {
            if (bytes == null || bytes.Length < BytesLength)
            {
                return null;
            }

            BufferReader br = new BufferReader(bytes, false);
            SectionPoint sp = new SectionPoint
            {
                TrcNo = br.ReadInt32(),
                InLine = br.ReadInt32(),
                CrossLine = br.ReadInt32(),
                X = br.ReadDouble(),
                Y = br.ReadDouble(),
                StartTime = br.ReadInt32()
            };
            return sp;
        }

        /// <summary>
        /// 类实例的字节长度,固定值32
        /// </summary>
        public static int BytesLength => 32;
    }
    /// <summary>
    /// SectionPoint的InLine比较器
    /// </summary>
    public class SecPtComparer_Inline : IComparer<SectionPoint>
    {
        public int Compare(SectionPoint a, SectionPoint b)
        {
            return a.InLine.CompareTo(b.InLine);
        }
    }

    /// <summary>
    /// SectionPoint的CrossLine比较器
    /// </summary>
    public class SecPtComparer_Crossline : IComparer<SectionPoint>
    {
        public int Compare(SectionPoint a, SectionPoint b)
        {
            return a.CrossLine.CompareTo(b.CrossLine);
        }
    }

    /// <summary>
    /// SectionPoint的X比较器
    /// </summary>
    public class SecPtComparer_X : IComparer<SectionPoint>
    {
        public int Compare(SectionPoint a, SectionPoint b)
        {
            return a.X.CompareTo(b.X);
        }
    }

    /// <summary>
    /// SectionPoint的Y比较器
    /// </summary>
    public class SecPtComparer_Y : IComparer<SectionPoint>
    {
        public int Compare(SectionPoint a, SectionPoint b)
        {
            return a.Y.CompareTo(b.Y);
        }
    }
}
