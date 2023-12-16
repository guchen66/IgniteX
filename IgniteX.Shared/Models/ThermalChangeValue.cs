using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.Shared.Models
{
    public class ThermalChangeValue
    {
        //检测时间、电点火管编号（关联电点火管表的主键）、初始电阻、最终电阻、差值、M值（变化率M与差值绝对值的比值，单位%
       
        public int TId { get; set; }

        /// <summary>
        /// 初始电阻
        /// </summary>
        public double InitRes { get; set; }

        /// <summary>
        /// 最终电阻
        /// </summary>
        public double FinalRes { get; set; }

        /// <summary>
        /// 差值
        /// </summary>
        public double Difference { get; set; }

        /// <summary>
        /// M值
        /// </summary>
        public double MValue { get; set; }

        /// <summary>
        /// 固定值
        /// </summary>
        public double FixedValue { get; set; }

        /// <summary>
        /// 上限值
        /// </summary>
        public double UpperTResult { get; set; }

        /// <summary>
        /// 下限值
        /// </summary>
        public double LowerTResult { get; set; }
    }
}
