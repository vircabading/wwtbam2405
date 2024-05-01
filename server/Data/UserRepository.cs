using server.Models;
using Microsoft.EntityFrameworkCore;

namespace server.Data;

class UserRepository : IUserRepository
{
  private readonly KsjvContext _context;
  public UserRepository(KsjvContext context)
  {
    _context = context;
  }

  //GET ONE
  public User? GetUserByName(string username)
  {
    return _context.Users.FirstOrDefault(u => u.Name.Equals(username));
  }

  public void DeleteUserByName(string username)
  {
    User? toBeDeleted = _context.Users.FirstOrDefault(u => u.Name.Equals(username));
    if (toBeDeleted != null)
    {
      _context.Users.Remove(toBeDeleted);
      _context.SaveChanges();
    }
  }


  public User? GetUserById(string userId)
  {
    return _context.Users.FirstOrDefault(u => u.Id.Equals(userId));

  }

  public void UpdateUser(User user)
  {
    User? existingUser = GetUserByName(user.Name);

    if (existingUser != null)
    {
      existingUser.Name = user.Name;
      Console.WriteLine("Name updated");
      _context.SaveChanges();
    }
  }
}