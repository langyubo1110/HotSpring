
using DotNet.Utilities;
using HotSpringProject.Entity;
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
    public class RegAuditService : IRegAuditService
    {
        
        private readonly IRegAuditRepository _regAuditRepository;

        public RegAuditService(IRegAuditRepository regAuditRepository)
        {
            _regAuditRepository = regAuditRepository;
        }
        public ResMessage Add(RegAudit regAudit)
        {
            regAudit.create_time = DateTime.Now;
            int flag = _regAuditRepository.Add(regAudit);
            return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
            //throw new NotImplementedException();
        }

        public ResMessage Delete(int id)
        {
            bool flag = _regAuditRepository.Delete(id);
            return flag ? ResMessage.Success() : ResMessage.Fail();
        }

        public List<RegAudit> GetList()
        {
            return _regAuditRepository.GetList().ToList();
        }
        public ResMessage GetListByPager(RegAuditFilter filter)
        {
            //对仓储层数据业务加工
            //分页业务：对仓储层的数据加工
            IEnumerable<RegAudit> list = _regAuditRepository.GetList();
            //List<RegAudit> list = _regAuditRepository.GetList().OrderBy(x => x.id).Skip((page - 1) * limit).Take(limit).ToList();
            int count = 0;
            list = MakeQuery(list, filter, out count);
            return ResMessage.Success(list, count);
        }
        public List<RegAudit> MakeQuery(IEnumerable<RegAudit> list, RegAuditFilter filter, out int count)
        {
            if (filter.recheckid != 0 && filter.recheckid != null)
            {
                list = list.Where(x => x.recheck_id == filter.recheckid);
            }
            count = list.Count();
            if (filter.page != 0 && filter.limit != 0)
            {
                list = list.OrderBy(x => x.id).Skip((filter.page - 1) * filter.limit).Take(filter.limit);
            }
            return list.ToList();
        }

        public RegAudit GetModel(int id)
        {
            return _regAuditRepository.GetModel(id);
        }

        public ResMessage Update(RegAudit regAudit)
        {
            bool flag = _regAuditRepository.Update(regAudit);
            return flag ? ResMessage.Success() : ResMessage.Fail();
        }
    }
}
