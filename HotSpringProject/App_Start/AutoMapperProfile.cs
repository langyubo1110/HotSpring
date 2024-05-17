using AutoMapper;
using HotSpringProject.Entity;
using HotSpringProject.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace OA_AutoWork.App_Start
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RepoGoodsStock, RepoGoodsStockDTO>();
            CreateMap<RepoGoodsStockDTO, RepoGoodsStock>();
        }
    }
}