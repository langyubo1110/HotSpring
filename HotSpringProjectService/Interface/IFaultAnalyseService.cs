using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.VO;
namespace HotSpringProjectService.Interface
{
    public interface IFaultAnalyseService
    {
        ResMessage GetList(int page, int limit);
        ResMessage Delete(int id);
        ResMessage Update(FaultAnalyse faultAnalyse);
        ResMessage Add(FaultAnalyse faultAnalyse);
        ResMessage GetModel(int id);
        ResMessage UpDateByAudit(List<FaultAnalyse> faultAnalyseslist);
        ResMessage GetRecord(int id);
    }
}
