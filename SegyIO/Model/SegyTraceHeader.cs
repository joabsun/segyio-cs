using System.Reflection;

namespace SegyIO.Model
{
    /// <summary>
    /// The following class defines the first 240 bytes of each trace
    /// record in a SEG-Y file.  This is called the "Trace Header."
    /// </summary>
    public class SegyTraceHeader
    {
        #region public properties

        /// <summary>
        /// * bytes 1-4
        /// Trace sequence number within line — Numbers continue to increase if the same
        /// line continues across multiple SEG Y files. Highly recommended for all types of  data.
        /// 一条测线中的道顺序号，如果一条测线有若干卷磁带，顺序号连续递增。
        /// </summary>
        public uint TrcNoInLn { get; set; }

        /// <summary>
        /// bytes 5-8,
        /// Trace sequence number within SEG Y file — Each file starts with trace sequence one.
        /// 在本卷磁带中的道顺序号。每卷磁带的道顺序号从l开始。
        /// </summary>
        public uint TrcNoInTp { get; set; }

        /// <summary>
        /// bytes 9-12,
        /// *Original field record number. Highly recommended for all types of data.
        /// 原始的野外记录号（炮号）。
        /// </summary>
        public uint FFID { get; set; }

        /// <summary>
        ///  bytes 13-16, 
        /// *Trace number within the original field record. Highly recommended for all types of data.
        /// 在原始野外记录中的道号。
        /// </summary>
        public uint TrcNoInFld { get; set; }       

        /// <summary>
        /// bytes 17-20,
        /// Energy source point number — Used when more than one record occurs at the
        /// same effective surface location. It is recommended that the new entry defined in
        /// Trace Header bytes 197-202 be used for shotpoint number.
        /// 震源点号(在同一个地面点有多于一个记录时使用)。
        /// </summary>
        public uint EnergySrcPt { get; set; }   

        /// <summary>
        /// bytes 21-24,
        /// Ensemble number (i.e. CDP, CMP, CRP, etc)
        /// CMP号（或CDP,CRP号）。（弯线=共反射面元号）
        /// </summary>
        public uint CdpNo { get; set; }

        /// <summary>
        /// bytes 25-28,
        /// Trace number within the ensemble — Each ensemble starts with trace number one.
        /// 在CMP道集中的道号(在每个CMP道集中道号从1开始)。
        /// </summary>
        public uint TrcNoInCmp { get; set; } 

        /// <summary>
        /// bytes 29-30,
        /// Trace identification code,Highly recommended for all types of data.
        ///         1=seismic, 2=dead, 3=dummy, 4=time break, 
        ///         5=uphole, 6=sweep, 7=timing, 8=water break, 9->N=optional
        /// 道识别码：  l=地震数据；        4=爆炸信号；    7＝计时信号；
        ///             2=死道；            5=井口道；      8＝水断信号；
        ///             3=无效道（空道）；  6＝扫描道；     9…N=选择使用(N=32767)
        /// </summary>
        public ushort TrcIdCd { get; set; }

        /// <summary>
        /// bytes 31-32,  + Number of vertically summed traces yielding this trace (1=one trace, 2=two summed traces, ...)
        /// 构成该道的垂直叠加道数(1是一道；2是两道相加;…)
        /// </summary>
        public ushort NumVertSum { get; set; } 

        /// <summary>
        /// bytes 33-34,  + Number of horizontally stacked traces yielding this trace
        /// 构成该道的水平叠加道数(1是一道; 2是两道叠加;…)
        /// </summary>
        public ushort NumHorznSum { get; set; } 

        /// <summary>
        /// bytes 35-36,    Data Use: 1=production, 2=test  
        /// 数据类型：1=生产；2=试验
        /// </summary>
        public ushort DataUse { get; set; }

        /// <summary>
        /// bytes 37-40,    Source to receiver offset (negative if opposite to direction in which line is shot)
        /// 从炮点到接收点的距离(如果排列与激发前进方向相反取负值) （分米）
        /// </summary>
        public int RecvOff { get; set; } 

        /// <summary>
        /// bytes 41-44,  + Receiver group elevation; all elevations above sea level are positive and below sea level are negative
        /// 接收点高程。高于海平而的高程为正，低于海平面为负。
        /// </summary>
        public int RecvElev { get; set; } 

        /// <summary>
        /// bytes 45-48,  + Surface elevation at source (fish altitude)
        /// 炮点的地面高程。
        /// </summary>
        public int SrcElev { get; set; } 

        /// <summary>
        /// bytes 49-52,  + Source depth below surface (a positive number) (fish depth)
        /// 炮点低于地面的深度(正数)(井深)。
        /// </summary>
        public int SrcDepth { get; set; } 

        /// <summary>
        /// bytes 53-56,  + Datum Elevation at receiver group (ocean tide)
        /// 接收点的基准面高程。
        /// </summary>
        public int RecDatumElev { get; set; } 

        /// <summary>
        /// bytes 57-60,  + Datum elevation at source (heave)
        /// 炮点的基准面高程。
        /// </summary>
        public int SrcDatumElev { get; set; } 

        /// <summary>
        /// bytes 61-64,  + Water depth at source
        /// 炮点的水深。
        /// </summary>
        public int WaterDepthSrc { get; set; } 

        /// <summary>
        /// bytes 65-68,  + Water depth at receiver group
        /// 接收点的水深。
        /// </summary>
        public int WaterDepthRec { get; set; } 

        /// <summary>
        /// bytes 69-70,  + Scale factor to be applied to all values in 40-67 to give true value: =1, +-10, +=1000, +=10000.  
        /// If great than 0, multiply.  If little than 0, divide
        /// 对41-68字节中的所有高程和深度应用了此因子给出真值。比例因子=1，±10，±100，±1000 或者±10000。
        /// 如果为正，乘以因子； 如果为负，则除以因子。
        /// </summary>
        public short SrvyScal { get; set; } 

        /// <summary>
        /// bytes 71-72,  + Same, but for 72-87
        /// 对73-88字节中的所有坐标应用了此因子给出真值。比例因子=1，±10，±100，±1000 或者±10000。
        /// 如果为正，乘以因子； 如果为负，则除以因子(在GRISYS中为10)。
        /// </summary>
        public short MapScale { get; set; } 

        /// <summary>
        /// bytes 73-76,  + intitude or Northing of source, depending on value of CoordUnit below
        /// 炮点坐标--X |
        /// ---- 如果坐标单位是弧度的秒，X值代表径度，Y值代表纬度。正值代表格林威治子午线东或者赤道北的秒数。负值则为西或者南的秒数
        /// </summary>
        public int SrcX { get; set; }

        /// <summary>
        /// bytes 77-80,  + Latitude or Easting of source, depending on value of CoordUnit below
        /// 炮点坐标--Y
        /// </summary>
        public int SrcY { get; set; } 

        /// <summary>
        /// bytes 81-84,  + intitude or Northing of receiver, depending on value of CoordUnit below
        /// 检波点坐标--X
        /// </summary>
        public int RecvX { get; set; } 

        /// <summary>
        /// bytes 85-88,  + Latitude or Easting of receiver, depending on value of CoordUnit below
        /// 检波点坐标--Y
        /// </summary>
        public int RecvY { get; set; } 

        /// <summary>
        /// bytes 89-90,  + Coordinate units: 
        /// 1=length (meters or feet),  2=seconds of arc
        /// 3 = Decimal degrees         4 = Degrees, minutes, seconds (DMS)
        /// Note: To encode ±DDDMMSS bytes 89-90 equal = ±DDD*104 + MM*102 + SS
        ///        with bytes 71-72 set to 1; To encode ±DDDMMSS.ss bytes 89-90 equal =±DDD*106 + MM*104 + SS*102 with bytes 71-72 set to -100.
        /// 坐标单位； 1=长度(米或者英尺)； 2=弧度的秒。
        /// </summary>
        public ushort CoordUnit { get; set; } 

        /// <summary>
        /// bytes 91-92,  Weathering velocity. (ft/s or m/s as specified in Binary File Header bytes 3255-3256)
        /// 风化层速度。
        /// </summary>
        public short WeatherV { get; set; } 

        /// <summary>
        /// bytes 93-94,  Subweathering velocity. (ft/s or m/s as specified in Binary File Header bytes 3255-3256)
        /// 降速层速度。
        /// </summary>
        public short SubweatherV { get; set; }   

        /// <summary>
        /// bytes 95-96,    Uphole time at source in milliseconds.
        /// 震源处的井口时间。
        /// </summary>
        public short SrcUpholeT { get; set; }

        /// <summary>
        /// bytes 97-98,    Uphole time at group in milliseconds.
        /// 接收点处的井口时间。
        /// </summary>
        public short RecUpholeT { get; set; }  

        /// <summary>
        /// bytes 99-100,    Source static correction in milliseconds.
        /// 炮点的静校正。
        /// </summary>
        public short SrcStaticCor { get; set; }

        /// <summary>
        /// bytes 101-102,  Receiver group static correction in milliseconds.
        /// 接收点的静校正。
        /// </summary>
        public short RecStaticCor { get; set; } 

        /// <summary>
        /// bytes 103-104,  Total static applied (zero if none)
        /// 应用的总静校正量(如果没有应用静校正为零)。
        /// </summary>
        public short TotalStatic { get; set; } 

        /// <summary>
        /// bytes 105-106,  In milliseconds, See spec for details
        /// Lag time A — Time in milliseconds between end of 240-byte trace identification header and time break. The value is
        /// positive if time break occurs after the end of header; negative if time break occurs before the end of header. Time break is
        /// defined as the initiation pulse that may be recorded on an auxiliary trace or as otherwise specified by the recording system.
        /// 延迟时间-A，以 ms 表示。240字节的道标识的结束和时间信号之间的时间。如果时间信号出现在道头结束之前为正。
        /// 如果时间信号出现在道头结束之后为负。时间信号就是起始脉冲，它记录在辅助道上或者由记录系统指定。
        /// </summary>
        public short LagTimeA { get; set; } 

        /// <summary>
        /// bytes 107-108, Lag Time B — Time in milliseconds between time break and 
        /// the initiation time of the energy source. May be positive or negative.
        /// 时间延迟-B，以 ms 表示。为时间信号和能量起爆之间的时间。可正可负。
        /// </summary>
        public short LagTimeB { get; set; }  

        /// <summary>
        /// bytes 109-110,+ In milliseconds, time between transmit and data collection
        /// Delay recording time — Time in milliseconds between initiation time of energy source and the time when recording
        /// of data samples begins. In SEG Y rev 0 this entry was intended for deep-water work if data recording does not start
        /// at zero time. The entry can be negative to accommodate negative start times (i.e. data recorded before time zero,
        /// presumably as a result of static application to the data trace).
        /// If a non-zero value (negative or positive) is recorded in this entry, a comment to that effect should appear 
        /// in the Textual File Header.
        /// 时间延迟时间，以 ms 表示。能量源的起爆时间和开始记录
        /// 数据样点之间的时间(深水时，数据记录不从时间零开始。)
        /// </summary>
        public short DelayRec { get; set; }

        /// <summary>
        /// bytes 111-112,  Mute time — Start time in milliseconds.
        /// 起始切除时间。
        /// </summary>
        public short MuteTimeStart { get; set; } 

        /// <summary>
        /// bytes 113-114,  Mute time -- End time in milliseconds.
        /// 结束切除时间。
        /// </summary>
        public short MuteTimeEnd { get; set; } 

        /// <summary>
        /// bytes 115-116,* Number of samples in this trace. Highly recommended for all types of data.
        ///  本道的采样点数。
        /// </summary>
        public ushort SamplesThisTrace { get; set; }

        /// <summary>
        /// bytes 117-118,* Sample interval for this trace (in microseconds)
        /// Sample interval in microseconds (μs) for this trace. 
        /// The number of bytes in a trace record must be consistent with the number of samples written in the trace header. 
        /// This is important for all recording media;but it is particularly crucial for the correct processing of SEG Y data 
        /// in disk files(see Appendix C).
        /// If the fixed length trace flag in bytes 3503-3504 of the Binary File Header is set,the sample interval and number 
        /// of samples in every trace in the SEG Y file must be the same as the values recorded in the Binary File Header. 
        /// If the fixed length trace flag is not set, the sample interval and number of samples may vary from trace to trace.
        /// Highly recommended for all types of data.
        /// 本道的采样间隔，以 μs 表示。
        /// </summary>
        public ushort SmplIntvl { get; set; } 

        /// <summary>
        /// bytes 119-120,  AmpScale type of field instruments: 1=fixed, 2=binary, 3=floating point, 4->N=optional use
        /// 野外仪器的增益类型：1=固定增益；2=二进制增益；3=浮点增益；4…N=选择使用。
        /// </summary>
        public ushort GainType { get; set; } 

        /// <summary>
        /// bytes 121-122,  Instrument gain constant(dB)
        /// 仪器增益常数。
        /// </summary>
        public short GainConst { get; set; }  

        /// <summary>
        /// bytes 123-124,+ Instrument early or initial gain (db)
        /// 仪器起始增益(DB)。
        /// </summary>
        public short IniGain { get; set; } 

        /// <summary>
        /// bytes 125-126,+ Correlated? 1=no, 2=yes
        /// 相关码：1=没有相关；2=相关。
        /// </summary>
        public ushort Correlated { get; set; } 

        /// <summary>
        /// bytes 127-128,+ Sweep frequency at start
        /// 起始扫描频率(HZ)。
        /// </summary>
        public short SweepStartFreq { get; set; } 

        /// <summary>
        /// bytes 129-130,+ Sweep frequency at end
        /// 结束扫描频率(HZ)。
        /// </summary>
        public short SweepEndFreq { get; set; }

        /// <summary>
        /// bytes 131-132,  Sweep length in milliseconds
        /// 扫描长度，以 ms 表示。
        /// </summary>
        public short SweepTime { get; set; }

        /// <summary>
        /// bytes 133-134,  Sweep type: 1=linear, 2=parabolic, 3=exponential, 4=other
        /// 扫描类型：1=线性；2=抛物线；3=指数；4=其他
        /// </summary>
        public ushort SweepType { get; set; } 

        /// <summary>
        /// bytes 135-136,  Sweep trace taper length at start in milliseconds
        /// 扫描道起始斜坡长度，以 ms 表示
        /// </summary>
        public short SweepTaperStart { get; set; } 

        /// <summary>
        /// bytes 137-138,  Sweep trace taper length at end in milliseconds
        /// 扫描道终了斜坡长度，以 ms 表示。
        /// </summary>
        public short SweepTaperEnd { get; set; }

        /// <summary>
        /// bytes 139-140,  Taper type: 1=linear, 2=cos*cos, 3=other
        /// 斜坡类型：1=线性；2=COS 2；3=其他。
        /// </summary>
        public short TaperType { get; set; }

        /// <summary>
        /// bytes 141-142,  Alias filter frequency, if used
        /// 滤假频的频率(如果使用)。
        /// </summary>
        public short AliasFilter { get; set; }

        /// <summary>
        /// bytes 143-144,  Alias filter slope
        /// 滤假频的斜率。
        /// </summary>
        public short AliasSlope { get; set; }

        /// <summary>
        /// bytes 145-146,  Notch filter frequency, if used
        /// 陷波滤波器频率。
        /// </summary>
        public short NotchFilter { get; set; } 

        /// <summary>
        /// bytes 147-148,  Notch filter slope
        /// 陷波斜率。
        /// </summary>
        public short NotchSlope { get; set; }

        /// <summary>
        /// bytes 149-150,  Low cut frequency, if used
        /// 低截频率(如果使用)。
        /// </summary>
        public short LowCut { get; set; }

        /// <summary>
        /// bytes 151-152,  High cut frequency, if used
        /// 高截频率(如果使用)。
        /// </summary>
        public short HighCut { get; set; } 

        /// <summary>
        /// bytes 153-154,  Low cut slope
        /// 低截频率的斜率。
        /// </summary>
        public short LowCutSlope { get; set; } 

        /// <summary>
        /// bytes 155-156,  High cut slope
        /// 高截频率的斜率。
        /// </summary>
        public short HighCutSlope { get; set; } 

        /// <summary>
        /// bytes 157-158,+ Year data was recorded
        /// The 1975 standard is unclear as to whether this should be recorded as a 2-digit or a 4-digit year and both have been used. 
        /// For SEG Y revisions beyond rev 0, the year should be recorded as the complete 4-digit Gregorian calendar year (i.e. the 
        /// year 2001 should be recorded as 200110(7D116)).
        /// 数据记录的年。
        /// </summary>
        public ushort Year { get; set; }  

        /// <summary>
        /// bytes 159-160,+ Day of year (Julian day)
        /// 数据记录的日。
        /// </summary>
        public ushort JulianDay { get; set; } 

        /// <summary>
        /// bytes 161-162,+ Hour of day of source trigger
        /// 小时(24时制)。
        /// </summary>
        public ushort Hour { get; set; } 

        /// <summary>
        /// bytes 163-164,+ Minute of hour of source trigger
        /// 分。
        /// </summary>
        public ushort Minute { get; set; } 

        /// <summary>
        /// bytes 165-166,+ Second of minute of source trigger
        /// 秒
        /// </summary>
        public ushort Second { get; set; } 

        /// <summary>
        /// bytes 167-168,Time basis code:
        /// 1 = Local   2 = GMT (Greenwich Mean Time)
        /// 3 = Other, should be explained in a user defined stanza in the Extended Textual File Header
        /// 4 = UTC (Coordinated Universal Time)
        /// 时间代码：1=当地时间；2=格林威治时间；3=其他
        /// </summary>
        public ushort TimeBasisCode { get; set; }

        /// <summary>
        /// bytes 169-170,+ Trace weighting factor — Defined as 2-N volts for the least significant bit. (N = 0,1, …, 32767)
        /// 道加权因子。(最小有效位定义为2**(-n), n=0，1，2，…，32767)
        /// </summary>
        public short TraceWeightFactor { get; set; }

        /// <summary>
        /// bytes 171-172,Geophone group number of roll switch position one.
        /// 覆盖开关位置1的检波器道号。
        /// </summary>
        public short GeophnNoRoll { get; set; }

        /// <summary>
        /// bytes 173-174,Geophone group number of trace number one within original field record.
        /// 在原始野外记录中道号1的检波点号。
        /// </summary>
        public short GeophnNoFldStart { get; set; }

        /// <summary>
        /// bytes 175-176,Geophone group number of trace number one within original field record.
        /// 在原始野外记录中最后一道的检波点号。
        /// </summary>
        public short GeophnNoFldEnd { get; set; }

        /// <summary>
        /// bytes 177-178,Gap size (total number of groups dropped).
        /// 缺口大小(缺少的检波点总数)。
        /// </summary>
        public short GapSize { get; set; }

        /// <summary>
        /// bytes 179-180,Over travel associated with taper at beginning or end of line:1 = down (or behind),2 = up (or ahead)
        /// 在测线的开始或者结束处的斜坡位置：1=在后面；2=在前面。
        /// </summary>
        public ushort OverTravelCode { get; set; }

        #endregion

        #region 20141218新增属性

        /// <summary>
        /// bytes 181-184,X coordinate of ensemble (CDP) position of this trace (scalar in Trace Header
        ///    bytes 71-72 applies). The coordinate reference system should be identified
        ///    through an extended header Location Data stanza (see section D-1).
        /// </summary>
        public int CdpX { get; set; }

        /// <summary>
        /// bytes 185-188,Y coordinate of ensemble (CDP) position of this trace (scalar in Trace Header
        ///    bytes 71-72 applies). The coordinate reference system should be identified
        ///    through an extended header Location Data stanza (see section D-1).
        /// </summary>
        public int CdpY { get; set; }

        /// <summary>
        /// bytes 189-192,For 3-D poststack data, this field should be used for the in-line number. If one
        ///     in-line per SEG Y file is being recorded, this value should be the same for all
        ///     traces in the file and the same value will be recorded in bytes 3205-3208 of the
        ///     Binary File Header.
        /// </summary>
        public int Inline { get; set; }

        /// <summary>
        /// bytes 193-196,For 3-D poststack data, this field should be used for the cross-line number. This 
        ///     will typically be the same value as the ensemble (CDP) number in Trace Header bytes 21-24, 
        ///     but this does not have to be the case.
        /// </summary>
        public int Crossline { get; set; }

        /// <summary>
        /// bytes 197-200, Shotpoint number — This is probably only applicable to 2-D poststack data.
        ///     Note that it is assumed that the shotpoint number refers to the source location 
        ///     nearest to the ensemble (CDP) location for a particular trace. If this is not the
        ///     case, there should be a comment in the Textual File Header explaining what the
        ///     shotpoint number actually refers to.
        /// </summary>
        public int ShotpointNumber { get; set; }

        /// <summary>
        /// bytes 201-202, Scalar to be applied to the shotpoint number in Trace Header bytes 197-200 to 
        ///     give the real value. If positive, scalar is used as a multiplier; if negative as a 
        ///     divisor; if zero the shotpoint number is not scaled (i.e. it is an integer. A typical 
        ///     value will be -10, allowing shotpoint numbers with one decimal digit to the right of 
        ///     the decimal point).
        /// </summary>
        public short ShotpointNumberScale { get; set; }

        /// <summary>
        /// bytes 203-204, Trace value measurement unit:
        ///     -1 = Other (should be described in Data Sample Measurement Units Stanza)
        ///     0 = Unknown
        ///     1 = Pascal (Pa)
        ///     2 = Volts (v)
        ///     3 = Millivolts (mV)
        ///     4 = Amperes (A)
        ///     5 = Meters (m)
        ///     6 = Meters per second (m/s)
        ///     7 = Meters per second squared (m/s2)
        ///     8 = Newton (N)
        ///     9 = Watt (W)
        /// </summary>
        public short TraceValueUnit { get; set; }

        /// <summary>
        /// bytes 205-208, Transduction Constant mantissa --The multiplicative constant used to convert the Data
        ///     Trace samples to the Transduction Units (specified in Trace Header bytes 211-212). 
        ///     The constant is (i.e. Bytes 205-208 * 10**Bytes 209-210).
        /// </summary>
        public int TransductConstant { get; set; }

        /// <summary>
        /// bytes 209-210, Transduction Constant exponent--The multiplicative constant used to convert the Data
        ///     Trace samples to the Transduction Units (specified in Trace Header bytes 211-212). 
        ///     The constant is (i.e. Bytes 205-208 * 10**Bytes 209-210).
        /// </summary>
        public short TransductConstantExp { get; set; }

        /// <summary>
        /// bytes 211-212, Transduction Units--The unit of measurement of the Data Trace samples after
        ///   they have been multiplied by the Transduction Constant specified in Trace
        ///   Header bytes 205-210.
        ///     -1 = Other (should be described in Data Sample Measurement Unit stanza, page 36)
        ///     0 = Unknown
        ///     1 = Pascal (Pa)
        ///     2 = Volts (v)
        ///     3 = Millivolts (mV)
        ///     4 = Amperes (A)
        ///     5 = Meters (m)
        ///     6 = Meters per second (m/s)
        ///     7 = Meters per second squared (m/s2)
        ///     8 = Newton (N)
        ///     9 = Watt (W)
        /// </summary>
        public short TransductUnit { get; set; }

        /// <summary>
        /// bytes 213-214, Device/Trace Identifier — The unit number or id number of the device associated
        ///     with the Data Trace (i.e. 4368 for vibrator serial number 4368 or 20316 for gun
        ///     16 on string 3 on vessel 2). This field allows traces to be associated across
        ///     trace ensembles independently of the trace number (Trace Header bytes 25-28).
        /// </summary>
        public short DeviceTraceIdentifier { get; set; }

        /// <summary>
        /// bytes 215-216, Scalar to be applied to times specified in Trace Header bytes 95-114 to give the
        ///     true time value in milliseconds. Scalar = 1, +10, +100, +1000, or +10,000. If
        ///     positive, scalar is used as a multiplier; if negative, scalar is used as divisor. A
        ///     value of zero is assumed to be a scalar value of 1.
        /// </summary>
        public short TimeScalar { get; set; }

        /// <summary>
        /// bytes 217-218, Source Type/Orientation — Defines the type and the orientation of the energy
        ///   source. The terms vertical, cross-line and in-line refer to the three axes of an
        ///   orthogonal coordinate system. The absolute azimuthal orientation of the
        ///   coordinate system axes can be defined in the Bin Grid Definition Stanza (page 27).
        ///     -1 to -n = Other (should be described in Source Type/Orientation stanza,page 38)
        ///     0 = Unknown
        ///     1 = Vibratory - Vertical orientation
        ///     2 = Vibratory - Cross-line orientation
        ///     3 = Vibratory - In-line orientation
        ///     4 = Impulsive - Vertical orientation
        ///     5 = Impulsive - Cross-line orientation
        ///     6 = Impulsive - In-line orientation
        ///     7 = Distributed Impulsive - Vertical orientation
        ///     8 = Distributed Impulsive - Cross-line orientation
        ///     9 = Distributed Impulsive - In-line orientation
        /// </summary>
        public short SourceOrientationType { get; set; }

        /// <summary>
        /// bytes 219-220, Source Energy Direction with respect to the source orientation — The positive
        /// orientation direction is defined in Bytes 217-218 of the Trace Header. The energy direction 
        /// is encoded in tenths of degrees (i.e. 347.8º is encoded as 3478).
        /// </summary>
        public short SourceEnergyDirection1 { get; set; }

        /// <summary>
        /// bytes 221-224, Source Energy Direction with respect to the source orientation — The positive
        /// orientation direction is defined in Bytes 217-218 of the Trace Header. The energy direction 
        /// is encoded in tenths of degrees (i.e. 347.8º is encoded as 3478).
        /// </summary>
        public int SourceEnergyDirection2 { get; set; }

        /// <summary>
        /// bytes 225-228, Source Measurement — Describes the source effort used to generate the trace.
        /// The measurement can be simple, qualitative measurements such as the total weight of 
        /// explosive used or the peak air gun pressure or the number of vibrators times the sweep
        /// duration. Although these simple measurements are acceptable, it is preferable to use 
        /// true measurement units of energy or work. 
        /// The constant is encoded as a four-byte, two's complement integer (bytes 225-228) which 
        /// is the mantissa and a two-byte, two's complement integer (bytes 209-230) which is the 
        /// power of ten exponent (i.e. Bytes 225-228 * 10**Bytes 229-230).
        /// </summary>
        public int SourceMeasurement1 { get; set; }

        /// <summary>
        /// bytes 229-230, Source Measurement — Describes the source effort used to generate the trace.
        /// The measurement can be simple, qualitative measurements such as the total weight of 
        /// explosive used or the peak air gun pressure or the number of vibrators times the sweep
        /// duration. Although these simple measurements are acceptable, it is preferable to use 
        /// true measurement units of energy or work. 
        /// The constant is encoded as a four-byte, two's complement integer (bytes 225-228) which 
        /// is the mantissa and a two-byte, two's complement integer (bytes 209-230) which is the 
        /// power of ten exponent (i.e. Bytes 225-228 * 10**Bytes 229-230).
        /// </summary>
        public short SourceMeasurement2 { get; set; }

        /// <summary>
        /// bytes 231-232, Source Measurement Unit — The unit used for the Source Measurement, Trace
        /// header bytes 225-230.
        ///     -1 = Other (should be described in Source Measurement Unit stanza, page 39)
        ///     0 = Unknown
        ///     1 = Joule (J)
        ///     2 = Kilowatt (kW)
        ///     3 = Pascal (Pa)
        ///     4 = Bar (Bar)
        ///     4 = Bar-meter (Bar-m)
        ///     5 = Newton (N)
        ///     6 = Kilograms (kg)
        /// </summary>
        public short SourceMeasurementUnit { get; set; }

        #endregion

        /// <summary>
        /// bytes 233-240,  Spare
        /// </summary>
        public byte[] Spare { get; set; }

        /// <summary>
        /// 按缺省值初始化道头
        /// </summary>
        public void Initialize()
        {
            TrcNoInLn = 1;
            TrcNoInTp = 1;
            FFID = 1;
            TrcNoInFld = 1;
            EnergySrcPt = 0;
            CdpNo = 1;
            TrcNoInCmp = 1;
            TrcIdCd = 1;
            NumVertSum = 1;
            NumHorznSum = 1;
            DataUse = 1;
            RecvOff = 0;
            RecvElev = 0;
            SrcElev = 0;
            SrcDepth = 0;
            RecDatumElev = 0;
            SrcDatumElev = 0;
            WaterDepthSrc = 0;
            WaterDepthRec = 0;
            SrvyScal = 0;
            MapScale = 0;
            SrcX = 0;
            SrcY = 0;
            RecvX = 0;
            RecvY = 0;
            CoordUnit = 0;
            WeatherV = 0;
            SubweatherV = 0;
            SrcUpholeT = 0;
            RecUpholeT = 0;
            SrcStaticCor = 0;
            RecStaticCor = 0;
            TotalStatic = 0;
            LagTimeA = 0;
            LagTimeB = 0;
            DelayRec = 0;
            MuteTimeStart = 0;
            MuteTimeEnd = 0;
            SamplesThisTrace = 0;
            SmplIntvl = 0;
            GainType = 1;
            GainConst = 0;
            IniGain = 0;
            Correlated = 0;
            SweepStartFreq = 0;
            SweepEndFreq = 0;
            SweepTime = 0;
            SweepType = 0;
            SweepTaperStart = 0;
            SweepTaperEnd = 0;
            TaperType = 0;
            AliasFilter = 0;
            AliasSlope = 0;
            NotchFilter = 0;
            NotchSlope = 0;
            LowCut = 0;
            HighCut = 0;
            LowCutSlope = 0;
            HighCutSlope = 0;
            Year = 0;
            JulianDay = 0;
            Hour = 0;
            Minute = 0;
            Second = 0;
            TimeBasisCode = 0;
            TraceWeightFactor = 0;
            GeophnNoRoll = 0;
            GeophnNoFldStart = 0;
            GeophnNoFldEnd = 0;
            GapSize = 0;
            OverTravelCode = 0;

            //新增属性
            CdpX = 0;
            CdpY = 0;
            Inline = 0;
            Crossline = 0;
            ShotpointNumber = 0;
            ShotpointNumberScale = 0;
            TraceValueUnit = 0;
            TransductConstant = 0;
            TransductConstantExp = 0;
            TransductUnit = 0;
            DeviceTraceIdentifier = 0;
            TimeScalar = 0;
            SourceOrientationType = 0;
            SourceEnergyDirection1 = 0;
            SourceEnergyDirection2 = 0;
            SourceMeasurement1 = 0;
            SourceMeasurement2 = 0;
            SourceMeasurementUnit = 0;

            Spare = new byte[8];
        }

        /// <summary>
        /// 按缺省值初始化道头
        /// </summary>
        public void Initialize(uint trcNo, ushort smpInt, ushort smpPerTrc)
        {
            TrcNoInLn = trcNo;
            TrcNoInTp = trcNo;
            FFID = 1;
            TrcNoInFld = trcNo;
            EnergySrcPt = 0;
            CdpNo = trcNo;
            TrcNoInCmp = trcNo;
            TrcIdCd = 1;
            NumVertSum = 1;
            NumHorznSum = 1;
            DataUse = 1;
            RecvOff = 0;
            RecvElev = 0;
            SrcElev = 0;
            SrcDepth = 0;
            RecDatumElev = 0;
            SrcDatumElev = 0;
            WaterDepthSrc = 0;
            WaterDepthRec = 0;
            SrvyScal = 0;
            MapScale = 0;
            SrcX = 0;
            SrcY = 0;
            RecvX = 0;
            RecvY = 0;
            CoordUnit = 0;
            WeatherV = 0;
            SubweatherV = 0;
            SrcUpholeT = 0;
            RecUpholeT = 0;
            SrcStaticCor = 0;
            RecStaticCor = 0;
            TotalStatic = 0;
            LagTimeA = 0;
            LagTimeB = 0;
            DelayRec = 0;
            MuteTimeStart = 0;
            MuteTimeEnd = 0;
            SamplesThisTrace = smpPerTrc;
            SmplIntvl = smpInt;
            GainType = 1;
            GainConst = 0;
            IniGain = 0;
            Correlated = 0;
            SweepStartFreq = 0;
            SweepEndFreq = 0;
            SweepTime = 0;
            SweepType = 0;
            SweepTaperStart = 0;
            SweepTaperEnd = 0;
            TaperType = 0;
            AliasFilter = 0;
            AliasSlope = 0;
            NotchFilter = 0;
            NotchSlope = 0;
            LowCut = 0;
            HighCut = 0;
            LowCutSlope = 0;
            HighCutSlope = 0;
            Year = 0;
            JulianDay = 0;
            Hour = 0;
            Minute = 0;
            Second = 0;
            TimeBasisCode = 0;
            TraceWeightFactor = 0;
            GeophnNoRoll = 0;
            GeophnNoFldStart = 0;
            GeophnNoFldEnd = 0;
            GapSize = 0;
            OverTravelCode = 0;

            //新增属性
            CdpX = 0;
            CdpY = 0;
            Inline = 0;
            Crossline = 0;
            ShotpointNumber = 0;
            ShotpointNumberScale = 0;
            TraceValueUnit = 0;
            TransductConstant = 0;
            TransductConstantExp = 0;
            TransductUnit = 0;
            DeviceTraceIdentifier = 0;
            TimeScalar = 0;
            SourceOrientationType = 0;
            SourceEnergyDirection1 = 0;
            SourceEnergyDirection2 = 0;
            SourceMeasurement1 = 0;
            SourceMeasurement2 = 0;
            SourceMeasurementUnit = 0;

            Spare = new byte[8];
        }

        /// <summary>
        /// 克隆SegyTraceHeader
        /// </summary>
        /// <returns></returns>
        public SegyTraceHeader Clone()
        {
            SegyTraceHeader header = new SegyTraceHeader
            {
                TrcNoInLn = this.TrcNoInLn,
                TrcNoInTp = this.TrcNoInTp,
                FFID = this.FFID,
                TrcNoInFld = this.TrcNoInFld,
                EnergySrcPt = this.EnergySrcPt,
                CdpNo = this.CdpNo,
                TrcNoInCmp = this.TrcNoInCmp,
                TrcIdCd = this.TrcIdCd,
                NumVertSum = this.NumVertSum,
                NumHorznSum = this.NumHorznSum,
                DataUse = this.DataUse,
                RecvOff = this.RecvOff,
                RecvElev = this.RecvElev,
                SrcElev = this.SrcElev,
                SrcDepth = this.SrcDepth,
                RecDatumElev = this.RecDatumElev,
                SrcDatumElev = this.SrcDatumElev,
                WaterDepthSrc = this.WaterDepthSrc,
                WaterDepthRec = this.WaterDepthRec,
                SrvyScal = this.SrvyScal,
                MapScale = this.MapScale,
                SrcX = this.SrcX,
                SrcY = this.SrcY,
                RecvX = this.RecvX,
                RecvY = this.RecvY,
                CoordUnit = this.CoordUnit,
                WeatherV = this.WeatherV,
                SubweatherV = this.SubweatherV,
                SrcUpholeT = this.SrcUpholeT,
                RecUpholeT = this.RecUpholeT,
                SrcStaticCor = this.SrcStaticCor,
                RecStaticCor = this.RecStaticCor,
                TotalStatic = this.TotalStatic,
                LagTimeA = this.LagTimeA,
                LagTimeB = this.LagTimeB,
                DelayRec = this.DelayRec,
                MuteTimeStart = this.MuteTimeStart,
                MuteTimeEnd = this.MuteTimeEnd,
                SamplesThisTrace = this.SamplesThisTrace,
                SmplIntvl = this.SmplIntvl,
                GainType = this.GainType,
                GainConst = this.GainConst,
                IniGain = this.IniGain,
                Correlated = this.Correlated,
                SweepStartFreq = this.SweepStartFreq,
                SweepEndFreq = this.SweepEndFreq,
                SweepTime = this.SweepTime,
                SweepType = this.SweepType,
                SweepTaperStart = this.SweepTaperStart,
                SweepTaperEnd = this.SweepTaperEnd,
                TaperType = this.TaperType,
                AliasFilter = this.AliasFilter,
                AliasSlope = this.AliasSlope,
                NotchFilter = this.NotchFilter,
                NotchSlope = this.NotchSlope,
                LowCut = this.LowCut,
                HighCut = this.HighCut,
                LowCutSlope = this.LowCutSlope,
                HighCutSlope = this.HighCutSlope,
                Year = this.Year,
                JulianDay = this.JulianDay,
                Hour = this.Hour,
                Minute = this.Minute,
                Second = this.Second,
                TimeBasisCode = this.TimeBasisCode,
                TraceWeightFactor = this.TraceWeightFactor,
                GeophnNoRoll = this.GeophnNoRoll,
                GeophnNoFldStart = this.GeophnNoFldStart,
                GeophnNoFldEnd = this.GeophnNoFldEnd,
                GapSize = this.GapSize,
                OverTravelCode = this.OverTravelCode,

                //新增属性
                CdpX = this.CdpX,
                CdpY = this.CdpY,
                Inline = this.Inline,
                Crossline = Crossline,
                ShotpointNumber = ShotpointNumber,
                ShotpointNumberScale = ShotpointNumberScale,
                TraceValueUnit = TraceValueUnit,
                TransductConstant = TransductConstant,
                TransductConstantExp = TransductConstantExp,
                TransductUnit = TransductUnit,
                DeviceTraceIdentifier = DeviceTraceIdentifier,
                TimeScalar = TimeScalar,
                SourceOrientationType = SourceOrientationType,
                SourceEnergyDirection1 = SourceEnergyDirection1,
                SourceEnergyDirection2 = SourceEnergyDirection2,
                SourceMeasurement1 = SourceMeasurement1,
                SourceMeasurement2 = SourceMeasurement2,
                SourceMeasurementUnit = SourceMeasurementUnit,

                Spare = this.Spare
            };

            return header;
        }

        /// <summary>
        /// 将道头数据转换为道头各变量描述类的列表
        /// </summary>
        public static IList<TraceHeaderItem> ToTraceHeaderItems(SegyTraceHeader tr)
        {
            Dictionary<int, string> dictTrcHdr = _dictCommentsEn;
            IList<TraceHeaderItem> trItems = new List<TraceHeaderItem>();

            Type type = tr.GetType();
            PropertyInfo[] properties = type.GetProperties();

            var byteCount = 0;
            foreach (PropertyInfo info in properties)
            {
                var varLength = 0;
                if (info.PropertyType == typeof(int))
                    varLength = 4;
                if (info.PropertyType == typeof(uint))
                    varLength = 4;
                if (info.PropertyType == typeof(short))
                    varLength = 2;
                if (info.PropertyType == typeof(ushort))
                    varLength = 2;
                if (info.PropertyType.IsArray)
                    varLength = 60;

                var startIndex = byteCount + 1;
                var endIndex = byteCount + varLength;
                object obj = info.GetValue(tr, null);
                TraceHeaderItem item = new TraceHeaderItem
                {
                    VariableName = info.Name,
                    ByteLoc = startIndex+"-"+endIndex,
                    VariableValue = obj.ToString() ,
                    Comment = dictTrcHdr[startIndex]
                };

                trItems.Add(item);
                //Console.WriteLine(item.ByteLoc + @" " + item.VariableName);
                byteCount += varLength;
            }

            return trItems;
        }

        /// <summary>
        /// 初始化道头各变量描述类的列表,变量名为0或null
        /// </summary>
        public static IList<TraceHeaderItem> ToTraceHeaderItems()
        {
            Dictionary<int, string> dictTrcHdr = _dictCommentsEn;
            IList<TraceHeaderItem> trItems = new List<TraceHeaderItem>();

            SegyTraceHeader tr = new SegyTraceHeader();
            Type type = tr.GetType();
            PropertyInfo[] properties = type.GetProperties();

            var byteCount = 0;
            foreach (PropertyInfo info in properties)
            {
                var varLength = 0;
                if (info.PropertyType == typeof(int))
                    varLength = 4;
                if (info.PropertyType == typeof(uint))
                    varLength = 4;
                if (info.PropertyType == typeof(short))
                    varLength = 2;
                if (info.PropertyType == typeof(ushort))
                    varLength = 2;
                if (info.PropertyType.IsArray)
                    varLength = 60;

                var startIndex = byteCount + 1;
                var endIndex = byteCount + varLength;
                TraceHeaderItem item = new TraceHeaderItem
                {
                    VariableName = info.Name,
                    ByteLoc = startIndex + "-" + endIndex,
                    VariableValue = "",
                    Comment = dictTrcHdr[startIndex]
                };

                trItems.Add(item);
                //Console.WriteLine(item.ByteLoc + @" " + item.VariableName);
                byteCount += varLength;
            }

            return trItems;
        }

        /// <summary>
        /// 得到指定道头变量的位置和长度
        /// </summary>
        /// <param name="varName">道头变量</param>
        /// <returns>长度为2的数组：0--起始字节, 1--字节数</returns>
        public static int[] GetVarPos(string varName)
        {
            IList<TraceHeaderItem> trItems = ToTraceHeaderItems();

            foreach (TraceHeaderItem item in trItems)
            {
                if (string.Equals(item.VariableName, varName))
                {
                    return item.GetVarPos();
                }
            }

            return null;
        }

        /// <summary>
        /// 存储说明用的字符串, key: start byte
        /// </summary>
        private static Dictionary<int, string> _dictCommentsEn
        {
            get
            {
                Dictionary<int, string> dict = new Dictionary<int, string>();

                #region 1-180

                dict.Add(1,"Trace sequence number within line — Numbers continue to increase if the same line continues across multiple SEG Y files.");

                dict.Add(5,"Trace sequence number within SEG Y file — Each file starts with trace sequence one.");

                dict.Add(9,"Original field record number. ");

                dict.Add(13,"Trace number within the original field record.");

                dict.Add(17,"Energy source point number — Used when more than one record occurs at the same effective surface location.");

                dict.Add(21,"Ensemble number (i.e. CDP, CMP, CRP, etc)");

                dict.Add(25, "Trace number within the ensemble — Each ensemble starts with trace number one.");

                dict.Add(29,"Trace identification code.");

                dict.Add(31,"Number of vertically summed traces yielding this trace. (1 is one trace, 2 is two summed traces, etc.)");

                dict.Add(33,"Number of horizontally stacked traces yielding this trace. (1 is one trace, 2 is two summed traces, etc.)");

                dict.Add(35,"Data use: 1 = Production; 2 = Test");

                dict.Add(37,"Distance from center of the source point to the center of the receiver group (negative if opposite to direction in which line is shot).");

                dict.Add(41,"Receiver group elevation (all elevations above the Vertical datum are positive and below are negative).");

                dict.Add(45,"Surface elevation at source.");

                dict.Add(49,"Source depth below surface (a positive number).");

                dict.Add(53,"Datum elevation at receiver group.");

                dict.Add(57,"Datum elevation at source.");

                dict.Add(61,"Water depth at source.");

                dict.Add(65,"Water depth at group.");

                dict.Add(69, "Scalar to be applied to all elevations and depths specified in Trace Header"
                        + " bytes 41-68 to give the real value. Scalar = 1, +10, +100, +1000, or +10,000. "
                        + "If positive, scalar is used as a multiplier; if negative, scalar is used as a divisor.");

                dict.Add(71,"Scalar to be applied to all coordinates specified in Trace Header bytes 73-88 and"
                              + " to bytes Trace Header 181-188 to give the real value. Scalar = 1, +10, +100, +1000, "
                              + "or +10,000. If positive, scalar is used as a multiplier; if negative, scalar is used as divisor.");

                dict.Add(73, "Source coordinate - X.");

                dict.Add(77, "Source coordinate - Y.");

                dict.Add(81, "Group coordinate - X.");

                dict.Add(85,"Group coordinate - Y.");

                dict.Add(89, "Coordinate units: 1 = Length (meters or feet) 2 = Seconds of arc 3 = Decimal degrees 4 = Degrees, minutes, seconds (DMS)");

                dict.Add(91,"Weathering velocity. (ft/s or m/s as specified in Binary File Header bytes 3255-3256)");

                dict.Add(93, "Subweathering velocity. (ft/s or m/s as specified in Binary File Header bytes 3255-3256)");

                dict.Add(95,"Uphole time at source in milliseconds.");

                dict.Add(97,"Uphole time at group in milliseconds.");

                dict.Add(99, "Source static correction in milliseconds.");

                dict.Add(101, "Group static correction in milliseconds.");

                dict.Add(103, "Total static applied in milliseconds. (Zero if no static has been applied)");

                dict.Add(105, "Lag time A — Time in milliseconds between end of 240-byte"
                              + " trace identification header and time break. The value is"
                              + " positive if time break occurs after the end of header; negative"
                              + " if time break occurs before the end of header. Time break is"
                              + " defined as the initiation pulse that may be recorded on an"
                              + " auxiliary trace or as otherwise specified by the recording system.");

                dict.Add(107, "Lag Time B — Time in milliseconds between time break and "
                              + "the initiation time of the energy source. May be positive ornegative.");

                dict.Add(109, "Delay recording time — Time in milliseconds between "
                              + "initiation time of energy source and the time when recording of data samples begins.");

                dict.Add(111, "Mute time — Start time in milliseconds.");

                dict.Add(113, "Mute time — End time in milliseconds.");

                dict.Add(115, "Number of samples in this trace.");

                dict.Add(117, "Sample interval in microseconds (μs) for this trace.");

                dict.Add(119,"Gain type of field instruments: 1 = fixed 2 = binary 3 = floating point 4 … N = optional use");

                dict.Add(121, "Instrument gain constant (dB).");

                dict.Add(123, "Instrument early or initial gain (dB).");

                dict.Add(125, "Correlated: 1 = no 2 = yes");

                dict.Add(127,"Sweep frequency at start (Hz).");

                dict.Add(129,"Sweep frequency at end (Hz).");

                dict.Add(131, "Sweep length in milliseconds.");

                dict.Add( 133, "Sweep type: 1 = linear 2 = parabolic 3 = exponential 4 = other");

                dict.Add( 135, "Sweep trace taper length at start in milliseconds.");

                dict.Add( 137,"Sweep trace taper length at end in milliseconds.");

                dict.Add(139,"Taper type: 1 = linear 2 = cos2 3 = other");

                dict.Add( 141, "Alias filter frequency (Hz), if used.");

                dict.Add(143,"Alias filter slope (dB/octave).");

                dict.Add(145, "Notch filter frequency (Hz), if used.");

                dict.Add(147, "Notch filter slope (dB/octave).");

                dict.Add(149,"Low-cut frequency (Hz), if used.");

                dict.Add(151, "High-cut frequency (Hz), if used.");

                dict.Add(153, "Low-cut slope (dB/octave)");

                dict.Add( 155, "High-cut slope (dB/octave)");

                dict.Add(157,"Year data recorded.");

                dict.Add(159, "Day of year (Julian day for GMT and UTC time basis).");

                dict.Add(161, "Hour of day (24 hour clock).");

                dict.Add( 163, "Minute of hour.");

                dict.Add(165,"Second of minute.");

                dict.Add(167,"Time basis code: 1 = Local 2 = GMT (Greenwich Mean Time) "
                              + "3 = Other, should be explained in a user defined stanza in the Extended Textual File Header"
                              + " 4 = UTC (Coordinated Universal Time)");

                dict.Add(169, "Trace weighting factor — Defined as 2^-N volts for the least significant bit. (N = 0,1, …, 32767)");

                dict.Add(171, "Geophone group number of roll switch position one.");

                dict.Add(173, "Geophone group number of trace number one within original field record.");

                dict.Add(175,"Geophone group number of last trace within original field record.");

                dict.Add(177, "Gap size (total number of groups dropped).");

                dict.Add(179, "Over travel associated with taper at beginning or end of line: 1 = down (or behind) 2 = up (or ahead)");

                #endregion

                #region 181-240

                dict.Add(181, "X coordinate of ensemble (CDP) position of this trace (scalar in Trace Header "
                              + "bytes 71-72 applies). The coordinate reference system should be identified "
                              + "through an extended header Location Data stanza (see section D-1).");

                dict.Add(185, "Y coordinate of ensemble (CDP) position of this trace (scalar in Trace Header "
                              + "bytes 71-72 applies). The coordinate reference system should be identified "
                              + "through an extended header Location Data stanza (see section D-1).");

                dict.Add(189, "For 3-D poststack data, this field should be used for the in-line number. If one "
                              + "in-line per SEG Y file is being recorded, this value should be the same for all "
                              + "traces in the file and the same value will be recorded in bytes 3205-3208 of the "
                              + "Binary File Header.");

                dict.Add(193, "For 3-D poststack data, this field should be used for the cross-line number. This "
                              + "will typically be the same value as the ensemble (CDP) number in Trace Header bytes 21-24, "
                              + "but this does not have to be the case.");

                dict.Add(197, "Shotpoint number — This is probably only applicable to 2-D poststack data. "
                              + "Note that it is assumed that the shotpoint number refers to the source location "
                              + "nearest to the ensemble (CDP) location for a particular trace. If this is not the "
                              + "case, there should be a comment in the Textual File Header explaining what the "
                              + "shotpoint number actually refers to.");

                dict.Add(201, "Scalar to be applied to the shotpoint number in Trace Header bytes 197-200 to "
                              + "give the real value. If positive, scalar is used as a multiplier; if negative as a "
                              + "divisor; if zero the shotpoint number is not scaled (i.e. it is an integer. A typical "
                              + "value will be -10, allowing shotpoint numbers with one decimal digit to the right of "
                              + "the decimal point).");
                dict.Add(203, "Trace value measurement unit: "
                              + "-1 = Other (should be described in Data Sample Measurement Units Stanza) "
                              + "0 = Unknown "
                              + "1 = Pascal (Pa) "
                              + "2 = Volts (v) "
                              + "3 = Millivolts (mV) "
                              + "4 = Amperes (A) "
                              + "5 = Meters (m) "
                              + "6 = Meters per second (m/s) "
                              + "7 = Meters per second squared (m/s2) "
                              + "8 = Newton (N) "
                              + "9 = Watt (W) ");

                dict.Add(205, "Transduction Constant mantissa --The multiplicative constant used to convert the Data "
                              + "Trace samples to the Transduction Units (specified in Trace Header bytes 211-212).  "
                              + "The constant is (i.e. Bytes 205-208 * 10**Bytes 209-210). ");

                dict.Add(209, "Transduction Constant exponent--The multiplicative constant used to convert the Data "
                              + "Trace samples to the Transduction Units (specified in Trace Header bytes 211-212).  "
                              + "The constant is (i.e. Bytes 205-208 * 10**Bytes 209-210). ");
                dict.Add(211, "Transduction Units--The unit of measurement of the Data Trace samples after "
                                    + "they have been multiplied by the Transduction Constant specified in Trace "
                                    + "Header bytes 205-210. "
                                    +
                                    "-1 = Other (should be described in Data Sample Measurement Unit stanza, page 36) "
                                    + "0 = Unknown "
                                    + "1 = Pascal (Pa) "
                                    + "2 = Volts (v) "
                                    + "3 = Millivolts (mV) "
                                    + "4 = Amperes (A) "
                                    + "5 = Meters (m) "
                                    + "6 = Meters per second (m/s) "
                                    + "7 = Meters per second squared (m/s2) "
                                    + "8 = Newton (N) "
                                    + "9 = Watt (W) ");

                dict.Add(213, "Device/Trace Identifier — The unit number or id number of the device associated "
                              + "with the Data Trace (i.e. 4368 for vibrator serial number 4368 or 20316 for gun "
                              + "16 on string 3 on vessel 2). This field allows traces to be associated across "
                              + "trace ensembles independently of the trace number (Trace Header bytes 25-28). ");

                dict.Add(215, "Scalar to be applied to times specified in Trace Header bytes 95-114 to give the "
                              + "true time value in milliseconds. Scalar = 1, +10, +100, +1000, or +10,000. If "
                              + "positive, scalar is used as a multiplier; if negative, scalar is used as divisor. A "
                              + "value of zero is assumed to be a scalar value of 1. ");

                dict.Add(217, "Source Type/Orientation — Defines the type and the orientation of the energy "
                              + "source. The terms vertical, cross-line and in-line refer to the three axes of an "
                              + "orthogonal coordinate system. The absolute azimuthal orientation of the "
                              + "coordinate system axes can be defined in the Bin Grid Definition Stanza (page 27). "
                              + "-1 to -n = Other (should be described in Source Type/Orientation stanza,page 38) "
                              + "0 = Unknown "
                              + "1 = Vibratory - Vertical orientation "
                              + "2 = Vibratory - Cross-line orientation "
                              + "3 = Vibratory - In-line orientation "
                              + "4 = Impulsive - Vertical orientation "
                              + "5 = Impulsive - Cross-line orientation "
                              + "6 = Impulsive - In-line orientation "
                              + "7 = Distributed Impulsive - Vertical orientation "
                              + "8 = Distributed Impulsive - Cross-line orientation "
                              + "9 = Distributed Impulsive - In-line orientation ");

                dict.Add(219, "Source Energy Direction with respect to the source orientation — The positive "
                              + "orientation direction is defined in Bytes 217-218 of the Trace Header. The energy direction  "
                              + "is encoded in tenths of degrees (i.e. 347.8º is encoded as 3478). ");

                dict.Add(221, "Source Energy Direction with respect to the source orientation — The positive "
                              +"orientation direction is defined in Bytes 217-218 of the Trace Header. The energy direction  "
                              + "is encoded in tenths of degrees (i.e. 347.8º is encoded as 3478). ");

                dict.Add(225, "Source Measurement — Describes the source effort used to generate the trace. "
                              + "The measurement can be simple, qualitative measurements such as the total weight of  "
                              +"explosive used or the peak air gun pressure or the number of vibrators times the sweep "
                              + "duration. Although these simple measurements are acceptable, it is preferable to use  "
                              + "true measurement units of energy or work.  "
                              +"The constant is encoded as a four-byte, two's complement integer (bytes 225-228) which  "
                              +"is the mantissa and a two-byte, two's complement integer (bytes 209-230) which is the  "
                              + "power of ten exponent (i.e. Bytes 225-228 * 10**Bytes 229-230). ");

                dict.Add(229, "Source Measurement — Describes the source effort used to generate the trace. "
                              + "The measurement can be simple, qualitative measurements such as the total weight of  "
                              +"explosive used or the peak air gun pressure or the number of vibrators times the sweep "
                              + "duration. Although these simple measurements are acceptable, it is preferable to use  "
                              + "true measurement units of energy or work.  "
                              +"The constant is encoded as a four-byte, two's complement integer (bytes 225-228) which  "
                              +"is the mantissa and a two-byte, two's complement integer (bytes 209-230) which is the  "
                              + "power of ten exponent (i.e. Bytes 225-228 * 10**Bytes 229-230). ");

                dict.Add(231, "Source Measurement Unit — The unit used for the Source Measurement, Trace "
                              + "header bytes 225-230. "
                              + "-1 = Other (should be described in Source Measurement Unit stanza, page 39) "
                              + "0 = Unknown "
                              + "1 = Joule (J) "
                              + "2 = Kilowatt (kW) "
                              + "3 = Pascal (Pa) "
                              + "4 = Bar (Bar) "
                              + "4 = Bar-meter (Bar-m) "
                              + "5 = Newton (N) "
                              + "6 = Kilograms (kg) ");

                dict.Add(233, "Reserved");

                #endregion

                return dict;
            }
        }

        /// <summary>
        /// 根据变量名,得到变量值
        /// </summary>
        public object GetValue(string varName)
        {
            Type trType = this.GetType();//获得道头类的Type
            object temp1 = trType.GetProperty(varName).GetValue(this, null); //得到varName变量的值
            return temp1;
        }


        /// <summary>
        /// 按缺省值初始化道头
        /// </summary>
        public static SegyTraceHeader InitializeSegyTraceHeader(uint trcNo, ushort smpInt, ushort smpPerTrc)
        {
            SegyTraceHeader hdr = new SegyTraceHeader();
            hdr.Initialize(trcNo,smpInt,smpPerTrc);
            return hdr;
        }

    }

}
