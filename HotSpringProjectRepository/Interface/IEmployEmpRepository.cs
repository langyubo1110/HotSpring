using HotSpringProject.Entity;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    public interface IEmployEmpRepository
    {
        IEnumerable<EmployEmp> GetList();
        //IEnumerable<EmployEmp> GetListByID(int id);
        int Add(EmployEmp employemp);
        bool Update(EmployEmp employemp);
        bool Delete(int id);
        EmployEmp GetModel(int id);
        bool Varfy(string username, string password);
    }
}
