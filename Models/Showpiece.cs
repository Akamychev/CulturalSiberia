using System;
using System.Collections.Generic;

namespace CulturalSiberiaProject.Models;

public partial class Showpiece
{
    public int Id { get; set; }

    public string? Nameing { get; set; }

    public bool? Originality { get; set; }

    public string? Subject { get; set; }

    public decimal? Price { get; set; }

    public DateOnly? Borndate { get; set; }

    public string? History { get; set; }

    public virtual ICollection<Museum> Museums { get; set; } = new List<Museum>();
}
