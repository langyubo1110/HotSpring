using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    public interface IRepoOutInRecordRepository
    {
        IQueryable<RepoOutInRecord> GetList();
        int Delete(int id);
        bool Update(RepoOutInRecord repoOutInRecord);
        bool Add(RepoOutInRecord repoOutInRecord);
        RepoOutInRecord GetModel(int id);
    }
}
