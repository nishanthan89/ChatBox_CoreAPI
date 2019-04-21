using ChatBox.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBox
{
    public interface LoginInterface
    {
        int UserLogin(LoginModel loginModel);
    }
}
