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

        public RegEquipResService(IRegEquipResRepositpry regEquipResRepositpry,IRegVoteRepository regVoteRepository)
        {
            _regEquipResRepositpry = regEquipResRepositpry;
            _regVoteRepository = regVoteRepository;
        }
        public ResMessage Add(RegEquipRes regEquipRes)
        {
            int flag = _regEquipResRepositpry.Add(regEquipRes);
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
        {
            List<RegResVO> regResList = _regEquipResRepositpry.QueryBySql<RegResVO>($@"select * from Reg_Equip_Research rr where rr.reg_buy_id={id}").ToList();
            foreach(var item in regResList)
            {
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
            return regResList == null ? ResMessage.Fail() : ResMessage.Success(regResList, 0);
        }
        public ResMessage GetListForBind(int id)
        {
            RegResVO regResVO = new RegResVO();
            List<ResLessVO> lessList = _regEquipResRepositpry.QueryBySql<ResLessVO>($@"select rr.fac_name,ee.name,rb.id from Reg_Equip_Research rr left join Reg_Research_Vote_Record v on rr.id=v.equip_research_id
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
