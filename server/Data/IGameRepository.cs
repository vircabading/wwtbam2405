using server.Models;
namespace server.Data;

public interface IGameRepository
{
    // Create
    public Game AddNewGame(Game newGame);
    // Retrieve
    IEnumerable<Game> GetAllGames();
    public Game? GetGameById(int id);

    public IEnumerable<Game> GetAllGamesSorted();
    // Update
    // Delete
    public Game? DeleteGame(int id);
}