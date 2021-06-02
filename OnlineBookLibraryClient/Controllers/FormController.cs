using Microsoft.AspNetCore.Mvc;
using OnlineBookLibrary.Lib.DTO;
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
    public class FormController: Controller
    {
        [HttpGet]
        public IActionResult Book()
        {
            BookViewModel model = new BookViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Book(BookViewModel model)
        {
            var url = "http://localhost:39523/api/book/register";
            HttpClient client = new HttpClient();
            BookDetailsDTO book = new BookDetailsDTO
            {
                Title = model.Title,
                Genre = new Genre
                {
                    GenreName = model.GenreName,
                },
                //book.Photo = model.Photo;
                ISBN = model.ISBN,
                Language = model.Language,
                Publisher = new Publisher
                {
                    PublisherName = model.PublisherName
                },
                PublicationDate = model.PublicationDate,
                DateAddedToLibrary = model.DateAddedToLibrary,
                Author = new Author
                {
                    FirstName = model.AuthorsFirstName,
                    LastName = model.AuthorsLastName
                },
                Pages = model.Pages,
                Description = model.Description
            };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(book)
            };

            var response = await client.SendAsync(postRequest);
            var content = await response.Content.ReadAsStringAsync();
            if(content != "Book Created Successfully")
            {
                return View();
            }
            else
            {
                return View();
            }

            
        }
    }
}
