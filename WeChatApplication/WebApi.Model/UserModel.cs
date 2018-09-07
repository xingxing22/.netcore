using System;

namespace WebApi.Model
{
    public class UserModel
    {
        public string UserName { get; set; }

        public decimal UserId { get; set; }

        public string UserCode { get; set; }

        public decimal DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        /// <summary>
        /// 职务ID
        /// </summary>
        public decimal LevelId { get; set; }

        /// <summary>
        /// 职务名称
        /// </summary>
        public string LevelName { get; set; }
    }

    public class UserInfo
    {
        public WeiXinResultModel WeiXin { get; set; }

        public UserModel User { get; set; }
    }
}
