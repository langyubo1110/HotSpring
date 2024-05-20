using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class SystemPageCorrespondenceRepository : ISystemPageCorrespondenceRepository
    {
        private readonly HotSpringDbContext _db;

        public SystemPageCorrespondenceRepository(HotSpringDbContext keDa2024DbContext)
        {
            _db = keDa2024DbContext;
        }
        public int Add(int roleId, List<int> pageIds)
        {
            var pagesToAdd = pageIds.Select(pageId => new SystemPageCorrespondence { role_id = roleId, pages_id = pageId });
            _db.SystemPageCorrespondences.AddRange(pagesToAdd);
            int flag = _db.SaveChanges();
            return flag;
        }

        public bool Delete(int roleId)
        {
            var isLeader = _db.EmployRole.Where(r => r.id == roleId).Select(r => r.is_leader).FirstOrDefault();
            // 获取具有相同 isLeader 值的所有 roleId
            var rolesToDelete = _db.EmployRole.Where(r => r.is_leader == isLeader).Select(r => r.id).ToList();
            // 删除所有 rolesToDelete 对应的 SystemPageCorrespondences 表内的项
            var pagesToDelete = _db.SystemPageCorrespondences.Where(p => rolesToDelete.Contains(p.role_id));
            _db.SystemPageCorrespondences.RemoveRange(pagesToDelete);
            int flag = _db.SaveChanges();
            if (flag > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<SystemPageCorrespondence> GetList()
        {
            return _db.SystemPageCorrespondences.ToList();
        }

        public SystemPageCorrespondence GetModel(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> QueryBySql<T>(string sql)
        {
            return _db.Database.SqlQuery<T>(sql).ToList();
        }

        public bool Update(SystemPageCorrespondence sp)
        {
            throw new NotImplementedException();
        }

      
    }
}
