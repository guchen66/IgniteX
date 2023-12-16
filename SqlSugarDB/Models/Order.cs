using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugarDB.Models
{
    /// <summary>
    /// MES下发工单任务表
    /// </summary>
    [SugarTable("Order", "订单表")]
    public class Order : DataBaseEntity
    {
        /* [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
         public int Id { get; set; }       */

        [SugarColumn(ColumnDataType = "Nvarchar(50)")]              //工单号
        public string? OrderNumber { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)")]              //产品名称
        public string? ProductName { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(32)")]
        public string? ProductBatchNumber { get; set; }             //产品批号

        [SugarColumn(ColumnDataType = "Nvarchar(32)")]
        public string? CodeBatchNumber { get; set; }                //喷码批号

        public int SerialNumber { get; set; }                      //喷码后的唯一序列号

        public byte OrderState { get; set; }                        //工单状态，0未完成，1已完成 2半成品


    }
}
