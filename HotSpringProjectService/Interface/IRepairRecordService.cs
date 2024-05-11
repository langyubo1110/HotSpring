using DotNet.Utilities;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    public interface IRepairRecordService
    {
        ResMessage GetList(int page, int limit);
        bool Delete(int id);
        ResMessage UpDate(RepairRecord repairRecord);

        ResMessage Add(RepairRecord  repairRecord);
        ResMessage GetModel(int id);

    }
}
