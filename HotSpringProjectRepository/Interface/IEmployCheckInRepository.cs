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
    }
}
