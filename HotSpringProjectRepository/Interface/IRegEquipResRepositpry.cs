using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    public interface IRegEquipResRepositpry
    {
        IEnumerable<RegEquipRes> GetList();
        
        int Add(RegEquipRes regEquipRes);
        bool Update(RegEquipRes regEquipRes);
        bool Delete(int id);
        RegEquipRes GetModel(int id);
        IEnumerable<T> QueryBySql<T>(string sql);
    }
}
