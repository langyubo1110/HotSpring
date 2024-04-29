using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectService.Interface;
using HotSpringProjectRepository.Interface;
namespace HotSpringProjectService
{
    public class RepairTaskReportService:IRepairTaskReportService
    {
        private readonly IRepairTaskReportRepository _repairTaskReportRepository;

        public RepairTaskReportService(IRepairTaskReportRepository repairTaskReportRepository) {
            _repairTaskReportRepository = repairTaskReportRepository;

        }

        public ResMessage add(RepaieTaskReport repaieTaskReport)
        {
            return _repairTaskReportRepository.add(repaieTaskReport)>  0 ? ResMessage.Success():ResMessage.Fail(); 
        }

        public bool delete(int id)
        {   
            return _repairTaskReportRepository.delete(id)>0? true: false;
          
        }

        public ResMessage getlist(int page,int limit)
        {
            IQueryable<RepaieTaskReport> list=_repairTaskReportRepository.getlist();
            int count = list.Count();
            List<RepaieTaskReport> list1=list.OrderBy(x=>x.id).Skip((page-1)*limit).Take(limit).ToList();
            return list == null ? ResMessage.Fail() : ResMessage.Success(list1,count);
        }

        public ResMessage getmodel(int id)
        {
            RepaieTaskReport repaieTaskReport=_repairTaskReportRepository.getmodel(id);
            return repaieTaskReport!=null? ResMessage.Success(repaieTaskReport) : ResMessage.Fail();
        }

        public ResMessage update(RepaieTaskReport repaieTaskReport)
        {

            return _repairTaskReportRepository.update(repaieTaskReport) > 0 ? ResMessage.Success() : ResMessage.Fail();
        }
        //更新设备表的设备状态为停用
       // public ResMessage updatestatus(Equipment equipment)
       //{
           // int id=equipment.id;
          //  Equipment existingEquipment =_  // 获取设备对象

       // }
    }
}
