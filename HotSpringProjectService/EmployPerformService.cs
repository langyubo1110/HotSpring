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
    public class EmployPerformService:IEmployPerformService
    {
        private readonly IEmployPerformRepository _employPerformRepository;

        public EmployPerformService(IEmployPerformRepository employPerformRepository)
        {
            _employPerformRepository= employPerformRepository;
        }

        public ResMessage Add(EmployPerform employPerform)
        {
            bool result = _employPerformRepository.Add(employPerform);
            return result==true?ResMessage.Success():ResMessage.Fail();
        }

        public ResMessage Delete(int id)
        {
            int flag = _employPerformRepository.Delete(id);
            return flag>0?ResMessage.Success():ResMessage.Fail();
        }

        public ResMessage GetList()
        {
            List<EmployPerform> list = _employPerformRepository.GetList().ToList();
            return list==null?ResMessage.Fail():ResMessage.Success();
        }

        public ResMessage GetModel(int id)
        {
            EmployPerform employPerform = _employPerformRepository.GetModel(id);
            return employPerform==null?ResMessage.Fail() : ResMessage.Success();
        }

        public ResMessage Update(EmployPerform employPerform)
        {
            bool result =_employPerformRepository.Update(employPerform);
            return result==true?ResMessage.Success() :ResMessage.Fail();
        }
    }
}
