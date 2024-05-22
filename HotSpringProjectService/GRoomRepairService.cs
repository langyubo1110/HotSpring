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
    public class GRoomRepairService:IGRoomRepairService
    {
        private readonly IGRoomRepairRepository _gRoomRepairRepository;
        private readonly IRepoOutInRecordRepository _repoOutInRecordRepository;
        private readonly IGRoomSparePartsRepository _gRoomSparePartsRepository;
        private readonly IEmployPerformRepository _employPerformRepository;
        private readonly IEmployAllsalaryRepository _employAllsalaryRepository;
        private readonly IEmployEmpRepository _employEmpRepository;
        private readonly IEmployRoleRepository _employRoleRepository;

        public GRoomRepairService(IGRoomRepairRepository gRoomRepairRepository,IRepoOutInRecordRepository repoOutInRecordRepository,IGRoomSparePartsRepository gRoomSparePartsRepository,IEmployPerformRepository employPerformRepository,IEmployAllsalaryRepository employAllsalaryRepository,IEmployEmpRepository employEmpRepository,IEmployRoleRepository employRoleRepository) 
        {
            _gRoomRepairRepository=gRoomRepairRepository;
            _repoOutInRecordRepository = repoOutInRecordRepository;
            _gRoomSparePartsRepository = gRoomSparePartsRepository;
            _employPerformRepository=employPerformRepository;
            _employAllsalaryRepository=employAllsalaryRepository;
            _employEmpRepository= employEmpRepository;
            _employRoleRepository= employRoleRepository;
        }
        //维修任务上报成功：
        //维修任务上报表插入一条数据
        //维修任务备件使用表插入n条数据（取决于备件使用种类）
        //商品出入库表插入一条入库记录（需要审批audit=0）
        //点击审核按钮后商品库存表相应的商品的库存数量更新
        //绩效表插入一条绩效数据
        public ResMessage Add(GRoomRepair gRoomRepair, List<GRoomSparePartsVO> gRoomSPVOlist)
        {
            _repoOutInRecordRepository.TransBegin();
            try
            {
                gRoomRepair.create_time = DateTime.Now;
                //维修任务表插入
                //接收维修任务上报表执行插入操作后最新数据的ID
                int last_id = _gRoomRepairRepository.Add(gRoomRepair);
                foreach (GRoomSparePartsVO gRoomSparePartsVO in gRoomSPVOlist)
                {
                    GRoomSpareParts gRoomSpareParts = new GRoomSpareParts();
                    gRoomSpareParts.repair_id = last_id;
                    gRoomSpareParts.spare_parts_id = gRoomSparePartsVO.id;
                    gRoomSpareParts.used_number = gRoomSparePartsVO.used_number;
                    gRoomSpareParts.create_time = DateTime.Now;
                    //判断oi_number是否为0，不为0产生入库记录
                    if (gRoomSparePartsVO.rest_number != 0)
                    {
                        RepoOutInRecord repoOutInRecord = new RepoOutInRecord();
                        //入库记录的商品id
                        repoOutInRecord.goods_id = gRoomSparePartsVO.id;
                        //维修任务上报人就是剩余备件的入库人
                        repoOutInRecord.outin_person_id = gRoomRepair.reporter_id;
                        repoOutInRecord.type = 0;
                        repoOutInRecord.audit = 0;
                        //操作前的库存数量为库存表的库存数量
                        repoOutInRecord.start_number = gRoomSparePartsVO.goods_number;
                        //剩余备件数量就是这里的入库数量
                        repoOutInRecord.oi_number = gRoomSparePartsVO.rest_number;
                        repoOutInRecord.end_number = gRoomSparePartsVO.goods_number + gRoomSparePartsVO.rest_number;
                        repoOutInRecord.recipient_id = 1;
                        repoOutInRecord.create_time = DateTime.Now;
                        //出入库表插入
                        _repoOutInRecordRepository.Add(repoOutInRecord);
                    }
                    //备件表插入
                    _gRoomSparePartsRepository.Add(gRoomSpareParts);
                }
                //获取员工id(维修任务上报人id)
                int employ_id = gRoomRepair.reporter_id;
                EmployPerform employPerform = new EmployPerform();
                employPerform.emp_id = employ_id;
                employPerform.repair_id = last_id;
                //通过员工id查询员工表获取role_id
                int role_id = _employEmpRepository.GetList().Where(x => x.id == employ_id).ToList()[0].role_id;
                //通过role_id去查询角色表获取角色对应的工时费(labor_hours)
                decimal labor_hours = _employRoleRepository.GetList().Where(x => x.id == role_id).ToList()[0].labor_hours;
                //获取工作时间
                TimeSpan ts = gRoomRepair.end_time - gRoomRepair.start_time;
                int work_hours = ts.Hours;
                //绩效金额通过工作时间*工时费计算
                employPerform.repair_up_money = work_hours*labor_hours;
                employPerform.create_time = DateTime.Now;
                //绩效表插入数据
                _employPerformRepository.Add(employPerform);
                _repoOutInRecordRepository.Commit();
                return ResMessage.Success();
            }
            catch (Exception ex)
            {
                _repoOutInRecordRepository.Rollback();
                return ResMessage.Fail(ex.Message);
            }

        }

        public ResMessage Delete(int id)
        {
            int flag = _gRoomRepairRepository.Delete(id);
            return flag>0?ResMessage.Success(): ResMessage.Fail();
        }

        public ResMessage GetList()
        {
           List<GRoomRepair> list = _gRoomRepairRepository.GetList().ToList();
            return list==null?ResMessage.Fail(): ResMessage.Success(list);
        }

        public ResMessage GetListById(string yyyy_MM,int id,int page,int limit)
        {
            IEnumerable<RepairVO> list = _gRoomRepairRepository.QueryBySql<RepairVO>($@"SELECT g.*, e.name,p.repair_up_money FROM GRoom_Repair g
                                                                                     JOIN Employ_Emp e ON g.reporter_id = e.id
                                                                                     JOIN Employ_Perform p ON p.repair_id=g.id
                                                                                     where g.reporter_id={id}");
            if (page!=0&&limit!=0) 
            {
                if (!string.IsNullOrEmpty(yyyy_MM))
                {
                    list = list.Where(x => x.create_time.ToString("yyyy-MM") == yyyy_MM);
                }
                int count =list.Count();
                List<RepairVO> result = list.OrderBy(x=>x.create_time).Skip((page-1)*limit).Take(limit).ToList();
                return result == null ? ResMessage.Fail() : ResMessage.Success(result,count);
            }
            return ResMessage.Fail();
        }

        public ResMessage GetListId(int id)
        {
            List<GRoomRepair> list = _gRoomRepairRepository.GetList().Where(x => x.id == id).ToList();
            return list == null ? ResMessage.Fail() : ResMessage.Success(list);
        }

        public ResMessage GetModel(int id)
        {
            GRoomRepair gRoomRepair =_gRoomRepairRepository.GetModel(id);
            return gRoomRepair==null?ResMessage.Fail() : ResMessage.Success(gRoomRepair);
        }

        public ResMessage Update(GRoomRepair gRoomRepair)
        {
            bool result = _gRoomRepairRepository.Update(gRoomRepair);
            return result==true?ResMessage.Success() : ResMessage.Fail();
        }
    }
}
