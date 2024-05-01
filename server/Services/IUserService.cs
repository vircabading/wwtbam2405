namespace server.Services;
using server.Models;
public interface IUserService
{
  // User? GetUserByName(string username);
  // User? GetUserById(string userId);

  void DeleteUserByName(string username);
  void UpdateUser(User user);

  

}
