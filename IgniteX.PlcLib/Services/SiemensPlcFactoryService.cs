using IgniteX.PlcLib.SiemensPlc;

namespace IgniteX.PlcLib.Services
{
    public class SiemensPlcFactoryService : ISiemensPlcFactoryService
    {
        public SiemensPlcFactoryService(IGenSiemensPlcInfoUtil genSiemensPlcInfoUtil)
        {
            this.genSiemensPlcInfoUtil = genSiemensPlcInfoUtil;
            InitFactory();
        }

        private List<ConnectionSiemensPLC> ListConnectionSiemensPLC = new();
        private readonly IGenSiemensPlcInfoUtil genSiemensPlcInfoUtil;

        /// <summary>
        /// 初始化工厂，创建PLC通讯实列
        /// </summary>
        public string InitFactory()
        {
            if (ListConnectionSiemensPLC.Count > 0) return "工厂已存在";
            //根据动态生成的PLC信息，若返回NULL，表示PLC还没有动态生成
            var plcInfos = genSiemensPlcInfoUtil.GetSiemensPLCInfo();
            //创建实例
            foreach (var plcInfo in plcInfos)
            {
                var connectionSiemensPLC = new ConnectionSiemensPLC();
                connectionSiemensPLC.SetPlcInfo(plcInfo);
                ListConnectionSiemensPLC.Add(connectionSiemensPLC);
            }

            return "成功";
        }

        public void StartPLC()
        {
            foreach (var connection in ListConnectionSiemensPLC)
            {
                connection.StartWork();
            }
        }

        public string StartPLC(ConnectionSiemensPLC connectionSiemensPLC)
        {
            return connectionSiemensPLC.StartWork();
        }

        public string StopPLC(ConnectionSiemensPLC connectionSiemensPLC)
        {
            return connectionSiemensPLC.StopWork();
        }

        public void StopPLC()
        {
            foreach (var connection in ListConnectionSiemensPLC)
            {
                connection.StopWork();
            }
        }

        public List<ConnectionSiemensPLC> GetConnectionSiemensPLCList()
        {
            return ListConnectionSiemensPLC;
        }
    }
}