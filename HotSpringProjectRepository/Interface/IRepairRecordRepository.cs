using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    public  interface IRepairRecordRepository
    {
        IQueryable<RepairRecord> GetList();
        int Delete(int id);
        int UpDate(RepairRecord  repairRecord);
        int Add(RepairRecord  repairRecord);

        RepairRecord GetModel(int id);
    }
}
