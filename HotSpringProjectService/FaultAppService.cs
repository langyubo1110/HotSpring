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
        private readonly IEmployRoleRepository _employRoleRepository;

        public FaultAppService(IFaultAppRepository faultAppRepository,IEquipmentRepository equipmentRepository,IEmployEmpRepository employEmpRepository,IFaultAnalyseRepository faultAnalyseRepository,IEmployRoleRepository employRoleRepository)
        {
            _employEmpRepository= employEmpRepository;
            _equipmentRepository =equipmentRepository;
            _faultAppRepository =faultAppRepository;
            _faultAnalyseRepository=faultAnalyseRepository;
            _employRoleRepository=employRoleRepository;
        }
        public ResMessage Add(FaultApp faultApp)
        {
            try
            {  //1,22,25,26
               IEnumerable<EmployEmpVO> ilist= _employEmpRepository.GetList().Join(_employRoleRepository.GetList(), x => x.role_id, y => y.id, (x, y) => new EmployEmpVO
                {
                    id = x.id,
                     is_leader=y.is_leader
                });
                ilist = ilist.Where(x => x.is_leader == 1);

               //申报表最新ID
                int[] a =ilist.Select(x => x.id).ToArray();
                int last_id = _faultAppRepository.Add(faultApp);
                foreach (var item in a)
                {
                    FaultAnalyse faultAnalyse = new FaultAnalyse();
                    faultAnalyse.fault_app_id = last_id;
                    faultAnalyse.final_scheme = 0;
                    faultAnalyse.contents = "";
                    faultAnalyse.analyse_id = item;
                    faultAnalyse.create_time = DateTime.Now;
                    _faultAnalyseRepository.Add(faultAnalyse);
                }
                return ResMessage.Success();
            }
            catch (Exception ex)
            {
                return ResMessage.Fail(ex.Message);
            }
           
           
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
        //修改和更新
        public ResMessage StopAndAdd(int eid, FaultAnalyse faultAnalyse)
        {
            Equipment equipment = _equipmentRepository.GetModel(eid);
            equipment.status = 0;
            _equipmentRepository.Update(equipment);
            //添加故障内容
            FaultAnalyse existingFaultAnalyse = _faultAnalyseRepository.GetList().Where(x => x.fault_app_id==faultAnalyse.fault_app_id && x.analyse_id==faultAnalyse.analyse_id).FirstOrDefault();
            existingFaultAnalyse.contents = faultAnalyse.contents;
            int flag = _faultAnalyseRepository.UpDate(existingFaultAnalyse);
            return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
        }
        //得到领导链表
        public ResMessage GetListByRole()
        {
            int[] RoleList = { 1, 2, 3, 25 };
            List<EmployEmp> list = _employEmpRepository.GetList().Where(x => RoleList.Contains(x.role_id)).ToList();
            return list == null ? ResMessage.Fail() : ResMessage.Success(list);
        }
      
        public ResMessage GetRepairList(string name,int page, int limit, int? fault_app_id)
        {
            IQueryable<FaultApp> List = _faultAppRepository.GetList();
            int count = List.Count();
            List<FaultApp> pagedResult = List.OrderBy(x => x.id).Skip((page - 1) * limit).Take(limit).ToList();
            
            List<FaultAnalyse> faultAnalyses = _faultAnalyseRepository.GetList().ToList();
            int end_count = 0;
            
           
            //查故障申报全表
            List<FaultAppVO> list = _faultAppRepository.QueryBySql<FaultAppVO>($@"select Rep_Fault_App.id,equip_id,fault_report,fault_describe,fault_time,Rep_Fault_App.create_time,name from Rep_Fault_App inner join Equ_Equipment on Rep_Fault_App.equip_id=Equ_Equipment.id  ").ToList();
            
            List<FaultAppVO> res = new List<FaultAppVO>();
            foreach(var item in list)
            {
             
                end_count = faultAnalyses.Where(x => x.fault_app_id == item.id & x.final_scheme == 1).Count();
                
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
                vo.auditcount = faultAnalyses.Where(x => x.contents != "" && x.contents != null && x.fault_app_id==item.id).ToList().Count;
                vo.end_count = end_count;//最终解决方案数量
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
            if (!string.IsNullOrEmpty(name)) 
            {
                res=res.Where(x=>x.name.Contains(name)).ToList();
            }

            return  res==null ? ResMessage.Fail() : ResMessage.Success(res,count);
        }
       
       public ResMessage GetAnalyseContents(int id)
       {
            IEnumerable<FaultAnalyse> ilist = _faultAnalyseRepository.GetList().Where(x => x.fault_app_id==id );
            List<FaultAnalyse> list=ilist.ToList();
            return list == null ? ResMessage.Fail() : ResMessage.Success(list);
        }
    }
}
