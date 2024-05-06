using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    // 裴晨旭 2024-04-25 
    public interface IRepoGoodsStockRepository
    {
        IQueryable<RepoGoodsStock> GetListByPager();
        int Delete(int id);
        bool Update(RepoGoodsStock repoGoodsStock);
        int Add(RepoGoodsStock repoGoodsStock);
        RepoGoodsStock GetModel(int id);
        IEnumerable<RepoGoodsStock> Getlist();
    }
}
