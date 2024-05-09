using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    public interface ISystemPageCorrespondenceRepository
    {
        IEnumerable<SystemPageCorrespondence> GetList();
        int Add(int roleId, List<int> pageIds);
        bool Update(SystemPageCorrespondence sp);
        bool Delete(int roleId);
        SystemPageCorrespondence GetModel(int id);
        List<T> QueryBySql<T>(string sql);
    }
}
