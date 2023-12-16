using IgniteX.PlcLib.SiemensPlc;
using System.Collections.Generic;

namespace IgniteX.PlcLib.Services
{
    public interface ISiemensPlcFactoryService
    {
        /// <summary>
        /// 初始化工厂，创建PLC通讯实列
        /// </summary>
        string InitFactory();

        /// <summary>
        /// 全部开始
        /// </summary>
        /// <param name="connectionSiemensPLC"></param>
        /// <returns></returns>
        void StartPLC();

        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="connectionSiemensPLC"></param>
        /// <returns></returns>
        string StartPLC(ConnectionSiemensPLC connectionSiemensPLC);

        /// <summary>
        /// 结束
        /// </summary>
        /// <param name="connectionSiemensPLC"></param>
        /// <returns></returns>
        string StopPLC(ConnectionSiemensPLC connectionSiemensPLC);

        /// <summary>
        /// 全部结束
        /// </summary>
        /// <param name="connectionSiemensPLC"></param>
        /// <returns></returns>
        void StopPLC();

        /// <summary>
        /// 获取连接PLC
        /// </summary>
        /// <returns></returns>
        List<ConnectionSiemensPLC> GetConnectionSiemensPLCList();
    }
}