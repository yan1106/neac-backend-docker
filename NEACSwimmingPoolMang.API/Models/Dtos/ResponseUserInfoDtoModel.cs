using NEACSwimmingPoolMang.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NEACSwimmingPoolMang.Models.Dtos
{
    public class ResponseDtoModel
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }
        public int? StatusCode { get; set; }
        public string Msg { get; set; }
        public string Data { get; set; }
    }
}
