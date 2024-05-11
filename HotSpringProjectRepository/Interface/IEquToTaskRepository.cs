using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    public interface IEquToTaskRepository
    {
        //sql查询
        IEnumerable<T> QueryBySql<T>(string sql);
        int delete(int id);
        int execBySql(string sql);
    }
}
