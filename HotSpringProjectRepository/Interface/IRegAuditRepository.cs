using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotSpringProject.Entity;

namespace HotSpringProjectRepository.Interface
{
    public interface IRegAuditRepository
    {

        List<RegAudit> GetList();
        int Add(RegAudit movie);
        bool Update(RegAudit movie);
        bool Delete(int id);
        RegAudit GetModel(int id);
    }
}
