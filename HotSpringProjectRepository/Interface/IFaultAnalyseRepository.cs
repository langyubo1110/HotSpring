using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotSpringProject.Entity;
namespace HotSpringProjectRepository.Interface
{
    public interface IFaultAnalyseRepository
    {
        IEnumerable<FaultAnalyse> GetList();
        int Delete(int id);
        int UpDate(FaultAnalyse faultAnalyse);

        int Add(FaultAnalyse faultAnalyse);

        FaultAnalyse GetModel(int id);
        IEnumerable<T> QueryBySql<T>(string sql);
    }
}
