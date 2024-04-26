using DotNet.Utilities;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    public interface IRegAuditService
    {
        List<RegAudit> GetList();
        ResMessage GetListByPager(int page, int limit);
        ResMessage Add(RegAudit movie);
        ResMessage Update(RegAudit movie);
        ResMessage Delete(int id);
        RegAudit GetModel(int id);
    }
}
