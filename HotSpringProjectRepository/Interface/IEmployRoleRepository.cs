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
        //查询
        IEnumerable<EmployRole> GetList();
        //添加
        int Add(EmployRole employRole);
        //删除
        bool Delete(int id);
        //修改
        bool Update(EmployRole employRole);
        //获得角色实体
        EmployRole GetModel(int id);
    }
}
