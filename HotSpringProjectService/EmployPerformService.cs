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
    public class EmployPerformService:IEmployPerformService
    {
        private readonly IEmployPerformRepository _employPerformRepository;
        private readonly IGRoomRepairRepository _gRoomRepairRepository;

        public EmployPerformService(IEmployPerformRepository employPerformRepository,IGRoomRepairRepository gRoomRepairRepository)
        {
            _employPerformRepository= employPerformRepository;
            _gRoomRepairRepository= gRoomRepairRepository;
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
        public ResMessage GetListByPager(int page, int limit, string yyyy_MM,int emp_id)
        {
            //此处接收id为员工id
            IEnumerable<EmployPerform> plist = _employPerformRepository.GetList();
            IEnumerable<EmployPerformVO> list = plist.Join(_gRoomRepairRepository.GetList(),x=>x.repair_id,y=>y.id,(x,y)=>new EmployPerformVO { 
                emp_id = x.emp_id,
                start_time=y.start_time,
                end_time=y.end_time,
                confirmer=y.confirmer,
                create_time=x.create_time,
                repair_up_money=x.repair_up_money,
            });
            //查找符合员工id条件的绩效数据
            //还有当月月份条件
            
            if (emp_id != 0 && !string.IsNullOrEmpty(yyyy_MM)) 
            {
                list = list.Where(x => x.emp_id == emp_id && x.create_time.ToString("yyyy-MM")==yyyy_MM ).ToList();  
            }
            //分页
            List<EmployPerformVO> result = list.OrderBy(x => x.create_time).Skip((page - 1) * limit).Take(limit).ToList();
            int count = result.Count;
            return result == null ? ResMessage.Fail() : ResMessage.Success(result, count);
        }
        public List<EmployPerform> GetListByRepairId(int id)
        {
            List<EmployPerform> list = _employPerformRepository.GetList().Where(item => item.repair_id == id).ToList();
            return list ;
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
