using HotSpringProject.Entity;
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

        public SalaryPostService(IEmployAllsalaryRepository employAllsalaryRepository,IEmployCheckInService employCheckInService) 
        {
            _employCheckInService=employCheckInService;
            _employAllsalaryRepository = employAllsalaryRepository;
        }
        public Task Execute()
        {
            List<EmployAllsalary> list = _employAllsalaryRepository.GetList().ToList();
            foreach (var item in list) 
            {
                //计算每个员工的当月底薪，去签到表确认出勤
                //获取上月该员工出勤比例
                decimal rate = _employCheckInService.GetWorkRate(item.id);
                item.salary = 0;
                //计算每个员工的当月绩效，去绩效表
                item.perform_money = 0;
            }
            return Task.CompletedTask;
            
        }
    }
}
