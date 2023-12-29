namespace SegyIO.Model
{
    /// <summary>
    /// 封装道头中每个变量的名称,字节位置,值及描述
    /// </summary>
    public struct TraceHeaderItem
    {
        /// <summary>
        /// Variable Name
        /// </summary>
        public string VariableName;

        /// <summary>
        /// 字节位置,格式: 起始字节-结束字节
        /// </summary>
        public string ByteLoc;

        /// <summary>
        /// variable value
        /// </summary>
        public string VariableValue;

        public string Comment;

        /// <summary>
        /// 返回道头变量的起始字节（从1开始）和长度的数组
        /// </summary>
        /// <returns>道头变量的起始字节（从1开始）和长度的数组</returns>
        public int[] GetVarPos()
        {
            int[] pos = new int[2];
            string s = ByteLoc;
            //s = s.Substring(0, 9).Trim();
            string[] s1 = s.Split('-');
            var startByte = Convert.ToInt32(s1[0]);
            var byteLen = Convert.ToInt32(s1[1]) - startByte + 1;
            pos[0]=startByte;
            pos[1]=byteLen;
            return pos;
        }
    }
}
