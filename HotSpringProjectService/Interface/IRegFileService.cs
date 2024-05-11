using DotNet.Utilities;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    public interface IRegFileService
    {
        ResMessage GetList();
        ResMessage GetListForFile(int id);
        ResMessage Add(RegFile regFile);
        ResMessage AddWithId(int buyid, string filepath);
        ResMessage Update(RegFile regFile);
        ResMessage Delete(int id);
        RegFile GetModel(int id);
    }
}
