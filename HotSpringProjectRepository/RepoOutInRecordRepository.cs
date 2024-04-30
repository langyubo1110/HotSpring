using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class RepoOutInRecordRepository:IRepoOutInRecordRepository
    {
        private readonly HotSpringDbContext _Db;

        public RepoOutInRecordRepository(HotSpringDbContext hotSpringDb)
        { 
            _Db= hotSpringDb;
        }

        //添加
        public bool Add(RepoOutInRecord repoOutInRecord)
        {
            _Db.Entry(repoOutInRecord).State = System.Data.Entity.EntityState.Added;
            int flag = _Db.SaveChanges();
            if (flag > 0)
                return true;
            return false;
        }
        //删除
        public int Delete(int id)
        {
            RepoOutInRecord repoOutInRecord = _Db.RepoOutInRecord.Find(id);
            _Db.Entry(repoOutInRecord).State = System.Data.Entity.EntityState.Deleted;
            int flag = _Db.SaveChanges();
            return flag;
        }
        //查全表
        public IQueryable<RepoOutInRecord> GetList()
        {
            IQueryable<RepoOutInRecord> list = _Db.RepoOutInRecord;
            return list;
        }
        //更新
        public bool Update(RepoOutInRecord repoOutInRecord)
        {
            _Db.Entry(repoOutInRecord).State = System.Data.Entity.EntityState.Modified;
            int flag = _Db.SaveChanges();
            if (flag > 0)
                return true;
            return false;
        }
        //获得实体
        public RepoOutInRecord GetModel(int id)
        {
            RepoOutInRecord repoOutInRecord = _Db.RepoOutInRecord.Find(id);
            return repoOutInRecord;
        }
    }
}
