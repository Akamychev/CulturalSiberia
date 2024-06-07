using System;
using System.Collections.Generic;

namespace CulturalSiberiaProject.Models;

public partial class Cinema
{
    public int Id { get; set; }

    public DateOnly? Realisedate { get; set; }

    public string? Genre { get; set; }

    public int? Budget { get; set; }

    public string? Nameing { get; set; }

    public string? Contry { get; set; }

    public string? Languages { get; set; }

    public string? Runningtime { get; set; }

    public string? Studio { get; set; }

    public int? Artistsid { get; set; }

    public virtual Artist? Artists { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
