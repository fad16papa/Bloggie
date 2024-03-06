using System.Net;
using System.Security.Claims;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
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
                    //assign the userrole
                    var roleIdentityResult = await _userManager.AddToRoleAsync(identityUser, "User");

                    if (roleIdentityResult.Succeeded)
                    {
                        //show success notification
                        return RedirectToAction("Register");
                    }
                }
            }

            //show error notification
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var model = new LoginViewModel()
            {
                ReturnUrl = returnUrl
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

        public async Task SignInWithGoogle()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleCallback"),
            });
        }

        public async Task<IActionResult> GoogleCallback()
        {
            try
            {
                var authenticationResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                if (!authenticationResult.Succeeded)
                {
                    var failureReason = authenticationResult.Failure?.Message;
                    var properties = authenticationResult.Properties;
                    // var claims = authenticationResult.Principal.Claims;
                    // Log or print the details for analysis
                    Console.WriteLine($"Authentication failed: {failureReason}");
                    Console.WriteLine($"Authentication properties: {properties}");
                    // Console.WriteLine($"Claims: {string.Join(", ", claims.Select(c => $"{c.Type}: {c.Value}"))}");
                }

                if (authenticationResult.Succeeded)
                {
                    // The user is successfully authenticated with Google
                    var googleClaims = authenticationResult.Principal.Claims;

                    // Extract user information from Google claims
                    var userId = googleClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    var userEmail = googleClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                    var userName = googleClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                    // Your logic for user registration or sign-in goes here
                    // For example, you can check if the user already exists in your database and sign them in.

                    // TODO: Implement your user registration or sign-in logic here

                    // Optionally, you can sign in the user using ASP.NET Core Identity
                    // Example: await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Handle the case where authentication failed
                    // You may want to log the failure or redirect to an error page
                    // Example: return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions appropriately
                // Example: _logger.LogError(ex, "An error occurred during Google authentication callback");
                return RedirectToAction("Error");
            }

            return PartialView("Error", "This is just a test!!!");
        }

        [HttpGet]
        public IActionResult SignOut()
        {
            return SignOut(new AuthenticationProperties { RedirectUri = "/" },
                CookieAuthenticationDefaults.AuthenticationScheme, GoogleDefaults.AuthenticationScheme);
        }
    }
}
