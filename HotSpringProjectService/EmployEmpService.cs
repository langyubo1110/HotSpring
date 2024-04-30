
using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService
{
    public class EmployEmpService : IEmployEmpService
    {
        private readonly IEmployEmpRepository _EmployEmpRepository;

        public EmployEmpService(IEmployEmpRepository employemprepository)
        {
            _EmployEmpRepository = employemprepository;
        }
        public ResMessage Add(EmployEmp employemp)
        {
            employemp.onboarding_time = DateTime.Now;
            employemp.create_time = DateTime.Now;
            employemp.account_status = 1;
            int flag = _EmployEmpRepository.Add(employemp);
            return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
        }

        public ResMessage Delete(int Id)
        {
            bool flag = _EmployEmpRepository.Delete(Id);
            return flag ? ResMessage.Success() : ResMessage.Fail();
        }

        public IEnumerable<EmployEmp> GetList()
        {
            return _EmployEmpRepository.GetList();
        }
        /// <summary>
        /// 带分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public ResMessage GetListByPager(EmployEmpFilter filter)
        {
            IEnumerable<EmployEmp> list = _EmployEmpRepository.GetList();
            int count = 0;
            list = MakeQuery(list, filter, out count);
            return ResMessage.Success(list, count);
        }
        public IEnumerable<EmployEmp> MakeQuery(IEnumerable<EmployEmp> list, EmployEmpFilter filter, out int count)
        {
            if (!string.IsNullOrEmpty(filter.name))
            {
                list = list.Where(x => x.name.Contains(filter.name));
            }
            if (filter.role_id != 0)
            {
                list = list.Where(x => x.role_id == filter.role_id);
            }
            count = list.Count();
            //开启分页
            if (filter.page != 0 && filter.limit != 0)
            {
                list = list.OrderByDescending(x => x.create_time).Skip((filter.page - 1) * filter.limit).Take(filter.limit);
            }
            return list.ToList();
        }

        public ResMessage getModel(int id)
        {
            EmployEmp model = _EmployEmpRepository.GetModel(id);
            return model != null ? ResMessage.Success(model) : ResMessage.Fail();
        }

        public ResMessage Update(EmployEmp employemp)
        {
            bool flag = _EmployEmpRepository.Update(employemp);
            return flag ? ResMessage.Success() : ResMessage.Fail();
        }
    }
}
