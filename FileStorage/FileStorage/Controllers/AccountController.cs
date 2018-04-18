using FileStorage.DAL.Interfaces;
using FileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FileStorage.Controllers
{
    public class AccountController : Controller
    {
        private const int userNotExist = 0;
        private const int defaultUserStatus = 1;
        private IFileRepository fileRepository;
        private IUserRepository userRepository;

        public AccountController(IFileRepository fileRepositoryParam, IUserRepository userRepositoryParam)
        {
            fileRepository = fileRepositoryParam;
            userRepository = userRepositoryParam;
        }

        [AllowAnonymous]
        public ActionResult LogIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogIn(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            user.PasswordHash = userRepository.GetHashString(user.PasswordHash);
            if (userRepository.CheckUser(user))
            {
                FormsAuthentication.SetAuthCookie(user.Login, true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Пользователя с такими данными нет. Проверьте правильность и введите снова.");
            }

            return View(user);
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Registration()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Registration(User user)
        {
            if (user == null)
            {
                return View(user);
            }

            if (ModelState.IsValid)
            {
                user.PasswordHash = userRepository.GetHashString(user.PasswordHash);
                user.Role = new Role();
                user.Role.RoleID = defaultUserStatus;
                var userID = userRepository.CreateUser(user);

                if (userID != userNotExist)
                {
                    FormsAuthentication.SetAuthCookie(user.Login, true);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(user);
        }
    }
}