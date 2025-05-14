using AgriEnergyConnect.Data;
using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace AgriEnergyConnect.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employee/ManageUsers
        [Authorize(Roles = "Employee")]

        public IActionResult ManageUsers()
        {
            ViewBag.UserRole = "Employee";
            var users = _context.Users.ToList();
            return View(users);
        }

        // GET: Employee/CreateUser
        [Authorize(Roles = "Employee")]

        public IActionResult CreateUser()
        {
            ViewBag.Roles = new SelectList(new[]
            {
        new { Value = "Farmer", Text = "Farmer" },
        new { Value = "Employee", Text = "Employee" }
    }, "Value", "Text");

            return View();
        }


        // POST: Employee/CreateUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee")]

        public IActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                var hasher = new PasswordHasher<User>();
                user.Password = hasher.HashPassword(user, user.Password);
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction(nameof(ManageUsers));
            }

            // Fix: Remove the hashed password so it doesn’t get shown back in the form
            user.Password = string.Empty;

            ViewBag.Roles = new SelectList(new[] {
        new { Value = "Farmer", Text = "Farmer" },
        new { Value = "Employee", Text = "Employee" }
    }, "Value", "Text");

            return View(user);
        }

        [Authorize(Roles = "Employee")]
        public IActionResult EditUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        // POST: Employee/EditUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee")]

        public IActionResult EditUser(User user)
        {
            var existingUser = _context.Users.Find(user.UserID); // Find the user by ID (or another unique field)

            if (existingUser != null)
            {
                existingUser.FullName = user.FullName; // Update only the Name
                existingUser.Email = user.Email; // Update only the Email

                _context.Users.Update(existingUser); // Mark the entity for update
                _context.SaveChanges(); // Save changes to the database
            }

            return RedirectToAction(nameof(ManageUsers)); // Redirect after update
        }


        // GET: Employee/DeleteUser/5
        [Authorize(Roles = "Employee")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(ManageUsers));
        }
    }
}
