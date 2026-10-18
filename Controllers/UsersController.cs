using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ProductsValidation.Models;
using ProductsValidation.Services;

namespace ProductsValidation.Controllers
{
    public class UsersController : Controller
    {
        private List<User> users;

        public UsersController(Data data)
        {
            users = data.Users;
        }
        
        public IActionResult Index(string id)
        {
            return View("Index", users);
        }
        [HttpGet]
        public IActionResult Create([FromQuery] User user)
        {
            return View(user);
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreatePost(User user)
        {
            user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
            users.Add(user);
            return RedirectToAction("View", new { id = user.Id });
        }

        public IActionResult View(int id)
        {
            User user = users.Find(u => u.Id == id);
            if (user == null)
                return NotFound();

            return View(user);
        }
    }
}
