using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class EquToTaskRepository : IEquToTaskRepository
    {
        private readonly HotSpringDbContext _db;

        //构造函数注入
        public EquToTaskRepository(HotSpringDbContext HotSpringDbContext)
        {
            _db = HotSpringDbContext;
        }

        public int delete(int id)
        {
            // 查询要删除的数据
            var dataToDelete = _db.EquToTask.Where(x=>x.equ_id==id).ToList();

            // 删除数据
            _db.EquToTask.RemoveRange(dataToDelete);

            // 提交更改到数据库
            int flag = _db.SaveChanges();
            return flag;
        }

        public IEnumerable<T> QueryBySql<T>(string sql)
        {
            return _db.Database.SqlQuery<T>(sql);
        }

        public int execBySql(string sql)
        {
            int flag= _db.Database.ExecuteSqlCommand(sql);
            return flag;
        }
    }
}
