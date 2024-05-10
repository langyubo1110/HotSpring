using DotNet.Utilities;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    public interface IRegVoteService
    {
        ResMessage GetList();
 
        ResMessage Add(RegVote regVote);
        ResMessage UpdateWithVote(int regId, int resId, int userID);
        ResMessage Update(RegVote regVote);
        ResMessage Delete(int id);
        RegVote GetModel(int id);
    }
}
