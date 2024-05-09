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
    public class EmployAllsalaryService:IEmployAllsalaryService
    {
        private readonly IEmployAllsalaryRepository _employAllsalaryRepository;

        public EmployAllsalaryService(IEmployAllsalaryRepository employAllsalaryRepository)
        {
            _employAllsalaryRepository= employAllsalaryRepository;
        }

        public ResMessage Add(EmployAllsalary employAllsalary)
        {
            bool result = _employAllsalaryRepository.Add(employAllsalary);
            return result == true ? ResMessage.Success() : ResMessage.Fail();
        }

        public ResMessage Delete(int id)
        {
            int flag = _employAllsalaryRepository.Delete(id);
            return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
        }

        public ResMessage GetList()
        {
            List<EmployAllsalary> list = _employAllsalaryRepository.GetList().ToList();
            return list == null ? ResMessage.Fail() : ResMessage.Success();
        }

        public ResMessage GetModel(int id)
        {
            EmployAllsalary employAllsalary = _employAllsalaryRepository.GetModel(id);
            return employAllsalary == null ? ResMessage.Fail() : ResMessage.Success();
        }

        public ResMessage Update(EmployAllsalary employAllsalary)
        {
            bool result = _employAllsalaryRepository.Update(employAllsalary);
            return result == true ? ResMessage.Success() : ResMessage.Fail();
        }
    }
}
