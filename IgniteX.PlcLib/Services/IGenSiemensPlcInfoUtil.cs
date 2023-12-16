

using IgniteX.PlcLib.PlcModel;

namespace IgniteX.PlcLib.Services
{
    public interface IGenSiemensPlcInfoUtil
    {
        PlcInfo[] GetSiemensPLCInfo();
    }
}