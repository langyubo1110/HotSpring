using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.DTO;
using HotSpringProject.Entity.VO;
using HotSpringProjectRepository;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService
{
    public class RepoBuyService : IRepoBuyService
    {
        private readonly IRepoBuyRepository _repoBuyRepository;

        public RepoBuyService(IRepoBuyRepository repoBuyRepository)
        {
            _repoBuyRepository = repoBuyRepository;
        }

        public ResMessage Add(RepoBuy repoBuy)
        {
            repoBuy.create_time = DateTime.Now;
            bool result = _repoBuyRepository.Add(repoBuy);
            return result == true ? ResMessage.Success("添加成功") : ResMessage.Fail();
        }

        public ResMessage Delete(int id)
        {
            int flag =_repoBuyRepository.Delete(id);
            return flag>0?ResMessage.Success():ResMessage.Fail();
        }
        //通过商品id查符合条件的所有采购记录
        public ResMessage GetList(int? id)
        {
            if (id ==null)
            {
                List<RepoBuy> list = _repoBuyRepository.GetList().Where(x => x.goods_id == id).ToList();
                return list == null ? ResMessage.Fail() : ResMessage.Success(list);
            }
            else {
                List<RepoBuy> list = _repoBuyRepository.GetList().ToList();
                return list == null ? ResMessage.Fail() : ResMessage.Success(list);
            }
        }
        public ResMessage GetListByPager(int page,int limit,int id)
        {
            IEnumerable<RepoBuy> list = _repoBuyRepository.GetList().Where(x => x.goods_id == id).ToList();
            int count = list.Count();
            List<RepoBuy> result = list.OrderBy(x=>x.create_time).Skip((page-1)*limit).Take(limit).ToList();
            return list == null ? ResMessage.Fail() : ResMessage.Success(result, count);
        }
        public ResMessage GetModel(int id)
        {
            RepoBuy repoBuy = _repoBuyRepository.GetModel(id);
            return repoBuy == null ? ResMessage.Fail() : ResMessage.Success();
        }

        public ResMessage Update(RepoBuy repoBuy)
        {
            bool result= _repoBuyRepository.Update(repoBuy);
            return result==true ? ResMessage.Success() : ResMessage.Fail();
        }
    }
}
