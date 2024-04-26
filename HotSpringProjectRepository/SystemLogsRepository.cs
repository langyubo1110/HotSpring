using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class SystemLogsRepository:ISystemLogsRepository
    {
        private readonly HotSpringDbContext _db;

        public SystemLogsRepository(HotSpringDbContext hotSpringDbContext)
        {
            _db = hotSpringDbContext;
        }
        public int Add(SystemLogs movie)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<SystemLogs> GetList()
        {
            return _db.SystemLogs.ToList();
        }

        public SystemLogs GetModel(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(SystemLogs movie)
        {
            throw new NotImplementedException();
        }
    }
}
