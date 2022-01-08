using System;
using System.Collections.Generic;

#nullable disable

namespace NEACSwimmingPoolMang.API.Models
{
    public partial class ClassMangBannerDatum
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string Title { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public string Link { get; set; }
        public Guid ImgGuid { get; set; }
        public int SysStatus { get; set; }
        public long CreaterId { get; set; }
        public DateTime CreateTime { get; set; }
        public long UpdaterId { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
