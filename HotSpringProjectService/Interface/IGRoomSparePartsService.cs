using DotNet.Utilities;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    public interface IGRoomSparePartsService
    {
        ResMessage Delete(int id);
        ResMessage Update(GRoomSpareParts gRoomSpareParts);
        ResMessage Add(GRoomSpareParts gRoomSpareParts);
        ResMessage GetModel(int id);
        ResMessage GetList();
    }
}
