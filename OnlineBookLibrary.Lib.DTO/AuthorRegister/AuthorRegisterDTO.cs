﻿using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.DTO.AuthorRegister
{
    public class AuthorRegisterDTO
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Book> Book { get; set; }
    }
}