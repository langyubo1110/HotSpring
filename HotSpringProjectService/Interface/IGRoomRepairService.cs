using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    public interface IGRoomRepairService
    {
        ResMessage Delete(int id);
        ResMessage Update(GRoomRepair gRoomRepair);
        ResMessage Add(GRoomRepair gRoomRepair,List<GRoomSparePartsVO> gRoomSPVOlist);
        ResMessage GetModel(int id);
        ResMessage GetList();
        ResMessage GetListById(int id);
    }
}
