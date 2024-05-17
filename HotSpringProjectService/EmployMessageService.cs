using DotNet.Utilities;
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
        private readonly IEmployEmpRepository _dbEmp;

        public EmployMessageService(IEmployMessageRepository EmployMessagerepository, IEmployEmpRepository employEmpRepository)
        {
            _EmployMessageRepository = EmployMessagerepository;
            _dbEmp = employEmpRepository;
        }
        public List<EmployMessage> GetList()
        {
            return _EmployMessageRepository.GetList();
        }
        public ResMessage Add(EmployMessageVO employMessagevo)
        {
            if (employMessagevo.apply_id == null || employMessagevo.apply_id == 0)
            {
                EmployMessage employMessage = new EmployMessage();
                employMessage.create_time = DateTime.Now;
                employMessage.send_time = DateTime.Now;
                employMessage.link = "abcd";
                employMessage.part = employMessagevo.part;
                employMessage.sender_id = employMessagevo.sender_id;
                employMessage.recipients_id = employMessagevo.recipients_id;
                int flag = _EmployMessageRepository.Add(employMessage);
                return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
            }
            else
            {
                int? roleId = employMessagevo.apply_id;//得到角色ID，下一步找对应的人
                List<EmployEmp> employ = _dbEmp.GetList().Where(x => x.role_id == roleId).ToList();
                List<EmployMessage> messagesToAdd = employ.Select(item => new EmployMessage
                {
                    recipients_id = item.id,
                    sender_id = employMessagevo.sender_id,
                    part = employMessagevo.part,
                    create_time = DateTime.Now,
                    send_time = DateTime.Now,
                    link = "abcd"
                }).ToList();

                int flag = _EmployMessageRepository.AddRange(messagesToAdd);
                return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
            }

        }
    }
}
