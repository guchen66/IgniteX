using SqlSugar;
using SqlSugarDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugarDB.Services
{
    /// <summary>
    /// 点火管产品服务类
    /// </summary>
    public class IgniteTubeService
    {
        private readonly SqlSugarClient db;
        public IgniteTubeService()
        {
            db = DatabaseService.GetClient();
        }


        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public List<IgniteTubeInfo> QueryList()
        {
            return db.Queryable<IgniteTubeInfo>().ToList();
        }

        /// <summary>
        /// 跟据Id查询
        /// </summary>
        /// <returns></returns>
        public List<IgniteTubeInfo> QueryById(int id)
        {
            return db.Queryable<IgniteTubeInfo>().Where(it => it.Id == id).ToList();
        }

        /// <summary>
        /// 跟据名称查询
        /// </summary>
        /// <returns></returns>
        public List<IgniteTubeInfo> QueryByName(string name)
        {
            return db.Queryable<IgniteTubeInfo>().Where(it => it.IgniteName == name).ToList();
        }

        /// <summary>
        /// 新增点火管
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddIgniteX(IgniteTubeInfo user)
        {
            if (db.Insertable(user).ExecuteCommand() > 0)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="igniteTubeInfo"></param>
        /// <returns></returns>
        public bool Update(IgniteTubeInfo igniteTubeInfo)
        {
            if (db.Updateable(igniteTubeInfo).ExecuteCommand()>0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 跟据Id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteById(int id)
        {        
            if (db.Deleteable<IgniteTubeInfo>().Where(u => u.Id == id).ExecuteCommand() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 异步查询全部
        /// </summary>
        /// <returns></returns>
        public async Task<List<IgniteTubeInfo>> QueryListAsync()
        {
            return await db.Queryable<IgniteTubeInfo>().ToListAsync();
        }
        /// <summary>
        /// 异步跟据Id查询
        /// </summary>
        /// <returns></returns>
        public async Task<List<IgniteTubeInfo>> QueryByIdAsync(int id)
        {
            return await db.Queryable<IgniteTubeInfo>().Where(it=>it.Id==id).ToListAsync();
        }

        /// <summary>
        /// 异步跟据Name查询
        /// </summary>
        /// <returns></returns>
        public async Task<List<IgniteTubeInfo>> QueryByNameAsync(string name)
        {
            return await db.Queryable<IgniteTubeInfo>().Where(it => it.IgniteName==name).ToListAsync();
        }

        /// <summary>
        /// 异步修改
        /// </summary>
        /// <param name="igniteTubeInfo"></param>
        /// <returns></returns>
        public async Task UpdateAsync(IgniteTubeInfo igniteTubeInfo)
        {
             await db.Updateable(igniteTubeInfo).ExecuteCommandAsync();
        }

        /// <summary>
        /// 异步跟据Id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         public async Task DeleteByIdAsync(int id)
         {
            await db.Deleteable<IgniteTubeInfo>().Where(u => u.Id == id).ExecuteCommandAsync();
         }

/*
         public async Task<IgniteXInfo> QueryAsync(int id)
         {
             return await db.GetByIdAsync(id);
         }*/

        /*  public virtual async Task<List<TEntity>> QueryListAsync()
          {
              return await base.GetListAsync();
          }

          public virtual async Task<List<TEntity>> QueryListAsync(Expression<Func<TEntity, bool>> func)
          {
              return await base.GetListAsync(func);
          }

          public virtual async Task<List<TEntity>> QueryListAsync(int page, int size, RefAsync<int> total)
          {
              return await base.Context.Queryable<TEntity>().ToPageListAsync(page, size, total);
          }

          public virtual async Task<List<TEntity>> QueryListAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> total)
          {
              return await base.Context.Queryable<TEntity>().Where(func).ToPageListAsync(page, size, total);
          }

          public new async Task<bool> UpdateAsync(TEntity entity)
          {
              return await base.UpdateAsync(entity);
          }*/
    }
}
