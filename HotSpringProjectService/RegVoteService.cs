using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService
{
    public class RegVoteService : IRegVoteService
    {
        private readonly IRegVoteRepository _regVoteRepository;

        public RegVoteService(IRegVoteRepository regVoteRepository)
        {
            _regVoteRepository = regVoteRepository;
            
        }
        public ResMessage Add(RegVote regVote)
        {
            int flag = _regVoteRepository.Add(regVote);
            return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
        }



        public ResMessage UpdateWithVote(int regId, int resId, int userID)
        {
            RegVote regVote = _regVoteRepository.GetModelWithId(userID,regId);
            regVote.equip_research_id = resId;
            regVote.vote_status = 1;
            bool flag = _regVoteRepository.Update(regVote);
            return flag==true ? ResMessage.Success() : ResMessage.Fail();
        }

        public ResMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResMessage GetList()
        {
            List<RegVote> result = _regVoteRepository.GetList().ToList();
            return result == null ? ResMessage.Fail() : ResMessage.Success(result, 0);
        }

        public RegVote GetModel(int id)
        {
            throw new NotImplementedException();
        }

        public ResMessage Update(RegVote regVote)
        {
            throw new NotImplementedException();
        }
    }
}
