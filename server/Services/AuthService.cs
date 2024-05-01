using Microsoft.AspNetCore.Identity;
using server.Dtos;
using server.Models;
using server.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace server.Services;

public class AuthService : IAuthService
{
  private readonly SignInManager<User> _signInManager;
  private readonly UserManager<User> _userManager; private readonly IUserRepository _userRepository;
  public AuthService(IUserRepository userRepository, SignInManager<User> signInManager, UserManager<User> userManager)
  {
    _userRepository = userRepository;
    _signInManager = signInManager;
    _userManager = userManager;
  }
  public async Task<(bool isVerified, User user)> Authorize(HttpContext context)
  {
    var _user = context.User;
    var principal = new ClaimsPrincipal(_user);
    var result = _signInManager.IsSignedIn(principal);

    if (!result)
    {
      return (false, null);
    }

    var user = await _signInManager.UserManager.GetUserAsync(principal);
    return (true, user);
  }

  public async Task<User> Login(LoginDto dto)
  {
    try
    {
      User? user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == dto.Username);
      if (user is not null){
        var result = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.Remember, false);
        if (result.Succeeded)
        {
          var updated = await _userManager.UpdateAsync(user);
          // updated.Succeeded
          return user;
        }
        Console.WriteLine("Email or password was incorrect, please try again");
        return null;
      }
      Console.WriteLine("User not found");
      return null;
    }
    catch (Exception e)
    {
      throw new Exception("Error logging in: " + e.Message);
    }
  }

  public async Task<bool> Logout()
  {
    try
    {
      await _signInManager.SignOutAsync();
      Console.WriteLine("User logged out");
      return true;
    }
    catch (Exception e)
    {
      Console.WriteLine("Error logging out: " + e.Message);
      return false;
    }
  }

  public async Task<(IdentityResult, User?)> Register(RegisterDto dto)
  {
    try
    {
      User newUser = new User()
      {
        Name = dto.Name,
        UserName = dto.Username,
        Email = dto.Email
      };
      var result = await _userManager.CreateAsync(newUser, dto.Password);
      if(result.Succeeded){
          return (result, newUser);
      } else{
        return (result, null);
      }

    }
    catch (Exception e)
    {
      throw new Exception("Error registering user: " + e.Message);

    }
  }

}