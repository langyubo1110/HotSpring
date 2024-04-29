using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNet.Utilities;
using HotSpringProject.Entity;
namespace HotSpringProjectService.Interface
{
    public interface IRepairTaskReportService
    { 
        ResMessage getlist(int page,int limit);
        bool delete(int id);
        ResMessage update(RepaieTaskReport repaieTaskReport);

        ResMessage add(RepaieTaskReport repaieTaskReport);
        ResMessage getmodel(int id);
    }
}
