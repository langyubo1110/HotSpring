using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public interface IRegApplyRepository
    {
        IEnumerable<RegApply> GetList();
        int Add(RegApply movie);
        bool Update(RegApply movie);
        bool Delete(int id);
        RegApply GetModel(int id);
        IEnumerable<T> QueryBySql<T>(string sql);
        DbContextTransaction TransBegin();
        void Commit();
        void RollBack();

    }
}
