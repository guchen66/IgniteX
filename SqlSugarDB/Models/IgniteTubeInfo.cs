using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugarDB.Models
{
    [SugarTable("IgniteTubeInfo", "产品信息表")]//, IsDisabledUpdateAll = true安全级别高，只创建，不修改和删除
    public class IgniteTubeInfo : DataBaseEntity
    {
        /*  [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]//主键自增
          public int Id { get; set; }*/

        [SugarColumn(ColumnDataType = "Nvarchar(50)")]
        public string? OrderNumber { get; set; }                    //工单号

        [SugarColumn(ColumnDataType = "Nvarchar(50)")]
        public string? IgniteName { get; set; }                     //点火管名称   

        [SugarColumn(ColumnDescription = "绝缘电阻")]
        public double InsulResistive { get; set; }                //绝缘电阻

        [SugarColumn(ColumnDescription = "桥路电阻")]
        public double BridgeResistive { get; set; }                //桥路电阻

        [SugarColumn(ColumnDescription = "初始热瞬态")]
        public double? ThermalInitState { get; set; }                //初始热瞬态

        [SugarColumn(ColumnDescription = "热瞬态")]
        public double? ThermalState { get; set; }                    //热瞬态

        [SugarColumn(ColumnDescription = "最终热瞬态")]
        public double? ThermalFinalState { get; set; }                   //最终热瞬态

        [SugarColumn(ColumnDescription = "电阻变化率")]
        public double? ResChange { get; set; }                     //电阻变化率

        [SugarColumn(ColumnDescription = "绝缘电阻结果集")]
        public int? InsulResistiveResult { get; set; }              //绝缘结果集     

        [SugarColumn(ColumnDescription = "桥式电阻结果集")]
        public int? BridgeResistiveResult { get; set; }              //桥式电阻结果集      

        [SugarColumn(ColumnDescription = "初始热瞬态结果集")]
        public int? ThermalInitStateResult { get; set; }              //初始热瞬态结果集

        [SugarColumn(ColumnDescription = "热瞬态结果集")]
        public int? ThermalStateResult { get; set; }                //热瞬态结果集       

        [SugarColumn(ColumnDescription = "最终热瞬态结果集")]
        public int? ThermalFinalStateResult { get; set; }                //最终热瞬态结果集

        [SugarColumn(ColumnDescription = "电阻变化率结果")]
        public int? ResChangeResult { get; set; }                 //电阻变化率结果

        [SugarColumn(ColumnDescription = "CCD缺陷")]
        public double? CCDDefect { get; set; }                    //CCD缺陷

        [SugarColumn(ColumnDescription = "CCD缺陷结果")]
        public int? CCDDefectResult { get; set; }              //CCD缺陷结果

        [SugarColumn(ColumnDescription = "CCD大圆")]
        public double? CCDBig { get; set; }                     //CCD大圆

        [SugarColumn(ColumnDescription = "CCD大圆结果")]
        public int? CCDBigResult { get; set; }                     //CCD大圆结果

        [SugarColumn(ColumnDescription = "CCD小圆")]
        public double? CCDSmall { get; set; }                  //CCD小圆

        [SugarColumn(ColumnDescription = "CCD小圆结果")]
        public int? CCDSmallResult { get; set; }                   //CCD小圆结果

        [SugarColumn(ColumnDescription = "CCD总高")]
        public double? CCDHeight { get; set; }                        //CCD总高

        [SugarColumn(ColumnDescription = "CCD总高结果")]
        public int? CCDHeightResult { get; set; }                     //CCD总高结果

        [SugarColumn(ColumnDescription = "CCDPin高")]
        public double? CCDPin { get; set; }                          //CCDPin高

        [SugarColumn(ColumnDescription = "CCDPin高结果")]
        public int? CCDPinResult { get; set; }                   //CCDPin高结果

        [SugarColumn(IsIgnore = true)]
        public DeviceStatus DeviceStatus { get; set; }

        [SugarColumn(IsIgnore = true)]
        public Amount Amounts { get; set; }

        [SugarColumn(IsIgnore = true)]
        public DataResult DataResults { get; set; }



        /*[SugarColumn(ColumnDataType = "Nvarchar(50)")]
        public string? Polarity { get; set; }                       //极性识别*/
    }



}
