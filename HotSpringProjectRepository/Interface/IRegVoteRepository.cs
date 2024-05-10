using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    public interface IRegVoteRepository
    {
        IEnumerable<RegVote> GetList();
        int Add(RegVote regVote);
        bool Update(RegVote regVote);
        bool Delete(int id);
        RegVote GetModel(int id);
        RegVote GetModelWithId(int userID, int regId);
        IEnumerable<T> QueryBySql<T>(string sql);
    }
}
