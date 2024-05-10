using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.VO;
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
        private readonly IRepoOutInRecordRepository _repoOutInRecordRepository;
        private readonly IGRoomSparePartsRepository _gRoomSparePartsRepository;

        public GRoomRepairService(IGRoomRepairRepository gRoomRepairRepository,IRepoOutInRecordRepository repoOutInRecordRepository,IGRoomSparePartsRepository gRoomSparePartsRepository) 
        {
            _gRoomRepairRepository=gRoomRepairRepository;
            _repoOutInRecordRepository = repoOutInRecordRepository;
            _gRoomSparePartsRepository = gRoomSparePartsRepository;
        }

        public ResMessage Add(GRoomRepair gRoomRepair, List<GRoomSparePartsVO> gRoomSPVOlist)
        {
            _repoOutInRecordRepository.TransBegin();
            try
            {
                //接收维修任务上报表执行插入操作后最新数据的ID
                gRoomRepair.create_time = DateTime.Now;
                int last_id = _gRoomRepairRepository.Add(gRoomRepair);
                foreach (GRoomSparePartsVO gRoomSparePartsVO in gRoomSPVOlist)
                {
                    GRoomSpareParts gRoomSpareParts = new GRoomSpareParts();
                    gRoomSpareParts.repair_id = last_id;
                    gRoomSpareParts.spare_parts_id = gRoomSparePartsVO.id;
                    gRoomSpareParts.used_number = gRoomSparePartsVO.used_number;
                    gRoomSpareParts.create_time = DateTime.Now;
                    //判断oi_number是否为0，不为0产生入库记录
                    if (gRoomSparePartsVO.oi_number != 0)
                    {
                        RepoOutInRecord repoOutInRecord = new RepoOutInRecord();
                        //入库记录的商品id就是备件id
                        repoOutInRecord.goods_id = gRoomSparePartsVO.spare_parts_id;
                        //维修任务上报人就是剩余备件的入库人
                        repoOutInRecord.outin_person_id = gRoomRepair.reporter_id;
                        repoOutInRecord.type = 0;
                        repoOutInRecord.audit = 0;
                        //操作前的库存数量为库存表的库存数量
                        repoOutInRecord.start_number = gRoomSparePartsVO.goods_number;
                        //剩余备件数量就是这里的入库数量
                        repoOutInRecord.oi_number = gRoomSparePartsVO.oi_number;
                        repoOutInRecord.end_number = gRoomSparePartsVO.goods_number + gRoomSparePartsVO.oi_number;
                        repoOutInRecord.recipient_id = 1;
                        repoOutInRecord.create_time = DateTime.Now;
                        _repoOutInRecordRepository.Add(repoOutInRecord);
                    }
                    _gRoomSparePartsRepository.Add(gRoomSpareParts);
                }
                _repoOutInRecordRepository.Commit();
                return ResMessage.Success();
            }
            catch (Exception ex)
            {
                _repoOutInRecordRepository.Rollback();
                return ResMessage.Fail(ex.Message);
            }

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

        public ResMessage GetListById(int id)
        {
            List<GRoomRepair> list = _gRoomRepairRepository.GetList().Where(x => x.reporter_id ==id ).ToList();
            return list == null ? ResMessage.Fail() : ResMessage.Success(list);
        }

        public ResMessage GetListId(int id)
        {
            List<GRoomRepair> list = _gRoomRepairRepository.GetList().Where(x => x.id == id).ToList();
            return list == null ? ResMessage.Fail() : ResMessage.Success(list);
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
