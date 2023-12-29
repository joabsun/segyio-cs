namespace SegyIO.Common
{
    internal class SysConstants
    {
        /// <summary>
        /// 用于内存映射文件访问的MemoryMappedViewAccessor内存空间大小
        /// </summary>
        //public static long MemoryMappedViewAccessorSize = 0x10000000; // 256megabytes
        public static long MemoryMappedViewAccessorSize = 0x4000000; // 64megabytes
    }
}
