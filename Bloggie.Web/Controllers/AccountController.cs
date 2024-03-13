using System.Security.Claims;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new IdentityUser()
                {
                    UserName = registerViewModel.Username,
                    Email = registerViewModel.Email,
                };

                var identityResult = await _userManager.CreateAsync(identityUser, registerViewModel.Password);

                if (identityResult.Succeeded)
                {
                    // Generate email confirmation token
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);

                    // Build confirmation link
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = identityUser.Id, code = token }, protocol: HttpContext.Request.Scheme);

                    //assign the userrole
                    var roleIdentityResult = await _userManager.AddToRoleAsync(identityUser, "User");

                    // Send confirmation email
                    await _emailSender.SendEmailAsync(registerViewModel.Email, "Confirm your email",
                        $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");

                    if (roleIdentityResult.Succeeded)
                    {
                        //show success notification
                        return RedirectToAction("Account", "ConfirmEmailSent");
                    }
                }
            }

            //show error notification
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction("Error");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return RedirectToAction("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return RedirectToAction("Account", "EmailConfirmed");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public IActionResult ConfirmEmailSent()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EmailConfirmed()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginViewModel.Password))
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                // Set a custom claim or additional data in the authentication cookie, if needed
                // For example, you can store user roles, claims, or other information.
                // await _userManager.AddClaimAsync(user, new Claim("customClaimType", "customClaimValue"));

                // Check if there is a valid return URL
                if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl) && Url.IsLocalUrl(loginViewModel.ReturnUrl))
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }

                ViewBag.Usrname = User.Identity.Name;

                // Redirect to the default page after successful login
                return RedirectToAction("Index", "Home");
            }

            // If login fails, add an error message to ModelState
            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return View(loginViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            var loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");

                return View("Login", loginViewModel);
            }

            // Get the login information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");

                return View("Login", loginViewModel);
            }

            // If the user already has a login (i.e if there is a record in AspNetUserLogins
            // table) then sign-in the user with this external login provider
            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            // If there is no record in AspNetUserLogins table, the user may not have
            // a local account
            else
            {
                // Get the email claim value
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    // Create a new user without password if we do not have a user already
                    var user = await _userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new IdentityUser
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                        };

                        await _userManager.CreateAsync(user);
                    }

                    // Add a login (i.e insert a row for the user in AspNetUserLogins table)
                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }

                // If we cannot find the user email we cannot continue
                ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
                ViewBag.ErrorMessage = "Please contact support on Pragim@PragimTech.com";

                return View("Error");
            }
        }
    }
}
