using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.VO;
using HotSpringProjectRepository;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService
{
    public class RegApplyService : IRegApplyService
    {
        private readonly IRegApplyRepository _regApplyRepository;
        private readonly IEmployEmpRepository _EmployEmpRepository;
        private readonly IRegAuditRepository _regAuditRepository;

        public RegApplyService(IRegApplyRepository regApplyRepository, IEmployEmpRepository employemprepository,IRegAuditRepository regAuditRepository)
        {
            _regApplyRepository = regApplyRepository;
            _EmployEmpRepository = employemprepository;
            _regAuditRepository = regAuditRepository;
        }
    

            
        public int Add(RegApply regApply)
        {
            regApply.create_time = DateTime.Now;
            int flag = _regApplyRepository.Add(regApply);
            
            return flag > 0 ? regApply.id : 0;
        }
        //加申请带审批表
        public ResMessage AddWithRegAudit(RegApply regApply)
        {
            //加申请
            _regApplyRepository.Add(regApply);
            int applyId = regApply.id;
            //加审批
            //--1有多少个管理员、
            IEnumerable<EmployEmp> emplist = _EmployEmpRepository.GetList().ToList().Where(x => x.role_id == 2);
            _regApplyRepository.TransBegin();
            try
            {
                foreach (EmployEmp emp in emplist)
                {
                    RegAudit regAudit = new RegAudit();
                    {
                        regAudit.reg_equip_reaearch_id = applyId;
                        regAudit.recheck_id = emp.id;
                        regAudit.recheck_opin = 0;
                    }
                    //--2审批插库
                    _regAuditRepository.Add(regAudit);

                }
                return ResMessage.Success();
            }
            catch (Exception ex)
            {
                return ResMessage.Fail(ex.Message);
            }
        }

        public ResMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<RegApply> GetList()
        {
            return _regApplyRepository.GetList().ToList();
        }

        public ResMessage GetListByPager(RegApplyFilter filter)
        {
            IEnumerable<RegApplyVO> list = GetListSqlAudit();
            int count = list.Count();
            List<RegApplyVO> result = list.OrderBy(x => x.id).Skip((filter.page - 1) * filter.limit).Take(filter.limit).ToList();
            return  result==null?ResMessage.Fail():ResMessage.Success(list, count);
        }
        public List<RegApplyVO> GetListUnion()
        {
            IEnumerable<EmployEmp> employEmp = _EmployEmpRepository.GetList();
            IEnumerable<RegApply> regApply = _regApplyRepository.GetList();
            IEnumerable<RegApplyVO> list = regApply.Join(employEmp, x => x.apply_id, y => y.id, (x, y) => new RegApplyVO
            {
                id = x.id,
                name = y.name,
                equip_name = x.equip_name,
                fac_name = x.fac_name,
                price = x.price,
                batch_number = x.batch_number,
                basic_info = x.basic_info,
                buy_reason = x.buy_reason,
                income = x.income,
                apply_id = x.apply_id,
                create_time = x.create_time,
            });
            return list.ToList();
        }
        public IEnumerable<RegApplyVO> GetListUnionSql()
        {
            IEnumerable<RegApplyVO> list = _regApplyRepository.QueryBySql<RegApplyVO>($@"SELECT  rb.*,  ee.name, (SELECT COUNT(*) FROM Employ_Emp WHERE role_id = 2) AS count
                                        FROM  Reg_Buy rb INNER JOIN Employ_Emp ee ON ee.id = rb.apply_id");
            return list;
        }
        public IEnumerable<RegApplyVO> GetListSqlAudit()
        {
            List<RegApplyVO> list = _regApplyRepository.QueryBySql<RegApplyVO>($@"SELECT  rb.*,  ee.name, (SELECT COUNT(*) FROM Employ_Emp WHERE role_id = 2) AS count
                                        FROM  Reg_Buy rb INNER JOIN Employ_Emp ee ON ee.id = rb.apply_id").ToList();
           
            foreach (var item in list)
            {
                 List<RegAudit> regAuditList=_regAuditRepository.GetList().ToList().Where(x=>x.reg_equip_reaearch_id==item.id &&x.recheck_opin==1).ToList();
                int count=regAuditList.Count();
                item.countAudit=count;
            }
            var result = list.ToList();
            return list.ToList();
        }
        public List<RegApplyVO> MakeQuery(IEnumerable<RegApplyVO> list, RegApplyFilter filter, out int count)
        {
            if (filter.applyid != 0 && filter.applyid != null)
            {
                list = list.Where(x => x.apply_id == filter.applyid);
            }
            count = list.Count();
            if (filter.page != 0 && filter.limit != 0)
            {
                list = list.OrderBy(x => x.id).Skip((filter.page - 1) * filter.limit).Take(filter.limit);
            }
            return list.ToList();
        }

        public RegApply GetModel(int id)
        {
            return _regApplyRepository.GetModel(id);
        }

        public bool Update(RegApply regApply)
        {
            return _regApplyRepository.Update(regApply);
        }
        public bool Check(int RegId, string txtAdvice,int userID)
        {
            RegAudit regAudit= _regAuditRepository.GetList().ToList().Where(x => x.reg_equip_reaearch_id == RegId && x.recheck_id == userID).FirstOrDefault();
            regAudit.recheck_opin = 1;
            regAudit.recheck_advice=txtAdvice;
            
            return _regAuditRepository.Update(regAudit);
        }
    }
}
