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


        public UserController(UserManager<Account> acntMngr)
            => _accountManager = acntMngr;


        public IActionResult Index()
            => View(_accountManager.Users.Include(acnt => acnt.Role).ToList());

        public async Task<IActionResult> AddOrUpdate(string userName)
            => View(await _accountManager.FindByNameAsync(userName));


        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(Account userCredentials, string userPassword)
        {
            if (ModelState.IsValid)
            {
                if (userCredentials.Id != null)
                {
                    await _accountManager.UpdateAsync(userCredentials);

                    return RedirectToAction("Index");
                }
                else
                {
                    Account userToAdd = new Account
                    {
                        UserName = userCredentials.UserName,
                        RoleId = userCredentials.RoleId,
                        Email = userCredentials.Email,
                        PhoneNumber = userCredentials.PhoneNumber,
                    };
                    userToAdd.PasswordHash = _accountManager.PasswordHasher.HashPassword(userToAdd, userPassword);

                    IdentityResult idnttyRslt = await _accountManager.CreateAsync(userToAdd);

                    if (idnttyRslt.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }

                    foreach (IdentityError err in idnttyRslt.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }

                    return RedirectToAction("AddOrUpdate", userToAdd);
                }
            }

            return View(userCredentials);
        }


        [Authorize(Roles = "admin")]
        public IActionResult Delete(string userName)
        {
            _accountManager.DeleteAsync(_accountManager.FindByNameAsync(userName).Result);
            return RedirectToAction("Index");
        }
    }
}
