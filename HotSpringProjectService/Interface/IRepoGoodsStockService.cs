using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    public interface IRepoGoodsStockService
    {
        IQueryable<RepoGoodsStock> GetList();
        int Delete(int id);
        bool Update(RepoGoodsStock repoGoodsStock);
        bool Add(RepoGoodsStock repoGoodsStock);
        RepoGoodsStock GetModel(int id);

    }
}
