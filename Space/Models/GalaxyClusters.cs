#nullable disable
using System;
using System.Collections.Generic;

namespace Space.Models;

public partial class GalaxyClusters
{
    public int GlxclusterId { get; set; }

    public int ConsId { get; set; }

    public string GlxclusterName { get; set; }

    public string GlxclusterType { get; set; }

    public TimeSpan? GlxclusterRightAscension { get; set; }

    public string GlxclusterDeclination { get; set; }

    public double? GlxclusterRedshift { get; set; }

    public virtual Constellations Cons { get; set; }

    public virtual ICollection<Galaxies> Galaxies { get; } = new List<Galaxies>();
}