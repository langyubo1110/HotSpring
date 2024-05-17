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
using HotSpringProjectRepository;
using System.Diagnostics.Eventing.Reader;
namespace HotSpringProjectService
{
    public class RepoGoodsStockService: IRepoGoodsStockService
    {
        private readonly IRepoGoodsStockRepository _repoGoodsStockRepository;
        private readonly IRepoOutInRecordRepository _repoOutInRecordRepository;

        public RepoGoodsStockService(IRepoGoodsStockRepository repo_Goods_StockRepository,IRepoOutInRecordRepository repoOutInRecordRepository) {
            _repoGoodsStockRepository = repo_Goods_StockRepository;
            _repoOutInRecordRepository=repoOutInRecordRepository;
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
            if (filter.goods_type != null)
            {
                list = list.Where(x => x.goods_type == filter.goods_type);
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
           
            return repoGoodsStock==null? ResMessage.Fail("通过该主键获取的实体为空"):ResMessage.Success(repoGoodsStock);
        }

        public ResMessage Update(RepoGoodsStock repoGoodsStock)
        {
            repoGoodsStock.update_time = DateTime.Now;
            bool result = _repoGoodsStockRepository.Update(repoGoodsStock);
            return result==true? ResMessage.Success("修改成功"):ResMessage.Fail();
        }
        //归还备件插入出入库记录表（入库操作）
        //通过出入库记录页的审核按钮进行库存数量更新
        //同时这条入库记录的审核状态更新为1（0未审核）
        public ResMessage UpdateByAudit(RepoGoodsStockDTO repoGoodsStockDTO)
        {
            //EF事务
            _repoOutInRecordRepository.TransBegin();
            try
            {
                //库存数量更新
                RepoGoodsStock repoGoodsStock = _repoGoodsStockRepository.GetModel(repoGoodsStockDTO.id);
                repoGoodsStock.goods_number += repoGoodsStockDTO.oi_number;
                repoGoodsStock.update_time = DateTime.Now;
                _repoGoodsStockRepository.Update(repoGoodsStock);
                //入库记录更新审核状态
                RepoOutInRecord repoOutInRecord = _repoOutInRecordRepository.GetModel(repoGoodsStockDTO.oi_id);
                repoOutInRecord.audit = 1;
                _repoOutInRecordRepository.Update(repoOutInRecord);
                _repoOutInRecordRepository.Commit();
                return ResMessage.Success();
            }
            catch (Exception ex)
            {
                _repoOutInRecordRepository.Rollback();
                return ResMessage.Fail(ex.Message);
            }
        }
        public ResMessage GetList(string keywords,RepoGoodsStockFilter filter)
        {
            IEnumerable<RepoGoodsStock> ilist = _repoGoodsStockRepository.Getlist();
            //自动补全
            if (!string.IsNullOrEmpty(keywords))
            {
                List<RepoGoodsStock> list = ilist.Where(x => x.goods_name.Contains(keywords)).ToList();
                int count = list.Count;
                return list == null ? ResMessage.Fail() : ResMessage.Success(list, count);
            }
            else 
            {
                List<RepoGoodsStock> list = ilist.ToList();
                int count = list.Count;
                return list == null ? ResMessage.Fail() : ResMessage.Success(list, count);
            }
        }
    }
}
