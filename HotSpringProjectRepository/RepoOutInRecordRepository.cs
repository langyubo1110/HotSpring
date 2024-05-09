using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class RepoOutInRecordRepository:IRepoOutInRecordRepository
    {
        private readonly HotSpringDbContext _Db;
        private DbContextTransaction _DbTrans;
        public RepoOutInRecordRepository(HotSpringDbContext hotSpringDb)
        { 
            _Db= hotSpringDb;
        }

        //添加
        public bool Add(RepoOutInRecord repoOutInRecord)
        {
            _Db.Entry(repoOutInRecord).State = System.Data.Entity.EntityState.Added;
            int flag = _Db.SaveChanges();
            return flag>0?true:false;
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
            return flag > 0?true:false;
        }
        //获得实体
        public RepoOutInRecord GetModel(int id)
        {
            RepoOutInRecord repoOutInRecord = _Db.RepoOutInRecord.Find(id);
            return repoOutInRecord;
        }
        //三表联合 库存表、出入库表、采购表
        public IEnumerable<T> GetListBySql<T>(string sql)
        {
            return _Db.Database.SqlQuery<T>(sql);
        }
        //返回一个事务
        public DbContextTransaction TransBegin() 
        {
            _DbTrans = _Db.Database.BeginTransaction();
            return _DbTrans; 
        }
        //事务提交
        public void Commit() 
        {
            _DbTrans.Commit();
        }
        //事务回滚
        public void Rollback()
        {
            _DbTrans.Rollback();
        }
    }
}
