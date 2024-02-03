using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserInterface _userInterface;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminUsersController(IUserInterface userInterface, UserManager<IdentityUser> userManager)
        {
            _userInterface = userInterface;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await _userInterface.GetAll();

            var usersViewModel = new UserViewModel();
            usersViewModel.Users = new List<User>();

            foreach (var item in users)
            {
                usersViewModel.Users.Add(new Models.ViewModels.User
                {
                    Id = Guid.Parse(item.Id),
                    Username = item.UserName,
                    Email= item.Email,
                });
            }

            return View(usersViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(UserViewModel userViewModel)
        {
            var identityUser = new IdentityUser()
            {
                UserName = userViewModel.Username, 
                Email = userViewModel.Email
            };

            var identityResult = await _userManager.CreateAsync(identityUser, userViewModel.Password);

            if(identityResult != null)
            {
                if(identityResult.Succeeded) 
                {
                    //assign new roles for the new user
                    var roles = new List<string> { "User" };

                    if(userViewModel.AdminRole)
                    {
                        roles.Add("Admin");
                    }

                    identityResult = await _userManager.AddToRolesAsync(identityUser, roles);

                    if (identityResult != null && identityResult.Succeeded)
                    {
                        return RedirectToAction("List", "AdminUsers");
                    }
                }
            }

            return View();
        }
    }
}
