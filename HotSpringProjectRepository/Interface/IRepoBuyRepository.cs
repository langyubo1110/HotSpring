using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    public interface IRepoBuyRepository
    {
        int Delete(int id);
        bool Update(RepoBuy repoBuy);
        bool Add(RepoBuy repoBuy);
        RepoBuy GetModel(int id);
        IEnumerable<RepoBuy> GetList();
    }
}
