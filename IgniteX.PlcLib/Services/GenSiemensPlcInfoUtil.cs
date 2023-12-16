

using IgniteX.PlcLib.PlcModel;

namespace IgniteX.PlcLib.Services
{
    public class GenSiemensPlcInfoUtil : IGenSiemensPlcInfoUtil
    {
        public PlcInfo[] GetSiemensPLCInfo()
        {
            return new PlcInfo[]{
                new PlcInfo()
                {
                  OP = "OP10",
                  IP = "192.168.0.10",
                  Port = 102,
                  Rack = 0,
                  Slot = 0,
                  Version = "S1500",
                  PI = new PublicInfo()
                    {
                    ReadAddr = "DB5001.0",
                    ReadClassName = "General_PI_PlcToEap",
                    WriteAddr = "DB5000.0",                                //写入是往PLC写
                    WriteClassName = "General_PI_EapToPlc",
                    },
                    EIs = new List<EventInfo>
                    {
                        new EventInfo()
                        {
                            Idx = 0,
                            ReadAddr = "DB5001.200",
                            ReadClassName = "Base_PlcToEap_IN",
                            WriteAddr = "DB5000.200",
                            WriteClassName = "Base_EapToPlc_IN",
                        },
                        new EventInfo()
                        {
                            Idx = 1,
                            ReadAddr = "DB5001.204",
                            ReadClassName = "Base_PlcToEap_OUT",
                            WriteAddr = "DB5000.310",
                            WriteClassName = "Base_EapToPlc_OUT",
                        },
                        new EventInfo()
                        {
                            Idx = 2,
                            ReadAddr = "DB5001.250",
                            ReadClassName = "Base_PlcToEap_IN",
                            WriteAddr = "DB5000.316",
                            WriteClassName = "Base_EapToPlc_IN",
                        },
                        new EventInfo()
                        {
                            Idx = 3,
                            ReadAddr = "DB5001.296",
                            ReadClassName = "Base_PlcToEap_OUT",
                            WriteAddr = "DB5000.322",
                            WriteClassName = "Base_EapToPlc_OUT",
                        },
                        new EventInfo()
                        {
                            Idx = 4,
                            ReadAddr = "DB5001.342",
                            ReadClassName = "Base_PlcToEap_OUT",
                            WriteAddr = "DB5000.328",
                            WriteClassName = "Base_EapToPlc_OUT",
                        },
                        new EventInfo()
                        {
                            Idx = 5,
                            ReadAddr = "DB5001.380",
                            ReadClassName = "Base_PlcToEap_OUT",
                            WriteAddr = "DB5000.336",
                            WriteClassName = "Base_EapToPlc_OUT",
                        },
                        new EventInfo()
                        {
                            Idx = 6,
                            ReadAddr = "DB5001.426",
                            ReadClassName = "Base_PlcToEap_OUT",
                            WriteAddr = "DB5000.342",
                            WriteClassName = "Base_EapToPlc_OUT",
                        },
                        new EventInfo()
                        {
                            Idx = 7,
                            ReadAddr = "DB5001.524",
                            ReadClassName = "Base_PlcToEap_OUT",
                            WriteAddr = "DB5000.348",
                            WriteClassName = "Base_EapToPlc_OUT",
                        },
                        new EventInfo()
                        {
                            Idx = 8,
                            ReadAddr = "DB5001.560",
                            ReadClassName = "Base_PlcToEap_OUT",
                            WriteAddr = "DB5000.386",
                            WriteClassName = "Base_EapToPlc_OUT",
                        },
                        new EventInfo()
                        {
                            Idx = 9,
                            ReadAddr = "DB5001.606",
                            ReadClassName = "Base_PlcToEap_OUT",
                            WriteAddr = "DB5000.392",
                            WriteClassName = "Base_EapToPlc_OUT",
                        },
                        new EventInfo()
                        {
                            Idx = 10,
                            ReadAddr = "DB5001.652",
                            ReadClassName = "Base_PlcToEap_OUT",
                            WriteAddr = "DB5000.398",
                            WriteClassName = "Base_EapToPlc_OUT",
                        },
                        new EventInfo()
                        {
                            Idx = 11,
                            ReadAddr = "DB5001.688",
                            ReadClassName = "Base_PlcToEap_OUT",
                            WriteAddr = "DB5000.406",
                            WriteClassName = "Base_EapToPlc_OUT",
                        },
                        new EventInfo()
                        {
                            Idx = 12,
                            ReadAddr = "DB5001.692",
                            ReadClassName = "Base_PlcToEap_OUT",
                            WriteAddr = "DB5000.618",
                            WriteClassName = "Base_EapToPlc_OUT",
                        },
                        new EventInfo()
                        {
                            Idx = 13,
                            ReadAddr = "DB5001.904",
                            ReadClassName = "Base_PlcToEap_OUT",
                            WriteAddr = "DB5000.624",
                            WriteClassName = "Base_EapToPlc_OUT",
                        },
                    }
                  },
              /*  new PlcInfo()
                    {
                    OP = "OP20",
                    IP = "192.168.0.30",
                    Port = 102,
                    Rack = 0,
                    Slot = 0,
                    Version = "S1500",
                    PI = new PublicInfo()
                        {
                        ReadAddr = "DB5001.0",
                        ReadClassName = "General_PI_PlcToEap",
                        WriteAddr = "DB5000.0",
                        WriteClassName = "General_PI_EapToPlc",
                        },
                    EIs = new List<EventInfo>
                    {
                        new EventInfo()
                        {
                            Idx = 0,
                            ReadAddr = "DB5001.200",
                            ReadClassName = "Base_PlcToEap_IN",
                            WriteAddr = "DB5000.200",
                            WriteClassName = "Base_EapToPlc_IN",
                        },
                        new EventInfo()
                        {
                            Idx = 1,
                            ReadAddr = "DB5001.202",
                            ReadClassName = "Base_PlcToEap_OUT",
                            WriteAddr = "DB5000.412",
                            WriteClassName = "Base_EapToPlc_OUT",
                        },
                        new EventInfo()
                        {
                            Idx = 2,
                            ReadAddr = "DB5001.210",
                            ReadClassName = "Base_PlcToEap_IN",
                            WriteAddr = "DB5000.522",
                            WriteClassName = "Base_EapToPlc_IN",
                        },
                        new EventInfo()
                        {
                            Idx = 3,
                            ReadAddr = "DB5001.212",
                            ReadClassName = "OP2002_PlcToEap_OUT",
                            WriteAddr = "DB5000.734",
                            WriteClassName = "Base_EapToPlc_OUT",
                        },
                        new EventInfo()
                        {
                            Idx = 4,
                            ReadAddr = "DB5001.328",
                            ReadClassName = "Base_PlcToEap_ScrewGun",
                            WriteAddr = "DB5000.844",
                            WriteClassName = "Base_EapToPlc_ScrewGun",
                        },
                        new EventInfo()
                        {
                            Idx = 5,
                            ReadAddr = "DB5001.376",
                            ReadClassName = "Base_PlcToEap_ScrewGun",
                            WriteAddr = "DB5000.922",
                            WriteClassName = "Base_EapToPlc_ScrewGun",
                        },
                        new EventInfo()
                        {
                            Idx = 6,
                            ReadAddr = "DB5001.424",
                            ReadClassName = "Base_PlcToEap_Pcb",
                            WriteAddr = "DB5000.1000",
                            WriteClassName = "Base_EapToPlc_Pcb",
                        },
                    }
                }*/
            };
        }

    }
}