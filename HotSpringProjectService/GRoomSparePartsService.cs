using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectRepository;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService
{
    public class GRoomSparePartsService:IGRoomSparePartsService
    {
        private readonly IGRoomSparePartsRepository _gRoomSparePartsRepository;

        public GRoomSparePartsService(IGRoomSparePartsRepository gRoomSparePartsRepository)
        {
            _gRoomSparePartsRepository=gRoomSparePartsRepository;
        }

        public ResMessage Add(GRoomSpareParts gRoomSpareParts)
        {
            throw new NotImplementedException();
        }

        public ResMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResMessage GetList()
        {
            throw new NotImplementedException();
        }

        public ResMessage GetModel(int id)
        {
            throw new NotImplementedException();
        }

        public ResMessage Update(GRoomSpareParts gRoomSpareParts)
        {
            throw new NotImplementedException();
        }
    }
}
