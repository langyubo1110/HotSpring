using DotNet.Utilities;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    public interface IEmployPerformService
    {
        ResMessage Delete(int id);
        ResMessage Update(EmployPerform employPerform);
        ResMessage Add(EmployPerform employPerform);
        ResMessage GetModel(int id);
        ResMessage GetList();
    }
}
