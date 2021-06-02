using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OnlineBookLibraryClient.Lib.Model;
using OnlineBookLibraryClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var url = "http://localhost:39523/api/User/Login";
            var client = new HttpClient();
            var userDto = new LoginViewModel
            {
                Email = model.Email,
                Password = model.Password,
                RememberMe = false,
            };
            var postRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(userDto)
            };
            var response = await client.SendAsync(postRequest);



            //response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            if (content == "fail")
            {
                return View();
            }
            else
            {
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(content);
                //var jwt = content;
                //var handler = new JwtSecurityTokenHandler();
                //var token = handler.ReadJwtToken(jwt);
                HttpContext.Session.SetString("token", loginResponse.Token);
                var tokenString = HttpContext.Session.GetString("token");
                if (tokenString != null)
                {
                    return View("../Home/index");
                }
                return View(model);

            }


        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel registerModel)
        {
            //var url = BASE_URL + "api/auth/register";
            var url = "http://localhost:39523/api/User/Register";
            HttpClient client = new HttpClient();
            var registerDto = new RegisterUserViewModel()
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                Password = registerModel.Password,
                ConfirmPassword = registerModel.ConfirmPassword,
                Email = registerModel.Email,

            };
            var postRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(registerDto)
            };
            var response = await client.SendAsync(postRequest);

            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<RegisterUserViewModel>(content);


            return View(responseDto);

        }
        [HttpGet]
        public IActionResult UserDashBoard()
        {
            return View();
        }
        public IActionResult AdminDashboard()
        {
            return View();
        }
    }
}
