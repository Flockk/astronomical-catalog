#nullable disable
using System;
using System.Collections.Generic;

namespace Space.Models;

public partial class Galaxies
{
    public int GlxId { get; set; }

    public int? ConsId { get; set; }

    public int? GlxclusterId { get; set; }

    public int? GlxgroupId { get; set; }

    public string GlxName { get; set; }

    public string GlxType { get; set; }

    public TimeSpan? GlxRightAscension { get; set; }

    public string GlxDeclination { get; set; }

    public double? GlxRedshift { get; set; }

    public int? GlxDistance { get; set; }

    public double? GlxApparentMagnitude { get; set; }

    public int? GlxRadialVelocity { get; set; }

    public double? GlxRadius { get; set; }

    public virtual ICollection<BlackHoles> BlackHoles { get; } = new List<BlackHoles>();

    public virtual Constellations Cons { get; set; }

    public virtual GalaxyClusters Glxcluster { get; set; }

    public virtual GalaxyGroups Glxgroup { get; set; }

    public virtual ICollection<Nebulae> Nebulae { get; } = new List<Nebulae>();

    public virtual ICollection<PlanetarySystems> PlanetarySystems { get; } = new List<PlanetarySystems>();

    public virtual ICollection<StarClusters> StarClusters { get; } = new List<StarClusters>();

    public virtual ICollection<Stars> Stars { get; } = new List<Stars>();
}