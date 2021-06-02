using Microsoft.AspNetCore.Mvc;
using OnlineBookLibraryClient.Lib.Model;
using OnlineBookLibraryClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult Book(BookViewModel model)
        {
            Book book = new Book();
            book.Title = model.Title;
            
            return View();
        }
    }
}
