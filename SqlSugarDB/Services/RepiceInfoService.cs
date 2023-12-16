using SqlSugar;
using SqlSugarDB.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugarDB.Services
{
    /// <summary>
    /// 配方服务类
    /// </summary>
    public class RepiceInfoService
    {
        private readonly SqlSugarClient db;
        public RepiceInfoService()
        {
            db = DatabaseService.GetClient();
        }
        
        public List<RepiceInfo> QueryList()
        {
            return db.Queryable<RepiceInfo>().ToList();
        }


        /// <summary>
        /// 异步查询全部
        /// </summary>
        /// <returns></returns>
        public async Task<List<RepiceInfo>> QueryListAsync()
        {
            return await db.Queryable<RepiceInfo>().ToListAsync();
        }


        /// <summary>
        /// 按照Id查询配方
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RepiceInfo GetWorkPlaceById(int id)
        {
            return db.Queryable<RepiceInfo>().Where(u => u.Id == id).First();
        }

        /// <summary>
        /// 按照名称查询配方
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RepiceInfo GetWorkPlaceByName(string name)
        {
            return db.Queryable<RepiceInfo>().Where(u => u.RepiceName == name).First();
        }

        public bool AddWorkPlace(RepiceInfo workPlace)
        {
            using (db)
            {
                if (db.Insertable(workPlace).ExecuteCommand() > 0)
                {
                    return true;
                }
                return false;
            }

        }

        public void UpdateWorkPlace(RepiceInfo workPlace)
        {
            using (db)
            {
                db.Updateable(workPlace).ExecuteCommand();
            }

        }

        public void DeleteWorkPlace(int id)
        {
            /* if (IsWorkInProgress)
             {
                 // 现场正在工作，不能删除工位
                 MessageBox.Show("现场正在工作，不能删除工位。");
                 return;
             }
 */
            /*   var result = MessageBox.Show("是否连同对应工序一起删除？", "删除工位", MessageBoxButton.YesNo);
               if (result == MessageBoxResult.Yes)
               {

               }*/

           /* using (db)
            {
                db.Ado.BeginTran(); // 开启事务
                try
                {
                    // 删除工位
                    db.Deleteable<WorkPlace>().Where(u => u.WorkPlaceId == id).ExecuteCommand();

                    // 删除关联的工序
                    db.Deleteable<RepiceInfo>().Where(u => u.WorkPlaceId == id).ExecuteCommand();

                    db.Ado.CommitTran(); // 提交事务
                }
                catch (Exception)
                {
                    db.Ado.RollbackTran();// 回滚事务
                    throw; // 抛出异常
                }
            }*/
        }
    }
}
