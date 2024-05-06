using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    public interface IRepoBuyService
    {
        ResMessage GetList(int id);
        ResMessage Delete(int id);
        ResMessage Update(RepoBuy repoBuy);
        ResMessage Add(RepoBuy repoBuy);
        ResMessage GetModel(int id);
        ResMessage GetListByPager(int page, int limit, int id);
    }
}
