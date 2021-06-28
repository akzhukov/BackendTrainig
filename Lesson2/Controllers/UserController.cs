using AutoMapper;
using Lesson2.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.DTOs;
using Shared.Models;
using Shared.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Services.JWT;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Lesson2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IMapper mapper;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IJwtService jwtService;

        public UserController(ILogger<UserController> logger,
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
        [Route("auth")]
        [HttpPost]
        public async Task<ActionResult<string>> LogIn(UserLogInDto item)
        {
            var user = userManager.Users.FirstOrDefault(user => user.UserName == item.Login);
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

        [Route("password/update")]
        [HttpPost]
        public async Task<ActionResult<UserUpdatePasswordDto>> UpdatePassword(UserUpdatePasswordDto updatedUser)
        {
            var identity = (ClaimsIdentity)HttpContext.User.Identity;
            var name = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (name == null)
            {
                return NotFound("User name was not found");
            }
            var user = userManager.Users.FirstOrDefault(user => user.UserName == name);
            var res = await signInManager.PasswordSignInAsync(user, updatedUser.OldPassword, false, false);
            if (!res.Succeeded)
            {
                logger.LogError($"Error on updating password for user with id {user.Id}");
                return BadRequest("Wrong password");
            }

            user.PasswordHash = userManager.PasswordHasher.HashPassword(user, updatedUser.NewPassword);

            var updateRes = await userManager.UpdateAsync(user);
            if (updateRes.Succeeded)
            {
                logger.LogInformation($"Password updated for user with id {user.Id}");
                return Ok("Password updated"); ;
            }
            logger.LogError($"Error on updating password for user with id {user.Id}");
            return Problem($"Error on updating password for user with id {user.Id}");
        }

        [Authorize(Policy = "Administrator")]
        [Route("all")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            var users = await userManager.Users.ToArrayAsync();
            return Ok(mapper.Map<IEnumerable<UserDto>>(users));
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Post(UserCreateDto item)
        {
            try
            {
                var userAlreadyExist = userManager.FindByNameAsync(item.UserName);
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

        [Authorize(Policy = "Administrator")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(string id)
        {
            var user = userManager.Users.FirstOrDefault(user => user.Id == id);
            if (user == null)
            {
                return BadRequest($"User with id {id} does not found");
            }
            return Ok(mapper.Map<UserDto>(user));

        }

        [Authorize(Policy = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = userManager.Users.FirstOrDefault(user => user.Id == id);
                if (user == null)
                {
                    return BadRequest($"User with id {id} does not found");
                }
                var res = await userManager.DeleteAsync(user);
                if (!res.Succeeded)
                {
                    throw new Exception($"Error on deletion user with id {user.Id}.  {res.Errors}");
                }
                return Ok($"User with id {id} removed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }

        }


    }
}
