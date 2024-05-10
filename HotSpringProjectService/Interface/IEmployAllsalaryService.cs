using DotNet.Utilities;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    public interface IEmployAllsalaryService
    {
        ResMessage Delete(int id);
        ResMessage Update(EmployAllsalary employAllsalary);
        ResMessage Add(EmployAllsalary employAllsalary);
        ResMessage GetModel(int id);
        ResMessage GetList();
    }
}
