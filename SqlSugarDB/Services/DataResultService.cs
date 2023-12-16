using SqlSugar;
using SqlSugarDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugarDB.Services
{
    public class DataResultService
    {
        private readonly SqlSugarClient db;
        public DataResultService()
        {
            db = DatabaseService.GetClient();
        }
        /// <summary>
        /// 查询设备计算数据
        /// </summary>
        /// <returns></returns>
        public List<DataResult> QueryList()
        {
            return db.Queryable<DataResult>().ToList();
        }

        /// <summary>
        /// 查询所有工位名称
        /// </summary>
        /// <returns></returns>
        public List<DataResult> GetAllWorkPlacesNames()
        {
            using (db)
            {
                return db.Queryable<DataResult>().ToList();
            }

        }


        /// <summary>
        /// 按照工位Id查询工位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataResult GetWorkPlaceById(int id)
        {
            using (db)
            {
                return db.Queryable<DataResult>().Where(u => u.Id == id).First();
            }
        }

        /// <summary>
        /// 按照工位名称查询工位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataResult GetWorkPlaceByName(string name)
        {
            using (db)
            {
                return db.Queryable<DataResult>().Where(u => u.ElectronicName== name).First();
            }
        }

        public bool AddWorkPlace(DataResult workPlace)
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

        public void UpdateWorkPlace(DataResult workPlace)
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
