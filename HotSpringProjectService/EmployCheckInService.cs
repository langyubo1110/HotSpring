using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService
{
    public class EmployCheckInService : IEmployCheckInService
    {
        private readonly IEmployCheckInRepository _db;

        public EmployCheckInService(IEmployCheckInRepository db)
        {
            _db = db;
        }
        public ResMessage Add(int empId,int type)
        {
            EmployCheckIn EmployCheckIn = new EmployCheckIn();
            int flag = _db.Add(EmployCheckIn, empId,type);
            return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
        }

        public IEnumerable<EmployCheckIn> GetList()
        {
            return _db.GetList();
        }

        public IEnumerable<EmployCheckInVO> GetListUnionSql()
        {
            IEnumerable<EmployCheckInVO> list = _db.QueryBySql<EmployCheckInVO>($@"SELECT ci.*, e.name AS emp_name FROM Employ_Check_In AS ci JOIN Employ_Emp AS e ON ci.emp_id = e.id WHERE ci.check_event = 1;");
            return list;
        }
    }
}
