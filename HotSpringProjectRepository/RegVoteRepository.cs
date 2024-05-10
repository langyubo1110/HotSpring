using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class RegVoteRepository : IRegVoteRepository
    {
        private readonly HotSpringDbContext _db;

        public RegVoteRepository(HotSpringDbContext hotSpringDbContext)
        {
            _db = hotSpringDbContext;
        }
        public int Add(RegVote regVote)
        {
            _db.Entry(regVote).State = System.Data.Entity.EntityState.Added;
            int flag = _db.SaveChanges();
            return flag;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RegVote> GetList()
        {
            return _db.RegVote;
        }

        public RegVote GetModel(int id)
        {
            RegVote regVote = _db.RegVote.Find(id);
            return regVote;
        }
        public RegVote GetModelWithId(int userID,int regId)
        {
            RegVote regVote = GetList().Where(x => x.reg_buy_id == regId && x.vote_id == userID).FirstOrDefault();
            return regVote;
        }

        public bool Update(RegVote regVote)
        {
            _db.Entry(regVote).State = System.Data.Entity.EntityState.Modified;
            int flag = _db.SaveChanges();
            if (flag > 0)
                return true;
            else
                return false;
        }
        public IEnumerable<T> QueryBySql<T>(string sql)
        {
            return _db.Database.SqlQuery<T>(sql);
        }
    }
}
