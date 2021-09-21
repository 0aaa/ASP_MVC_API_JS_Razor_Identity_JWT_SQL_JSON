using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MvcIntro0.Config;
using MvcIntro0.Models;
using MvcIntro0.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MvcIntro0.Controllers.API
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class UserController : Controller
    {
        private readonly SignInManager<Account> _loginManager;


        public UserController(SignInManager<Account> lgInMngr)
            => _loginManager = lgInMngr;


        private async Task<ClaimsIdentity> GetIdentity(string name, string password)
        {
            var loginRes = await _loginManager.PasswordSignInAsync(name, password, false, false);

            if (loginRes.Succeeded)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, name),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin")//
                };


                return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            }

            ModelState.AddModelError("", "Wrong name or password");

            return null;
        }


        [HttpPost]
        public IActionResult Token(User usr)
        {
            var idntty = GetIdentity(usr.Name, usr.Password).Result;

            if (idntty != null)
            {
                var tkn = new JwtSecurityToken(
                    issuer: AuthCredentials.ISSUER,
                    audience: AuthCredentials.AUDIENCE,
                    notBefore: DateTime.Now,
                    claims: idntty.Claims,
                    expires: DateTime.Now.Add(TimeSpan.FromMinutes(AuthCredentials.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthCredentials.GetKey(), SecurityAlgorithms.HmacSha256)
                    );

                var encodedTkn = new JwtSecurityTokenHandler().WriteToken(tkn);


                return Json(new
                {
                    access_token = encodedTkn,
                    username = idntty.Name
                });
            }


            return BadRequest(new { err = "Wrong login or password" });
        }
    }
}
