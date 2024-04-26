using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    // 裴晨旭 2024-04-25 
    public class RepoGoodsStockRepository:IRepoGoodsStockRepository
    {
        private readonly HotSpringDbContext _Db;

        public RepoGoodsStockRepository(HotSpringDbContext hotSpringDb) 
        { 
            _Db = hotSpringDb;
        }
        //添加
        public bool Add(RepoGoodsStock repoGoodsStock)
        {
            _Db.Entry(repoGoodsStock).State = System.Data.Entity.EntityState.Added;
            int flag = _Db.SaveChanges();
            if (flag > 0) 
                return true;
            return false;
        }
        //删除
        public int Delete(int id)
        {
            RepoGoodsStock repoGoodsStock = _Db.Repo_Goods_Stock.Find(id);
            _Db.Entry(repoGoodsStock).State = System.Data.Entity.EntityState.Deleted;
            int flag = _Db.SaveChanges();
            return flag;
        }
        //查全表
        public IQueryable<RepoGoodsStock> GetList() {
            IQueryable<RepoGoodsStock> list = _Db.Repo_Goods_Stock;
            return list;
        }
        //更新
        public bool Update(RepoGoodsStock repoGoodsStock)
        {
            _Db.Entry(repoGoodsStock).State = System.Data.Entity.EntityState.Modified;
            int flag = _Db.SaveChanges();
            if (flag > 0)
                return true;
            return false;
        }
        //获得实体
        public RepoGoodsStock GetModel(int id)
        {
            RepoGoodsStock repoGoodsStock = _Db.Repo_Goods_Stock.Find(id);
            return repoGoodsStock;
        }
    }
}
