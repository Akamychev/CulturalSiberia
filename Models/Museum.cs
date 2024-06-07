using System;
using System.Collections.Generic;

namespace CulturalSiberiaProject.Models;

public partial class Museum
{
    public int Id { get; set; }

    public string? Nameofmuseum { get; set; }

    public DateOnly? Foundationdate { get; set; }

    public string? Founder { get; set; }

    public DateTime? Hoursofworks { get; set; }

    public string? Hisory { get; set; }

    public int? Showpieces { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual Showpiece? ShowpiecesNavigation { get; set; }
}
