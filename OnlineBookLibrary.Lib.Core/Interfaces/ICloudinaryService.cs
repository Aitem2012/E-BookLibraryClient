using Microsoft.AspNetCore.Mvc;
using OnlineBookLibrary.Lib.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.Core.Interfaces
{
    public interface ICloudinaryService
    {
        public Task<string> AddPatchPhoto([FromForm] PhotoUpdateDTO photoUpdateDto);
    }
}
