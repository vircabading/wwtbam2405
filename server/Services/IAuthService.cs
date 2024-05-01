namespace server.Services;
using Microsoft.AspNetCore.Identity;
using server.Dtos;
using server.Models;

public interface IAuthService
{
  Task<(IdentityResult, User?)> Register(RegisterDto dto);
  Task<User> Login(LoginDto dto);
  Task<bool> Logout();
  Task<(bool isVerified, User user)> Authorize(HttpContext context);
}