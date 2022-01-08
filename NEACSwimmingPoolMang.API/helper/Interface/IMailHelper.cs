using NEACSwimmingPoolMang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEACSwimmingPoolMang.helper.Interface
{
    public interface IMailHelper
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
