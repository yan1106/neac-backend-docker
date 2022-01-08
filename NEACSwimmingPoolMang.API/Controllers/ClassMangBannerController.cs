using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc;
using NEACSwimmingPoolMang.API.helper;
using NEACSwimmingPoolMang.API.Models;
using NEACSwimmingPoolMang.Models.Dtos;
using NEACSwimmingPoolMang.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using NEACSwimmingPoolMang.API.Models.Dtos;
using NEACSwimmingPoolMang.API.Models.appsettings;
using Microsoft.Extensions.Options;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NEACSwimmingPoolMang.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ClassMangBannerController : ControllerBase
    {
        private readonly NEACSwimmingPoolMangQas1Context context;
        private readonly IMapper mapper;
        private AppSettings _appSetting;
        public ClassMangBannerController(NEACSwimmingPoolMangQas1Context _context, IMapper _mapper,
          IOptions<AppSettings> options)
        {
            this.context = _context;
            this.mapper = _mapper;
            this._appSetting = options.Value;
        }
        // GET: api/<ClassMangBannerController>
        [HttpGet]
        public IEnumerable<BannerDataDtoViewModel> Get()
        {
            DataMapping dm = new DataMapping();
            //var bannerModel = this.context.ClassMangBannerData.Where(m => m.SysStatus == Convert.ToInt32(DataStatus.Active)).ToList();

            var bannerModel = (from cmb in context.ClassMangBannerData
                               join fm in context.Filedata on cmb.ImgGuid equals fm.Guid
                               where cmb.SysStatus == Convert.ToInt32(DataStatus.Active) || cmb.SysStatus == Convert.ToInt32(DataStatus.Disable)
                               select new BannerDataDtoViewModel
                               {
                                   guid = Convert.ToString(cmb.Guid),
                                   Title = cmb.Title,
                                   StartDate = cmb.Startdate.ToString("yyyy-MM-dd"),
                                   EndDate = cmb.Enddate.ToString("yyyy-MM-dd"),
                                   Link = cmb.Link,
                                   Status = dm.StatusMapping[cmb.SysStatus],
                                   Image = $"<img src=\"https://localhost:44318/api/uploader/{cmb.ImgGuid.ToString()}\" width=\"200\" height=\"200\">",
                                   //ImageGuid = Convert.ToString(cmb.ImgGuid),
                                   ImageFileModel = new ImageFileDtoModel()
                                   {
                                       name = fm.FileName,
                                       uid = Convert.ToString(fm.Guid),
                                       status = "Done",
                                       url = $"https://localhost:44318/api/uploader/{cmb.ImgGuid.ToString()}"
                                   }

                               });



            //var md = this.context.Filedata.Where(m=>m.Guid=ba)
            //var results = mapper.Map<IEnumerable<BannerDataDtoViewModel>>(bannerModel);
            //foreach(var item in results)
            //{

            //    item.Image= $"<img src=\"data:{md.Filetype};base64,{Convert.ToBase64String(md.FileStream)}\">";
            //}
            return bannerModel;
        }

        // GET api/<ClassMangBannerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {

            return "value";
        }

        // POST api/<ClassMangBannerController>
        [HttpPost]
        public bool Post(BannerDataDtoModel bdDtoModel)
        {
            var t = bdDtoModel;
            var clbModel = new ClassMangBannerDatum()
            {
                Guid = Guid.NewGuid(),
                Title = $"{bdDtoModel.Title}",
                Startdate = Convert.ToDateTime(bdDtoModel.StartDate + " 00:00"),
                Enddate = Convert.ToDateTime(bdDtoModel.EndDate + " 23:59"),
                Link = bdDtoModel.Link,
                ImgGuid = Guid.Parse(bdDtoModel.ImageUid),
                SysStatus = Convert.ToInt32(DataStatus.Active),
                CreaterId = 1,
                CreateTime = DateTime.Now,
                UpdaterId = 1,
                UpdateTime = DateTime.Now
            };

            this.context.ClassMangBannerData.Add(clbModel);
            this.context.SaveChanges();

            return true;

        }

        // PUT api/<ClassMangBannerController>/5
        [HttpPut("{id}")]
        public bool Put(string id, BannerDataUploadDtoViewModel bdDtoModel)
        {
            var bannerData = this.context.ClassMangBannerData.Where(m => m.Guid == Guid.Parse(id)).FirstOrDefault();
            bannerData.Enddate = Convert.ToDateTime(bdDtoModel.EndDate + " 23:59");
            bannerData.Startdate = Convert.ToDateTime(bdDtoModel.StartDate + " 00:00");
            bannerData.ImgGuid = Guid.Parse(bdDtoModel.ImageFileModel.uid);
            bannerData.Title = bdDtoModel.Title;
            bannerData.Link = bdDtoModel.Link;

            this.context.ClassMangBannerData.Update(bannerData);
            this.context.SaveChanges();
            
            return true;
        }

        // DELETE api/<ClassMangBannerController>/5
        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            //var bannerModel = (from cmb in context.ClassMangBannerData
            //                   join fm in context.Filedata on cmb.ImgGuid equals fm.Guid
            //                   where cmb.Id == id
            //                   select new 
            //                   {
            //                      cmbId = cmb.Id,
            //                      fmId = fm.Id
            //                   }).FirstOrDefault();
            //var 
            try
            {
                var bannerModel = this.context.ClassMangBannerData.Where(m => m.Guid == Guid.Parse(id)).FirstOrDefault();
                bannerModel.SysStatus = Convert.ToInt32(DataStatus.Delete);
                this.context.ClassMangBannerData.Update(bannerModel);
                var fileDataModel = this.context.Filedata.Where(m => m.Guid == bannerModel.ImgGuid).FirstOrDefault();
                fileDataModel.SysStatus = Convert.ToInt32(DataStatus.Delete);
                this.context.Filedata.Update(fileDataModel);
                this.context.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}