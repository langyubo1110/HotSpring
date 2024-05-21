using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.VO;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService
{
    public class RegEquipResService : IRegEquipResService
    {
        private readonly IRegEquipResRepositpry _regEquipResRepositpry;
        private readonly IRegVoteRepository _regVoteRepository;
        private readonly IEquipmentRepository _equipmentRepository;

        public RegEquipResService(IRegEquipResRepositpry regEquipResRepositpry,IRegVoteRepository regVoteRepository,IEquipmentRepository equipmentRepository)
        {
            _regEquipResRepositpry = regEquipResRepositpry;
            _regVoteRepository = regVoteRepository;
            _equipmentRepository = equipmentRepository;
        }
        public ResMessage Add(RegEquipRes regEquipRes)
        {
            regEquipRes.is_buy = 0;
            int flag = _regEquipResRepositpry.Add(regEquipRes);
            return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
        }
        public ResMessage AddWithEquip(int resId)
        {
            RegEquipRes regRes =_regEquipResRepositpry.GetModel(resId);
            Equipment equipment = new Equipment();
            equipment.used_time = resId;
            equipment.power = "0";
            equipment.name = regRes.prod_name;
            equipment.location = "0";
            equipment.status = 0;
            equipment.equ_type = 1;
            equipment.create_time=DateTime.Now;
            int flag = _equipmentRepository.Add(equipment);
            regRes.is_buy = 1;
            _regEquipResRepositpry.Update(regRes);
            return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
        }

        public ResMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResMessage GetList()
        {

            List<RegEquipRes> result = _regEquipResRepositpry.GetList().ToList();
            return result == null ? ResMessage.Fail() : ResMessage.Success(result, 0);
        }
        
        //采购表id  用户id
        public ResMessage GetListById(int id,int userId)
        {//查出一个对应采购id下的调研表全表数据

            List<RegResVO> regResList = _regEquipResRepositpry.QueryBySql<RegResVO>($@"select * from Reg_Equip_Research rr where rr.reg_buy_id={id}").ToList();
            foreach(var item in regResList)//处理投票表的字段，以及对应采购id，立案id，投票人id的按钮展示
            {//item 是 RegResVO
                
                RegVote regVote=_regVoteRepository.GetList().Where(x=>x.equip_research_id==item.id&&x.reg_buy_id==item.reg_buy_id&&x.vote_id==userId).FirstOrDefault();
                if (regVote != null)
                {
                    if (regVote.vote_status == 1)
                        item.Is_show = 0;
                    else
                        item.Is_show = 1;
                }
                else
                    item.Is_show = 1;
            }
            foreach(var item in regResList)
            {
                //votecount代表的是这个调研表被投了几票，方便下一步去统计谁最多
                 List<RegVote> list= _regVoteRepository.GetList().Where(x => x.equip_research_id == item.id && x.reg_buy_id == item.reg_buy_id ).ToList();
                int count =list.Count;
                if (list != null)
                    item.voteCount = count;
                else
                    item.voteCount = 0;
            }
            //总票数
            int allCount =_regVoteRepository.GetList().Where(x=>x.reg_buy_id==id).Count();
            int realCount = _regVoteRepository.GetList().Where(x => x.reg_buy_id == id&&x.vote_status==1).Count();
            if (allCount == realCount)//最后一票
            {
                // 使用 LINQ 查询找出具有最大 VoteCount 的 RegResVO 对象
                RegResVO regResVO = regResList.OrderByDescending(x=>x.voteCount).FirstOrDefault();
                regResVO.buy_is_show = 1;
                // 将所有 RegResVO 对象的 is_show 字段设置为 0
                foreach (var vo in regResList)
                {
                    vo.Is_show = 0;
                }
            }
            return regResList == null ? ResMessage.Fail() : ResMessage.Success(regResList, 0);
        }
        public ResMessage GetListForBind(int id)
        {
            RegResVO regResVO = new RegResVO();
            List<ResLessVO> lessList = _regEquipResRepositpry.QueryBySql<ResLessVO>($@"select rr.fac_name,rr.prod_name,ee.name,rb.id from Reg_Equip_Research rr left join Reg_Research_Vote_Record v on rr.id=v.equip_research_id
                                    inner join Employ_Emp ee on ee.id=v.vote_id
                                    inner join Reg_Buy rb on rr.reg_buy_id=rb.id
                                    where rr.reg_buy_id={id}").ToList();
            regResVO.lessList = lessList;
            return regResVO == null ? ResMessage.Fail() : ResMessage.Success(regResVO, 0);
        
        }

        public ResMessage GetModel(int id)
        {
            throw new NotImplementedException();
        }

        public ResMessage Update(RegEquipRes regEquipRes)
        {
            throw new NotImplementedException();
        }
    }
}
