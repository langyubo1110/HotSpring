using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotSpringProject.Entity;
using HotSpringProjectRepository;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using DotNet.Utilities;

namespace HotSpringProjectService
{
    public class EmployRoleService : IEmployRoleService
    {
        private readonly IEmployRoleRepository _employRoleRepository;

        public EmployRoleService(IEmployRoleRepository employRoleRepository) 
        {
            _employRoleRepository= employRoleRepository;
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
            if(employRole != null)

            employRole.create_time = DateTime.Now;
            int flag = _employRoleRepository.Add(employRole);
            return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
        }

    }
}
