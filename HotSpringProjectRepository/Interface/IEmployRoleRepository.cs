using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    public interface IEmployRoleRepository
    {
        IEnumerable<EmployRole> GetList();
        //增
        int Add(EmployRole EmployRole);
        //改
        bool Update(EmployRole EmployRole);
        //删
        bool Delete(int id);
        //查实体
        EmployRole GetModel(int id);
        //sql查询
        IEnumerable<T> QueryBySql<T>(string sql);
    }
}
