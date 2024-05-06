using DotNet.Utilities;
using HotSpringProject.Entity;
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
    public class SystemPagesService:ISystemPagesService
    {
        private readonly ISystemPagesRepository _systemPagesRepository;
        private readonly ISystemModuleRepository _systemModuleRepository;

        public SystemPagesService(ISystemPagesRepository systemPagesRepository, ISystemModuleRepository systemModuleRepository)
        {
            _systemPagesRepository = systemPagesRepository;
            _systemModuleRepository = systemModuleRepository;
        }
        public ResMessage Add(SystemPages systemPages)
        {
            systemPages.create_time = DateTime.Now;
            int flag = _systemPagesRepository.Add(systemPages);
            return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
        }
        public ResMessage Delete(int Id)
        {
            bool flag = _systemPagesRepository.Delete(Id);
            return flag ? ResMessage.Success() : ResMessage.Fail();
        }
        public IEnumerable<SystemPages> GetList()
        {
            return _systemPagesRepository.GetList();
        }
        public ResMessage GetListByPager(PagesFilter filter)
        {
            IEnumerable<PagesVO> list = GetListUnion();
            int count1 = 0;
            list = MakeQuery(list, filter, out count1);
            return ResMessage.Success(list, count1);
        }
        public IEnumerable<PagesVO> GetListUnion()
        {
            IEnumerable<SystemModule> systemModules = _systemModuleRepository.GetList();
            IEnumerable<SystemPages> systemPages = _systemPagesRepository.GetList();
            IEnumerable<PagesVO> list = systemPages.Join(systemModules, x => x.module_id, y => y.id, (x, y) => new PagesVO
            {
                id = x.id,
                page_name = x.page_name,
                page_address =x.page_address,
                icon = x.icon,
                module_id = x.module_id,
                module_name=y.module_name,
                create_time = x.create_time,
            });
            return list.ToList();
        }
        public IEnumerable<PagesVO> MakeQuery(IEnumerable<PagesVO> list, PagesFilter filter, out int count1)
        {
            if (!string.IsNullOrEmpty(filter.page_name))
            {
                list = list.Where(x => x.page_name.Contains(filter.page_name));
            }
            if (filter.module_id != 0)
            {
                list = list.Where(x => x.module_id == filter.module_id);
            }
            count1 = list.Count();
            //开启分页
            if (filter.page != 0 && filter.limit != 0)
            {
                list = list.OrderByDescending(x => x.create_time).Skip((filter.page - 1) * filter.limit).Take(filter.limit);
            }
            return list.ToList();
        }
        public ResMessage getModel(int id)
        {
            SystemPages model = _systemPagesRepository.GetModel(id);
            return model != null ? ResMessage.Success(model) : ResMessage.Fail();
        }

        public ResMessage Update(SystemPages systemPages)
        {
            bool flag = _systemPagesRepository.Update(systemPages);
            return flag ? ResMessage.Success() : ResMessage.Fail();
        }
        public IEnumerable<(int Id, string Name)> GetModuleList()
        {
            return _systemPagesRepository.GetModuleList();
        }

        //public IEnumerable<SystemPages> GetRoleList(int roleId)
        //{
        //    return _systemPagesRepository.GetList().Where(x=>x.id)
        //}
    }
}
