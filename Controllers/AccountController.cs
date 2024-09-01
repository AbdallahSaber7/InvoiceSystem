using InvoiceData.Entities;
using InvoiceSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        // Constructor to inject UserManager and SignInManager
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: /Account/Register
        // Displays the registration page
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        // Handles form submission for user registration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create a new user with the provided username and email
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                // Attempt to create the user with the specified password
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Optionally sign in the user after registration
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    // Redirect to the home page after successful registration
                    return RedirectToAction("Index", "Home");
                }

                // Add errors to ModelState if the user creation failed
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If model validation failed, redisplay the registration form
            return View(model);
        }

        // GET: /Account/Login
        // Displays the login page
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        // Handles form submission for user login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by username
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user != null)
                {
                    // Attempt to sign in the user with the provided password
                    var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        // Redirect to the home page after successful login
                        return RedirectToAction("Index", "Home");

                    }
                    else if (result.IsLockedOut)
                    {
                        // Redirect to lockout page if the user is locked out
                        return RedirectToAction("Lockout");
                    }
                    else
                    {
                        // Add an error message if login attempt failed
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }
                }
                else
                {
                    // Add an error message if the user was not found
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                }
            }

            // If model validation failed, redisplay the login form 
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
