using ChatBox.Model;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using ChatBox.DAL;

namespace ChatBox.Repo
{
    public class LoginRepo : ILoginRepo
    {
       private readonly LoginInterface loginInterface;
        public LoginRepo(LoginInterface loginInterface)
        {

            this.loginInterface = loginInterface;
        }

        
        //call data access class
        public int Login(LoginModel loginModel)
        {
            var data = loginInterface.UserLogin(loginModel);
            if (data >0)
            {
                return data;
            }
            return 0;
        }
    }
}
