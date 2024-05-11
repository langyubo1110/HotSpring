using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    public interface IGRoomSparePartsRepository
    {
        int Delete(int id);
        bool Update(GRoomSpareParts gRoomSpareParts);
        bool Add(GRoomSpareParts gRoomSpareParts);
        GRoomSpareParts GetModel(int id);
        IEnumerable<GRoomSpareParts> GetList();
    }
}
