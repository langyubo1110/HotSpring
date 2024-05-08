using DotNet.Utilities;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    public interface IGRoomRepairRepository
    {
        int Delete(int id);
        bool Update(GRoomRepair gRoomRepair);
        bool Add(GRoomRepair gRoomRepair);
        GRoomRepair GetModel(int id);
        IEnumerable<GRoomRepair> GetList();
    }
}
