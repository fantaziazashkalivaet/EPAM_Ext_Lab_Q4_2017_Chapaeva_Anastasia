namespace FileStorage.Controllers
{
    using System.Web.Mvc;
    using System.Web.Security;
    using DAL.Interfaces;
    using DAL.Models;
    using Models;

    [Authorize]
    public class AccountController : Controller
    {
        private const int UserNotExist = 0;
        private const int DefaultUserStatus = 1;
        private IUserRepository userRepository;

        public AccountController(IUserRepository userRepositoryParam)
        {
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
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            user.Password = userRepository.GetHashString(user.Password);
            if (userRepository.CheckLogin(user.Login, user.Password))
            {
                FormsAuthentication.SetAuthCookie(user.Login, true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Пользователя с такими данными нет. Проверьте правильность и введите снова.");
            }

            return View(user);
        }

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
        public ActionResult Registration(RegistrationViewModel user)
        {
            if (user == null)
            {
                return View(user);
            }

            if (ModelState.IsValid)
            {
                var newUser = new User()
                {
                    Login = user.Login,
                    PasswordHash = userRepository.GetHashString(user.Password),
                    Role = new Role() { RoleID = DefaultUserStatus }
                };

                var userID = userRepository.CreateUser(newUser);

                if (userID != UserNotExist)
                {
                    FormsAuthentication.SetAuthCookie(newUser.Login, true);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь с таким логином уже существует");
                }
            }

            return View(user);
        }
    }
}