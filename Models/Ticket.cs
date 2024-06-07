using System;
using System.Collections.Generic;

namespace CulturalSiberiaProject.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public string? Eventname { get; set; }

    public decimal? Price { get; set; }

    public DateOnly? Eventdate { get; set; }

    public int? Userid { get; set; }

    public int? Eventid { get; set; }

    public virtual Event? Event { get; set; }

    public virtual User? User { get; set; }
}
