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
        ResMessage GetList(int page,int limit);
        bool Delete(int id);
        ResMessage UpDate(RepaieTaskReport repaieTaskReport);

        ResMessage Add(RepaieTaskReport repaieTaskReport);
        ResMessage GetModel(int id);
        ResMessage GetEqumentName(int id);
    }
}
