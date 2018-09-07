using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess
{
    public class UserInfoAccess
    {
        /// <summary>
        /// 查询用户名密码对应的用户数据
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public DataTable GetUserInfo(string userName, string password)
        {
            DataTable dt = new DataTable();
            StringBuilder sql = new StringBuilder();
            sql.Append(" select UserName,UserId,UserCode,DepartmentId,DepartmentName,LevelId,LevelName from UserInfo where UserName=@UserName and Pawssword=@Pawssword");
            List<MySqlParameter> param = new List<MySqlParameter>();
            param.Add(new MySqlParameter("@UserName", userName));
            param.Add(new MySqlParameter("@Pawssword", password));
            DBHelper.GetDataBySQL(sql.ToString(), param.ToArray());
            return dt;
        }
    }
}
