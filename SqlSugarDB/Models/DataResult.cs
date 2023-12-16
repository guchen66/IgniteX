using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugarDB.Models
{
    [SugarTable("DataResult", "数据计算表")]
    public class DataResult : DataBaseEntity
    {
        /*  [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]//主键自增
          public int Id { get; set; }*/

        [SugarColumn(ColumnDataType = "Nvarchar(50)")]              //工单号
        public string? OrderNumber { get; set; }

        public int ResultId { get; set; }                  //结果集

        [SugarColumn(ColumnDescription = "电子元件")]
        public string ElectronicName { get; set; }

        [SugarColumn(ColumnDescription = "测得值")]
        public string MeasuredValue { get; set; }

        [SugarColumn(ColumnDescription = "平均值")]
        public double AvgResult { get; set; }

        /*  [SugarColumn(ColumnDescription = "上限值")]
          public double UpperRResult { get; set; }

          [SugarColumn(ColumnDescription = "下限值")]
          public double LowerRResult { get; set; }
  */
        [SugarColumn(ColumnDescription = "最大值")]
        public double MaxResult { get; set; }

        [SugarColumn(ColumnDescription = "最小值")]
        public double MixResult { get; set; }

        [SugarColumn(ColumnDescription = "标准值")]
        public double StandardResult { get; set; }

        [SugarColumn(ColumnDescription = "偏差")]
        public double Deviation { get; set; }

        [SugarColumn(ColumnDescription = "过程能力指数")]
        public double CpResult { get; set; }

        [SugarColumn(ColumnDescription = "过程能力K指数")]
        public double CpkResult { get; set; }

        [SugarColumn(IsIgnore = true)]
        public IgniteTubeInfo IgniteTubeInfo { get; set; }
        /* public int ProductId { get; set; }
         [Navigate(NavigateType.OneToOne, nameof(ProductId))]                 
         public IgniteXInfo IgniteXInfo { get; set; }  */                   //一个电阻对应一个产品，一个产品对应多个电阻
    }
}
