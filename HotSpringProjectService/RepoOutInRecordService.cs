using DotNet.Utilities;
using HotSpringProject.Entity;
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
    public class RepoOutInRecordService:IRepoOutInRecordService
    {
        private readonly IRepoOutInRecordRepository _repoOutInRecordRepository;

        public RepoOutInRecordService(IRepoOutInRecordRepository repoOutInRecordRepository) 
        {
            _repoOutInRecordRepository=repoOutInRecordRepository;
        }

        public ResMessage Add(RepoOutInRecord repoOutInRecordRepository)
        {
            repoOutInRecordRepository.create_time = DateTime.Now;
            bool result = _repoOutInRecordRepository.Add(repoOutInRecordRepository);
            if (result)
            {
                return ResMessage.Success("添加成功");
            }
            return ResMessage.Fail();
        }

        public ResMessage Delete(int id)
        {
            int flag = _repoOutInRecordRepository.Delete(id);
            return flag > 0 ? ResMessage.Success("删除成功", null, flag) : ResMessage.Fail("删除失败");
        }

        public ResMessage GetList(int page, int limit, RepoOutInRecordFilter filter)
        {
            IQueryable<RepoOutInRecord> list = _repoOutInRecordRepository.GetList();
            if (filter.goods_id != 0)
            {
                list = list.Where(x => x.goods_id==filter.goods_id);
            }
            if (filter.outin_person_id!=0)
            {
                list = list.Where(x => x.outin_person_id==filter.outin_person_id);
            }
            if (filter.type!=0)
            {
                list = list.Where(x => x.type==filter.type);
            }
            int count = list.Count();
            List<RepoOutInRecord> result = list.OrderBy(x => x.id).Skip((page - 1) * limit).Take(limit).ToList();
            return result != null ? ResMessage.Success(result, count) : ResMessage.Fail();
        }
        public ResMessage GetModel(int id)
        {
            if (id == 0)
            {
                return ResMessage.Fail("主键不能为空");
            }
            RepoOutInRecord repoOutInRecord = _repoOutInRecordRepository.GetModel(id);
            if (repoOutInRecord == null)
            {
                return ResMessage.Fail("通过该主键获取的实体为空");
            }
            return ResMessage.Success(repoOutInRecord);
        }

        public ResMessage Update(RepoOutInRecord repoOutInRecord)
        {
            repoOutInRecord.create_time = DateTime.Now;
            bool result = _repoOutInRecordRepository.Update(repoOutInRecord);
            if (result)
            {
                return ResMessage.Success("修改成功");
            }
            return ResMessage.Fail();
        }
    }
}
