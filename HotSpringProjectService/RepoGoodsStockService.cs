using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNet.Utilities;
using HotSpringProject.Entity.VO;
namespace HotSpringProjectService
{
    public class RepoGoodsStockService: IRepoGoodsStockService
    {
        private readonly IRepoGoodsStockRepository _repoGoodsStockRepository;

        public RepoGoodsStockService(IRepoGoodsStockRepository repo_Goods_StockRepository) {
            _repoGoodsStockRepository = repo_Goods_StockRepository;
        }

        public ResMessage Add(RepoGoodsStock repoGoodsStock)
        {
            repoGoodsStock.create_time = DateTime.Now;
            bool result = _repoGoodsStockRepository.Add(repoGoodsStock);
            if (result)
            {
                return ResMessage.Success("添加成功");
            }
            return ResMessage.Fail();
        }

        public ResMessage Delete(int id)
        {
            int flag = _repoGoodsStockRepository.Delete(id);
            return flag>0 ? ResMessage.Success("删除成功",null,flag):ResMessage.Fail("删除失败");
        }

        public ResMessage GetList(int page,int limit,RepoGoodsStockFilter filter)
        {
            IQueryable<RepoGoodsStock> list = _repoGoodsStockRepository.GetList();
            if (!String.IsNullOrEmpty(filter.goods_name)) 
            {
                list = list.Where(x => x.goods_name.Contains(filter.goods_name));
            }
            if (!String.IsNullOrEmpty(filter.threshold)) 
            {
                list = list.Where(x => x.threshold.Contains(filter.threshold));
            }
            if (!String.IsNullOrEmpty(filter.factory))
            {
                list = list.Where(x => x.factory.Contains(filter.factory));
            }
            int count = list.Count();
            List<RepoGoodsStock> result = list.OrderBy(x => x.id).Skip((page - 1) * limit).Take(limit).ToList();
            return result!=null?ResMessage.Success(result,count):ResMessage.Fail();
        }
        public ResMessage GetModel(int id)
        {
            if (id == 0)
            {
                return ResMessage.Fail("主键不能为空");
            }
            RepoGoodsStock repoGoodsStock = _repoGoodsStockRepository.GetModel(id);
            if (repoGoodsStock == null)
            {
                return ResMessage.Fail("通过该主键获取的实体为空");
            }
            return ResMessage.Success(repoGoodsStock);
        }

        public ResMessage Update(RepoGoodsStock repoGoodsStock)
        {
            repoGoodsStock.create_time = DateTime.Now;
            bool result = _repoGoodsStockRepository.Update(repoGoodsStock);
            if (result)
            {
                return ResMessage.Success("修改成功");
            }
            return ResMessage.Fail();
        }
    }
}
