
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
    public interface IRepoGoodsStockService
    {
        ResMessage GetListByPager(int page,int limit,RepoGoodsStockFilter repoGoodsStockFilter);
        ResMessage Delete(int id);
        ResMessage Update(RepoGoodsStock repoGoodsStock);
        ResMessage Add(RepoGoodsStock repoGoodsStock);
        ResMessage GetModel(int id);
        ResMessage GetList(string keywords);

    }
}
