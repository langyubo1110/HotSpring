using HotSpringProject.Entity.VO;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService
{
    public class EmployTasksService : IEmployTasksService
    {
        private readonly IEquUpkeepTaskRepository _dbUp;

        public EmployTasksService(IEquUpkeepTaskRepository upkeepTaskRepository)
        {
            _dbUp = upkeepTaskRepository;
        }
        //public IEnumerable<EquUpkeepTaskVO> GetList(int reportID)
        //{
        //    return _dbUp.execBySql($"");
        //}

    }
}
