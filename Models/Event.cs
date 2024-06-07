using System;
using System.Collections.Generic;

namespace CulturalSiberiaProject.Models;

public partial class Event
{
    public int Id { get; set; }

    public bool Isfavorite { get; set; }

    public int? Cinemaid { get; set; }

    public int? Concertid { get; set; }

    public int? Museumid { get; set; }

    public virtual Cinema? Cinema { get; set; }

    public virtual Concert? Concert { get; set; }

    public virtual Museum? Museum { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
