using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HotSpringProjectService.SystemLogsService;

namespace HotSpringProjectService
{
    public class SystemLogsService : ISystemLogsService
    {

        private readonly ISystemLogsRepository _systemLogsRepository;

        public SystemLogsService(ISystemLogsRepository systemLogsRepository)
        {
            _systemLogsRepository = systemLogsRepository;
        }

        public ResMessage Add(SystemLogs systemlogs)
        {
            systemlogs.create_time = DateTime.Now;
            int flag = _systemLogsRepository.Add(systemlogs);
            return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
        }

        public ResMessage Delete(int Id)
        {
            bool flag = _systemLogsRepository.Delete(Id);
            return flag ? ResMessage.Success() : ResMessage.Fail();
        }

        public List<SystemLogs> GetList()
        {
            return _systemLogsRepository.GetList();
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
            List<SystemLogs> list = _systemLogsRepository.GetList().OrderByDescending(x => x.create_time).Skip((page - 1) * limit).Take(limit).ToList();
            //{data code msg count}
            return ResMessage.Success(list, list.Count);
        }
        public ResMessage Update(SystemLogs systemLogs)
        {
            bool flag = _systemLogsRepository.Update(systemLogs);
            return flag ? ResMessage.Success() : ResMessage.Fail();
        }


    }
}
