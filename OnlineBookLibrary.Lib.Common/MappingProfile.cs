using AutoMapper;
using OnlineBookLibrary.Lib.DTO;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Text;


namespace OnlineBookLibrary.Lib.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Book, BookDetailsDTO>().ReverseMap();
            this.CreateMap<AppUser, UserDTO>().ReverseMap();
        }
    }
}
