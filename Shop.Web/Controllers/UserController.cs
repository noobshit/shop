using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Data.Context;
using Shop.Data.Models;
using Shop.Web.ViewModels;

namespace Shop.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ShopUser> _userManager;
        private readonly SignInManager<ShopUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ShopContext _shopContext;

        public UserController(UserManager<ShopUser> userManager, SignInManager<ShopUser> signInManager, IMapper mapper, ShopContext shopContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _shopContext = shopContext;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ShopUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Cart = new Cart()
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if(model.IsAdmin)
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }

                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("index", "home");
                }

                foreach( var error in result.Errors )
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager
                        .PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("index", "home");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditDetails()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null)
            {
                return RedirectToAction("SignIn");
            }

            var model =  _mapper.Map<ContactViewModel>(user.Contact);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditDetails(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                if (user == null)
                {
                    return RedirectToAction("SignIn");
                }

                if (user.Contact != null)
                {
                    model.Id = user.Contact.Id;
                    _mapper.Map(model, user.Contact);
                    _shopContext.SaveChanges();
                } 
                else
                {
                    var contact = _mapper.Map<Contact>(model);
                    user.Contact = contact;
                    _shopContext.SaveChanges();
                }
            }

            return View(model);
        }
    }
}