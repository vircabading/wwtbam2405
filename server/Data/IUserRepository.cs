namespace server.Data;
using server.Models;
public interface IUserRepository
{
  //GET
  User? GetUserByName(string username);
  User? GetUserById(string userId);

  void DeleteUserByName(string username);
  void UpdateUser(User user);
}