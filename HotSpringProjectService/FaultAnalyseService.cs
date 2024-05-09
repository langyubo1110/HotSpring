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
namespace HotSpringProjectService
{
    public class FaultAnalyseService :IFaultAnalyseService
    {
        private readonly IFaultAnalyseRepository _faultAnalyseRepository;

        public FaultAnalyseService(IFaultAnalyseRepository faultAnalyseRepository) {
            _faultAnalyseRepository=faultAnalyseRepository;
        }

        public ResMessage Add(FaultAnalyse faultAnalyse)
        {
            return _faultAnalyseRepository.Add(faultAnalyse) > 0 ? ResMessage.Success() : ResMessage.Fail();
           
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
            throw new NotImplementedException();
        }
    }
}
