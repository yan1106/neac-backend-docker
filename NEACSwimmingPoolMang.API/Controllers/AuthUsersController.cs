using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NEACSwimmingPoolMang.API.helper;
using NEACSwimmingPoolMang.API.Models;
using NEACSwimmingPoolMang.helper;
using NEACSwimmingPoolMang.helper.Interface;
using NEACSwimmingPoolMang.Models.Dtos;
using NEACSwimmingPoolMang.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NEACSignupCourseDemo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthUsersController : ControllerBase
    {
        private readonly NEACSwimmingPoolMangQas1Context context;
        private readonly JwtHelpers jwt;
        private readonly IConfiguration config;
        private readonly ICommonHelper common;
        public AuthUsersController(NEACSwimmingPoolMangQas1Context _context,
        IConfiguration _config, ICommonHelper _common, JwtHelpers _jwt)
        {
            context = _context;
            config = _config;
            common = _common;
            jwt = _jwt;
        }
        // GET: api/<AuthUserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //var connString = this.config.GetSection("AllowedHosts").ToString();
            return new string[] { "connString", "value2" };
        }

        // GET api/<AuthUserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"value {id}";
        }

        // POST api/<AuthUserController>
        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public ActionResult<ResponseUserInfoDtoModel> Login(UserDtoModel dm)
        {

            var md = new ResponseUserInfoDtoModel()
            {
                id = "",
                token = "",
                userName = string.Empty,
                isVaild = false,
                isChange = false
            };

            if (!string.IsNullOrEmpty(dm.UserName) && !string.IsNullOrEmpty(dm.Pwd))
            {
                var t = context.Users.Where(m => m.Username == dm.UserName &&
                this.common.VerifyHashedPassword(dm.Pwd) == m.Password).ToList();

                string token = string.Empty;

                if (t.Count > 0)
                {
                    token = this.jwt.GenerateToken(dm.UserName);
                    md.id = t[0].Id.ToString();
                    md.userName = string.Empty;
                    md.token = token;
                    md.isVaild = true;
                    md.isChange = true;
                }

            }

            return md;

        }

        [Route("GetUserItem/{id}")]
        [HttpGet]
        public async Task<ActionResult<User>> GetUserItem(long id)
        {
            var todoItem = await context.Users.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [AllowAnonymous]
        // POST api/<AuthUserController>
        [Route("ManualCreateSystemUser")]
        [HttpPost]
        public async Task<ActionResult<ResponseDtoModel>> ManualCreateSystemUser(User user)
        {
            var rdm = new ResponseDtoModel();
            try
            {

                if (!this.context.Users.Any(o => o.Email.Equals(user.Email)))
                {

                    string classTime = string.Empty;
                    user.Password = this.common.VerifyHashedPassword(user.Password);
                    this.context.Users.Add(user);
                    await context.SaveChangesAsync();
                    var body = CreatedAtAction(nameof(GetUserItem), new { id = user.Id }, user.Id);

                    if (body.StatusCode == 201)
                    {

                        rdm = new ResponseDtoModel()
                        {
                            Status = Status.Success,
                            StatusCode = body.StatusCode,
                            Data = JsonSerializer.Serialize(user.Id),
                            //Data = JsonConvert.SerializeObject(user.Id),
                            Msg = null
                        };
                    }
                }
                else
                {
                    rdm = new ResponseDtoModel()
                    {
                        Status = Status.Error,
                        StatusCode = null,
                        Data = null,
                        Msg = "Email已被註冊!"
                    };
                    ;
                }

                return Ok(rdm);

            }
            catch (Exception ex)
            {
                rdm = new ResponseDtoModel()
                {
                    Status = Status.Error,
                    StatusCode = null,
                    Data = null,
                    Msg = "系統出錯!"
                };
                return Ok(rdm);
            }
        }

    }
}
