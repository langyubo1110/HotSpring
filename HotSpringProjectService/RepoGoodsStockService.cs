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
using HotSpringProject.Entity.DTO;
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
            repoGoodsStock.update_time = DateTime.Now;
            int last_id = _repoGoodsStockRepository.Add(repoGoodsStock);
            return last_id==0 ? ResMessage.Fail(): ResMessage.Success("添加成功");
            
        }

        public ResMessage Delete(int id)
        {
            int flag = _repoGoodsStockRepository.Delete(id);
            return flag>0 ? ResMessage.Success("删除成功",null,flag):ResMessage.Fail("删除失败");
        }

        public ResMessage GetListByPager(int page,int limit,RepoGoodsStockFilter filter)
        {
            IQueryable<RepoGoodsStock> list = _repoGoodsStockRepository.GetListByPager();
            if (!String.IsNullOrEmpty(filter.goods_name)) 
            {
                list = list.Where(x => x.goods_name.Contains(filter.goods_name));
            }
            if (filter.goods_number != null)
            {
                list = list.Where(x => x.goods_number==filter.goods_number);
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
        public ResMessage GetList(string keywords,int? goods_type)
        {
            //自动补全
            if (!string.IsNullOrEmpty(keywords))
            {
                List<RepoGoodsStock> list = _repoGoodsStockRepository.Getlist().Where(x => x.goods_name.Contains(keywords)).ToList();
                int count = list.Count;
                return list == null ? ResMessage.Fail() : ResMessage.Success(list, count);
            }
            //查全表
            else if (goods_type != null)
            {
                List<RepoGoodsStock> list = _repoGoodsStockRepository.Getlist().Where(x => x.goods_type == goods_type).ToList();
                int count = list.Count;
                return list == null ? ResMessage.Fail() : ResMessage.Success(list, count);
            }
            else 
            {
                List<RepoGoodsStock> list = _repoGoodsStockRepository.Getlist().ToList();
                int count = list.Count;
                return list == null ? ResMessage.Fail() : ResMessage.Success(list, count);
            }
        }
    }
}
