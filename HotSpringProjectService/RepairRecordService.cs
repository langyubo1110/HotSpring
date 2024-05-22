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
using System.Windows.Documents;

namespace HotSpringProjectService
{
    public class RepairRecordService:IRepairRecordService
    {
        private readonly IRepairRecordRepository _repairRecordRepository;
        private readonly IEmployEmpRepository _employEmpRepository;

        public  RepairRecordService(IRepairRecordRepository repairRecordRepository,IEmployEmpRepository employEmpRepository)
        {
          _repairRecordRepository=repairRecordRepository;
            _employEmpRepository=employEmpRepository;
        }

        public ResMessage Add(RepairRecord repairRecord)
        {
            return ResMessage.Success();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResMessage GetList(int id)
        {

            IQueryable<RepairRecord> list = _repairRecordRepository.GetList().Where(x => x.fault_app_id == id);
            IEnumerable<RepairRecord> list1 = list;
            List<RepairRecordVO> result=list1.Join(_employEmpRepository.GetList(),x=>x.repair_people_id,y=>y.id,(x,y)=>new RepairRecordVO
            {
               repair_people=y.name,
               repair_phone=x.repair_phone,
               repair_spend=x.repair_spend,
               create_time=x.create_time,
            }).ToList();
            return result == null ? ResMessage.Fail() : ResMessage.Success(result);
        }

        public ResMessage GetModel(int id)
        {
            throw new NotImplementedException();
        }

        public ResMessage UpDate(RepairRecord repairRecord)
        {
            throw new NotImplementedException();
        }
    }
}
