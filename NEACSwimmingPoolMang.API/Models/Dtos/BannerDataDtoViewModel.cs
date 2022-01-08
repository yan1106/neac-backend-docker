using NEACSwimmingPoolMang.API.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEACSwimmingPoolMang.Models.Dtos
{
    public class BannerDataDtoViewModel
    {
        public string guid { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        //public IEnumerable<ImageFileDtoModel> ImageFileModel { get; set; }
        public ImageFileDtoModel ImageFileModel { get; set; }
        public UserInfosModel UserInfos { get; set; }
    }

    public class BannerDataUploadDtoViewModel
    {
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Link { get; set; }
        //public IEnumerable<ImageFileDtoModel> ImageFileModel { get; set; }
        public ImageFileDtoModel ImageFileModel { get; set; }
    }
    

}