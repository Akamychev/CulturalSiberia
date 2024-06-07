using System;
using System.Collections.Generic;

namespace CulturalSiberiaProject.Models;

public partial class Artist
{
    public int Id { get; set; }

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string? Mname { get; set; }

    public string? Works { get; set; }

    public DateOnly? Birthday { get; set; }

    public DateOnly? Deathdate { get; set; }

    public virtual ICollection<Cinema> Cinemas { get; set; } = new List<Cinema>();

    public virtual ICollection<Concert> Concerts { get; set; } = new List<Concert>();
}
