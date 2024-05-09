using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    public interface ISystemPagesRepository
    {
        IEnumerable<SystemPages> GetList();
        int Add(SystemPages systemPages);
        bool Update(SystemPages systemPages);
        bool Delete(int id);
        SystemPages GetModel(int id);
        IEnumerable<(int Id, string Name)> GetModuleList();
        List<SystemPages> GetPagesByModuleId(int moduleId);

    }
}
