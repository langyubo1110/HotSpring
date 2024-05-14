using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    public interface IRegFileRepository
    {
        IEnumerable<RegFile> GetList();
        int Add(RegFile regFile);
        bool Update(RegFile regFile);
        bool Delete(int id);
        IEnumerable<T> QueryBySql<T>(string sql);
        RegFile GetModel(int id);
    }
}
