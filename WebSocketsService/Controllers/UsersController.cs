using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.DTOs;
using Shared.Models;
using Shared.Services.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebSocketsService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> logger;
        private readonly IMapper mapper;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IJwtService jwtService;

        public UsersController(ILogger<UsersController> logger,
                              IMapper mapper,
                              RoleManager<IdentityRole> roleManager,
                              UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              IJwtService jwtService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<ActionResult<string>> LogIn([FromBody]UserLogInDto item)
        {
            var user = userManager.Users.FirstOrDefault(user => user.UserName == item.Login);
            if(user == null)
            {
                return NotFound("User was not found");
            }
            var result = await signInManager.PasswordSignInAsync(user, item.Password, false, false);
            if (result.Succeeded)
            {
                return await jwtService.GenerateToken(user, userManager);
            }
            logger.LogInformation("Invalid login attempt");
            throw new ApplicationException("Invalid login attempt");
        }

        [Route("current")]
        [HttpGet]
        public ActionResult<UserDto> GetCurrentUser()
        {
            var identity = (ClaimsIdentity)HttpContext.User.Identity;
            var name = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (name == null)
            {
                return NotFound("User name was not found");
            }
            var user = userManager.Users.FirstOrDefault(user => user.UserName == name);
            return Ok(mapper.Map<UserDto>(user));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserDto>> Post([FromBody]UserCreateDto item)
        {
            try
            {
                var userAlreadyExist = await userManager.FindByNameAsync(item.UserName);
                if (userAlreadyExist != null)
                {
                    return BadRequest($"Such user already exists.");
                }
                var roleExist = await roleManager.RoleExistsAsync(item.Role);
                if (!roleExist)
                {
                    return BadRequest($"Role {item.Role} does not exist.");
                }
                var user = mapper.Map<User>(item);
                user.PasswordHash = userManager.PasswordHasher.HashPassword(user, item.Password);
                var res = await userManager.CreateAsync(user);
                if (!res.Succeeded)
                {
                    throw new Exception($"User creation failed. {res.Errors}");
                }

                var createdUser = await userManager.FindByNameAsync(user.UserName);
                res = await userManager.AddToRoleAsync(createdUser, item.Role);
                if (!res.Succeeded)
                {
                    throw new Exception($"Error on creation user with id {user.Id}.  {res.Errors}");
                }
                return Ok(mapper.Map<UserDto>(user));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
