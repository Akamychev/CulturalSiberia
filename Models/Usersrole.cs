using System;
using System.Collections.Generic;

namespace CulturalSiberiaProject.Models;

public partial class Usersrole
{
    public string Roles { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
