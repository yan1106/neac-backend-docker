using System;
using System.Collections.Generic;

#nullable disable

namespace NEACSwimmingPoolMang.API.Models
{
    public partial class Filedatum
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string FileName { get; set; }
        public byte[] FileStream { get; set; }
        public string Filetype { get; set; }
        public int SysStatus { get; set; }
        public long CreaterId { get; set; }
        public DateTime CreateTime { get; set; }
        public long UpdaterId { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}