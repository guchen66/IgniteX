using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugarDB.Models
{
    [SugarTable("WorkPlace", "工位表")]
    public class WorkPlace : DataBaseEntity
    {

        public int WorkPlaceId { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)")]//自定义情况Length不要设置
        public string WorkPlaceName { get; set; }

        public bool IsWorkInProgress { get; set; }
        /// <summary>
        ///一对多的使用，一个工位多个工序
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(ProcessModel.WorkPlaceName))]
        public List<ProcessModel> ProcessModelList { get; set; }
    }
}
