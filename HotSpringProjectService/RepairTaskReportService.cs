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
using HotSpringProject.Entity.VO;
using System.Runtime.Remoting.Contexts;
namespace HotSpringProjectService
{
    public class RepairTaskReportService : IRepairTaskReportService
    {
        private readonly IRepairTaskReportRepository _repairTaskReportRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IFaultAnalyseRepository _faultAnalyseRepository;
        private readonly IEmployEmpRepository _employEmpRepository;
        private readonly IEmployRoleRepository _employRoleRepository;

        public RepairTaskReportService(IRepairTaskReportRepository repairTaskReportRepository, IEquipmentRepository equipmentRepository, IFaultAnalyseRepository faultAnalyseRepository,IEmployEmpRepository  employEmpRepository,IEmployRoleRepository employRoleRepository)
        {
            _repairTaskReportRepository = repairTaskReportRepository;
            _equipmentRepository = equipmentRepository;
            _faultAnalyseRepository = faultAnalyseRepository;
            _employEmpRepository=employEmpRepository;
            _employRoleRepository=employRoleRepository;
        }

        public ResMessage Add(RepaieTaskReport repaieTaskReport)
        {
            return _repairTaskReportRepository.Add(repaieTaskReport) > 0 ? ResMessage.Success() : ResMessage.Fail();
        }

        public bool Delete(int id)
        {
            return _repairTaskReportRepository.Delete(id) > 0 ? true : false;

        }

        public ResMessage GetList(int page, int limit)
        {
            IQueryable<RepaieTaskReport> list = _repairTaskReportRepository.GetList();
            int count = list.Count();
            List<RepaieTaskReport> list1 = list.OrderBy(x => x.id).Skip((page - 1) * limit).Take(limit).ToList();
            return list == null ? ResMessage.Fail() : ResMessage.Success(list1, count);
        }

        public ResMessage GetModel(int id)
        {
            RepaieTaskReport repaieTaskReport = _repairTaskReportRepository.GetModel(id);
            return repaieTaskReport != null ? ResMessage.Success(repaieTaskReport) : ResMessage.Fail();
        }

        public ResMessage UpDate(RepaieTaskReport repaieTaskReport)
        {

            return _repairTaskReportRepository.UpDate(repaieTaskReport) > 0 ? ResMessage.Success() : ResMessage.Fail();
        }
        
        public ResMessage UpdateEquipmentStatus(Equipment equipment)
        {   //向故障分析表插入n条数据
            List<RepairTaskReportVO> list = _repairTaskReportRepository.QueryBySql<RepairTaskReportVO>($@"SELECT *, (SELECT COUNT(*) FROM Employ_Emp WHERE Employ_Role.id=1) AS count
            FROM Employ_Emp
            INNER JOIN Employ_Role ON Employ_Emp.role_id = Employ_Role.id
            WHERE Employ_Role.role = 1 ").ToList();
            //foreach(var item in list)
            //{
            //    FaultAnalyse faultAnalyse = _faultAnalyseRepository.Add().Where(x => x.analyse_id ==item.id).FirstOrDefault();
            //}

            //更新设备表的设备状态为停用
            int equipmentId = equipment.id;
            Equipment equipment1 = _equipmentRepository.GetList().Where(X => X.id == equipmentId).FirstOrDefault();
            equipment1.status = 0;
            _equipmentRepository.Update(equipment1);  
            

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
        //修改和增加
        public ResMessage StopAndAdd(int id,string contents)
        {
            Equipment equipment=_equipmentRepository.GetModel(id);
            equipment.status = 0;
            _equipmentRepository.Update(equipment);
            
            return ResMessage.Success();
        }
        //得到领导链表
        public ResMessage GetListByRole()
        {
            //int[] RoleList = { 1, 2, 3, 25 };
            //List<EmployEmp> list = _employEmpRepository.GetList().Where(x => RoleList.Contains(x.role_id)).ToList();
            //return list == null ? ResMessage.Fail() : ResMessage.Success(list);

            //得到role_id
            int[] Role_Id = _employRoleRepository.GetList().Where(x => x.is_leader == 1).Select(x => x.id).ToArray();
            //根据Role_Id 在员工表找到员工Id
            List<EmployEmp> list = _employEmpRepository.GetList().Where(x => Role_Id.Contains(x.role_id)).ToList();
            return list==null?ResMessage.Fail():ResMessage.Success(list);
        }
        //查维修上报全表
        //public ResMessage GetList()
        //{
        //    List<FaultAnalyseVO> list=_faultAnalyseRepository.QueryBySql<FaultAnalyseVO>($@)
        //}

    }
}
