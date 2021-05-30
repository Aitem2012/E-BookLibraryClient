using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineBookLibraryClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibraryClient.Controllers
{
    public class AuthController : Controller
    {
        //private readonly UserManager _userManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            ModelState.AddModelError("FirstName", "Please fill it");
            return View(registerModel);
        }
    }
}
