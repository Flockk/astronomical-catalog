#nullable disable
using System;
using System.Collections.Generic;

namespace Space.Models;

public partial class PlanetarySystems
{
    public int PlanetsystemId { get; set; }

    public int? ConsId { get; set; }

    public int? GlxId { get; set; }

    public string PlanetsystemName { get; set; }

    public byte PlanetsystemConfirmedPlanets { get; set; }

    public virtual Constellations Cons { get; set; }

    public virtual Galaxies Glx { get; set; }

    public virtual ICollection<Stars> Stars { get; } = new List<Stars>();
}