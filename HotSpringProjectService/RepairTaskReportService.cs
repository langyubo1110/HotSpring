using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectService.Interface;
using HotSpringProjectRepository.Interface;
using System.Runtime.Remoting.Messaging;
namespace HotSpringProjectService
{
    public class RepairTaskReportService:IRepairTaskReportService
    {
        private readonly IRepairTaskReportRepository _repairTaskReportRepository;
        private readonly IEquipmentRepository _equipmentRepository;

        public RepairTaskReportService(IRepairTaskReportRepository repairTaskReportRepository,IEquipmentRepository equipmentRepository) {
            _repairTaskReportRepository = repairTaskReportRepository;
            _equipmentRepository = equipmentRepository;
        }
        
        public ResMessage Add(RepaieTaskReport repaieTaskReport)
        {
            return _repairTaskReportRepository.add(repaieTaskReport)>  0 ? ResMessage.Success():ResMessage.Fail(); 
        }

        public bool Delete(int id)
        {   
            return _repairTaskReportRepository.delete(id)>0? true: false;
          
        }

        public ResMessage GetList(int page,int limit)
        {
            IQueryable<RepaieTaskReport> list=_repairTaskReportRepository.getlist();
            int count = list.Count();
            List<RepaieTaskReport> list1=list.OrderBy(x=>x.id).Skip((page-1)*limit).Take(limit).ToList();
            return list == null ? ResMessage.Fail() : ResMessage.Success(list1,count);
        }

        public ResMessage GetModel(int id)
        {
            RepaieTaskReport repaieTaskReport=_repairTaskReportRepository.getmodel(id);
            return repaieTaskReport!=null? ResMessage.Success(repaieTaskReport) : ResMessage.Fail();
        }

        public ResMessage UpDate(RepaieTaskReport repaieTaskReport)
        {

            return _repairTaskReportRepository.update(repaieTaskReport) > 0 ? ResMessage.Success() : ResMessage.Fail();
        }
        //更新设备表的设备状态为停用
       
        //public int UpdateEquipmentStatus(int equipmentId)
        //{
        //    return _equipmentRepository.UpdateStatus(equipmentId);
        //}

        //public ResMessage GetEquipName()
        //{
        //    List<string> EquipmentName = _equipmentRepository.GetList().Select(x => x.name).ToList();
        //    return ResMessage.Success(EquipmentName);
        //}

        public ResMessage GetEqumentName()
        {
            List<string> EquipmentName = _equipmentRepository.GetList().Select(x => x.name).ToList();
            return ResMessage.Success(EquipmentName); 
        }
    }
}
