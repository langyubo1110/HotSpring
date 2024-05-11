using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    public interface IEmployAllsalaryRepository
    {
        int Delete(int id);
        bool Update(EmployAllsalary employAllsalary);
        bool Add(EmployAllsalary employAllsalary);
        EmployAllsalary GetModel(int id);
        IEnumerable<EmployAllsalary> GetList();
    }
}
