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
    public class RegFileService : IRegFileService
    {
        private readonly IRegFileRepository _regFileRepository;

        public RegFileService(IRegFileRepository regFileRepository)
        {
            _regFileRepository = regFileRepository;

        }
        public ResMessage Add(RegFile regFile)
        {
            int flag = _regFileRepository.Add(regFile);
            return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
            
        }
        public ResMessage AddWithId(int buyid,string filepath)
        {
            RegFile regFile = new RegFile();
            regFile.create_time = DateTime.Now;
            regFile.type = 0;
            regFile.reg_buy_id = buyid;
            regFile.file_path = filepath;
            int flag = _regFileRepository.Add(regFile);
            return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
            
        }

        public ResMessage Delete(int id)
        {
            throw new NotImplementedException(); 
        }

        public ResMessage GetList()
        {
            List<RegFile> result = _regFileRepository.GetList().ToList();
            return result == null ? ResMessage.Fail() : ResMessage.Success(result, 0);
        }
        public ResMessage GetListForFile(int id)
        {
            List<RegFileVO> result = _regFileRepository.QueryBySql<RegFileVO>($@"select * from Reg_Files where reg_buy_id={id}").ToList();
            foreach(var regFile in result)
            {
                regFile.file_path_name = regFile.file_path.Substring(regFile.file_path.LastIndexOf('/') + 1);
            }
            return result == null ? ResMessage.Fail() : ResMessage.Success(result, 0);
        }

        public RegFile GetModel(int id)
        {
            throw new NotImplementedException();
        }

        public ResMessage Update(RegFile regFile)
        {
            throw new NotImplementedException();
        }
    }
}
