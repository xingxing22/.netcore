using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using WebApi.Model;

namespace WeChat.WebApi.Controllers
{
    public class BaseController : Controller
    {
        private UserInfo _userInfo = null;
        private RedisHelper<UserInfo> _redis = new RedisHelper<UserInfo>();
        private string _redisKey = string.Empty;

        public string RedisKey
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_redisKey))
                {
                    return _redisKey;
                }
                else
                {
                    return _redisKey = Utility.GetMD5String(CurrentUser.User.UserId.ToString());
                }
            }
            set
            {
                _redisKey = value;
            }
        }

        public UserInfo CurrentUser
        {
            get
            {
                if(_userInfo != null)
                {
                    return _userInfo;
                }
                else
                {
                    _userInfo = (UserInfo)_redis.GetObj(RedisKey);
                }
                _redis.SetObj(RedisKey, _userInfo, new TimeSpan(1, 0, 0));
                return _userInfo;
            }
            set
            {
                _userInfo = value;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string cookieUser = filterContext.HttpContext.Request.Cookies["user"];
            if (!string.IsNullOrEmpty(cookieUser))
            {
                string plain = Utility.DecryptDES(cookieUser);
                if (string.IsNullOrEmpty(plain))
                {
                    filterContext.Result = new JsonResult("请登录");
                    return;
                }
                _redisKey = Utility.GetMD5String(plain);
            }
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = RouteData.Values["controller"].ToString().ToLower();
            var action = RouteData.Values["action"].ToString().ToLower();
            if(!string.IsNullOrEmpty(action) && action.Equals("logout"))
            {
                base.OnActionExecuted(context);
                return;
            }
            _redisKey = Utility.GetMD5String(CurrentUser.User.UserId.ToString());
            _redis.SetObj(_redisKey, CurrentUser, new TimeSpan(1, 0, 0));
            base.OnActionExecuted(context);
        }
    }
}