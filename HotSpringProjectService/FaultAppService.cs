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
      
        public ResMessage GetRepairList(int page, int limit)
        {
            IQueryable<FaultApp> List = _faultAppRepository.GetList();
            int count = List.Count();
            List<FaultApp> pagedResult = List.OrderBy(x => x.id).Skip((page - 1) * limit).Take(limit).ToList();
            List<FaultAnalyse> faultAnalyses = _faultAnalyseRepository.GetList().ToList();
            //查故障申报全表
            //List<FaultApp> list = _faultAppRepository.GetList().ToList();
            List<FaultAppVO> list = _faultAppRepository.QueryBySql<FaultAppVO>($@"select Rep_Fault_App.id,equip_id,fault_report,fault_describe,fault_time,Rep_Fault_App.create_time,name from Rep_Fault_App inner join Equ_Equipment on Rep_Fault_App.equip_id=Equ_Equipment.id  ").ToList();
            
            List<FaultAppVO> res = new List<FaultAppVO>();
            foreach(var item in list)
            {
                FaultAppVO vo = new FaultAppVO();
                vo.fault_report = item.fault_report;
                vo.fault_time= item.fault_time;
                vo.fault_describe = item.fault_describe;
                vo.equip_id = item.equip_id;
                vo.name= item.name;
                vo.create_time = item.create_time;
                vo.id = item.id;
                vo.count = faultAnalyses.Where(x => x.fault_app_id == item.id).ToList().Count;
                //已审批数量
                vo.auditcount = faultAnalyses.Where(x => x.contents != "" && x.contents != null).ToList().Count;
                if (vo.count == vo.auditcount)
                {
                    //隐藏按钮
                    vo.show = 0;
                }
                else
                {
                    vo.show = 1;
                }
                res.Add(vo);
            }

            return  list==null ? ResMessage.Fail() : ResMessage.Success(res,count);
        }
    }
}
