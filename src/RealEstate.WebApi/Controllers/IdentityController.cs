using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Domain.ViewModels;
using RealEstate.Infrasctructure.Token;
using RealEstate.WebApi.Controllers;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CyberMaster.Backend.WebApi.Controllers
{
    public class IdentityController : BaseApiController
    {
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly SignInManager<IdentityUser<int>> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IJWTTokenGeneratorService _jWTTokenGeneratorService;

        public IdentityController(
            UserManager<IdentityUser<int>> userManager,
            SignInManager<IdentityUser<int>> signInManager,
            RoleManager<IdentityRole<int>> roleManager,
            IJWTTokenGeneratorService jWTTokenGeneratorService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jWTTokenGeneratorService = jWTTokenGeneratorService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
                return BadRequest("Username not found");

            var singIn = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!singIn.Succeeded)
                return BadRequest("Password is incorrect");

            IList<Claim> claims = await _userManager.GetClaimsAsync(user);

            var result = new
            {
                Result = singIn,
                user.UserName,
                user.Email,
                Token = _jWTTokenGeneratorService.GenerateToken(user, claims)
            };

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            var userToCreate = new IdentityUser<int>
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(userToCreate, model.Password);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }
    }
}