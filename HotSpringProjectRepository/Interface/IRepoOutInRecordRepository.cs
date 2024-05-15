using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    public interface IRepoOutInRecordRepository
    {
        IEnumerable<RepoOutInRecord> GetList();
        int Delete(int id);
        bool Update(RepoOutInRecord repoOutInRecord);
        bool Add(RepoOutInRecord repoOutInRecord);
        RepoOutInRecord GetModel(int id);
        IEnumerable<T> GetListBySql<T>(string sql);
        DbContextTransaction TransBegin();
        void Commit();
        void Rollback();
    }
}
