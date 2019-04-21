using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ChatBox.Model;
using ChatBox.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ChatBox.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
       private readonly ILoginRepo iloginRepo;

        //interface define in constructor
        public AccountController(ILoginRepo iloginRepo)
        {
            this.iloginRepo = iloginRepo;

        }
       //This is for Login
        [Route("Login")]
        public IActionResult Login(LoginModel loginModel)
        {

            var loginresponse = iloginRepo.Login(loginModel); //calling interface method 
            if (loginresponse>0)
            {

                //create token here
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:61895",
                    audience: "http://localhost:4200",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(125),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString ,loginModel.UserName, loginresponse});
                
            }
            return BadRequest();
        }
    }
}
