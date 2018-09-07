using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bussiness;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;

namespace WeChat.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Home")]
    public class HomeController : BaseController
    {
        [HttpGet]
        [Route("Login")]
        public ActionResult Login(string userName,string password)
        {
            if(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) 
                || string.IsNullOrEmpty(userName.Trim()) || string.IsNullOrEmpty(password.Trim()))
            {
                return Json("请输入正确的用户名密码");
            }
            var userInfo = new UserInfoBLL().GetUserInfo(userName, password);
            if (userInfo != null)
            {
                UserInfo user = new UserInfo { User = userInfo, WeiXin = new WeiXinResultModel() };
                CurrentUser = user;
            }
            HttpContext.Response.Cookies.Append("user", Utility.EncryptDES(userInfo.UserId.ToString()));
            return Json("登录成功");
        }

        [HttpGet]
        [Route("GetUserInfo")]
        public ActionResult GetUserInfo()
        {
            return Json(CurrentUser);
        }
    }
}