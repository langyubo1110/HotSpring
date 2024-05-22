using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    public interface IEmployMessageRepository
    {
        List<EmployMessage> GetList();
        int Add(EmployMessage employMessage);
        IEnumerable<T> QueryBySql<T>(string sql);
        int AddRange(List<EmployMessage> messages);
        EmployMessage GetModel(int id);
    }
}
