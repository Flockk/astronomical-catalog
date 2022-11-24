#nullable disable
using System;
using System.Collections.Generic;

namespace Space.Models;

public partial class StarClusters
{
    public int StarclusterId { get; set; }

    public int? ConsId { get; set; }

    public int? GlxId { get; set; }

    public string StarclusterName { get; set; }

    public string StarclusterType { get; set; }

    public TimeSpan? StarclusterRightAscension { get; set; }

    public string StarclusterDeclination { get; set; }

    public double? StarclusterDistance { get; set; }

    public int? StarclusterAge { get; set; }

    public double? StarclusterDiameter { get; set; }

    public virtual Constellations Cons { get; set; }

    public virtual Galaxies Glx { get; set; }

    public virtual ICollection<Stars> Stars { get; } = new List<Stars>();
}