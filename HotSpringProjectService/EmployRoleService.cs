using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotSpringProject.Entity;
using HotSpringProjectRepository;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;


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
    }
}
