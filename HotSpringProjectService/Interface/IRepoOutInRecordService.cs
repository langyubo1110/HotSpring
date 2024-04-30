using DotNet.Utilities;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotSpringProject.Entity.VO;
namespace HotSpringProjectService.Interface
{
    public interface IRepoOutInRecordService
    {
        ResMessage GetList(int page, int limit, RepoOutInRecordFilter filter);
        ResMessage Delete(int id);
        ResMessage Update(RepoOutInRecord repoOutInRecord);
        ResMessage Add(RepoOutInRecord repoOutInRecord);
        ResMessage GetModel(int id);
    }
}
