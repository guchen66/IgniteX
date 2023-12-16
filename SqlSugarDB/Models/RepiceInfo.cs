using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugarDB.Models
{
    [SugarTable("RepiceInfo", "配方信息表")]
    public class RepiceInfo : DataBaseEntity
    {
        /*  [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]//主键自增
          public int Id { get; set; }
  */
        [SugarColumn(ColumnDataType = "Nvarchar(50)")]//自定义情况Length不要设置
        public string? RepiceName { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(32)")]
        public string? ProductColor { get; set; }                   //点火管颜色

        [SugarColumn(ColumnDescription = "绝缘电阻下限值")]
        public double? InsulResistiveLow { get; set; }              //绝缘电阻下限值

        [SugarColumn(ColumnDescription = "绝缘电阻上限值")]
        public double? InsulResistiveUp { get; set; }              //绝缘电阻上限值

        [SugarColumn(ColumnDescription = "桥路电阻下限值")]
        public double? BridgeResistiveLow { get; set; }              //桥式电阻下限值

        [SugarColumn(ColumnDescription = "桥路电阻上限值")]
        public double? BridgeResistiveUp { get; set; }              //桥式电阻上限值

        [SugarColumn(ColumnDescription = "热瞬态下限值")]
        public double? ThermalStateLow { get; set; }                //热瞬态下限值

        [SugarColumn(ColumnDescription = "热瞬态上限值")]
        public double? ThermalStateUp { get; set; }                //热瞬态上限值

        [SugarColumn(ColumnDescription = "CCD大圆下限值")]
        public double? CCDBigLow { get; set; }                    //CCD大圆下限值

        [SugarColumn(ColumnDescription = "CCD大圆上限值")]
        public double? CCDBigUp { get; set; }                     //CCD大圆上限值

        [SugarColumn(ColumnDescription = "CCD小圆下限值")]
        public double? CCDSmallLow { get; set; }                  //CCD小圆下限值

        [SugarColumn(ColumnDescription = "CCD小圆上限值")]
        public double? CCDSmallUp { get; set; }                   //CCD小圆上限值

        [SugarColumn(ColumnDescription = "CCD总高下限值")]
        public double? CCDHeightLow { get; set; }                    //CCD总高下限值

        [SugarColumn(ColumnDescription = "CCD总高上限值")]
        public double? CCDHeightUp { get; set; }                     //CCD总高上限值

        [SugarColumn(ColumnDescription = "CCDPin高下限值")]
        public double? CCDPinLow { get; set; }                  //CCDPin高下限值

        [SugarColumn(ColumnDescription = "CCDPin高上限值")]
        public double? CCDPinUp { get; set; }                   //CCDPin高上限值

        public byte IsMaxDosage { get; set; }                      //是否最大药量




    }
}
