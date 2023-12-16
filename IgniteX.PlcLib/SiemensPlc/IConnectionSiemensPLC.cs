

using IgniteX.PlcLib.PlcModel;

namespace IgniteX.PlcLib.SiemensPlc
{
    public interface IConnectionSiemensPLC
    {
        /// <summary>
        /// 设置PLCInfo
        /// </summary>
        /// <param name="plcInfo"></param>
        void SetPlcInfo(PlcInfo plcInfo);

        /// <summary>
        /// 启动plc
        /// </summary>
        /// <returns></returns>
        string StartWork();

        /// <summary>
        /// 停止PLC
        /// </summary>
        /// <returns></returns>
        string StopWork();
    }
}