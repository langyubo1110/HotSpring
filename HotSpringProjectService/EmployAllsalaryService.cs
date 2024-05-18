using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.VO;
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
    public class EmployAllsalaryService : IEmployAllsalaryService
    {
        private readonly IEmployAllsalaryRepository _employAllsalaryRepository;
        private readonly IEmployEmpRepository _employEmpRepository;
        private readonly IEmployCheckInService _employCheckInService;

        public EmployAllsalaryService(IEmployAllsalaryRepository employAllsalaryRepository, IEmployEmpRepository employEmpRepository, IEmployCheckInService employCheckInService)
        {
            _employAllsalaryRepository = employAllsalaryRepository;
            _employEmpRepository = employEmpRepository;
            _employCheckInService= employCheckInService;
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
            return list == null ? ResMessage.Fail() : ResMessage.Success(list);
        }
        //因为需要绑定员工姓名使用VO
        //相比薪资全表多了一个name
        public ResMessage GetListByPager(EmployAllsalaryFilter filter)
        {
            if (filter.page != null && filter.limit != null)
            {
                int page = (int)filter.page;
                int limit = (int)filter.limit;
                IEnumerable<EmployAllsalary> slist = _employAllsalaryRepository.GetList();
                IEnumerable<EmployAllsalaryVO> list = slist.Join(_employEmpRepository.GetList(), x => x.emp_id, y => y.id, (x, y) => new EmployAllsalaryVO
                {
                    id = x.id,
                    name = y.name,
                    emp_id = x.emp_id,
                    create_time = x.create_time,
                    pay_month = x.pay_month,
                    pay_time = x.pay_time,
                    perform_money = x.perform_money,
                    post_status = x.post_status,
                    salary = x.salary,
                });
                int count = list.Count();
                if (!string.IsNullOrEmpty(filter.name)) 
                {
                    list = list.Where(x => x.name.Contains(filter.name));
                }
                if(filter.pay_month !=null)
                {
                    list = list.Where(x => x.pay_month == filter.pay_month);
                }
                List<EmployAllsalaryVO> result = list.OrderBy(x => x.post_status).Skip((page - 1) * limit).Take(limit).ToList();
                return result == null ? ResMessage.Fail() : ResMessage.Success(result, count);
            }
            return ResMessage.Fail();
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
