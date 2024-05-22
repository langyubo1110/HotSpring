using DotNet.Utilities;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    public interface IEmployMessageService
    {
        List<EmployMessage> GetList();
        ResMessage Add(EmployMessageVO employMessagevo);
        ResMessage AddWithRoleId(int roleId);
        EmployMessage GetModel(int id);
        ResMessage Read(int id);
    }
}
