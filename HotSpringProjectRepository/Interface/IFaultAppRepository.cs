using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotSpringProject.Entity;
namespace HotSpringProjectRepository.Interface
{
    public interface IFaultAppRepository
    {
        IQueryable<FaultApp> GetList();
        int Delete(int id);
        int UpDate(FaultApp faultApp);

        int Add(FaultApp faultApp);
        IEnumerable<T> QueryBySql<T>(string sql);
        FaultApp GetModel(int id);
    }
}
