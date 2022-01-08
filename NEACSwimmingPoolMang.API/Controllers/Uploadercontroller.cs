//using AutoMapper.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NEACSwimmingPoolMang.API.Models;
using NEACSwimmingPoolMang.API.Models.appsettings;
using NEACSwimmingPoolMang.API.Models.Dtos;
using NEACSwimmingPoolMang.Models.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NEACSwimmingPoolMang.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploaderController : ControllerBase
    {

        private NEACSwimmingPoolMangQas1Context context;
        private readonly IConfiguration config;
        //private IConfiguration Configuration;
        private AppSettings _appSetting;
        public UploaderController(NEACSwimmingPoolMangQas1Context _context, 
            IOptions<AppSettings> options, IConfiguration _config)
        {
            context = _context;
            config = _config;
            this._appSetting = options.Value;
            //Configuration = _configuration;
            //var builder = new ConfigurationBuilder()
            //.SetBasePath(env.ContentRootPath)
            //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            //Configuration = builder.Build();
        }


       // GET: api/<UploaderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var connString = this.config["ConnectionStrings:NEACSwimmingPoolMangContext"];
            return new string[] { connString, "value2" };
        }

        // GET api/<UploaderController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var filehost = this._appSetting.FileHost;
            var sch = HttpContext.Request.Scheme.ToString();
            var url = Request.HttpContext.ToString();
            var model = this.context.Filedata.Where(m => m.Guid == Guid.Parse(id)).FirstOrDefault();
            if (model == null)
            {
                return new JsonResult(false);
            }
            return File(model.FileStream, model.Filetype);
        }

        // POST api/<UploaderController>
        [Route("Image")]
        [HttpPost]
        public ImageFileDtoModel UploadImage([FromForm] IFormFile file)
        {
            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            Filedatum md = new Filedatum()
            {
                Guid = Guid.NewGuid(),
                FileName = file.FileName,
                Filetype = file.ContentType,
                FileStream = ms.ToArray(),
                SysStatus = Convert.ToInt32(DataStatus.Active),
                CreaterId = 1,
                CreateTime = DateTime.Now,
                UpdaterId = 1,
                UpdateTime = DateTime.Now
            };
            ms.Dispose();
            this.context.Filedata.Add(md);
            this.context.SaveChanges();
            var mdoel = new ImageFileDtoModel()
            {
                name = md.FileName,
                uid = md.Guid.ToString(),
                status = "Done",
                url = $"https://localhost:44318/api/uploader/{md.Guid.ToString()}"
                //url = $"{this._appSetting.FileHost}/api/uploader/{md.Guid.ToString()}"
            };
            return mdoel;
        }

        // DELETE api/<UploaderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}