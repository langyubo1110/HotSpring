using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService
{
    public interface IRegApplyService
    {
        List<RegApply> GetList();
        ResMessage GetListByPager(RegApplyFilter filter);
        int Add(RegApply regApply);
        //int AddAll(RegApply regApply, List<RegAudit> list);
        ResMessage AddWithRegAudit(RegApply regApply);
        bool Update(RegApply regApply);
        bool Check(int RegId, string txtAdvice, int userID);
        ResMessage Delete(int id);
        RegApply GetModel(int id);
        IEnumerable<RegApplyVO> GetListUnionSql();
    }
}
