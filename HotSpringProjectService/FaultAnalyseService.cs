using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectService.Interface;
using HotSpringProjectRepository.Interface;
using HotSpringProjectRepository;
using HotSpringProject.Entity.VO;
namespace HotSpringProjectService
{
    public class FaultAnalyseService :IFaultAnalyseService
    {
        private readonly IFaultAnalyseRepository _faultAnalyseRepository;
        private readonly IEmployEmpRepository _employEmpRepository;
        private readonly IEmployRoleRepository _employRoleRepository;

        public FaultAnalyseService(IFaultAnalyseRepository faultAnalyseRepository,IEmployEmpRepository employEmpRepository,IEmployRoleRepository employRoleRepository) {
            _faultAnalyseRepository=faultAnalyseRepository;
            _employEmpRepository=employEmpRepository;
            _employRoleRepository=employRoleRepository;
        }
       
        public ResMessage Add(FaultAnalyse faultAnalyse)
        {
            
            return ResMessage.Success();
           
        }
        public ResMessage VoteCheck(int id)
        {
            IEnumerable<FaultAnalyse> list = _faultAnalyseRepository.GetList().Where(x => x.fault_app_id == id);
       
            List<FaultAnalyseVO> result = list.Join(_employEmpRepository.GetList(), x => x.analyse_id, y => y.id, (x, y) => new FaultAnalyseVO
            {
                analyse_name=y.name,
                contents=x.contents,
            }).ToList();
            foreach(var item in result)
            {
                if (item.contents == null||item.contents=="")
                {
                    item.vote = 0;
                }
                else
                {
                    item.vote = 1;
                }
            }
            return result == null ? ResMessage.Fail() : ResMessage.Success(result);
        }
        public ResMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResMessage GetList(int page, int limit)
        {
            throw new NotImplementedException();
        }

        public ResMessage GetModel(int id)
        {
            FaultAnalyse faultAnalyse = _faultAnalyseRepository.GetModel(id);
            return faultAnalyse!=null ?ResMessage.Success(faultAnalyse) : ResMessage.Fail();
            
        }
       
        public ResMessage Update(FaultAnalyse faultAnalyse)
        {
            
            return _faultAnalyseRepository.UpDate(faultAnalyse) > 0 ? ResMessage.Success() : ResMessage.Fail();
        }
        public ResMessage UpDateByAudit(List<FaultAnalyse> faultAnalyseslist)
        {
            foreach(var item in faultAnalyseslist)
            {
                FaultAnalyse faultAnalyse = _faultAnalyseRepository.GetModel(item.id);
                faultAnalyse.auditor = item.auditor;
                faultAnalyse.final_scheme = 1;
                _faultAnalyseRepository.UpDate(faultAnalyse);
            }
            return ResMessage.Success();
        }
        //获取审核记录
        public ResMessage GetRecord(int id)
        {
            //接收的id为申报id
            IEnumerable<FaultAnalyse> ilist = _faultAnalyseRepository.GetList().Where(x => x.fault_app_id==id & x.final_scheme==1);
            List<FaultAnalyseVO> list = ilist.Join(_employEmpRepository.GetList(),x=>x.analyse_id,y=>y.id,(x,y)=>new FaultAnalyseVO
            {
                auditor= x.auditor,
                analyse_name=y.name,
                contents=x.contents
            }).ToList();
            return list != null ? ResMessage.Success(list) : ResMessage.Fail();
        }
    }
}
