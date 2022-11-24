#nullable disable
using System;
using System.Collections.Generic;

namespace Space.Models;

public partial class Stars
{
    public int StarId { get; set; }

    public int? ConsId { get; set; }

    public int? GlxId { get; set; }

    public int? StarclusterId { get; set; }

    public int? PlanetsystemId { get; set; }

    public string StarName { get; set; }

    public double? StarApparentMagnitude { get; set; }

    public string StarStellarClass { get; set; }

    public double? StarDistance { get; set; }

    public string StarDeclination { get; set; }

    public virtual ICollection<Asteroids> Asteroids { get; } = new List<Asteroids>();

    public virtual ICollection<Comets> Comets { get; } = new List<Comets>();

    public virtual Constellations Cons { get; set; }

    public virtual Galaxies Glx { get; set; }

    public virtual ICollection<Planets> Planets { get; } = new List<Planets>();

    public virtual PlanetarySystems Planetsystem { get; set; }

    public virtual StarClusters Starcluster { get; set; }
}