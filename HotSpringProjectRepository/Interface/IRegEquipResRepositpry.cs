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
    }
}
