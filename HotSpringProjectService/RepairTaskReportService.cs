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
using System.Runtime.InteropServices;
using HotSpringProjectRepository;
namespace HotSpringProjectService
{
    public class RepairTaskReportService:IRepairTaskReportService
    {
        private readonly IRepairTaskReportRepository _repairTaskReportRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IFaultAnalyseRepository _faultAnalyseRepository;

        public RepairTaskReportService(IRepairTaskReportRepository repairTaskReportRepository,IEquipmentRepository equipmentRepository,IFaultAnalyseRepository faultAnalyseRepository) {
            _repairTaskReportRepository = repairTaskReportRepository;
            _equipmentRepository = equipmentRepository;
            _faultAnalyseRepository=faultAnalyseRepository;
        }
        
        public ResMessage Add(RepaieTaskReport repaieTaskReport)
        {
            return _repairTaskReportRepository.Add(repaieTaskReport)>  0 ? ResMessage.Success():ResMessage.Fail(); 
        }

        public bool Delete(int id)
        {   
            return _repairTaskReportRepository.Delete(id)>0? true: false;
          
        }

        public ResMessage GetList(int page,int limit)
        {
            IQueryable<RepaieTaskReport> list=_repairTaskReportRepository.GetList();
            int count = list.Count();
            List<RepaieTaskReport> list1=list.OrderBy(x=>x.id).Skip((page-1)*limit).Take(limit).ToList();
            return list == null ? ResMessage.Fail() : ResMessage.Success(list1,count);
        }

        public ResMessage GetModel(int id)
        {
            RepaieTaskReport repaieTaskReport=_repairTaskReportRepository.GetModel(id);
            return repaieTaskReport!=null? ResMessage.Success(repaieTaskReport) : ResMessage.Fail();
        }

        public ResMessage UpDate(RepaieTaskReport repaieTaskReport)
        {
            
            return _repairTaskReportRepository.UpDate(repaieTaskReport) > 0 ? ResMessage.Success() : ResMessage.Fail();
        }
        //更新设备表的设备状态为停用
        public ResMessage UpdateEquipmentStatus(Equipment equipment)
        {
            int equipmentId = equipment.id;
            Equipment equipment1 = _equipmentRepository.GetList().Where(X => X.id == equipmentId).FirstOrDefault();
            equipment1.status = 0;
            _equipmentRepository.Update(equipment1);  // 调用Update方法更新设备状态
            //插入N条数据
           
            return ResMessage.Success();
        }
        public ResMessage GetEquipmentList()
        {
            IEnumerable<Equipment> equipmentList = _equipmentRepository.GetList(); // 调用仓储层的GetList方法获取全表的Equipment对象

            foreach (var equipment in equipmentList)
            {
                int id = equipment.id; // 获取id属性
                string name = equipment.name; // 获取name属性
               
            }
            return ResMessage.Success(equipmentList);
        }
        

    }
}
