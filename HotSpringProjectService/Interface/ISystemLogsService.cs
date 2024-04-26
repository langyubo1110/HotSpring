
using DotNet.Utilities;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    public interface ISystemLogsService
    {
        List<SystemLogs> GetList();

        ResMessage GetListByPager(int page, int limit);

        ResMessage Add(SystemLogs movies);
        ResMessage Delete(int Id);
        ResMessage Update(SystemLogs movies);
    }
}
