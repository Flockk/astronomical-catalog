#nullable disable
using System;
using System.Collections.Generic;

namespace Space.Models;

public partial class Constellations
{
    public int ConsId { get; set; }

    public string ConsName { get; set; }

    public string ConsAbbreviation { get; set; }

    public string ConsSymbolism { get; set; }

    public string ConsRightAscension { get; set; }

    public string ConsDeclination { get; set; }

    public int? ConsSquare { get; set; }

    public string ConsVisibleInLatitudes { get; set; }

    public virtual ICollection<BlackHoles> BlackHoles { get; } = new List<BlackHoles>();

    public virtual ICollection<Galaxies> Galaxies { get; } = new List<Galaxies>();

    public virtual ICollection<GalaxyClusters> GalaxyClusters { get; } = new List<GalaxyClusters>();

    public virtual ICollection<GalaxyGroups> GalaxyGroups { get; } = new List<GalaxyGroups>();

    public virtual ICollection<Nebulae> Nebulae { get; } = new List<Nebulae>();

    public virtual ICollection<PlanetarySystems> PlanetarySystems { get; } = new List<PlanetarySystems>();

    public virtual ICollection<Planets> Planets { get; } = new List<Planets>();

    public virtual ICollection<StarClusters> StarClusters { get; } = new List<StarClusters>();

    public virtual ICollection<Stars> Stars { get; } = new List<Stars>();
}