using System;
using System.Collections.Generic;

namespace server.Models;

public partial class Game
{
    public int GameId { get; set; }

    public string UserId { get; set; }

    public int? Score { get; set; }

    public DateTime? PlayedAt { get; set; }

    public virtual User CreatedBy { get; set; } = null!;
}
