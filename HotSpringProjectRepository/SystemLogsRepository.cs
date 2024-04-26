
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
        public SystemLogsRepository(HotSpringDbContext hotSpringDb)
        {
            List<RepoGoodsStock> list = hotSpringDb.Repo_Goods_Stock.ToList();
        }
    }
}
