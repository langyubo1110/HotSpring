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
    public class GRoomRepairService:IGRoomRepairService
    {
        private readonly IGRoomRepairRepository _gRoomRepairRepository;

        public GRoomRepairService(IGRoomRepairRepository gRoomRepairRepository) 
        {
            _gRoomRepairRepository=gRoomRepairRepository;
        }

        public ResMessage Add(GRoomRepair gRoomRepair)
        {
            gRoomRepair.create_time = DateTime.Now;
            bool result =_gRoomRepairRepository.Add(gRoomRepair);
            return result==true?ResMessage.Success():ResMessage.Fail();
        }

        public ResMessage Delete(int id)
        {
            int flag = _gRoomRepairRepository.Delete(id);
            return flag>0?ResMessage.Success(): ResMessage.Fail();
        }

        public ResMessage GetList()
        {
           List<GRoomRepair> list = _gRoomRepairRepository.GetList().ToList();
            return list==null?ResMessage.Fail(): ResMessage.Success(list);
        }

        public ResMessage GetModel(int id)
        {
            GRoomRepair gRoomRepair =_gRoomRepairRepository.GetModel(id);
            return gRoomRepair==null?ResMessage.Fail() : ResMessage.Success(gRoomRepair);
        }

        public ResMessage Update(GRoomRepair gRoomRepair)
        {
            bool result = _gRoomRepairRepository.Update(gRoomRepair);
            return result==true?ResMessage.Success() : ResMessage.Fail();
        }
    }
}
