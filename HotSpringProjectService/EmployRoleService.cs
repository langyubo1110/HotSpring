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
        private readonly IEmployRoleRepository _employRoleRepository;

        public EmployRoleService(IEmployRoleRepository employRoleRepository)
        {
            _employRoleRepository = employRoleRepository;
        }
        public IEnumerable<EmployRole> GetEmployRoles()
        {
            return _employRoleRepository.GetList();
        }
        //将数据返回为ResMessage
        //success = true,
        //code = 200,
        //msg = "success",
        //count = count,
        //data = data
        public ResMessage GetRoles()
        {
            IEnumerable<EmployRole> employRoles = _employRoleRepository.GetList();
            int count = employRoles.Count();
            return ResMessage.Success(employRoles, count);
        }
        public ResMessage Add(EmployRole employRole)
        {
            if (employRole != null)

                employRole.create_time = DateTime.Now;
            int flag = _employRoleRepository.Add(employRole);
            return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
        }

        public ResMessage Delete(int id)
        {
            bool flag = _employRoleRepository.Delete(id);
            return flag ? ResMessage.Success() : ResMessage.Fail();
        }
        public IEnumerable<EmployRole> GetList()
        {
            return _employRoleRepository.GetList();
        }

        public ResMessage GetModel(int id)
        {
            EmployRole model = _employRoleRepository.GetModelById(id);
            return model != null ? ResMessage.Success(model) : ResMessage.Fail();
        }

        public ResMessage Update(EmployRole employRole)
        {
            employRole.create_time = DateTime.Now;
            bool flag = _employRoleRepository.Update(employRole);
            return flag ? ResMessage.Success() : ResMessage.Fail();
        }
    }
} 

