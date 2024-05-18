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
using System.Windows.Navigation;

namespace HotSpringProjectService
{
    //薪资发放定时调度
    //2024-05-10
    //裴晨旭
    public class SalaryPostService : ISalaryPostService
    {
        private readonly IEmployCheckInService _employCheckInService;
        private readonly IEmployAllsalaryRepository _employAllsalaryRepository;

        public SalaryPostService(IEmployAllsalaryRepository employAllsalaryRepository, IEmployCheckInService employCheckInService)
        {
            _employCheckInService = employCheckInService;
            _employAllsalaryRepository = employAllsalaryRepository;
        }
        public Task Execute()
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
                double rate = _employCheckInService.GetWorkRate(item.emp_id);
                item.salary = 0;
                slist = slist.Where(s => s.emp_id == item.emp_id).ToList();
                //item.salary = slist[0].salary * rate;
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
            return Task.CompletedTask;
        }
    }
}
