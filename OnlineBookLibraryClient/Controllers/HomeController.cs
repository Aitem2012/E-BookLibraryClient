using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OnlineBookLibrary.Lib.DTO.BookResponse;
using OnlineBookLibraryClient.Models;
using OnlineBookLibraryClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnlineBookLibraryClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var url = "http://localhost:39523/api/book/all-books/";
            HttpClient client = new HttpClient();
            var userResponse = await client.GetAsync(url);
            var output = new List<BookResponseViewModel>();
            var content = await userResponse.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<IEnumerable<BookResponseDTO>>(content);
            foreach(var item in responseDto)
            {
                var obj = new BookResponseViewModel()
                {
                    Title = item.Title,
                    GenreName = item.GenreName,
                    Photo = item.Photo,
                    Rating = item.Rating,
                    AuthorsFirstName = item.AuthorName,
                    ISBN = item.ISBN,
                    Description = item.Description
                };
                output.Add(obj);
            }
            
            return View(output);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AdminDashboard()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
