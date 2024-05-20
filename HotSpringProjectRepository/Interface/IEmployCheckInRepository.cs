using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    public interface IEmployCheckInRepository
    {
        int Add(EmployCheckIn EmployCheckIn,int empId,int type);
        IEnumerable<EmployCheckIn> GetList();
        IEnumerable<T> QueryBySql<T>(string sql);
        bool Verify(int empId);

    }
}
