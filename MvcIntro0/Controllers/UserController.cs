using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcIntro0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcIntro0.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<Account> _accountManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserController(UserManager<Account> acntMngr, RoleManager<IdentityRole> rleMngr)
        {
            _accountManager = acntMngr;
            _roleManager = rleMngr;
        }


        public IActionResult Index()
            => View(_accountManager.Users.Include(acnt => acnt.Role).ToList());

        public async Task<IActionResult> AddOrUpdate(string userName)
            => View(userName == null ? null : await _accountManager.FindByNameAsync(userName));


        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(Account userCredentials, string userPassword)
        {
            if (ModelState.IsValid)
            {
                if (userCredentials.Id != null)//cookies need to be cleared
                {
                    var userTemp = _accountManager.FindByIdAsync(userCredentials.Id).Result;

                    userTemp.UserName = userCredentials.UserName;
                    userTemp.PasswordHash = userPassword == null ? userTemp.PasswordHash : _accountManager.PasswordHasher.HashPassword(userTemp, userPassword);
                    userTemp.Role = userCredentials.Role.Name == null ? userTemp.Role : await _roleManager.FindByNameAsync(userCredentials.Role.Name);
                    userTemp.Email = userCredentials.Email;
                    userTemp.PhoneNumber = userCredentials.PhoneNumber;

                    await _accountManager.UpdateAsync(userTemp);

                    return RedirectToAction("Index", "Home");
                }
                else if (userPassword != null)
                {
                    var userToAdd = new Account
                    {
                        UserName = userCredentials.UserName,
                        Role = userCredentials.Role.Name == null ? await _roleManager.FindByNameAsync("customer") : await _roleManager.FindByNameAsync(userCredentials.Role.Name),
                        Email = userCredentials.Email,
                        PhoneNumber = userCredentials.PhoneNumber
                    };
                    userToAdd.PasswordHash = _accountManager.PasswordHasher.HashPassword(userToAdd, userPassword);

                    var idnttyRslt = await _accountManager.CreateAsync(userToAdd);

                    if (idnttyRslt.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }

                    foreach (var err in idnttyRslt.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }

                    return RedirectToAction("AddOrUpdate", userToAdd);
                }
            }

            return View(userCredentials);
        }


        public IActionResult Delete(string userName)
        {
            _accountManager.DeleteAsync(_accountManager.FindByNameAsync(userName).Result);

            return RedirectToAction("Index");
        }
    }
}
