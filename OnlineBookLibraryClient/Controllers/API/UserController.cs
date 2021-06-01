using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineBookLibrary.Lib.Core;
using OnlineBookLibrary.Lib.Core.Services;
using OnlineBookLibrary.Lib.DTO;
using OnlineBookLibraryClient.Lib.Infrastructure;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookLibraryClient.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly LibraryDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly JwtConfig _options;
        private readonly IOptionsMonitor<JwtConfig> options;

        public UserController(LibraryDbContext context, UserManager<AppUser> userManager,
                                SignInManager<AppUser> signManager, RoleManager<IdentityRole> roleManager,
                                IMapper mapper, IOptionsMonitor<JwtConfig> options)
        {
            _context = context;
            _userManager = userManager;
            _signManager = signManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _options = options.CurrentValue;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            //Creating New User
            var user = new AppUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            //Checks if Role exists , Creates Role if it doesn't exist
            var role = await _roleManager.RoleExistsAsync("Regular User");
            if (!role)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Regular User" });
            }

            if (!result.Succeeded)
            {
                return BadRequest("something went wrong");
            }
            //Add Role to User
            await _userManager.AddToRoleAsync(user, "Regular User");
            return Ok("Registration successful");
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var x = await _signManager.PasswordSignInAsync(user, model.Password, false, false); ;

            if (!x.Succeeded)
            {
                return BadRequest("something went wrong");
            }

            var tokenGen = new TokenGenerator(_userManager, options);
            var token = await tokenGen.GenerateToken(user);
            if (token == null)
            {
                return BadRequest();
            }

            return Ok(token);
        }



        [HttpPatch]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, UserDTO userUpdate)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound($"Could not find user with id of {id}");
                }
                user.FirstName = userUpdate.FirstName;
                user.LastName = userUpdate.LastName;
                var result = _userManager.UpdateAsync(user);

                return Ok(_mapper.Map<UserDTO>(user));

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null) return NotFound("User Not Found");
                await _userManager.DeleteAsync(user);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return BadRequest();
        }
    }
}
