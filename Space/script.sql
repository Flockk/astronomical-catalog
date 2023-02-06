USE [master]
GO
CREATE DATABASE [Space]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Space', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Space.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Space_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Space_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Space] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Space].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Space] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Space] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Space] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Space] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Space] SET ARITHABORT OFF 
GO
ALTER DATABASE [Space] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Space] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Space] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Space] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Space] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Space] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Space] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Space] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Space] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Space] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Space] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Space] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Space] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Space] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Space] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Space] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Space] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Space] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Space] SET  MULTI_USER 
GO
ALTER DATABASE [Space] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Space] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Space] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Space] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Space] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Space] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Space] SET QUERY_STORE = OFF
GO
USE [Space]
GO
/****** Object:  Table [dbo].[Asteroids]    Script Date: 07.02.2023 0:59:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Asteroids](
	[ast_id] [int] IDENTITY(1,1) NOT NULL,
	[star_id] [int] NULL,
	[ast_name] [nvarchar](50) NOT NULL,
	[ast_diameter] [int] NULL,
	[ast_orbital_eccentricity] [float] NULL,
	[ast_orbital_inclination] [float] NULL,
	[ast_argument_of_perihelion] [float] NULL,
	[ast_mean_anomaly] [float] NULL,
	[ast_image] [nvarchar](255) NULL,
 CONSTRAINT [PK_Asteroids] PRIMARY KEY CLUSTERED 
(
	[ast_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Asteroids_ast_name] UNIQUE NONCLUSTERED 
(
	[ast_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlackHoles]    Script Date: 07.02.2023 0:59:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlackHoles](
	[blackHole_id] [int] IDENTITY(1,1) NOT NULL,
	[cons_id] [int] NULL,
	[glx_id] [int] NULL,
	[blackhole_name] [nvarchar](50) NOT NULL,
	[blackhole_type] [nvarchar](14) NULL,
	[blackhole_right_ascension] [time](0) NULL,
	[blackhole_declination] [nvarchar](20) NULL,
	[blackhole_distance] [float] NULL,
	[blackhole_image] [nvarchar](255) NULL,
 CONSTRAINT [PK_BlackHoles] PRIMARY KEY CLUSTERED 
(
	[blackHole_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__BlackHoles_blackhole_name] UNIQUE NONCLUSTERED 
(
	[blackhole_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comets]    Script Date: 07.02.2023 0:59:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comets](
	[comet_id] [int] IDENTITY(1,1) NOT NULL,
	[star_id] [int] NULL,
	[comet_name] [nvarchar](50) NOT NULL,
	[comet_orbital_period] [float] NULL,
	[comet_semi_major_axis] [float] NULL,
	[comet_perihelion] [float] NULL,
	[comet_eccentricity] [float] NULL,
	[comet_orbital_inclination] [float] NULL,
	[comet_image] [nvarchar](255) NULL,
 CONSTRAINT [PK_Comets] PRIMARY KEY CLUSTERED 
(
	[comet_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Comets__comet_name] UNIQUE NONCLUSTERED 
(
	[comet_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Constellations]    Script Date: 07.02.2023 0:59:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Constellations](
	[cons_id] [int] IDENTITY(1,1) NOT NULL,
	[cons_name] [nvarchar](50) NOT NULL,
	[cons_abbreviation] [nvarchar](3) NOT NULL,
	[cons_symbolism] [nvarchar](22) NOT NULL,
	[cons_right_ascension] [nvarchar](17) NULL,
	[cons_declination] [nvarchar](15) NULL,
	[cons_square] [int] NULL,
	[cons_visible_in_latitudes] [nvarchar](20) NULL,
	[cons_image] [nvarchar](255) NULL,
 CONSTRAINT [PK__Constell__4A4973CF8BC86D92] PRIMARY KEY CLUSTERED 
(
	[cons_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Constellations_cons_abbreviation] UNIQUE NONCLUSTERED 
(
	[cons_abbreviation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Constellations_cons_name] UNIQUE NONCLUSTERED 
(
	[cons_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Constellations_cons_symbolism] UNIQUE NONCLUSTERED 
(
	[cons_symbolism] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Galaxies]    Script Date: 07.02.2023 0:59:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Galaxies](
	[glx_id] [int] IDENTITY(1,1) NOT NULL,
	[cons_id] [int] NULL,
	[glxcluster_id] [int] NULL,
	[glxgroup_id] [int] NULL,
	[glx_name] [nvarchar](50) NOT NULL,
	[glx_type] [nvarchar](14) NULL,
	[glx_right_ascension] [time](0) NULL,
	[glx_declination] [nvarchar](20) NULL,
	[glx_redshift] [float] NULL,
	[glx_distance] [int] NULL,
	[glx_apparent_magnitude] [float] NULL,
	[glx_radial_velocity] [int] NULL,
	[glx_radius] [float] NULL,
	[glx_image] [nvarchar](255) NULL,
 CONSTRAINT [PK__Galaxies] PRIMARY KEY CLUSTERED 
(
	[glx_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Galaxies_glx_name] UNIQUE NONCLUSTERED 
(
	[glx_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GalaxyClusters]    Script Date: 07.02.2023 0:59:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GalaxyClusters](
	[glxcluster_id] [int] IDENTITY(1,1) NOT NULL,
	[cons_id] [int] NULL,
	[glxcluster_name] [nvarchar](30) NOT NULL,
	[glxcluster_type] [nvarchar](20) NULL,
	[glxcluster_right_ascension] [time](0) NULL,
	[glxcluster_declination] [nvarchar](20) NULL,
	[glxcluster_redshift] [float] NULL,
	[glxcluster_image] [nvarchar](255) NULL,
 CONSTRAINT [PK_GalaxyClusters] PRIMARY KEY CLUSTERED 
(
	[glxcluster_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_GalaxyClusters_glxcluster_name] UNIQUE NONCLUSTERED 
(
	[glxcluster_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GalaxyGroups]    Script Date: 07.02.2023 0:59:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GalaxyGroups](
	[glxgroup_id] [int] IDENTITY(1,1) NOT NULL,
	[cons_id] [int] NULL,
	[glxgroup_name] [nvarchar](50) NOT NULL,
	[glxgroup_type] [nvarchar](11) NULL,
	[glxgroup_right_ascension] [time](0) NULL,
	[glxgroup_declination] [nvarchar](20) NULL,
	[glxgroup_redshift] [float] NULL,
	[glxgroup_image] [nvarchar](255) NULL,
 CONSTRAINT [PK__GalaxyGr__E2F8BA64BA727143] PRIMARY KEY CLUSTERED 
(
	[glxgroup_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_GalaxyGroups_glxgroup_name] UNIQUE NONCLUSTERED 
(
	[glxgroup_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nebulae]    Script Date: 07.02.2023 0:59:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nebulae](
	[nebula_id] [int] IDENTITY(1,1) NOT NULL,
	[cons_id] [int] NULL,
	[glx_id] [int] NULL,
	[nebula_name] [nvarchar](50) NOT NULL,
	[nebula_type] [nvarchar](20) NULL,
	[nebula_right_ascension] [time](0) NULL,
	[nebula_declination] [nvarchar](20) NULL,
	[nebula_distance] [int] NULL,
	[nebula_image] [nvarchar](255) NULL,
 CONSTRAINT [PK__Nebulae] PRIMARY KEY CLUSTERED 
(
	[nebula_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Nebulae_nebula_name] UNIQUE NONCLUSTERED 
(
	[nebula_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlanetarySystems]    Script Date: 07.02.2023 0:59:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlanetarySystems](
	[planetsystem_id] [int] IDENTITY(1,1) NOT NULL,
	[cons_id] [int] NULL,
	[glx_id] [int] NULL,
	[planetsystem_name] [nvarchar](50) NOT NULL,
	[planetsystem_confirmed_planets] [tinyint] NOT NULL,
	[planetsystem_image] [nvarchar](255) NULL,
 CONSTRAINT [PK_PlanetarySystems] PRIMARY KEY CLUSTERED 
(
	[planetsystem_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_PlanetarySystems_planetsystem_name] UNIQUE NONCLUSTERED 
(
	[planetsystem_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Planets]    Script Date: 07.02.2023 0:59:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Planets](
	[plnt_id] [int] IDENTITY(1,1) NOT NULL,
	[cons_id] [int] NULL,
	[star_id] [int] NULL,
	[plnt_name] [nvarchar](50) NOT NULL,
	[plnt_eccentricity] [float] NULL,
	[plnt_semi_major_axis] [float] NULL,
	[plnt_orbital_period] [float] NULL,
	[plnt_argument_of_perihelion] [float] NULL,
	[plnt_mass] [float] NULL,
	[plnt_image] [nvarchar](255) NULL,
 CONSTRAINT [PK_Planets] PRIMARY KEY CLUSTERED 
(
	[plnt_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Planets_exo_name] UNIQUE NONCLUSTERED 
(
	[plnt_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StarClusters]    Script Date: 07.02.2023 0:59:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StarClusters](
	[starcluster_id] [int] IDENTITY(1,1) NOT NULL,
	[cons_id] [int] NULL,
	[glx_id] [int] NULL,
	[starcluster_name] [nvarchar](50) NOT NULL,
	[starcluster_type] [nvarchar](20) NULL,
	[starcluster_right_ascension] [time](0) NULL,
	[starcluster_declination] [nvarchar](20) NULL,
	[starcluster_distance] [float] NULL,
	[starcluster_age] [int] NULL,
	[starcluster_diameter] [float] NULL,
	[starcluster_image] [nvarchar](255) NULL,
 CONSTRAINT [PK_StarClusters] PRIMARY KEY CLUSTERED 
(
	[starcluster_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_StarClusters_starcluster_name] UNIQUE NONCLUSTERED 
(
	[starcluster_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stars]    Script Date: 07.02.2023 0:59:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stars](
	[star_id] [int] IDENTITY(1,1) NOT NULL,
	[cons_id] [int] NULL,
	[glx_id] [int] NULL,
	[starcluster_id] [int] NULL,
	[planetsystem_id] [int] NULL,
	[star_name] [nvarchar](50) NOT NULL,
	[star_apparent_magnitude] [float] NULL,
	[star_stellar_class] [nvarchar](10) NULL,
	[star_distance] [float] NULL,
	[star_declination] [nvarchar](20) NULL,
	[star_image] [nvarchar](255) NULL,
 CONSTRAINT [PK_Stars] PRIMARY KEY CLUSTERED 
(
	[star_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Stars_star_name] UNIQUE NONCLUSTERED 
(
	[star_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Asteroids]  WITH CHECK ADD  CONSTRAINT [FK_Asteroids_To_Stars] FOREIGN KEY([star_id])
REFERENCES [dbo].[Stars] ([star_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Asteroids] CHECK CONSTRAINT [FK_Asteroids_To_Stars]
GO
ALTER TABLE [dbo].[BlackHoles]  WITH CHECK ADD  CONSTRAINT [FK_BlackHoles_To_Constellations] FOREIGN KEY([cons_id])
REFERENCES [dbo].[Constellations] ([cons_id])
GO
ALTER TABLE [dbo].[BlackHoles] CHECK CONSTRAINT [FK_BlackHoles_To_Constellations]
GO
ALTER TABLE [dbo].[BlackHoles]  WITH CHECK ADD  CONSTRAINT [FK_BlackHoles_To_Galaxies] FOREIGN KEY([glx_id])
REFERENCES [dbo].[Galaxies] ([glx_id])
GO
ALTER TABLE [dbo].[BlackHoles] CHECK CONSTRAINT [FK_BlackHoles_To_Galaxies]
GO
ALTER TABLE [dbo].[Comets]  WITH CHECK ADD  CONSTRAINT [FK_Comets_To_Stars] FOREIGN KEY([star_id])
REFERENCES [dbo].[Stars] ([star_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comets] CHECK CONSTRAINT [FK_Comets_To_Stars]
GO
ALTER TABLE [dbo].[Galaxies]  WITH CHECK ADD  CONSTRAINT [FK_Galaxies_To_Constellations] FOREIGN KEY([cons_id])
REFERENCES [dbo].[Constellations] ([cons_id])
GO
ALTER TABLE [dbo].[Galaxies] CHECK CONSTRAINT [FK_Galaxies_To_Constellations]
GO
ALTER TABLE [dbo].[Galaxies]  WITH CHECK ADD  CONSTRAINT [FK_Galaxies_To_GalaxyClusters] FOREIGN KEY([glxcluster_id])
REFERENCES [dbo].[GalaxyClusters] ([glxcluster_id])
GO
ALTER TABLE [dbo].[Galaxies] CHECK CONSTRAINT [FK_Galaxies_To_GalaxyClusters]
GO
ALTER TABLE [dbo].[Galaxies]  WITH CHECK ADD  CONSTRAINT [FK_Galaxies_To_GalaxyGroups] FOREIGN KEY([glxgroup_id])
REFERENCES [dbo].[GalaxyGroups] ([glxgroup_id])
GO
ALTER TABLE [dbo].[Galaxies] CHECK CONSTRAINT [FK_Galaxies_To_GalaxyGroups]
GO
ALTER TABLE [dbo].[GalaxyClusters]  WITH CHECK ADD  CONSTRAINT [FK_GalaxyClusters_To_Constellations] FOREIGN KEY([cons_id])
REFERENCES [dbo].[Constellations] ([cons_id])
GO
ALTER TABLE [dbo].[GalaxyClusters] CHECK CONSTRAINT [FK_GalaxyClusters_To_Constellations]
GO
ALTER TABLE [dbo].[GalaxyGroups]  WITH CHECK ADD  CONSTRAINT [FK_GalaxyGroups_To_Constellations] FOREIGN KEY([cons_id])
REFERENCES [dbo].[Constellations] ([cons_id])
GO
ALTER TABLE [dbo].[GalaxyGroups] CHECK CONSTRAINT [FK_GalaxyGroups_To_Constellations]
GO
ALTER TABLE [dbo].[Nebulae]  WITH CHECK ADD  CONSTRAINT [FK_Nebulae_Galaxies] FOREIGN KEY([glx_id])
REFERENCES [dbo].[Galaxies] ([glx_id])
GO
ALTER TABLE [dbo].[Nebulae] CHECK CONSTRAINT [FK_Nebulae_Galaxies]
GO
ALTER TABLE [dbo].[Nebulae]  WITH CHECK ADD  CONSTRAINT [FK_Nebulae_To_Constellations] FOREIGN KEY([cons_id])
REFERENCES [dbo].[Constellations] ([cons_id])
GO
ALTER TABLE [dbo].[Nebulae] CHECK CONSTRAINT [FK_Nebulae_To_Constellations]
GO
ALTER TABLE [dbo].[PlanetarySystems]  WITH CHECK ADD  CONSTRAINT [FK_PlanetarySystems_Galaxies] FOREIGN KEY([glx_id])
REFERENCES [dbo].[Galaxies] ([glx_id])
GO
ALTER TABLE [dbo].[PlanetarySystems] CHECK CONSTRAINT [FK_PlanetarySystems_Galaxies]
GO
ALTER TABLE [dbo].[PlanetarySystems]  WITH CHECK ADD  CONSTRAINT [FK_PlanetarySystems_To_Constellations] FOREIGN KEY([cons_id])
REFERENCES [dbo].[Constellations] ([cons_id])
GO
ALTER TABLE [dbo].[PlanetarySystems] CHECK CONSTRAINT [FK_PlanetarySystems_To_Constellations]
GO
ALTER TABLE [dbo].[Planets]  WITH CHECK ADD  CONSTRAINT [FK_Planets_To_Constellations] FOREIGN KEY([cons_id])
REFERENCES [dbo].[Constellations] ([cons_id])
GO
ALTER TABLE [dbo].[Planets] CHECK CONSTRAINT [FK_Planets_To_Constellations]
GO
ALTER TABLE [dbo].[Planets]  WITH CHECK ADD  CONSTRAINT [FK_Planets_To_Stars] FOREIGN KEY([star_id])
REFERENCES [dbo].[Stars] ([star_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Planets] CHECK CONSTRAINT [FK_Planets_To_Stars]
GO
ALTER TABLE [dbo].[StarClusters]  WITH CHECK ADD  CONSTRAINT [FK_StarClusters_Galaxies] FOREIGN KEY([glx_id])
REFERENCES [dbo].[Galaxies] ([glx_id])
GO
ALTER TABLE [dbo].[StarClusters] CHECK CONSTRAINT [FK_StarClusters_Galaxies]
GO
ALTER TABLE [dbo].[StarClusters]  WITH CHECK ADD  CONSTRAINT [FK_StarClusters_To_Constellations] FOREIGN KEY([cons_id])
REFERENCES [dbo].[Constellations] ([cons_id])
GO
ALTER TABLE [dbo].[StarClusters] CHECK CONSTRAINT [FK_StarClusters_To_Constellations]
GO
ALTER TABLE [dbo].[Stars]  WITH CHECK ADD  CONSTRAINT [FK_Stars_To_Constellations] FOREIGN KEY([cons_id])
REFERENCES [dbo].[Constellations] ([cons_id])
GO
ALTER TABLE [dbo].[Stars] CHECK CONSTRAINT [FK_Stars_To_Constellations]
GO
ALTER TABLE [dbo].[Stars]  WITH CHECK ADD  CONSTRAINT [FK_Stars_To_Galaxies] FOREIGN KEY([glx_id])
REFERENCES [dbo].[Galaxies] ([glx_id])
GO
ALTER TABLE [dbo].[Stars] CHECK CONSTRAINT [FK_Stars_To_Galaxies]
GO
ALTER TABLE [dbo].[Stars]  WITH CHECK ADD  CONSTRAINT [FK_Stars_To_PlanetarySystems] FOREIGN KEY([planetsystem_id])
REFERENCES [dbo].[PlanetarySystems] ([planetsystem_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Stars] CHECK CONSTRAINT [FK_Stars_To_PlanetarySystems]
GO
ALTER TABLE [dbo].[Stars]  WITH CHECK ADD  CONSTRAINT [FK_Stars_To_StarClusters] FOREIGN KEY([starcluster_id])
REFERENCES [dbo].[StarClusters] ([starcluster_id])
GO
ALTER TABLE [dbo].[Stars] CHECK CONSTRAINT [FK_Stars_To_StarClusters]
GO
ALTER TABLE [dbo].[Asteroids]  WITH CHECK ADD  CONSTRAINT [CK_Asteroids_diameter] CHECK  (([ast_diameter]>(0)))
GO
ALTER TABLE [dbo].[Asteroids] CHECK CONSTRAINT [CK_Asteroids_diameter]
GO
ALTER TABLE [dbo].[Asteroids]  WITH CHECK ADD  CONSTRAINT [CK_Asteroids_mean_anomaly] CHECK  (([ast_mean_anomaly]>(0)))
GO
ALTER TABLE [dbo].[Asteroids] CHECK CONSTRAINT [CK_Asteroids_mean_anomaly]
GO
ALTER TABLE [dbo].[Asteroids]  WITH CHECK ADD  CONSTRAINT [CK_Asteroids_orbital_eccentricity] CHECK  (([ast_orbital_eccentricity]>=(0)))
GO
ALTER TABLE [dbo].[Asteroids] CHECK CONSTRAINT [CK_Asteroids_orbital_eccentricity]
GO
ALTER TABLE [dbo].[Asteroids]  WITH CHECK ADD  CONSTRAINT [CK_Asteroids_orbital_inclination] CHECK  (([ast_orbital_inclination]>(0)))
GO
ALTER TABLE [dbo].[Asteroids] CHECK CONSTRAINT [CK_Asteroids_orbital_inclination]
GO
ALTER TABLE [dbo].[BlackHoles]  WITH CHECK ADD  CONSTRAINT [CK_BlackHoles_blackhole_distance] CHECK  (([blackhole_distance]>(0)))
GO
ALTER TABLE [dbo].[BlackHoles] CHECK CONSTRAINT [CK_BlackHoles_blackhole_distance]
GO
ALTER TABLE [dbo].[Comets]  WITH CHECK ADD  CONSTRAINT [CK_Comets_eccentricity] CHECK  (([comet_eccentricity]>=(0)))
GO
ALTER TABLE [dbo].[Comets] CHECK CONSTRAINT [CK_Comets_eccentricity]
GO
ALTER TABLE [dbo].[Comets]  WITH CHECK ADD  CONSTRAINT [CK_Comets_orbital_inclination] CHECK  (([comet_orbital_inclination]>(0)))
GO
ALTER TABLE [dbo].[Comets] CHECK CONSTRAINT [CK_Comets_orbital_inclination]
GO
ALTER TABLE [dbo].[Comets]  WITH CHECK ADD  CONSTRAINT [CK_Comets_orbital_period] CHECK  (([comet_orbital_period]>(0)))
GO
ALTER TABLE [dbo].[Comets] CHECK CONSTRAINT [CK_Comets_orbital_period]
GO
ALTER TABLE [dbo].[Comets]  WITH CHECK ADD  CONSTRAINT [CK_Comets_perihelion] CHECK  (([comet_perihelion]>(0)))
GO
ALTER TABLE [dbo].[Comets] CHECK CONSTRAINT [CK_Comets_perihelion]
GO
ALTER TABLE [dbo].[Comets]  WITH CHECK ADD  CONSTRAINT [CK_Comets_semi_major_axis] CHECK  (([comet_semi_major_axis]>(0)))
GO
ALTER TABLE [dbo].[Comets] CHECK CONSTRAINT [CK_Comets_semi_major_axis]
GO
ALTER TABLE [dbo].[Constellations]  WITH CHECK ADD  CONSTRAINT [CK_Constellations_cons_square] CHECK  (([cons_square]>(0)))
GO
ALTER TABLE [dbo].[Constellations] CHECK CONSTRAINT [CK_Constellations_cons_square]
GO
ALTER TABLE [dbo].[Galaxies]  WITH CHECK ADD  CONSTRAINT [CK_Galaxies_glx_distance] CHECK  (([glx_distance]>(0)))
GO
ALTER TABLE [dbo].[Galaxies] CHECK CONSTRAINT [CK_Galaxies_glx_distance]
GO
ALTER TABLE [dbo].[Galaxies]  WITH CHECK ADD  CONSTRAINT [CK_Galaxies_glx_radius] CHECK  (([glx_radius]>(0)))
GO
ALTER TABLE [dbo].[Galaxies] CHECK CONSTRAINT [CK_Galaxies_glx_radius]
GO
ALTER TABLE [dbo].[Nebulae]  WITH CHECK ADD  CONSTRAINT [CK_Nebulae_nebula_distance] CHECK  (([nebula_distance]>(0)))
GO
ALTER TABLE [dbo].[Nebulae] CHECK CONSTRAINT [CK_Nebulae_nebula_distance]
GO
ALTER TABLE [dbo].[PlanetarySystems]  WITH CHECK ADD  CONSTRAINT [CK_PlanetarySystems_confirmed_planets] CHECK  (([planetsystem_confirmed_planets]>(0)))
GO
ALTER TABLE [dbo].[PlanetarySystems] CHECK CONSTRAINT [CK_PlanetarySystems_confirmed_planets]
GO
ALTER TABLE [dbo].[Planets]  WITH CHECK ADD  CONSTRAINT [CK_Planets_eccentricity] CHECK  (([plnt_eccentricity]>=(0)))
GO
ALTER TABLE [dbo].[Planets] CHECK CONSTRAINT [CK_Planets_eccentricity]
GO
ALTER TABLE [dbo].[Planets]  WITH CHECK ADD  CONSTRAINT [CK_Planets_mass] CHECK  (([plnt_mass]>(0)))
GO
ALTER TABLE [dbo].[Planets] CHECK CONSTRAINT [CK_Planets_mass]
GO
ALTER TABLE [dbo].[Planets]  WITH CHECK ADD  CONSTRAINT [CK_Planets_orbital_period] CHECK  (([plnt_orbital_period]>(0)))
GO
ALTER TABLE [dbo].[Planets] CHECK CONSTRAINT [CK_Planets_orbital_period]
GO
ALTER TABLE [dbo].[Planets]  WITH CHECK ADD  CONSTRAINT [CK_Planets_semi_major_axis] CHECK  (([plnt_semi_major_axis]>(0)))
GO
ALTER TABLE [dbo].[Planets] CHECK CONSTRAINT [CK_Planets_semi_major_axis]
GO
ALTER TABLE [dbo].[StarClusters]  WITH CHECK ADD  CONSTRAINT [CK_StarClusters_starcluster_age] CHECK  (([starcluster_age]>(0)))
GO
ALTER TABLE [dbo].[StarClusters] CHECK CONSTRAINT [CK_StarClusters_starcluster_age]
GO
ALTER TABLE [dbo].[StarClusters]  WITH CHECK ADD  CONSTRAINT [CK_StarClusters_starcluster_diameter] CHECK  (([starcluster_diameter]>(0)))
GO
ALTER TABLE [dbo].[StarClusters] CHECK CONSTRAINT [CK_StarClusters_starcluster_diameter]
GO
ALTER TABLE [dbo].[StarClusters]  WITH CHECK ADD  CONSTRAINT [CK_StarClusters_starcluster_distance] CHECK  (([starcluster_distance]>(0)))
GO
ALTER TABLE [dbo].[StarClusters] CHECK CONSTRAINT [CK_StarClusters_starcluster_distance]
GO
ALTER TABLE [dbo].[Stars]  WITH CHECK ADD  CONSTRAINT [CK_Stars_star_distance] CHECK  (([star_distance]>(0)))
GO
ALTER TABLE [dbo].[Stars] CHECK CONSTRAINT [CK_Stars_star_distance]
GO
USE [master]
GO
ALTER DATABASE [Space] SET  READ_WRITE 
GO
