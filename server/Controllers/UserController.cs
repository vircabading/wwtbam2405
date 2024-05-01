using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


using server.Services;
using server.Models;
using server.Dtos;
namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
  //register
  private IUserService _userService;
  private IAuthService _authService;
  private ITokenService _tokenService;


  public UserController(IUserService userService, IAuthService authService, ITokenService tokenService)
  {
    _userService = userService;
    _authService = authService;
    _tokenService = tokenService;
  }
  // [HttpGet]
  // public IActionResult GetUserByName([FromQuery] string username)
  // {

  //   User user = _userService.GetUserByName(username);
  //   return user is not null ? Ok(user)
  //   : BadRequest("No user found with this username");

  // }

  // [HttpGet]
  // public IActionResult GetUserById([FromQuery] string userId)
  // {
  //   User user = _userService.GetUserById(userId);
  //   Console.WriteLine("Userid: " + userId);
  //   Console.WriteLine("User " + user);
  //   return user is not null ? Ok(user)
  //   : BadRequest("No user found with this userId");
  // }

  //register with email, password, name, username
  // {
  //   "Email": "default@gmail.com",
  //   "Password": "P@ssw0rd",
  //   "Name": "Default",
  //   "Username": "Default"
  // }
  [HttpPost("register")]
  public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
  {
        try
        {
      if(!ModelState.IsValid){
        return BadRequest(ModelState);
      }
      var result = await _authService.Register(registerDto);

      if (result.Item1.Succeeded)
      {
        return Ok(
          new NewUserDto{
            UserName = registerDto.Username,
            Email = registerDto.Email,
            Token = _tokenService.CreateToken(result.Item2)
          }
        );
      }
      else
      {
        return StatusCode(500);
      }
    } catch (Exception e) {
      Console.WriteLine(e.Message);
      return StatusCode(500);
    }
  }



  //login with username and password
  //{
  // "Username": "Default",
  // "Password": "P@ssw0rd"
  // }
  [HttpPost("login")]
  public async Task<ActionResult> SignIn([FromBody]LoginDto loginDto) 
  {
    try{
      if(!ModelState.IsValid){
        return BadRequest(ModelState);
      }
      var user = await _authService.Login(loginDto);
      if (user is not null)
      {
        return Ok(
          new NewUserDto{
            UserName = user.UserName,
            Email = user.Email,
            Token = _tokenService.CreateToken(user)
          }
        );
      }
      else
      {
        return Unauthorized();
      }
    } catch (Exception e) {
      Console.WriteLine(e.Message);
      return StatusCode(500);
    }
  }

  [HttpGet("logout"), Authorize]
  public async Task<ActionResult> Logout()
  {
    try
    {
      await _authService.Logout();
    }
    catch (Exception e)
    {
      return BadRequest("Logout failed, please try again" + e.Message);
    }
    return Ok("Logged out successfully");
  }

  [HttpGet("verify"), Authorize]
  public async Task<ActionResult> VerifyUser()
  {
    try
    {
      var (isVerified, user) = await _authService.Authorize(HttpContext);
      if (!isVerified)
      {
        return Unauthorized("Not authorized to view this page");
      }
      return Ok(new {message = "Login succuessful", user });
    } catch (Exception e)
    {
      return BadRequest("Error verifying user: " + e.Message);
    }
  }
}