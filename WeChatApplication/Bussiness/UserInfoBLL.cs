using Common;
using DataAccess;
using System;
using WebApi.Model;

namespace Bussiness
{
    public class UserInfoBLL
    {
        private readonly UserInfoAccess _dal = new UserInfoAccess();

        /// <summary>
        /// 查询用户名密码对应的用户数据
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public UserModel GetUserInfo(string userName,string password)
        {
            if(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return null;
            }
            UserModel model = new UserModel()
            {
                UserName = "俩蛋",
                UserCode = "201805141454",
                UserId = 8998101872,
                DepartmentId = 1,
                DepartmentName = "研发中心",
                LevelId = 10001,
                LevelName = "普通职员"
            };
            //var dt = _dal.GetUserInfo(userName, password);
            //if (Utility.TableHelper(dt))
            //{
            //    model = new MapperHelper<UserModel>().MapperModel(dt.Rows[0]);
            //}
            return model;

        }
    }
}
