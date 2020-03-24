using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
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
            var model = new SignUpViewModel();
            return View(model);
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
                    if(model.Role == "Admin")
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

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public async Task<IActionResult> SignIn()
        {
            var externalLogins = await _signInManager.GetExternalAuthenticationSchemesAsync();
            var model = new SignInViewModel
            {
                ExternalLogins = externalLogins,
            };
            return View(model);
        }

        public IActionResult UseExternalProvider(string provider)
        {
            var redirectUrl = Url.Action("ExternalProviderCallback", "User");
            var props = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, props);
        }

        public async Task<IActionResult> ExternalProviderCallback(string returnUrl, string remoteError)
        {
            returnUrl ??= Url.Action("Index", "Cart");
            var model = new SignInViewModel
            {
                ExternalLogins = await _signInManager.GetExternalAuthenticationSchemesAsync()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider {remoteError}");
                return View("SignIn", model);
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, $"Error loading external info");
                return View("SignIn", model);
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            
            var email = info.Principal.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null)
            {
                ModelState.AddModelError(string.Empty, $"Email claim not receveid from {info.ProviderDisplayName}");
                return View("SignIn", model);
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ShopUser
                {
                    Email = email,
                    UserName = email,
                    Cart = new Cart(),
                };

                await _userManager.CreateAsync(user);
            }
            await _userManager.AddLoginAsync(user, info);
            await _signInManager.SignInAsync(user, false);

            return LocalRedirect(returnUrl);
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
        public async Task<IActionResult> EditDetails(ContactViewModel model, string returnUrl)
        {
            if( ModelState.IsValid )
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                if( user == null )
                {
                    return RedirectToAction("SignIn");
                }

                if( user.Contact != null )
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

                if( !string.IsNullOrEmpty(returnUrl) )
                {
                    return Redirect(returnUrl);
                }
            }

            return View(model);
        }
    }
}