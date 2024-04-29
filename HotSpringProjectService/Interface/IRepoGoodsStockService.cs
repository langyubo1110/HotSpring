
using DotNet.Utilities;
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
        ResMessage GetList(int page,int limit);
        ResMessage Delete(int id);
        ResMessage Update(RepoGoodsStock repoGoodsStock);
        ResMessage Add(RepoGoodsStock repoGoodsStock);
        ResMessage GetModel(int id);

    }
}
