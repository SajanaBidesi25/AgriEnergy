using AgriEnergyConnect.Data;
using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AgriEnergyConnect.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Email already registered.");
                    return View(model);
                }

                var hasher = new PasswordHasher<User>();
                model.Password = hasher.HashPassword(model, model.Password);

                _context.Users.Add(model);
                _context.SaveChanges(); // Save first to generate UserId

                // Save into Farmer or Employee table
                if (model.Role == UserRole.Farmer)
                {
                    var farmer = new Farmer { UserID = model.UserID };
                    _context.Farmers.Add(farmer);
                }
                else if (model.Role == UserRole.Employee)
                {
                    var employee = new Employee { UserID = model.UserID };
                    _context.Employees.Add(employee);
                }

                _context.SaveChanges(); // Save Farmer/Employee entry

                return RedirectToAction("Login");
            }

            return View(model);
        }



        // GET: Account/Login
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // Find the user based on the email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            Farmer farmer = _context.Farmers.FirstOrDefault(f => f.UserID == user.UserID);


            if (user != null)
            {
                // Create a password hasher to check the password
                var hasher = new PasswordHasher<User>();

                // Verify the hashed password stored in the database with the password entered by the user
                var result = hasher.VerifyHashedPassword(user, user.Password, model.Password);

                if (result == PasswordVerificationResult.Success)
                {
                    // Create the claims for the user (Name and Role)
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()), // Store role as string
                new Claim("UserID", user.UserID.ToString())
            };

                    if (farmer != null)
                    {
                        claims.Add(new Claim("FarmerID", farmer.FarmerID.ToString()));
                    }

                    // Create claims identity based on authentication scheme
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    // Sign in the user with the claims identity
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    // Redirect to the Home page after successful login
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found.");
            }

            // If login fails, return the login view with error message
            return View(model);
        }


        // Logout 
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
