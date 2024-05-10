using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    public interface IEmployPerformRepository
    {
        int Delete(int id);
        bool Update(EmployPerform employPerform);
        bool Add(EmployPerform employPerform);
        EmployPerform GetModel(int id);
        IEnumerable<EmployPerform> GetList();
    }
}
