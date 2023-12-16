using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugarDB.Models
{
    /// <summary>
    /// 设备状态表
    /// </summary>
    [SugarTable("DeviceStatus", "设备状态表")]
    public class DeviceStatus : DataBaseEntity
    {

        [SugarColumn(ColumnDataType = "Nvarchar(50)")]              //工单号
        public string? OrderNumber { get; set; }

        public string StatusName { get; set; } // 状态名称

        public DateTime RunTime { get; set; }                        //运行时间

        public DateTime StopTime { get; set; }                      //停机时间

        public DateTime FaultTime { get; set; }                     //故障时间

        public string Cause { get; set; }                           //故障原因

        public string SensorNumber { get; set; }                     //传感器编号

        [SugarColumn(IsIgnore = true)]
        public IgniteTubeInfo IgniteTubeInfo { get; set; }


    }
}
