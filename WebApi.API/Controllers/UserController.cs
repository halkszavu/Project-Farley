﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.Bll.Dtos;

namespace WebApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration config;

        public UserController(UserManager<IdentityUser> usermanager, SignInManager<IdentityUser> signinmanager, IConfiguration configuration)
        {
            userManager = usermanager;
            signInManager = signinmanager;
            config = configuration;
        }

        /// <summary>
        /// Registers a new user to the database.
        /// </summary>
        /// <param name="model">A simple model that contains the username/email and the password</param>
        /// <returns>JWT token if the creation of the user was successful</returns>
        [HttpPost("register")]
        public async Task<string> Register([FromBody]LoginDto model)
        {
            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await userManager.CreateAsync(user, model.Password);

            if(result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                return GenerateJwtToken(model.Email, user);
            }

            throw new Exception("Unknown Error");
        }

        /// <summary>
        /// Login to the application
        /// </summary>
        /// <param name="model">A simple model that contains the username/email and the password</param>
        /// <returns>JWT token if the login was successful</returns>
        [HttpPost("login")]
        public async Task<string> Login([FromBody] LoginDto model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                return GenerateJwtToken(model.Email, appUser);
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        private string GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtKey"]));
            var credit = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(2);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: credit);

            string r;
            try
            {
                r = new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
                return r;
        }
    }
}