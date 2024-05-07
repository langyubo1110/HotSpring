using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectRepository;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService
{
    public class EmployRoleService : IEmployRoleService
    {
        private readonly IEmployRoleRepository _db;

        //构造函数注入
        public EmployRoleService(IEmployRoleRepository _employRoleRepository)
        {
            _db = _employRoleRepository;

        }
        public ResMessage Add(EmployRole EmployRole)
        {
            int flag = _db.Add(EmployRole);
            return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
        }

        public ResMessage Delete(int id)
        {
            bool flag = _db.Delete(id);
            return flag ? ResMessage.Success() : ResMessage.Fail();
        }

        public IEnumerable<EmployRole> GetList()
        {
            return _db.GetList();
        }

        public ResMessage GetModel(int id)
        {
            EmployRole EmployRole = _db.GetModel(id);
            return EmployRole != null ? ResMessage.Success(EmployRole) : ResMessage.Fail();
        }

        public ResMessage Update(EmployRole EmployRole)
        {
            bool flag = _db.Update(EmployRole);
            return flag ? ResMessage.Success() : ResMessage.Fail();
        }
    }
}
