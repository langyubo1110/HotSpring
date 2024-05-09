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
    public class FaultAppService : IFaultAppService
    {
        private readonly IEmployEmpRepository _employEmpRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IFaultAppRepository _faultAppRepository;
        private readonly IFaultAnalyseRepository _faultAnalyseRepository;

        public FaultAppService(IFaultAppRepository faultAppRepository,IEquipmentRepository equipmentRepository,IEmployEmpRepository employEmpRepository,IFaultAnalyseRepository faultAnalyseRepository)
        {
            _employEmpRepository= employEmpRepository;
            _equipmentRepository =equipmentRepository;
            _faultAppRepository =faultAppRepository;
            _faultAnalyseRepository=faultAnalyseRepository;
        }
        public ResMessage Add(FaultApp faultApp)
        {
            return _faultAppRepository.Add(faultApp) > 0 ? ResMessage.Success() : ResMessage.Fail();
           
        }

        public bool Delete(int id)
        {
            return _faultAppRepository.Delete(id) > 0 ? true : false;

        }

        public ResMessage GetList(int page, int limit)
        {
            IQueryable<FaultApp> list = _faultAppRepository.GetList();
            int count = list.Count();
            List<FaultApp> list1 = list.OrderBy(x => x.id).Skip((page - 1) * limit).Take(limit).ToList();
            return list == null ? ResMessage.Fail() : ResMessage.Success(list1, count);
        }

        public ResMessage GetModel(int id)
        {
            FaultApp faultApp =_faultAppRepository.GetModel(id);
            return faultApp != null ? ResMessage.Success(faultApp) : ResMessage.Fail();
        }

        public ResMessage UpDate(FaultApp faultApp)
        {
            return _faultAppRepository.UpDate(faultApp)>0 ? ResMessage.Success() :ResMessage.Fail();
        }
        //修改和增加
        public ResMessage StopAndAdd(int eid, FaultAnalyse faultAnalyse)
        {
            Equipment equipment = _equipmentRepository.GetModel(eid);
            equipment.status = 0;
            _equipmentRepository.Update(equipment);
            faultAnalyse.create_time = DateTime.Now;
            faultAnalyse.final_scheme= 0;
            int flag=_faultAnalyseRepository.Add(faultAnalyse);
            return flag>0? ResMessage.Success():ResMessage.Fail();
        }
        //得到领导链表
        public ResMessage GetListByRole()
        {
            int[] RoleList = { 1, 2, 3, 25 };
            List<EmployEmp> list = _employEmpRepository.GetList().Where(x => RoleList.Contains(x.role_id)).ToList();
            return list == null ? ResMessage.Fail() : ResMessage.Success(list);
        }
      
        public ResMessage GetRepairList()
        {
            List<FaultAppVO> list = _faultAppRepository.QueryBySql<FaultAppVO>($@"select a.*,COUNT(fault_app_id) count from Rep_Fault_App a left join Rep_Fault_Analyse s on a.id=s.fault_app_id Group By a.id,a.equip_id,a.fault_report,a.fault_describe,a.fault_time,a.create_time ").ToList();


            return list == null ? ResMessage.Fail() : ResMessage.Success(list);
        }
    }
}
