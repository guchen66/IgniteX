using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugarDB.Models
{
    /// <summary>
    /// 统计状态定义
    /// </summary>
    [SugarTable("Amount", "统计数量表")]
    public class Amount : DataBaseEntity
    {
        /*  [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
          public int Id { get; set; }*/

        [SugarColumn(ColumnDataType = "Nvarchar(50)")]              //工单号
        public string? OrderNumber { get; set; }

        public int? Count { get; set; }                             //下发订单写的总数量

        public int? OkCount { get; set; }                            //合格品总数量

        public int? NgCount { get; set; }                            //Ng品总数量

        public int? InsulNgCount { get; set; }                      //绝缘电阻Ng数量

        public int? BridgeNgCount { get; set; }                     //桥路电阻Ng数量

        public int? ThermalNgCount { get; set; }                    //热瞬态电阻Ng数量

        public int? CCDNgCount { get; set; }                        //CCD电阻Ng数量


        [SugarColumn(IsIgnore = true)]
        public IgniteTubeInfo IgniteTubeInfo { get; set; }
    }
}
