using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NEACSwimmingPoolMang.Models.Dtos
{
    public class ResponseUserInfoDtoModel
    {
       
        public string token { get; set; }
        public string id { get; set; }
        public bool isVaild  { get; set; }
        public bool isChange { get; set; }
        public string userName { get; set; }
    }
}
