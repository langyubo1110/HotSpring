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
    public class EquUpKeepDistributedService : IEquUpKeepDistributedService
    {
        private readonly IEquUpKeepDistributedService _db;
        public EquUpKeepDistributedService(IEquUpKeepDistributedService equUpKeepDistributed)
        {
            _db = equUpKeepDistributed;
        }
        public ResMessage Add(EquUpKeepDistributed EquUpKeepDistributed)
        {
            ResMessage res = _db.Add(EquUpKeepDistributed);
            return res;
        }

        public IEnumerable<EquUpKeepDistributed> GetList()
        {
            return _db.GetList();
        }
    }
}
