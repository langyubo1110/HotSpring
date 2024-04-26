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
            return _regAuditRepository.GetList();
        }
        public ResMessage GetListByPager(int page, int limit)
        {
            //对仓储层数据业务加工
            //分页业务：对仓储层的数据加工
            List<RegAudit> list = _regAuditRepository.GetList().OrderBy(x => x.id).Skip((page - 1) * limit).Take(limit).ToList();
            //{data code msg count}
            return ResMessage.Success(list, list.Count);
        }

        public RegAudit GetModel(int id)
        {
            throw new NotImplementedException();
        }

        public ResMessage Update(RegAudit regAudit)
        {
            bool flag = _regAuditRepository.Update(regAudit);
            return flag ? ResMessage.Success() : ResMessage.Fail();
        }
    }
}
