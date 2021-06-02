using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineBookLibrary.Lib.Core.Interfaces;
using OnlineBookLibrary.Lib.DTO;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.Core.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _configuration;
        
        
        public CloudinaryService(IConfiguration configuration)
        {
            _configuration = configuration;

            Account account = new Account
            {
                Cloud = _configuration.GetSection("CloudinarySettings:CloudName").Value,
                ApiKey = _configuration.GetSection("CloudinarySettings:ApiKey").Value,
                ApiSecret = _configuration.GetSection("CloudinarySettings:ApiSecret").Value
            };
            _cloudinary = new Cloudinary(account);
        }

        
        public async Task<string> AddPatchPhoto(PhotoUpdateDTO photoUpdateDto)
        {   
            var file = photoUpdateDto.PhotoUrl;
            if (file.Length <= 0) return ("Invalid File Size");
            var imageUploadResult = new ImageUploadResult();
            using (var fs = file.OpenReadStream())
            {
                var imageUploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, fs),
                    Transformation = new Transformation().Width(300).Height(300).Crop("fill").Gravity("face")
                };
                imageUploadResult = await _cloudinary.UploadAsync(imageUploadParams);
            }
            //var publicId = imageUploadResult.PublicId;
            var Url = imageUploadResult.Url.ToString();
            return Url;
        }
    }
}
