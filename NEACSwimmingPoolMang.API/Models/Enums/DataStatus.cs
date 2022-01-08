using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEACSwimmingPoolMang.Models.Enums
{
    public enum DataStatus
    {
        Disable,
        Active,
        Pending,
        Delete
    }

    public class DataMapping
    {
       public string[] StatusMapping = { "關閉", "啟用", "未決定", "刪除" };
    }
    
}
