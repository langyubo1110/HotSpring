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
        IQueryable<IFaultAnalyseRepository> GetList();
        int Delete(int id);
        int UpDate(FaultAnalyse faultAnalyse);

        int Add(FaultAnalyse faultAnalyse);

        FaultAnalyseRepository GetModel(int id);
        IEnumerable<FaultAnalyse> Add();
    }
}
