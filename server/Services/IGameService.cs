using server.Models;
using server.Data;

namespace server.Services;

public interface IGameService
{
    // Create

    public Game AddNewGame(Game newGame);

    // Retrieve

    public IEnumerable<Game> GetAllGames();
    public Game? GetGameById(int id);
    public IEnumerable<Game> GetHighestScoreGames(int numGames);

    // Update
    // Delete

    public Game? DeleteGame(int id);
}