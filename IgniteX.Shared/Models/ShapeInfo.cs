using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.Shared.Models
{
    /// <summary>
    /// 产品的外形尺寸
    /// </summary>
    public class ShapeInfo
    {
        public double ElectricPin { get; set; }                        //电击针

        public double TotalHeight { get; set; }                         //产品总高度

        //Φ8.13尺寸位置分别检测产品上部、中部、下部的尺寸取最大值
        public double PlasticUpper { get; set; }                       //塑料套上部

        [SugarColumn(ColumnDescription = "塑料套中部")]                
        public double PlasticMiddle { get; set; }                      //塑料套中部

        public double PlasticLower { get; set; }                      //塑料套下部

        //Φ11.1注塑体尺寸、 合模、射胶、保压、冷却、开模、制品取出
        public double AfterMolding { get; set; }                       //注塑体

        //针间距尺寸100%检测      两个针之间的距离
        public double PinBetween { get; set; }                          //针间距
    }
}
