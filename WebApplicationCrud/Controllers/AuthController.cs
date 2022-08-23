using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplicationCrud.Models;
using WebApplicationCrud.ViewModels;

namespace WebApplicationCrud.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private CRUDdbcontext _ctx;
        private RoleManager<IdentityRole> _roleManager;
        public AuthController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            CRUDdbcontext ctx,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _ctx = ctx;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login()
        {

            return View(new LoginViewModel());

        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {

            return View(new RegisterViewModel());

        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            var user = new IdentityUser
            {
                Email = vm.Email,
                UserName = vm.UserName,
                PasswordHash = vm.Password

            };
            var userinfo = new UserInfo()
            {
                Email = vm.Email,
                PhoneNum = vm.PhoneNum,
                address1 = vm.address1,
                FirstName = vm.FirstName,
                LastName = vm.LastName,


            };
            var result = await _userManager.CreateAsync(user, vm.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                _ctx.Add(userinfo);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Index", "Home");

            }


            return View(vm);




        }
    }
}


