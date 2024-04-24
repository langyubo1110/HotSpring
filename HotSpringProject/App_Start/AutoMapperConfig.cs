using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OA_AutoWork.App_Start
{
    public class AutoMapperConfig
    {
        public static void Config()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<AutoMapperProfile>();
            });
        }
    }
}