using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AccountController : ControllerBase
  {

    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return BadRequest(ModelState);
        }

        var appUser = new AppUser
        {
          UserName = registerDto.UserName,
          Email = registerDto.Email,

        };

        var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
        if (createdUser.Succeeded)
        {
          var roleResult = await _userManager.AddToRoleAsync(appUser, "USER");
          if (roleResult.Succeeded)
          {
            return Ok("User registered successfully");
          }
          else
          {
            return StatusCode(500, roleResult.Errors);
          }
        }
        else
        {
          return StatusCode(500, createdUser.Errors);
        }

      }
      catch (Exception ex)
      { 
        return StatusCode(500, ex);
      }

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var user = await _userManager.Users.FirstOrDefaultAsync(us => us.UserName == loginDto.UserName);
      if (user == null)
      {
        return Unauthorized("Invalid username or password");
      }

      var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

      if (!result.Succeeded)
      {
        return Unauthorized("Invalid username or password");
      }

      return Ok(new NewUserDto { UserName = user.UserName, Email = user.Email, Token = _tokenService.CreateToken(user) });
    }

  }
}