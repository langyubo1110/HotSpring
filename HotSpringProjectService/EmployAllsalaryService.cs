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



        //测试
        public ResMessage test()
        {
            List<EmployAllsalary> list = _employAllsalaryRepository.GetList().ToList();

            List<EmployAllsalaryVO> slist = _employAllsalaryRepository.QueryBySql<EmployAllsalaryVO>($@"select e.id,e.name,s.emp_id,s.pay_month,
                                          s.pay_time,s.post_status,s.perform_money,r.create_time,r.salary 
                                          from Employ_Emp e inner join Employ_Allsalary s on s.emp_id=e.id 
                                          inner join Employ_Role r on r.id=e.role_id ").ToList();

            List<EmployPerformVO> plist = _employAllsalaryRepository.QueryBySql<EmployPerformVO>($@"select p.id,p.allsalary_id,s.emp_id,p.repair_id,p.repair_up_money,p.create_time,g.start_time,g.end_time,g.confirmer
                                          from Employ_Allsalary s inner join Employ_Perform p on p.allsalary_id=s.id
                                          inner join GRoom_Repair g on g.id=p.repair_id").ToList();
            foreach (var item in list)
            {
                //计算每个员工的当月底薪，去签到表确认出勤
                //获取上月该员工出勤比例
                decimal rate = _employCheckInService.GetWorkRate(item.emp_id);
                item.salary = 0;
                slist = slist.Where(s => s.emp_id == item.emp_id).ToList();
                item.salary = slist[0].salary * rate;
                //计算每个员工的当月绩效，去绩效表
                item.perform_money = 0;
                plist = plist.Where(x => x.emp_id == item.emp_id).ToList();
                //循环相加
                foreach (var money in plist)
                {
                    item.perform_money += money.repair_up_money;
                }
                _employAllsalaryRepository.Update(item);
                EmployAllsalary employ = new EmployAllsalary();
                employ.emp_id = item.emp_id;
                //判断是不是12月
                if (item.pay_month == 12) employ.pay_month = 1;
                else employ.salary = item.pay_month + 1;
                employ.pay_time = DateTime.Now.AddDays(15);
                employ.post_status = 0;
                employ.salary = (decimal)0.00;
                employ.perform_money = (decimal)0.00;
                employ.create_time = DateTime.Now;
                _employAllsalaryRepository.Add(employ);
            }
            return ResMessage.Success();
        }

    }
}
