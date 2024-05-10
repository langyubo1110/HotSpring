using DotNet.Utilities;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    public interface IEquUpKeepDistributedService
    {
        IEnumerable<EquUpKeepDistributed> GetList();
        ResMessage Add(EquUpKeepDistributed EquUpKeepDistributed);
    }
}
