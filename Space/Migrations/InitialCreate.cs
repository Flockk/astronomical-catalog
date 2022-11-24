using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Space.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Constellations",
                columns: table => new
                {
                    consid = table.Column<int>(name: "cons_id", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    consname = table.Column<string>(name: "cons_name", type: "TEXT", maxLength: 50, nullable: false),
                    consabbreviation = table.Column<string>(name: "cons_abbreviation", type: "TEXT", maxLength: 3, nullable: false),
                    conssymbolism = table.Column<string>(name: "cons_symbolism", type: "TEXT", maxLength: 22, nullable: false),
                    consrightascension = table.Column<string>(name: "cons_right_ascension", type: "TEXT", maxLength: 17, nullable: true),
                    consdeclination = table.Column<string>(name: "cons_declination", type: "TEXT", maxLength: 15, nullable: true),
                    conssquare = table.Column<int>(name: "cons_square", type: "INTEGER", nullable: true),
                    consvisibleinlatitudes = table.Column<string>(name: "cons_visible_in_latitudes", type: "TEXT", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constellations", x => x.consid);
                });

            migrationBuilder.CreateTable(
                name: "GalaxyClusters",
                columns: table => new
                {
                    glxclusterid = table.Column<int>(name: "glxcluster_id", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    consid = table.Column<int>(name: "cons_id", type: "INTEGER", nullable: false),
                    glxclustername = table.Column<string>(name: "glxcluster_name", type: "TEXT", maxLength: 30, nullable: false),
                    glxclustertype = table.Column<string>(name: "glxcluster_type", type: "TEXT", maxLength: 20, nullable: true),
                    glxclusterrightascension = table.Column<TimeSpan>(name: "glxcluster_right_ascension", type: "time(0)", nullable: true),
                    glxclusterdeclination = table.Column<string>(name: "glxcluster_declination", type: "TEXT", maxLength: 20, nullable: true),
                    glxclusterredshift = table.Column<double>(name: "glxcluster_redshift", type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalaxyClusters", x => x.glxclusterid);
                    table.ForeignKey(
                        name: "FK_GalaxyClusters_To_Constellations",
                        column: x => x.consid,
                        principalTable: "Constellations",
                        principalColumn: "cons_id");
                });

            migrationBuilder.CreateTable(
                name: "GalaxyGroups",
                columns: table => new
                {
                    glxgroupid = table.Column<int>(name: "glxgroup_id", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    consid = table.Column<int>(name: "cons_id", type: "INTEGER", nullable: true),
                    glxgroupname = table.Column<string>(name: "glxgroup_name", type: "TEXT", maxLength: 50, nullable: false),
                    glxgrouptype = table.Column<string>(name: "glxgroup_type", type: "TEXT", maxLength: 11, nullable: true),
                    glxgrouprightascension = table.Column<TimeSpan>(name: "glxgroup_right_ascension", type: "time(0)", nullable: true),
                    glxgroupdeclination = table.Column<string>(name: "glxgroup_declination", type: "TEXT", maxLength: 20, nullable: true),
                    glxgroupredshift = table.Column<double>(name: "glxgroup_redshift", type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalaxyGroups", x => x.glxgroupid);
                    table.ForeignKey(
                        name: "FK_GalaxyGroups_To_Constellations",
                        column: x => x.consid,
                        principalTable: "Constellations",
                        principalColumn: "cons_id");
                });

            migrationBuilder.CreateTable(
                name: "Galaxies",
                columns: table => new
                {
                    glxid = table.Column<int>(name: "glx_id", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    consid = table.Column<int>(name: "cons_id", type: "INTEGER", nullable: true),
                    glxclusterid = table.Column<int>(name: "glxcluster_id", type: "INTEGER", nullable: true),
                    glxgroupid = table.Column<int>(name: "glxgroup_id", type: "INTEGER", nullable: true),
                    glxname = table.Column<string>(name: "glx_name", type: "TEXT", maxLength: 50, nullable: false),
                    glxtype = table.Column<string>(name: "glx_type", type: "TEXT", maxLength: 14, nullable: true),
                    glxrightascension = table.Column<TimeSpan>(name: "glx_right_ascension", type: "time(0)", nullable: true),
                    glxdeclination = table.Column<string>(name: "glx_declination", type: "TEXT", maxLength: 20, nullable: true),
                    glxredshift = table.Column<double>(name: "glx_redshift", type: "REAL", nullable: true),
                    glxdistance = table.Column<int>(name: "glx_distance", type: "INTEGER", nullable: true),
                    glxapparentmagnitude = table.Column<double>(name: "glx_apparent_magnitude", type: "REAL", nullable: true),
                    glxradialvelocity = table.Column<int>(name: "glx_radial_velocity", type: "INTEGER", nullable: true),
                    glxradius = table.Column<double>(name: "glx_radius", type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Galaxies", x => x.glxid);
                    table.ForeignKey(
                        name: "FK_Galaxies_To_Constellations",
                        column: x => x.consid,
                        principalTable: "Constellations",
                        principalColumn: "cons_id");
                    table.ForeignKey(
                        name: "FK_Galaxies_To_GalaxyClusters",
                        column: x => x.glxclusterid,
                        principalTable: "GalaxyClusters",
                        principalColumn: "glxcluster_id");
                    table.ForeignKey(
                        name: "FK_Galaxies_To_GalaxyGroups",
                        column: x => x.glxgroupid,
                        principalTable: "GalaxyGroups",
                        principalColumn: "glxgroup_id");
                });

            migrationBuilder.CreateTable(
                name: "BlackHoles",
                columns: table => new
                {
                    blackHoleid = table.Column<int>(name: "blackHole_id", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    consid = table.Column<int>(name: "cons_id", type: "INTEGER", nullable: false),
                    glxid = table.Column<int>(name: "glx_id", type: "INTEGER", nullable: true),
                    blackholename = table.Column<string>(name: "blackhole_name", type: "TEXT", maxLength: 50, nullable: false),
                    blackholetype = table.Column<string>(name: "blackhole_type", type: "TEXT", maxLength: 14, nullable: true),
                    blackholerightascension = table.Column<TimeSpan>(name: "blackhole_right_ascension", type: "time(0)", nullable: true),
                    blackholedeclination = table.Column<string>(name: "blackhole_declination", type: "TEXT", maxLength: 20, nullable: true),
                    blackholedistance = table.Column<double>(name: "blackhole_distance", type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackHoles", x => x.blackHoleid);
                    table.ForeignKey(
                        name: "FK_BlackHoles_To_Constellations",
                        column: x => x.consid,
                        principalTable: "Constellations",
                        principalColumn: "cons_id");
                    table.ForeignKey(
                        name: "FK_BlackHoles_To_Galaxies",
                        column: x => x.glxid,
                        principalTable: "Galaxies",
                        principalColumn: "glx_id");
                });

            migrationBuilder.CreateTable(
                name: "Nebulae",
                columns: table => new
                {
                    nebulaid = table.Column<int>(name: "nebula_id", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    consid = table.Column<int>(name: "cons_id", type: "INTEGER", nullable: false),
                    glxid = table.Column<int>(name: "glx_id", type: "INTEGER", nullable: true),
                    nebulaname = table.Column<string>(name: "nebula_name", type: "TEXT", maxLength: 50, nullable: false),
                    nebulatype = table.Column<string>(name: "nebula_type", type: "TEXT", maxLength: 20, nullable: true),
                    nebularightascension = table.Column<TimeSpan>(name: "nebula_right_ascension", type: "time(0)", nullable: true),
                    nebuladeclination = table.Column<string>(name: "nebula_declination", type: "TEXT", maxLength: 20, nullable: true),
                    nebuladistance = table.Column<int>(name: "nebula_distance", type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Nebulae", x => x.nebulaid);
                    table.ForeignKey(
                        name: "FK_Nebulae_Galaxies",
                        column: x => x.glxid,
                        principalTable: "Galaxies",
                        principalColumn: "glx_id");
                    table.ForeignKey(
                        name: "FK_Nebulae_To_Constellations",
                        column: x => x.consid,
                        principalTable: "Constellations",
                        principalColumn: "cons_id");
                });

            migrationBuilder.CreateTable(
                name: "PlanetarySystems",
                columns: table => new
                {
                    planetsystemid = table.Column<int>(name: "planetsystem_id", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    consid = table.Column<int>(name: "cons_id", type: "INTEGER", nullable: true),
                    glxid = table.Column<int>(name: "glx_id", type: "INTEGER", nullable: true),
                    planetsystemname = table.Column<string>(name: "planetsystem_name", type: "TEXT", maxLength: 50, nullable: false),
                    planetsystemconfirmedplanets = table.Column<byte>(name: "planetsystem_confirmed_planets", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanetarySystems", x => x.planetsystemid);
                    table.ForeignKey(
                        name: "FK_PlanetarySystems_Galaxies",
                        column: x => x.glxid,
                        principalTable: "Galaxies",
                        principalColumn: "glx_id");
                    table.ForeignKey(
                        name: "FK_PlanetarySystems_To_Constellations",
                        column: x => x.consid,
                        principalTable: "Constellations",
                        principalColumn: "cons_id");
                });

            migrationBuilder.CreateTable(
                name: "StarClusters",
                columns: table => new
                {
                    starclusterid = table.Column<int>(name: "starcluster_id", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    consid = table.Column<int>(name: "cons_id", type: "INTEGER", nullable: true),
                    glxid = table.Column<int>(name: "glx_id", type: "INTEGER", nullable: true),
                    starclustername = table.Column<string>(name: "starcluster_name", type: "TEXT", maxLength: 50, nullable: false),
                    starclustertype = table.Column<string>(name: "starcluster_type", type: "TEXT", maxLength: 20, nullable: true),
                    starclusterrightascension = table.Column<TimeSpan>(name: "starcluster_right_ascension", type: "time(0)", nullable: true),
                    starclusterdeclination = table.Column<string>(name: "starcluster_declination", type: "TEXT", maxLength: 20, nullable: true),
                    starclusterdistance = table.Column<double>(name: "starcluster_distance", type: "REAL", nullable: true),
                    starclusterage = table.Column<int>(name: "starcluster_age", type: "INTEGER", nullable: true),
                    starclusterdiameter = table.Column<double>(name: "starcluster_diameter", type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarClusters", x => x.starclusterid);
                    table.ForeignKey(
                        name: "FK_StarClusters_Galaxies",
                        column: x => x.glxid,
                        principalTable: "Galaxies",
                        principalColumn: "glx_id");
                    table.ForeignKey(
                        name: "FK_StarClusters_To_Constellations",
                        column: x => x.consid,
                        principalTable: "Constellations",
                        principalColumn: "cons_id");
                });

            migrationBuilder.CreateTable(
                name: "Stars",
                columns: table => new
                {
                    starid = table.Column<int>(name: "star_id", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    consid = table.Column<int>(name: "cons_id", type: "INTEGER", nullable: true),
                    glxid = table.Column<int>(name: "glx_id", type: "INTEGER", nullable: true),
                    starclusterid = table.Column<int>(name: "starcluster_id", type: "INTEGER", nullable: true),
                    planetsystemid = table.Column<int>(name: "planetsystem_id", type: "INTEGER", nullable: true),
                    starname = table.Column<string>(name: "star_name", type: "TEXT", maxLength: 50, nullable: false),
                    starapparentmagnitude = table.Column<double>(name: "star_apparent_magnitude", type: "REAL", nullable: true),
                    starstellarclass = table.Column<string>(name: "star_stellar_class", type: "TEXT", maxLength: 10, nullable: true),
                    stardistance = table.Column<double>(name: "star_distance", type: "REAL", nullable: true),
                    stardeclination = table.Column<string>(name: "star_declination", type: "TEXT", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stars", x => x.starid);
                    table.ForeignKey(
                        name: "FK_Stars_Constellations",
                        column: x => x.consid,
                        principalTable: "Constellations",
                        principalColumn: "cons_id");
                    table.ForeignKey(
                        name: "FK_Stars_To_Galaxies",
                        column: x => x.glxid,
                        principalTable: "Galaxies",
                        principalColumn: "glx_id");
                    table.ForeignKey(
                        name: "FK_Stars_To_PlanetarySystems",
                        column: x => x.planetsystemid,
                        principalTable: "PlanetarySystems",
                        principalColumn: "planetsystem_id");
                    table.ForeignKey(
                        name: "FK_Stars_To_StarClusters",
                        column: x => x.starclusterid,
                        principalTable: "StarClusters",
                        principalColumn: "starcluster_id");
                });

            migrationBuilder.CreateTable(
                name: "Asteroids",
                columns: table => new
                {
                    astid = table.Column<int>(name: "ast_id", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    starid = table.Column<int>(name: "star_id", type: "INTEGER", nullable: true),
                    astname = table.Column<string>(name: "ast_name", type: "TEXT", maxLength: 50, nullable: false),
                    astdiameter = table.Column<int>(name: "ast_diameter", type: "INTEGER", nullable: true),
                    astorbitaleccentricity = table.Column<double>(name: "ast_orbital_eccentricity", type: "REAL", nullable: true),
                    astorbitalinclination = table.Column<double>(name: "ast_orbital_inclination", type: "REAL", nullable: true),
                    astargumentofperihelion = table.Column<double>(name: "ast_argument_of_perihelion", type: "REAL", nullable: true),
                    astmeananomaly = table.Column<double>(name: "ast_mean_anomaly", type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asteroids", x => x.astid);
                    table.ForeignKey(
                        name: "FK_Asteroids_Stars",
                        column: x => x.starid,
                        principalTable: "Stars",
                        principalColumn: "star_id");
                });

            migrationBuilder.CreateTable(
                name: "Comets",
                columns: table => new
                {
                    cometid = table.Column<int>(name: "comet_id", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    starid = table.Column<int>(name: "star_id", type: "INTEGER", nullable: true),
                    cometname = table.Column<string>(name: "comet_name", type: "TEXT", maxLength: 50, nullable: false),
                    cometorbitalperiod = table.Column<double>(name: "comet_orbital_period", type: "REAL", nullable: true),
                    cometsemimajoraxis = table.Column<double>(name: "comet_semi_major_axis", type: "REAL", nullable: true),
                    cometperihelion = table.Column<double>(name: "comet_perihelion", type: "REAL", nullable: true),
                    cometeccentricity = table.Column<double>(name: "comet_eccentricity", type: "REAL", nullable: true),
                    cometorbitalinclination = table.Column<double>(name: "comet_orbital_inclination", type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comets", x => x.cometid);
                    table.ForeignKey(
                        name: "FK_Comets_Stars",
                        column: x => x.starid,
                        principalTable: "Stars",
                        principalColumn: "star_id");
                });

            migrationBuilder.CreateTable(
                name: "Planets",
                columns: table => new
                {
                    plntid = table.Column<int>(name: "plnt_id", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    consid = table.Column<int>(name: "cons_id", type: "INTEGER", nullable: true),
                    starid = table.Column<int>(name: "star_id", type: "INTEGER", nullable: true),
                    plntname = table.Column<string>(name: "plnt_name", type: "TEXT", maxLength: 50, nullable: false),
                    plnteccentricity = table.Column<double>(name: "plnt_eccentricity", type: "REAL", nullable: true),
                    plntsemimajoraxis = table.Column<double>(name: "plnt_semi_major_axis", type: "REAL", nullable: true),
                    plntorbitalperiod = table.Column<double>(name: "plnt_orbital_period", type: "REAL", nullable: true),
                    plntargumentofperihelion = table.Column<double>(name: "plnt_argument_of_perihelion", type: "REAL", nullable: true),
                    plntmass = table.Column<double>(name: "plnt_mass", type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planets", x => x.plntid);
                    table.ForeignKey(
                        name: "FK_Planets_Stars",
                        column: x => x.starid,
                        principalTable: "Stars",
                        principalColumn: "star_id");
                    table.ForeignKey(
                        name: "FK_Planets_To_Constellations",
                        column: x => x.consid,
                        principalTable: "Constellations",
                        principalColumn: "cons_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asteroids_star_id",
                table: "Asteroids",
                column: "star_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Asteroids_ast_name",
                table: "Asteroids",
                column: "ast_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlackHoles_cons_id",
                table: "BlackHoles",
                column: "cons_id");

            migrationBuilder.CreateIndex(
                name: "IX_BlackHoles_glx_id",
                table: "BlackHoles",
                column: "glx_id");

            migrationBuilder.CreateIndex(
                name: "UQ__BlackHoles_blackhole_name",
                table: "BlackHoles",
                column: "blackhole_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comets_star_id",
                table: "Comets",
                column: "star_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Comets__comet_name",
                table: "Comets",
                column: "comet_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Constellations_cons_abbreviation",
                table: "Constellations",
                column: "cons_abbreviation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Constellations_cons_name",
                table: "Constellations",
                column: "cons_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Constellations_cons_symbolism",
                table: "Constellations",
                column: "cons_symbolism",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Galaxies_cons_id",
                table: "Galaxies",
                column: "cons_id");

            migrationBuilder.CreateIndex(
                name: "IX_Galaxies_glxcluster_id",
                table: "Galaxies",
                column: "glxcluster_id");

            migrationBuilder.CreateIndex(
                name: "IX_Galaxies_glxgroup_id",
                table: "Galaxies",
                column: "glxgroup_id");

            migrationBuilder.CreateIndex(
                name: "UQ_Galaxies_glx_name",
                table: "Galaxies",
                column: "glx_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GalaxyClusters_cons_id",
                table: "GalaxyClusters",
                column: "cons_id");

            migrationBuilder.CreateIndex(
                name: "UQ_GalaxyClusters_glxcluster_name",
                table: "GalaxyClusters",
                column: "glxcluster_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GalaxyGroups_cons_id",
                table: "GalaxyGroups",
                column: "cons_id");

            migrationBuilder.CreateIndex(
                name: "UQ_GalaxyGroups_glxgroup_name",
                table: "GalaxyGroups",
                column: "glxgroup_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nebulae_cons_id",
                table: "Nebulae",
                column: "cons_id");

            migrationBuilder.CreateIndex(
                name: "IX_Nebulae_glx_id",
                table: "Nebulae",
                column: "glx_id");

            migrationBuilder.CreateIndex(
                name: "UQ_Nebulae_nebula_name",
                table: "Nebulae",
                column: "nebula_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanetarySystems_cons_id",
                table: "PlanetarySystems",
                column: "cons_id");

            migrationBuilder.CreateIndex(
                name: "IX_PlanetarySystems_glx_id",
                table: "PlanetarySystems",
                column: "glx_id");

            migrationBuilder.CreateIndex(
                name: "UQ_PlanetarySystems_planetsystem_name",
                table: "PlanetarySystems",
                column: "planetsystem_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Planets_cons_id",
                table: "Planets",
                column: "cons_id");

            migrationBuilder.CreateIndex(
                name: "IX_Planets_star_id",
                table: "Planets",
                column: "star_id");

            migrationBuilder.CreateIndex(
                name: "UQ_Planets_exo_name",
                table: "Planets",
                column: "plnt_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StarClusters_cons_id",
                table: "StarClusters",
                column: "cons_id");

            migrationBuilder.CreateIndex(
                name: "IX_StarClusters_glx_id",
                table: "StarClusters",
                column: "glx_id");

            migrationBuilder.CreateIndex(
                name: "UQ_StarClusters_starcluster_name",
                table: "StarClusters",
                column: "starcluster_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stars_cons_id",
                table: "Stars",
                column: "cons_id");

            migrationBuilder.CreateIndex(
                name: "IX_Stars_glx_id",
                table: "Stars",
                column: "glx_id");

            migrationBuilder.CreateIndex(
                name: "IX_Stars_planetsystem_id",
                table: "Stars",
                column: "planetsystem_id");

            migrationBuilder.CreateIndex(
                name: "IX_Stars_starcluster_id",
                table: "Stars",
                column: "starcluster_id");

            migrationBuilder.CreateIndex(
                name: "UQ_Stars_star_name",
                table: "Stars",
                column: "star_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asteroids");

            migrationBuilder.DropTable(
                name: "BlackHoles");

            migrationBuilder.DropTable(
                name: "Comets");

            migrationBuilder.DropTable(
                name: "Nebulae");

            migrationBuilder.DropTable(
                name: "Planets");

            migrationBuilder.DropTable(
                name: "Stars");

            migrationBuilder.DropTable(
                name: "PlanetarySystems");

            migrationBuilder.DropTable(
                name: "StarClusters");

            migrationBuilder.DropTable(
                name: "Galaxies");

            migrationBuilder.DropTable(
                name: "GalaxyClusters");

            migrationBuilder.DropTable(
                name: "GalaxyGroups");

            migrationBuilder.DropTable(
                name: "Constellations");
        }
    }
}
