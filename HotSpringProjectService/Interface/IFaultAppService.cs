using DotNet.Utilities;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    public interface IFaultAppService
    {
        ResMessage GetList(int page, int limit);
        bool Delete(int id);
        ResMessage UpDate(FaultApp faultApp);
        ResMessage Add(FaultApp faultApp);
        ResMessage GetModel(int id);
        ResMessage StopAndAdd(int eid, FaultAnalyse faultAnalyse);
        ResMessage GetListByRole();
        ResMessage GetRepairList(string name,int page,int limit,int? fault_app_id);
        ResMessage GetAnalyseContents(int id);
    }
}
