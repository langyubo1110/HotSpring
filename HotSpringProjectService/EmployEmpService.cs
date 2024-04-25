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
    public class EmployEmpService:IEmployEmpService
    {
        private readonly IEmployEmpRepository _EmployEmpRepository;

        public EmployEmpService(IEmployEmpRepository employemprepository)
        {
            _EmployEmpRepository = employemprepository;
        }
        public ResMessage Add(EmployEmp employemp)
        {
            employemp.create_time = DateTime.Now;
            int flag = _EmployEmpRepository.Add(employemp);
            return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
        }

        public ResMessage Delete(int Id)
        {
            bool flag = _EmployEmpRepository.Delete(Id);
            return flag ? ResMessage.Success() : ResMessage.Fail();
        }

        public List<EmployEmp> GetList()
        {
            return _EmployEmpRepository.GetList();
        }
        /// <summary>
        /// 带分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public ResMessage GetListByPager(int page, int limit)
        {
            //对仓储层数据业务加工
            //分页业务：对仓储层的数据加工
            List<EmployEmp> list = _EmployEmpRepository.GetList().OrderByDescending(x => x.create_time).Skip((page - 1) * limit).Take(limit).ToList();
            //{data code msg count}
            return ResMessage.Success(list, list.Count);
        }
        public ResMessage Update(EmployEmp employemp)
        {
            bool flag = _EmployEmpRepository.Update(employemp);
            return flag ? ResMessage.Success() : ResMessage.Fail();
        }
    }
}
