using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using server.Models;
using server.Services;

namespace server.Controllers;

[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;
    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    // Create

    [HttpPost]
    public ActionResult<Game> AddNewGame(Game newGame)
    {
        try
        {
            return Ok(_gameService.AddNewGame(newGame));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Retrieve
    
    [HttpGet()]
    // [Authorize]
    public ActionResult<IEnumerable<Game>> GetAllGames()
    {
        try
        {
            return Ok(_gameService.GetAllGames());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("/{id}")]
    public ActionResult<Game> GetGameById(int id)
    {
        try
        {
            return Ok(_gameService.GetGameById(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Highest/{numGames}")]
    public ActionResult<IEnumerable<Game>> GetHighestScoreGames(int numGames)
    {
        try
        {
            return Ok(_gameService.GetHighestScoreGames(numGames));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Update
    
    // Delete

    [HttpDelete]
    public ActionResult DeleteGame(int id)
    {
        try
        {
            return Ok(_gameService.DeleteGame(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}