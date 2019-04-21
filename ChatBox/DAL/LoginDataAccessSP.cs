using ChatBox.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBox.DAL
{
    public class LoginDataAccessSP : LoginInterface
    {
        private readonly IConfiguration configuration;
        
        public LoginDataAccessSP(IConfiguration config)
        {
            this.configuration = config;
        }
        
        //for Login database access (implementation LoginInterface)
        public int UserLogin(LoginModel loginModel)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("DefaultConnectionString");

                SqlConnection con = new SqlConnection(connectionString);

                con.Open();
                SqlCommand cmd = new SqlCommand("UserChatLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter p1 = new SqlParameter("username", loginModel.UserName);
                SqlParameter p2 = new SqlParameter("password", loginModel.PassWord);
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);

                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    var status = rd.GetInt32(0);
                    if (status>0)
                    {
                        return status;
                    }
                    return status;
                }
                else
                {
                    return 0;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
