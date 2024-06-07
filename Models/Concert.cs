using System;
using System.Collections.Generic;

namespace CulturalSiberiaProject.Models;

public partial class Concert
{
    public int Id { get; set; }

    public int? Numberofseats { get; set; }

    public DateOnly? Concertdate { get; set; }

    public int? Duration { get; set; }

    public string? Programofconcert { get; set; }

    public int? Artistsid { get; set; }

    public virtual Artist? Artists { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
