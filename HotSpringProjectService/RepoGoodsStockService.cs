using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNet.Utilities;
namespace HotSpringProjectService
{
    public class RepoGoodsStockService: IRepoGoodsStockService
    {
        private readonly IRepoGoodsStockRepository _repoGoodsStockRepository;

        public RepoGoodsStockService(IRepoGoodsStockRepository repo_Goods_StockRepository) {
            _repoGoodsStockRepository = repo_Goods_StockRepository;
        }

        public bool Add(RepoGoodsStock repoGoodsStock)
        {
            return _repoGoodsStockRepository.Add(repoGoodsStock);
        }

        public int Delete(int id)
        {
            int flag = _repoGoodsStockRepository.Delete(id);
            return flag;
        }

        public IQueryable<RepoGoodsStock> GetList()
        {
            IQueryable<RepoGoodsStock> list = _repoGoodsStockRepository.GetList();
            return list;
        }

        public RepoGoodsStock GetModel(int id)
        {
            return _repoGoodsStockRepository.GetModel(id);
        }

        public bool Update(RepoGoodsStock repoGoodsStock)
        {
           return _repoGoodsStockRepository.Update(repoGoodsStock);
        }
    }
}
