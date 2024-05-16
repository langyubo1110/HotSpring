using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.DTO;
using HotSpringProject.Entity.VO;
using HotSpringProjectRepository;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using AutoMapper;
namespace HotSpringProjectService
{
    public class RepoOutInRecordService:IRepoOutInRecordService
    {
        private readonly IRepoOutInRecordRepository _repoOutInRecordRepository;
        private readonly IRepoGoodsStockRepository _repoGoodsStockRepository;
        private readonly IRepoBuyRepository _repoBuyRepository;

        public RepoOutInRecordService(IRepoOutInRecordRepository repoOutInRecordRepository,IRepoGoodsStockRepository repoGoodsStockRepository,IRepoBuyRepository repoBuyRepository) 
        {
            _repoOutInRecordRepository=repoOutInRecordRepository;
            _repoGoodsStockRepository=repoGoodsStockRepository;
            _repoBuyRepository=repoBuyRepository;
        }
        //商品出入库表添加
        //出库商品库存表更新
        //入库商品库存表如果没有该商品,则添加一条数据
        //出库商品采购表不操作
        //入库商品采购表添加一条数据
        public ResMessage Add(RepoGoodsStockDTO repoGoodsStockDTO)
        {
            //使用EF事务，保证原子性
            _repoOutInRecordRepository.TransBegin();
            try
            {
                //商品库存表最新插入的数据的ID
                //默认为0,插入时赋值
                int last_id=0;
                //创建商品出入库表实体
                RepoOutInRecord repoOutInRecord = new RepoOutInRecord();
                //出库
                if (repoGoodsStockDTO.type == 1)
                {
                    //如果为出库，商品库存表更新
                    RepoGoodsStock repoGoodsStock = _repoGoodsStockRepository.GetModel(repoGoodsStockDTO.id);
                    repoGoodsStock.threshold = repoGoodsStockDTO.threshold;
                    repoGoodsStock.update_time = DateTime.Now;
                    repoGoodsStock.goods_number = repoGoodsStockDTO.goods_number - repoGoodsStockDTO.oi_number;
                    if (repoGoodsStock.goods_number < 0)
                        return ResMessage.Fail();
                    _repoGoodsStockRepository.Update(repoGoodsStock);
                    //库存表更新
                    //出入库表实体的商品id赋值
                    repoOutInRecord.goods_id = repoGoodsStockDTO.id;//商品ID
                    repoOutInRecord.end_number = repoGoodsStock.goods_number;
                }
                //入库
                else
                {
                    //入库，采购表插入
                    //创建商品采购表实体
                    RepoBuy repoBuy = new RepoBuy();
                    //如果为入库商品库存表判断商品是否存在
                    //1.存在,更新
                    if (repoGoodsStockDTO.id !=0)
                    {
                        RepoGoodsStock repoGoodsStock = _repoGoodsStockRepository.GetModel(repoGoodsStockDTO.id);
                        repoGoodsStock.threshold = repoGoodsStockDTO.threshold;
                        repoGoodsStock.update_time = DateTime.Now;
                        repoGoodsStock.goods_number = repoGoodsStockDTO.goods_number + repoGoodsStockDTO.oi_number;
                        _repoGoodsStockRepository.Update(repoGoodsStock);
                        //库存表更新
                        //出入库表实体的商品id赋值
                        repoOutInRecord.goods_id = repoGoodsStockDTO.id;//商品ID
                        //采购表实体的商品id赋值
                        repoBuy.goods_id = repoGoodsStockDTO.id;
                        repoOutInRecord.end_number = repoGoodsStock.goods_number;
                    }
                    //2.不存在,插入
                    else
                    {
                        RepoGoodsStock repoGoodsStock = new RepoGoodsStock();
                        repoGoodsStock.goods_name= repoGoodsStockDTO.goods_name;
                        repoGoodsStock.goods_number= repoGoodsStockDTO.oi_number;
                        repoGoodsStock.create_time = DateTime.Now;
                        repoGoodsStock.update_time = DateTime.Now;
                        repoGoodsStock.imgurl = repoGoodsStockDTO.imgurl;
                        repoGoodsStock.threshold= repoGoodsStockDTO.threshold;
                        repoGoodsStock.factory= repoGoodsStockDTO.factory;
                        repoGoodsStock.goods_type= repoGoodsStockDTO.goods_type;
                        _repoGoodsStockRepository.Add(repoGoodsStock);
                        last_id = repoGoodsStock.id;
                        //库存表插入
                        //出入库表实体的商品id赋值最新插入库存表数据的id
                        repoOutInRecord.goods_id = last_id;//商品ID
                        repoOutInRecord.end_number = repoGoodsStock.goods_number;
                        //采购表实体id赋值库存表最新插入id
                        repoBuy.goods_id = last_id;
                    }
                    //如果为入库，商品采购表插入
                    repoBuy.price = repoGoodsStockDTO.price;
                    repoBuy.create_time = DateTime.Now;
                    repoBuy.buyer = repoGoodsStockDTO.buyer;
                    repoBuy.buyer_phone = repoGoodsStockDTO.buyer_phone;
                    repoBuy.goods_number = repoGoodsStockDTO.oi_number;
                    _repoBuyRepository.Add(repoBuy);
                }
                //商品出入库表实体补全
                //插入
                repoOutInRecord.create_time = DateTime.Now;
                repoOutInRecord.oi_number = repoGoodsStockDTO.oi_number;//出入库数量
                repoOutInRecord.outin_person_id = repoGoodsStockDTO.outin_person_id;
                repoOutInRecord.type = repoGoodsStockDTO.type;//出入库
                repoOutInRecord.start_number = repoGoodsStockDTO.goods_number;
                repoOutInRecord.recipient_id = repoGoodsStockDTO.recipient_id;
                repoOutInRecord.audit = 1;
                _repoOutInRecordRepository.Add(repoOutInRecord);
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
            int flag = _repoOutInRecordRepository.Delete(id);
            return flag > 0 ? ResMessage.Success("删除成功", null, flag) : ResMessage.Fail("删除失败");
        }

        public ResMessage GetList()
        {
            List<RepoOutInRecord> list = _repoOutInRecordRepository.GetList().ToList();
            int count = list.Count;
            return list != null ? ResMessage.Success(list,count) : ResMessage.Fail();
        }
        //此方法为获取使用备件列表的方法
        //通过上报人id和当前时间的日期从出入库表中得到符合条件的数据，取goods_id
        //通过goods_id获取库存表中符合条件的商品信息链表
        public ResMessage GetListBySpareParts(RepoOutInRecordFilter filter)
        {
            IEnumerable<RepoOutInRecord> oi_list = _repoOutInRecordRepository.GetList();
            if (filter.outin_person_id != null && filter.outin_person_id != 0)
            {
                oi_list = oi_list.Where(x => x.recipient_id == filter.outin_person_id).ToList();
                oi_list = oi_list.Where(x => x.create_time.Date == DateTime.Now.Date).ToList();

                //获取商品id和出库数量对照的对象链表
                var relationlist = oi_list.Select(x => new { x.goods_id, x.oi_number }).ToList();
                //获取商品id的数组
                int[] goods_id = oi_list.Select(x => x.goods_id).ToArray();
                IEnumerable<RepoGoodsStock> goods_list = _repoGoodsStockRepository.Getlist();
                List<RepoGoodsStock> list = goods_list.Where(x => goods_id.Contains(x.id)).ToList();
                int count = list.Count;
                //拿到商品名称
                List<RepoGoodsStockDTO> result = Mapper.Map<List<RepoGoodsStockDTO>>(list);
                //拿到出库数量
                //循环商品链表
                //套循环关系对照链表
                //出库数量赋值
                foreach (var item in result)
                {
                    foreach (var item1 in relationlist)
                    {
                        if (item.id == item1.goods_id)
                        {
                            item.oi_number = item1.oi_number;
                        }
                    }
                }
                return result != null ? ResMessage.Success(result, count) : ResMessage.Fail();
            }
            else
            { 
                return ResMessage.Fail(); 
            }
        }
        public ResMessage GetModel(int id)
        {
            if (id == 0)
            {
                return ResMessage.Fail("主键不能为空");
            }
            RepoOutInRecord repoOutInRecord = _repoOutInRecordRepository.GetModel(id);
            if (repoOutInRecord == null)
            {
                return ResMessage.Fail("通过该主键获取的实体为空");
            }
            return ResMessage.Success(repoOutInRecord);
        }

        public ResMessage Update(RepoOutInRecord repoOutInRecord)
        {
            repoOutInRecord.create_time = DateTime.Now;
            bool result = _repoOutInRecordRepository.Update(repoOutInRecord);
            return result==true? ResMessage.Success("修改成功"):ResMessage.Fail();
        }

        //三表联合数据
        public ResMessage GetListBySql(int? page,int? limit,RepoOutInRecordFilter filter)
        {
            if (page != null && limit != null) 
            {
                int ipage = (int)page;//将可空整形转为不可空整形
                int ilimit = (int)limit;
                IEnumerable<RepoGoodsStockDTO> list = _repoOutInRecordRepository.GetListBySql<RepoGoodsStockDTO>("select s.id,s.goods_name,s.threshold,s.goods_type,oi.id oi_id,oi.start_number,oi.oi_number,oi.end_number,oi.type,oi.audit,e.name,oi.create_time from Repo_Goods_Stock s inner join Repo_Out_In_Record oi on s.id=oi.goods_id inner join Employ_Emp e on oi.outin_person_id=e.id");
                //商品名称搜索
                if (!String.IsNullOrEmpty(filter.goods_name))
                {
                    list = list.Where(x => x.goods_name.Contains(filter.goods_name));
                }
                //出库/入库人姓名搜索
                if (!String.IsNullOrEmpty(filter.name))
                {
                    list = list.Where(x => x.name.Contains(filter.name));
                }
                int count = list.Count();
                List<RepoGoodsStockDTO> result = list.OrderBy(x => x.create_time).Skip((ipage - 1) * ilimit).Take(ilimit).ToList();
                return result == null ? ResMessage.Fail() : ResMessage.Success(result, count);
            }
            return ResMessage.Fail();
        }

        public ResMessage UpLoad(string filename, string path, HttpPostedFileBase file)
        {
            HttpPostedFileBase filer = file;
            string ex = filename.Substring(filename.LastIndexOf("."));
            string filernmae = Guid.NewGuid().ToString() + ex;
            string filepath = path + "/" + filernmae;
            filer.SaveAs(filepath);
            string imgurl = "/assets/goods/images/" + filernmae;
            return ResMessage.Success(data:imgurl);
        }
    }
}
