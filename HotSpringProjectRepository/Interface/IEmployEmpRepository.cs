using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    public interface IEmployEmpRepository
    {
        List<EmployEmp> GetList();
        int Add(EmployEmp employemp);
        bool Update(EmployEmp employemp);
        bool Delete(int id);
        EmployEmp GetModel(int id);
    }
}
