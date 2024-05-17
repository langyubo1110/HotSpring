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
    public class EmployMessageService : IEmployMessageService
    {
        private readonly IEmployMessageRepository _EmployMessageRepository;

        public EmployMessageService(IEmployMessageRepository EmployMessagerepository)
        {
            _EmployMessageRepository = EmployMessagerepository;
        }
        public List<EmployMessage> GetList()
        {
            return _EmployMessageRepository.GetList();
        }
    }
}
