using server.Data;
using server.Models;

namespace server.Services;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;

  public UserService(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public void DeleteUserByName(string username)
  {
    _userRepository.DeleteUserByName(username);
  }

  // public User? GetUserById(string userId)
  // {
  //   return _userRepository.GetUserById(userId);
  // }

  // public User? GetUserByName(string username)
  // {
  //   return _userRepository.GetUserByName(username);
  // }

  
  public void UpdateUser(User user)
  {
    _userRepository.UpdateUser(user);
  }
}