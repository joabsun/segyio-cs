namespace SegyIO.Model
{
    /// <summary>
    /// SEG-Y��ͷ
    /// </summary>
    [Serializable]
    public sealed class SegyReelHeader : IDisposable
    {
        #region public properties

        /// <summary>
        /// bytes 0-3, Job identification number,��ҵ��ʶ��
        /// </summary>
        public int JobID{ get; set; }

        /// <summary>
        /// bytes 4-7,    * Line Number, ���ߺ�(ÿ���һ����)��
        /// </summary>
        public int LineNumber{ get; set; }

        /// <summary>
        /// bytes 8-11,   * Reel Number, ���
        /// </summary>
        public int ReelNumber { get; set; }

        /// <summary>
        /// bytes 12-13,  * Number of data traces per record //ÿ����¼�ĵ���(����DUMMY ���Ͳ��뵽��¼���߹���ȵ�����¼��)��(ÿ�ڵ������ܵ���)
        /// </summary>
        public ushort TracesPerRecord{ get; set; }

        /// <summary>
        /// bytes 14-15,  * Number of auxillary traces per record (sweep, timing, gain, sync and other nondata traces)
        /// ÿ����¼�ĸ�������(����ɨ�����ʱ�ϡ����桢ͬ�����������зǵ������ݵ�)��
        /// </summary>
        public ushort AuxTracesPerRec{ get; set; }

        /// <summary>
        /// bytes 16-17,  * Sampling interval in microseconds �������(΢��)
        /// ��һ����Ĳ����������΢���ʾ��
        /// </summary>
        public ushort SmpInt{ get; set; }

        /// <summary>
        /// bytes 18-19,    Original sampling interval for field recording ԭʼ��¼�Ĳ������(΢��)
        /// Ұ���¼�Ĳ����������΢���ʾ��
        /// </summary>
        public ushort OrigSampIntervl{ get; set; }

        /// <summary>
        /// bytes 20-21,  * Samples per trace, this reel  ÿ��������
        /// �������ݵ�ÿ�����ݵ�����������
        /// </summary>
        public ushort SmpPerTrc { get; set; }

        /// <summary>
        /// bytes 22-23,    Original Samples per trace, field recording ԭʼ��¼��ÿ��������
        /// Ұ���¼��ÿ�����ݵ�����������
        /// </summary>
        public ushort OrigSampsPerTrc{ get; set; }

        /// <summary>
        /// bytes 24-25,  * 1=4-byte float, 2=4-byte int, 3=2-byte int, 4=four-byte int with gain code
        /// 1 = 4-byte IBM floating-point           2 = 4-byte, two's complement integer
        /// 3 = 2-byte, two's complement integer    4 = 4-byte fixed-point with gain (obsolete)
        /// 5 = 4-byte IEEE floating-point          6 = Not currently used
        /// 7 = Not currently used                  8 = 1-byte, two's complement integer
        /// ���ݲ�����ʽ�룺
        ///     1=����(4�ֽ�)��2=����(4�ֽ�)��
        ///     3=����(2�ֽ�)��4=����W/������(4�ֽ�)
        ///��������ÿ������ʹ����ͬ���ֽ�����
        /// </summary>
        public ushort DataFormatCode{ get; set; }

        /// <summary>
        /// bytes 26-27,  * Expected number of data traces per CDP ensemble
        /// CMP���Ǵ���(ÿ��CMP������ϣ�������ݵ���)��
        /// CRP: ����������
        /// CDP������ȵ����
        /// CMD�������ĵ����
        /// </summary>
        public ushort CMPfold{ get; set; }

        /// <summary>
        /// bytes 28-29,    1=no sorting (as recorded), 2=CDP ensemble 3=single fold continuous profile, 4=horizontally stacked
        /// ����ѡ�룺1=ͬ��¼(û�з�ѡ)��2=CMP������3=���θ������棻4=ˮƽ�������档
        /// </summary>
        public ushort TraceSortCode{ get; set; }

        /// <summary>
        /// bytes 30-31,    Vertical sum code, 1=no sum, 2=two sum, ..., n=n summed
        /// ��ֱ�����룺1=û�е��ӣ�2=���ε��ӣ���N=N����� (N=2~32767)
        /// </summary>
        public ushort VerticalSumCode{ get; set; }

        /// <summary>
        /// bytes 32-33,    Sweep frequency at start
        /// ��ʼɨ��Ƶ�ʡ�
        /// </summary>
        public ushort SweepStartFreq{ get; set; }

        /// <summary>
        /// bytes 34-35,    Sweep frequency at end
        /// ��ֹɨ��Ƶ�ʡ�
        /// </summary>
        public ushort SweepEndFreq{ get; set; }

        /// <summary>
        /// bytes 36-37,    Sweep Length in milliseconds
        /// ɨ�賤�ȡ��� ms ��ʾ��
        /// </summary>
        public ushort SweepLength{ get; set; }

        /// <summary>
        /// bytes 38-39,    1=linear, 2=parabolic, 3=exponential, 4=other
        /// ɨ�������룺1=����ɨ�裻2=������ɨ�裻3=ָ��ɨ�裻4=������
        /// </summary>
        public ushort SweepTypeCode{ get; set; }

        /// <summary>
        /// bytes 40-41,    Trace number of sweep channel
        /// ɨ��ͨ���ĵ��š�
        /// </summary>
        public ushort SweepTraceNum{ get; set; }

        /// <summary>
        /// bytes 42-43,    Sweep trace taper length in milliseconds at start if tapered 
        /// (taper starts at tzero and lasts for this length)
        /// ��б��ʱ��Ϊ��ʼб�³���(б����ʼ��ʱ���㣬ʹ��ʱ��Ϊ�ó���)���� ms ��ʾ��
        /// </summary>
        public ushort SweepTaperLen{ get; set; }

        /// <summary>
        /// bytes 44-45,    Sweep trace taper length in  milliseconds at end (starts at sweep len minus taper length at end)
        /// ����б�³���(����б����ʼ��ɨ�賤�ȼ�����б�³���)���� ms ��ʾ��
        /// </summary>
        public ushort SweepTaperEnd{ get; set; }

        /// <summary>
        /// bytes 46-47,    1=linear, 2=cos*cos, 3=other
        /// б�����ͣ�1=���ԣ�2= cos2 ; 3=����
        /// </summary>
        public ushort TaperType{ get; set; }

        /// <summary>
        /// bytes 48-49,    Correllated data traces: 1=no, 2=yes
        /// ������ݵ���1=û����أ�2=��ء�
        /// </summary>
        public ushort DataTraceCorr{ get; set; }

        /// <summary>
        /// bytes 50-51,    Binary gain recovered: 1=yes, 2=no
        /// ����������ָ���1=�ָ���2=û�лָ�
        /// </summary>
        public ushort BinaryGainRecov{ get; set; }

        /// <summary>
        /// bytes 52-53,    Amplitude recovery method: 1=none, 2=spherical divergence, 3=AGC, 4=other
        /// ����ָ���ʽ��1=û�У�2=������ɢ�� 3=AGC��4=����
        /// </summary>
        public ushort AmplitudeRecov{ get; set; }

        /// <summary>
        /// bytes 54-55,  * Measurement system: 1=meters, 2=feet
        /// ����ϵͳ��1=�ף�2=Ӣ�ߡ�
        /// </summary>
        public ushort Units{ get; set; }

        /// <summary>
        /// bytes 56-57,    1=max return generates negative value, 2=max return generates positive value
        /// �����źż���:   1=ѹ�����ӻ���ʹ�첨�������˶��ڴŴ��ϼǵ��Ǹ�����
        ///                 2=ѹ�����ӻ���ʹ�첨�������˶��ڴŴ��ϼǵ���������
        /// </summary>
        public ushort Polarity { get; set; }

        /// <summary>
        /// bytes 58-59,    Vibratory polarity code.  See spec.
        /// �ɿ���Դ���Դ��� �����ź��ͺ������ź�
        /// 1 =337.5��-22.5��
        /// 2 =22.5��-67.5��
        /// 3 =67.5��-112.5��
        /// 4 =112.5��-157.5��
        /// 5 =157.5��-202.5��
        /// 6 =202.5��-247.5��
        /// 7 =247.5��-292.5��
        /// 8 =292.5��-337.5��
        /// </summary>
        public ushort VibratoryPol{ get; set; }

        /// <summary>
        /// bytes 60-299,Unassigned
        /// </summary>
        public byte[] Unassigned{ get; set; }

        /// <summary>
        /// bytes 300-301 * SEG Y Format Revision Number.
        /// SEG Y Format Revision Number. This is a 16-bit unsigned value with a Qpoint between the first and second bytes. 
        /// Thus for SEG Y Revision 1.0, as defined in this document, this will be recorded as 010016. 
        /// This field is mandatory for all versions of SEG Y, although a value of "zero" indicates
        /// ��traditional�� SEG Y conforming to the 1975 standard.
        /// </summary>
        public ushort RevisionNumber{ get; set; }

        /// <summary>
        /// bytes 302-303 * Fixed length trace flag
        /// Fixed length trace flag. A value of one indicates that all traces in this SEG Y file are guaranteed 
        /// to have the same sample interval and number of samples,as specified in Textual File Header bytes 3217-3218 and 3221-3222. 
        /// A value of zero indicates that the length of the traces in the file may vary and the number of samples in bytes 115-116 of 
        /// the Trace Header must be examined to determine the actual length of each trace. 
        /// This field is mandatory for all versions of SEG Y, although a value of zero indicates 
        /// ��traditional�� SEG Y conforming to the 1975 standard.
        /// </summary>
        public ushort FixLenTraceFlag{ get; set; }

        /// <summary>
        /// bytes 304-305 * Number of 3200-byte, Extended Textual File Header records following the Binary Header.
        /// Number of 3200-byte, Extended Textual File Header records following the Binary Header. A value of zero 
        /// indicates there are no Extended Textual File Header records (i.e. this file has no Extended Textual File Header(s)). 
        /// A value of -1 indicates that there are a variable number of Extended Textual File Header records and 
        /// the end of the Extended Textual File Header is denoted by an ((SEG: EndText)) stanza in the final record. 
        /// A positive value indicates that there are exactly that many Extended Textual File Header records. Note that,
        /// although the exact number of Extended Textual File Header records may be a useful piece of information, 
        /// it will not always be known at the time the Binary Header is written and it is not mandatory that a positive value be recorded
        /// here. This field is mandatory for all versions of SEG Y, although a value of zero indicates ��traditional�� SEG Y conforming to the 1975 standard.
        /// </summary>
        public ushort ExtTextFileHeader { get; set; }

        /// <summary>
        /// bytes 306-399
        /// </summary>
        public byte[] Unassigned1 { get; set; }

        #endregion

        /// <summary>
        /// ��ͷ��ʼ��
        /// </summary>
        public void Initialize()
        {
            JobID = 1;
            LineNumber = 1;
            ReelNumber = 1;
            TracesPerRecord = 1;
            AuxTracesPerRec = 0;
            SmpInt = 1000;
            OrigSampIntervl = 0;
            SmpPerTrc = 0;
            OrigSampsPerTrc = 0;
            DataFormatCode = 1;
            CMPfold = 0;
            TraceSortCode = 0;
            VerticalSumCode = 0;
            SweepStartFreq = 0;
            SweepEndFreq = 0;
            SweepLength = 0;
            SweepTypeCode = 0;
            SweepTraceNum = 0;
            SweepTaperLen = 0;
            SweepTaperEnd = 0;
            TaperType = 0;
            DataTraceCorr = 0;
            BinaryGainRecov = 0;
            AmplitudeRecov = 0;
            Units = 1;
            Polarity = 0;
            VibratoryPol = 0;
            Unassigned = new byte[240];
            RevisionNumber = 0;
            FixLenTraceFlag = 1;
            ExtTextFileHeader = 0;
            Unassigned1 = new byte[94];
        }

        /// <summary>
        /// ��ͷ��¡
        /// </summary>
        /// <returns></returns>
        public SegyReelHeader Clone()
        {
            SegyReelHeader header = new SegyReelHeader
                {
                    JobID = JobID,
                    LineNumber = LineNumber,
                    ReelNumber = ReelNumber,
                    TracesPerRecord = TracesPerRecord,
                    AuxTracesPerRec = AuxTracesPerRec,
                    SmpInt = SmpInt,
                    OrigSampIntervl = OrigSampIntervl,
                    SmpPerTrc = SmpPerTrc,
                    OrigSampsPerTrc = OrigSampsPerTrc,
                    DataFormatCode = DataFormatCode,
                    CMPfold = CMPfold,
                    TraceSortCode = TraceSortCode,
                    VerticalSumCode = VerticalSumCode,
                    SweepStartFreq = SweepStartFreq,
                    SweepEndFreq = SweepEndFreq,
                    SweepLength = SweepLength,
                    SweepTypeCode = SweepTypeCode,
                    SweepTraceNum = SweepTraceNum,
                    SweepTaperLen = SweepTaperLen,
                    SweepTaperEnd = SweepTaperEnd,
                    TaperType = TaperType,
                    DataTraceCorr = DataTraceCorr,
                    BinaryGainRecov = BinaryGainRecov,
                    AmplitudeRecov = AmplitudeRecov,
                    Units = Units,
                    Polarity = Polarity,
                    VibratoryPol = VibratoryPol,
                    Unassigned = Unassigned,
                    RevisionNumber = RevisionNumber,
                    FixLenTraceFlag = FixLenTraceFlag,
                    ExtTextFileHeader = ExtTextFileHeader,
                    Unassigned1 = Unassigned1
                };

            return header;
        }

        /// <summary>
        /// ����ͷ����ת��Ϊ��ͷ��������������б�
        /// </summary>
        public static IList<ReelHeaderItem> ToReelHeaderItems(SegyReelHeader rh)
        {
            IList<ReelHeaderItem> listRhItems = new List<ReelHeaderItem>
            {
                new ReelHeaderItem
                {
                    VariableName = "*JobID",
                    ByteLoc = "1-4",
                    VariableValue = rh.JobID.ToString(),
                    Comment = "��ҵ��ʶ��"
                },
                new ReelHeaderItem
                {
                    VariableName = "*LineNumber",
                    ByteLoc = "5-8",
                    VariableValue = rh.LineNumber.ToString(),
                    Comment = "���ߺ�(ÿ���һ����)"
                },
                new ReelHeaderItem
                {
                    VariableName = "*ReelNumber",
                    ByteLoc = "9-12",
                    VariableValue = rh.ReelNumber.ToString(),
                    Comment = "���"
                },
                new ReelHeaderItem
                {
                    VariableName = "*TracesPerRecord",
                    ByteLoc = "13-14",
                    VariableValue = rh.TracesPerRecord.ToString(),
                    Comment = "ÿ����¼�ĵ���(����DUMMY ���Ͳ��뵽��¼���߹���ȵ�����¼��)��(ÿ�ڵ������ܵ���)"
                },
                new ReelHeaderItem
                {
                    VariableName = "AuxTracesPerRec",
                    ByteLoc = "15-16",
                    VariableValue = rh.AuxTracesPerRec.ToString(),
                    Comment = "ÿ����¼�ĸ�������(����ɨ�����ʱ�ϡ����桢ͬ�����������зǵ������ݵ�)"
                },
                new ReelHeaderItem
                {
                    VariableName = "*SmplIntvl",
                    ByteLoc = "17-18",
                    VariableValue = rh.SmpInt.ToString(),
                    Comment = "��һ����Ĳ����������΢���ʾ"
                },
                new ReelHeaderItem
                {
                    VariableName = "OrigSampIntervl",
                    ByteLoc = "19-20",
                    VariableValue = rh.OrigSampIntervl.ToString(),
                    Comment = "Ұ���¼�Ĳ����������΢���ʾ"
                },
                new ReelHeaderItem
                {
                    VariableName = "SmpPerTrc",
                    ByteLoc = "21-22",
                    VariableValue = rh.SmpPerTrc.ToString(),
                    Comment = "�������ݵ�ÿ�����ݵ���������"
                },
                new ReelHeaderItem
                {
                    VariableName = "OrigSampsPerTrc",
                    ByteLoc = "23-24",
                    VariableValue = rh.OrigSampsPerTrc.ToString(),
                    Comment = "Ұ���¼��ÿ�����ݵ���������"
                },
                new ReelHeaderItem
                {
                    VariableName = "*DataFormatCode",
                    ByteLoc = "25-26",
                    VariableValue = rh.DataFormatCode.ToString(),
                    Comment = "���ݲ�����ʽ�룺1 = 4�ֽ�IBM���� 2 = 4-byte, two's complement integer "
                              + "3 = 2-byte, two's complement integer    4 = 4-byte fixed-point with gain (obsolete) 5 = 4�ֽ�IEEE���� "
                              + "6 = Not currently used 7 = Not currently used 8 = 1-byte, two's complement integer"
                },
                new ReelHeaderItem
                {
                    VariableName = "*CMPfold",
                    ByteLoc = "27-28",
                    VariableValue = rh.CMPfold.ToString(),
                    Comment = "CMP���Ǵ���(ÿ��CMP������ϣ�������ݵ���)�� CRP: ���������� CDP������ȵ���� CMD�������ĵ����"
                },
                new ReelHeaderItem
                {
                    VariableName = "TraceSortCode",
                    ByteLoc = "29-30",
                    VariableValue = rh.TraceSortCode.ToString(),
                    Comment = "����ѡ�룺1=ͬ��¼(û�з�ѡ)��2=CMP������3=���θ������棻4=ˮƽ��������"
                },
                new ReelHeaderItem
                {
                    VariableName = "VerticalSumCode",
                    ByteLoc = "31-32",
                    VariableValue = rh.VerticalSumCode.ToString(),
                    Comment = "��ֱ�����룺1=û�е��ӣ�2=���ε��ӣ���N=N����� (N=2~32767)"
                },
                new ReelHeaderItem
                {
                    VariableName = "SweepStartFreq",
                    ByteLoc = "33-34",
                    VariableValue = rh.SweepStartFreq.ToString(),
                    Comment = "��ʼɨ��Ƶ��"
                },
                new ReelHeaderItem
                {
                    VariableName = "SweepEndFreq",
                    ByteLoc = "35-36",
                    VariableValue = rh.SweepEndFreq.ToString(),
                    Comment = "��ֹɨ��Ƶ��"
                },
                new ReelHeaderItem
                {
                    VariableName = "SweepLength",
                    ByteLoc = "37-38",
                    VariableValue = rh.SweepLength.ToString(),
                    Comment = "ɨ�賤�ȡ��� ms ��ʾ"
                },
                new ReelHeaderItem
                {
                    VariableName = "SweepTypeCode",
                    ByteLoc = "39-40",
                    VariableValue = rh.SweepTypeCode.ToString(),
                    Comment = "ɨ�������룺1=����ɨ�裻2=������ɨ�裻3=ָ��ɨ�裻4=����"
                },
                new ReelHeaderItem
                {
                    VariableName = "SweepTraceNum",
                    ByteLoc = "41-42",
                    VariableValue = rh.SweepTraceNum.ToString(),
                    Comment = "ɨ��ͨ���ĵ���"
                },
                new ReelHeaderItem
                {
                    VariableName = "SweepTaperLen",
                    ByteLoc = "43-44",
                    VariableValue = rh.SweepTaperLen.ToString(),
                    Comment = "��б��ʱ��Ϊ��ʼб�³���(б����ʼ��ʱ���㣬ʹ��ʱ��Ϊ�ó���)���� ms ��ʾ"
                },
                new ReelHeaderItem
                {
                    VariableName = "SweepTaperEnd",
                    ByteLoc = "45-46",
                    VariableValue = rh.SweepTaperEnd.ToString(),
                    Comment = "����б�³���(����б����ʼ��ɨ�賤�ȼ�����б�³���)���� ms ��ʾ"
                },
                new ReelHeaderItem
                {
                    VariableName = "TaperType",
                    ByteLoc = "47-48",
                    VariableValue = rh.TaperType.ToString(),
                    Comment = "б�����ͣ�1=���ԣ�2= cos2 ; 3=����"
                },
                new ReelHeaderItem
                {
                    VariableName = "DataTraceCorr",
                    ByteLoc = "49-50",
                    VariableValue = rh.DataTraceCorr.ToString(),
                    Comment = "������ݵ���1=û����أ�2=���"
                },
                new ReelHeaderItem
                {
                    VariableName = "BinaryGainRecov",
                    ByteLoc = "51-52",
                    VariableValue = rh.BinaryGainRecov.ToString(),
                    Comment = "����������ָ���1=�ָ���2=û�лָ�"
                },
                new ReelHeaderItem
                {
                    VariableName = "AmplitudeRecov",
                    ByteLoc = "53-54",
                    VariableValue = rh.AmplitudeRecov.ToString(),
                    Comment = "����ָ���ʽ��1=û�У�2=������ɢ�� 3=AGC��4=����"
                },
                new ReelHeaderItem
                {
                    VariableName = "*Units",
                    ByteLoc = "55-56",
                    VariableValue = rh.Units.ToString(),
                    Comment = "������λ��1=�ף�2=Ӣ��"
                },
                new ReelHeaderItem
                {
                    VariableName = "Polarity",
                    ByteLoc = "57-58",
                    VariableValue = rh.Polarity.ToString(),
                    Comment = "�����źż���: 1=ѹ�����ӻ���ʹ�첨�������˶��ڴŴ��ϼǵ��Ǹ�����2=ѹ�����ӻ���ʹ�첨�������˶��ڴŴ��ϼǵ���������"
                },
                new ReelHeaderItem
                {
                    VariableName = "VibratoryPol",
                    ByteLoc = "59-60",
                    VariableValue = rh.VibratoryPol.ToString(),
                    Comment =
                        "�ɿ���Դ���Դ��� �����ź��ͺ������ź� 1 =337.5��-22.5�� 2 =22.5��-67.5��3 =67.5��-112.5��4 =112.5��-157.5��5 =157.5��-202.5�� 6 =202.5��-247.5�� 7 =247.5��-292.5��8 =292.5��-337.5��"
                },
                new ReelHeaderItem
                {
                    VariableName = "Unassigned",
                    ByteLoc = "61-300",
                    VariableValue = rh.Unassigned.ToString(),
                    Comment = "Unassigned"
                },
                new ReelHeaderItem
                {
                    VariableName = "*RevisionNumber",
                    ByteLoc = "301-302",
                    VariableValue = rh.RevisionNumber.ToString(),
                    Comment =
                        "SEG Y Format Revision Number. This is a 16-bit unsigned value with a Qpoint between the first and second bytes." +
                        "Thus for SEG Y Revision 1.0, as defined in this document, this will be recorded as 010016. " +
                        "This field is mandatory for all versions of SEG Y, although a value of 'zero' indicates " +
                        "��traditional�� SEG Y conforming to the 1975 standard"
                },
                new ReelHeaderItem
                {
                    VariableName = "*FixLenTraceFlag",
                    ByteLoc = "303-304",
                    VariableValue = rh.FixLenTraceFlag.ToString(),
                    Comment =
                        "Fixed length trace flag. A value of one indicates that all traces in this SEG Y file are guaranteed " +
                        "to have the same sample interval and number of samples,as specified in Textual File Header bytes 3217-3218 and 3221-3222.  " +
                        "A value of zero indicates that the length of the traces in the file may vary and the number of samples in bytes 115-116 of " +
                        "the Trace Header must be examined to determine the actual length of each trace.This field is mandatory for all versions of " +
                        "SEG Y, although a value of zero indicates ��traditional�� SEG Y conforming to the 1975 standard."
                },
                new ReelHeaderItem
                {
                    VariableName = "*ExtTextFileHeader",
                    ByteLoc = "305-306",
                    VariableValue = rh.ExtTextFileHeader.ToString(),
                    Comment =
                        "Number of 3200-byte, Extended Textual File Header records following the Binary Header. A value of zero " +
                        "indicates there are no Extended Textual File Header records (i.e. this file has no Extended Textual File Header(s)). " +
                        " A value of -1 indicates that there are a variable number of Extended Textual File Header records and  " +
                        "the end of the Extended Textual File Header is denoted by an ((SEG: EndText)) stanza in the final record." +
                        "A positive value indicates that there are exactly that many Extended Textual File Header records. Note that," +
                        "although the exact number of Extended Textual File Header records may be a useful piece of information, " +
                        "it will not always be known at the time the Binary Header is written and it is not mandatory that a positive value be recorded " +
                        "here. This field is mandatory for all versions of SEG Y, although a value of zero indicates ��traditional�� SEG Y conforming to the 1975 standard."
                },
                new ReelHeaderItem
                {
                    VariableName = "Unassigned1",
                    ByteLoc = "306-400",
                    VariableValue = rh.Unassigned1.ToString(),
                    Comment = "Unassigned1"
                }
            };


            return listRhItems;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Unassigned = null;
            Unassigned1 = null;
        }

        /// <summary>
        /// ��ʼ����ͷ
        /// </summary>
        /// <param name="smpIntvl"></param>
        /// <param name="smpPerTrc"></param>
        /// <param name="dataFormat"></param>
        /// <returns></returns>
        public static SegyReelHeader InitialReelHeader(ushort smpIntvl, ushort smpPerTrc, ushort dataFormat)
        {
            SegyReelHeader reel = new SegyReelHeader();
            reel.Initialize();
            reel.SmpInt = smpIntvl;
            reel.OrigSampIntervl = smpIntvl;
            reel.SmpPerTrc = smpPerTrc;
            reel.OrigSampsPerTrc = smpPerTrc;
            reel.DataFormatCode = dataFormat;

            return reel;
        }

    }
    /// <summary>
    /// ��װ��ͷ��ÿ������������,�ֽ�λ��,ֵ������
    /// </summary>
    public struct ReelHeaderItem
    {
        /// <summary>
        /// ������
        /// </summary>
        public string VariableName;
        /// <summary>
        /// ����λ��
        /// </summary>
        public string ByteLoc;
        /// <summary>
        /// ����ֵ
        /// </summary>
        public string VariableValue;
        /// <summary>
        /// ����ע��
        /// </summary>
        public string Comment;
    }

}
