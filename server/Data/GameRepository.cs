using server.Data;
using server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
namespace server.Data;

public class GameRepository : IGameRepository
{
    private readonly KsjvContext _context;
    public GameRepository(KsjvContext context)
    {
        _context = context;
    }

    // Create

    public Game AddNewGame(Game newGame)
    {
        _context.Games.Add(newGame);
        _context.SaveChanges();
        return newGame;
    }

    public Game? GetGameById(int id)
    {
        return _context.Games.Find(id);
    }

    public IEnumerable<Game> GetAllGamesSorted()
    {
        return _context.Games.OrderByDescending(g => g.Score).ToList();
    }

    // Retrieve
    
    public IEnumerable<Game> GetAllGames()
    {
        return _context.Games.ToList();
    }

    // Update

    // Delete

    public Game? DeleteGame(int id)
    {
        Game? gameToDelete = GetGameById(id);
        try
        {
            if (gameToDelete != null)
            {
                _context.Games.Remove(gameToDelete);
                _context.SaveChanges();
                return gameToDelete;
            } 
        }
        catch (Exception ex)
        {
            throw new Exception($"No Game to Delete: {ex.Message}");
        }
        return gameToDelete;
    }

}