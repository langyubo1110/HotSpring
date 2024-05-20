using DotNet.Utilities;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotSpringProject.Entity.VO;
using HotSpringProject.Entity.DTO;
using System.Web;
namespace HotSpringProjectService.Interface
{
    public interface IRepoOutInRecordService
    {
        ResMessage GetList();
        ResMessage GetListBySpareParts(RepoOutInRecordFilter filter);
        ResMessage Delete(int id);
        ResMessage Update(RepoOutInRecord repoOutInRecord);
        ResMessage Add(RepoGoodsStockDTO repoGoodsStockDTO,int userId);
        ResMessage GetModel(int id);
        ResMessage GetListBySql(int? page, int? limit,RepoOutInRecordFilter filter);
        ResMessage UpLoad(string filename,string path,HttpPostedFileBase file);
    }
}
