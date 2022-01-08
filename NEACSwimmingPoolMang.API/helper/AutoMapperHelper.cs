using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NEACSwimmingPoolMang.API.Models;
using NEACSwimmingPoolMang.Models.Dtos;

namespace NEACSwimmingPoolMang.API.helper
{
    public class AutoMapperHelper : Profile

    {
       public AutoMapperHelper()
        {
            
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<ClassMangBannerDatum, BannerDtoViewModel>());
            CreateMap<ClassMangBannerDatum, BannerDataDtoViewModel>();
            //CreateMap<IEnumerable<ClassMangBannerDatum>, IEnumerable<BannerDtoViewModel>>();
        }
    }
}
