using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineBookLibrary.Lib.Core;
using OnlineBookLibrary.Lib.Core.Interfaces;
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
        private readonly IOptionsMonitor<JwtConfig> _options;
        private TokenGenerator tokenGen; 
        private readonly ICloudinaryService _cloudinaryService;


        public UserController(LibraryDbContext context, UserManager<AppUser> userManager,
                                SignInManager<AppUser> signManager, RoleManager<IdentityRole> roleManager,
                                IMapper mapper, IOptionsMonitor<JwtConfig> options,
                                ICloudinaryService cloudinaryService)
        {
            _context = context;
            _userManager = userManager;
            _signManager = signManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _options = options;
            tokenGen = new TokenGenerator(_userManager, _options);
            _cloudinaryService = cloudinaryService;
        }

        [HttpGet]
        [Route("get-user-by-id/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound("user not found");

            return Ok(user);
        }
        [HttpPost]
        [Route("register")]
        public async Task<RegisterDTO> Register([FromBody] RegisterDTO model)
        {
            //Creating New User
            var userExist = await _userManager.FindByEmailAsync(model.Email);
            if (userExist != null) throw new Exception();
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
               throw new Exception ("something went wrong");
            }
            //Add Role to User
            await _userManager.AddToRoleAsync(user, "Regular User");
            var modelToReturn = new RegisterDTO
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };
            return modelToReturn;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return NotFound("fail");
            var x = await _signManager.PasswordSignInAsync(user, model.Password, false, false); ;

            if (!x.Succeeded)
            {
                return BadRequest("fail");
            }

            var tokenGen = new TokenGenerator(_userManager, _options);
            var token = await tokenGen.GenerateToken(user);
            if (token == null)
            {
                return BadRequest();
            }

            return Ok( new { token });
        }

        public IActionResult GetUserRole(string token)
        {
            var claims = tokenGen.GetTokenClaims(token);

            // List<string> admin = new List<string>();
            string role = "";

            foreach (var c in claims)
            {
                //admin.Add(c.Type + ":" + c.Value);
                if (c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                    role = c.Value;
            }

            //var admin = claims.Where(x => x.Type.Contains("roles"));

            // var admin = HttpContext.User.HasClaim(c => c.Type == "roles");

            return Ok(new { role });
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var users = _userManager.Users.ToList();
                if (users.Count() <= 0) return NotFound("No User Registered");
                return Ok(users);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
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

        [HttpPatch]
        [Route("updatephoto/{id}")]
        public async Task<IActionResult> UpdatePhoto(string id, [FromForm] PhotoUpdateDTO photoUpdate)
        {
            var userToUpdate = await _userManager.FindByIdAsync(id);
            if (userToUpdate == null) return NotFound($"Could not find book with ISBN of {id}");
            userToUpdate.Photo = await _cloudinaryService.AddPatchPhoto(photoUpdate);
            var photoIsUpdated = await _userManager.UpdateAsync(userToUpdate);
            if (!photoIsUpdated.Succeeded)
            {
                return StatusCode(500, "Something went wrong, try again");
            }
            return Ok($"Photo Path Successfully Updated");
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