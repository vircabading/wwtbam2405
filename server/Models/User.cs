using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace server.Models;

public partial class User : IdentityUser
{

    public string? Name { get; set; }

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
