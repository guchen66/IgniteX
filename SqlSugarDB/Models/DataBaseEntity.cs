using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugarDB.Models
{
    public class DataBaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]//主键自增
        public virtual int Id { get; set; }

        public virtual DateTime CreateTime { get; set; }

        public virtual DateTime UpdateTime { get; set; }
    }
}
