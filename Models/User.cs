using System;
using System.Collections.Generic;

namespace CulturalSiberiaProject.Models;

public partial class User
{
    public int Id { get; set; }

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string? Mname { get; set; }

    public string? Email { get; set; }

    public string? Login { get; set; }

    public string? Userpassword { get; set; }

    public string? Userrole { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual Usersrole? UserroleNavigation { get; set; }
}
