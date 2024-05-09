using System.Data;
using System.Data.SqlClient;
using SectorCreator.Global;
using SectorCreator.WorldBuilder;
using SectorCreator.WorldBuilder.Planet.Planet;
using SectorCreator.WorldBuilder.Star;

namespace ClassLibrary1;

public class WorldBuilderDatabase
{
    public static void WriteSectorToDB(WorldBuilderSector sector)
    {
        using SqlConnection openCon = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=WorldBuilderSectorGeneration;Trusted_Connection=True;");
        string saveSector = "INSERT into dbo.Sector (Id,Name) VALUES (@Id,@Name)";

        using SqlCommand querySaveSector = new SqlCommand(saveSector);
        querySaveSector.Connection = openCon;
        querySaveSector.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = sector.Id;
        querySaveSector.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = sector.Name;
        
        openCon.Open();
        querySaveSector.ExecuteNonQuery();
        openCon.Close();

        WriteSubsectorsToDB(sector.Id, sector.Subsectors, openCon);
    }

    private static void WriteSubsectorsToDB(Guid sectorId, List<WorldBuilderSubsector> subsectors, SqlConnection openCon)
    {
        foreach (var subsector in subsectors) {
            var saveSubsector = "INSERT INTO dbo.Subsector (Id,SectorId,XCoordinate,YCoordinate) VALUES (@Id,@SectorId,@XCoordinate,@YCoordinate)";

            using SqlCommand querySaveSubsector = new SqlCommand(saveSubsector);
            querySaveSubsector.Connection = openCon;
            querySaveSubsector.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = subsector.Id;
            querySaveSubsector.Parameters.Add("@SectorId", SqlDbType.UniqueIdentifier).Value = sectorId;
            querySaveSubsector.Parameters.Add("@XCoordinate", SqlDbType.Int).Value = subsector.Coordinates.X;
            querySaveSubsector.Parameters.Add("@YCoordinate", SqlDbType.Int).Value = subsector.Coordinates.Y;
            
            openCon.Open();
            querySaveSubsector.ExecuteNonQuery();
            openCon.Close();

            WriteHexesToDB(subsector.Id, subsector.Hexes, openCon);
        }
    }

    private static void WriteHexesToDB(Guid subsectorId, List<WorldBuilderHex> hexes, SqlConnection openCon)
    {
        foreach (var hex in hexes) {
            var saveHex =
                "INSERT INTO dbo.Hex (Id,Name,SubsectorId,XCoordinate,YCoordinate,GasGiantQuantity,BeltQuantity,TerrestrialQuantity,EmptyOrbits) " +
                "VALUES (@Id,@Name,@SubsectorId,@XCoordinate,@YCoordinate,@GasGiantQuantity,@BeltQuantity,@TerrestrialQuantity,@EmptyOrbits)";

            using SqlCommand querySaveHex = new SqlCommand(saveHex);
            querySaveHex.Connection = openCon;
            querySaveHex.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = hex.Id;
            querySaveHex.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = hex.Name;
            querySaveHex.Parameters.Add("@SubsectorId", SqlDbType.UniqueIdentifier).Value = subsectorId;
            querySaveHex.Parameters.Add("@XCoordinate", SqlDbType.Int).Value = hex.Coordinates.X;
            querySaveHex.Parameters.Add("@YCoordinate", SqlDbType.Int).Value = hex.Coordinates.Y;
            querySaveHex.Parameters.Add("@GasGiantQuantity", SqlDbType.Int).Value = hex.GasGiantQuantity;
            querySaveHex.Parameters.Add("@BeltQuantity", SqlDbType.Int).Value = hex.BeltQuantity;
            querySaveHex.Parameters.Add("@TerrestrialQuantity", SqlDbType.Int).Value = hex.TerrestrialPlanetQuantity;
            querySaveHex.Parameters.Add("@EmptyOrbits", SqlDbType.Int).Value = hex.EmptyOrbits;
            
            openCon.Open();
            querySaveHex.ExecuteNonQuery();
            openCon.Close();

            WriteStarSystemsToDB(hex.Id, hex.StarSystems, openCon);
        }
    }

    private static void WriteStarSystemsToDB(Guid hexId, List<WorldBuilderStarSystem> starSystems, SqlConnection openCon)
    {
        var isFirst = true;
        foreach (var starSystem in starSystems) {
            if (starSystem.BaselineOrbit == 0) {
                return;
            }

            var saveStarSystem = "INSERT INTO dbo.StarSystem (Id,Name,HexId,BaselineNumber,BaselineOrbit,Spread,OrbitNum,Component,Eccentricity)" +
                                 "VALUES(@Id,@Name,@HexId,@BaselineNumber,@BaselineOrbit,@Spread,@OrbitNum,@Component,@Eccentricity)";

            using SqlCommand querySaveStarSystem = new SqlCommand(saveStarSystem);
            querySaveStarSystem.Connection = openCon;
            querySaveStarSystem.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = starSystem.Id;
            querySaveStarSystem.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = starSystem.Name;
            querySaveStarSystem.Parameters.Add("@HexId", SqlDbType.UniqueIdentifier).Value = hexId;
            querySaveStarSystem.Parameters.Add("@BaselineNumber", SqlDbType.Float).Value = starSystem.BaselineNumber;
            querySaveStarSystem.Parameters.Add("@BaselineOrbit", SqlDbType.Float).Value = starSystem.BaselineOrbit;
            querySaveStarSystem.Parameters.Add("@Spread", SqlDbType.Float).Value = starSystem.Spread;
            querySaveStarSystem.Parameters.Add("@OrbitNum", SqlDbType.Float).Value = starSystem.OrbitNumber;
            querySaveStarSystem.Parameters.Add("@Component", SqlDbType.NVarChar, 10).Value = starSystem.Component;
            querySaveStarSystem.Parameters.Add("@Eccentricity", SqlDbType.Float).Value = starSystem.Eccentricity;
            
            openCon.Open();
            querySaveStarSystem.ExecuteNonQuery();
            openCon.Close();
            
            if (isFirst) {
                AddMainSystemToHex(hexId, starSystem.Id, openCon);
                isFirst = false;
            }

            AddStarToDB(starSystem.Id, starSystem.Star, openCon);
            AddPlanetsToDB(starSystem.Id, starSystem.Planets, openCon);
        }
    }

    private static void AddPlanetsToDB(Guid starSystemId, List<WorldBuilderPlanet> planets, SqlConnection openCon)
    {
        foreach (var planet in planets) {
            var savePlanet =
                "INSERT INTO dbo.Planet (Id, StarSystemId,Name,PlanetTypeId,[Primary],AnomalyId,Object,OrbitNum,Eccentricity,Period,Size,Diameter,Density,Atmosphere,AtmosphereSub,BAR,OxygenFraction,RunawayGreenhouse,Hydrographics,HydroPercent,FundamentalGeographyId,SurfaceDistributionId,MajorContinents,MinorContinents,Temperature,HighTemperature,LowTemperature,Albedo,GreenhouseFactor,HillSphereAU,MoonOrbitRange,SiderealDay,TidalForce,AxialTilt,IsTidallyLocked,IsTidallyLockedWithMoon,StarTidalEffect,MoonTidalEffect,ResidualSeismicStress,TidalHeatingFactor,MajorTectonicPlates,BiomassRating,BiocomplexityRating,CurrentSophontExists,ExtinctSophontExists,BiodiversityRating,CompatibilityRating,HabitabilityRating,ResourceRating,Importance,ResourceFactor,InfrastructureFactor,EfficiencyFactor,ResourceUnits,GWPPerCapita,WTNStarportModifier,WorldTradeNumber,InequalityRating,TariffRateId,TariffPercentage,Population,PValue,PopulationConcentrationRating,UrbanizationPercentage,MajorCityPopulation,Government,TechLevel,LowCommonTechLevel,EnergyTechLevel,ElectronicsTechLevel,ManufacturingTechLevel,MedicalTechLevel,EnvironmentalTechLevel,LandTransportTechLevel,WaterTransportTechLevel,AirTransportTechLevel,PersonalMilitaryTechLevel,HeavyMilitaryTechLevel,Diversity,Xenophilia,Uniqueness,Symbology,Cohesion,Progressiveness,Expansionism,Militancy,MilitaryBudgetPercent,TravelZone)" +
                "VALUES (@Id,@StarSystemId,@Name,@PlanetTypeId,@Primary,@AnomalyId,@Object,@OrbitNum,@Eccentricity,@Period,@Size,@Diameter,@Density,@Atmosphere,@AtmosphereSub,@BAR,@OxygenFraction,@RunawayGreenhouse,@Hydrographics,@HydroPercent,@FundamentalGeographyId,@SurfaceDistributionId,@MajorContinents,@MinorContinents,@Temperature,@HighTemperature,@LowTemperature,@Albedo,@GreenhouseFactor,@HillSphereAU,@MoonOrbitRange,@SiderealDay,@TidalForce,@AxialTilt,@IsTidallyLocked,@IsTidallyLockedWithMoon,@StarTidalEffect,@MoonTidalEffect,@ResidualSeismicStress,@TidalHeatingFactor,@MajorTectonicPlates,@BiomassRating,@BiocomplexityRating,@CurrentSophontExists,@ExtinctSophontExists,@BiodiversityRating,@CompatibilityRating,@HabitabilityRating,@ResourceRating,@Importance,@ResourceFactor,@InfrastructureFactor,@EfficiencyFactor,@ResourceUnits,@GWPPerCapita,@WTNStarportModifier,@WorldTradeNumber,@InequalityRating,@TariffRateId,@TariffPercentage,@Population,@PValue,@PopulationConcentrationRating,@UrbanizationPercentage,@MajorCityPopulation,@Government,@TechLevel,@LowCommonTechLevel,@EnergyTechLevel,@ElectronicsTechLevel,@ManufacturingTechLevel,@MedicalTechLevel,@EnvironmentalTechLevel,@LandTransportTechLevel,@WaterTransportTechLevel,@AirTransportTechLevel,@PersonalMilitaryTechLevel,@HeavyMilitaryTechLevel,@Diversity,@Xenophilia,@Uniqueness,@Symbology,@Cohesion,@Progressiveness,@Expansionism,@Militancy,@MilitaryBudgetPercent,@TravelZone)";

            using SqlCommand querySavePlanet = new SqlCommand(savePlanet);
            querySavePlanet.Connection = openCon;
            querySavePlanet.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = planet.Id;
            querySavePlanet.Parameters.Add("@StarSystemId", SqlDbType.UniqueIdentifier).Value = starSystemId;
            querySavePlanet.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = planet.Name;
            querySavePlanet.Parameters.Add("@IsMoon", SqlDbType.Bit).Value = planet.IsMoon;
            querySavePlanet.Parameters.Add("@PlanetTypeId", SqlDbType.Int).Value = planet.PlanetType;
            querySavePlanet.Parameters.Add("@Primary", SqlDbType.NVarChar, 50).Value = planet.Primary;
            querySavePlanet.Parameters.Add("@AnomalyId", SqlDbType.Int).Value = planet.Anomaly;
            querySavePlanet.Parameters.Add("@Object", SqlDbType.NVarChar, 50).Value = planet.Object;
            querySavePlanet.Parameters.Add("@OrbitNum", SqlDbType.Float).Value = planet.OrbitNumber;
            querySavePlanet.Parameters.Add("@Eccentricity", SqlDbType.Float).Value = planet.Eccentricity;
            querySavePlanet.Parameters.Add("@Period", SqlDbType.Float).Value = planet.Period;
            querySavePlanet.Parameters.Add("@Size", SqlDbType.Int).Value = planet.Size;
            querySavePlanet.Parameters.Add("@Diameter", SqlDbType.Int).Value = planet.Diameter;
            querySavePlanet.Parameters.Add("@Density", SqlDbType.Float).Value = planet.Density;
            querySavePlanet.Parameters.Add("@Atmosphere", SqlDbType.Int).Value = planet.Atmosphere;
            querySavePlanet.Parameters.Add("@AtmosphereSub", SqlDbType.Int).Value = planet.AtmosphereSub;
            querySavePlanet.Parameters.Add("@BAR", SqlDbType.Float).Value = planet.BAR;
            querySavePlanet.Parameters.Add("@OxygenFraction", SqlDbType.Float).Value = planet.OxygenFraction;
            querySavePlanet.Parameters.Add("@RunawayGreenhouse", SqlDbType.Bit).Value = planet.RunawayGreenhouse;
            querySavePlanet.Parameters.Add("@Hydrographics", SqlDbType.Int).Value = planet.Hydrographics;
            querySavePlanet.Parameters.Add("@HydroPercent", SqlDbType.Float).Value = planet.HydroPercent;
            querySavePlanet.Parameters.Add("@FundamentalGeographyId", SqlDbType.Int).Value = planet.FundamentalGeography;
            querySavePlanet.Parameters.Add("@SurfaceDistributionId", SqlDbType.Int).Value = planet.SurfaceDistribution;
            querySavePlanet.Parameters.Add("@MajorContinents", SqlDbType.Int).Value = planet.MajorContinents;
            querySavePlanet.Parameters.Add("@MinorContinents", SqlDbType.Int).Value = planet.MinorContinents;
            querySavePlanet.Parameters.Add("@Temperature", SqlDbType.Int).Value = planet.Temperature;
            querySavePlanet.Parameters.Add("@HighTemperature", SqlDbType.Int).Value = planet.HighTemperature;
            querySavePlanet.Parameters.Add("@LowTemperature", SqlDbType.Int).Value = planet.LowTemperature;
            querySavePlanet.Parameters.Add("@Albedo", SqlDbType.Float).Value = planet.Albedo;
            querySavePlanet.Parameters.Add("@GreenhouseFactor", SqlDbType.Float).Value = planet.GreenhouseFactor;
            querySavePlanet.Parameters.Add("@HillSphereAU", SqlDbType.Float).Value = planet.HillSphereAU;
            querySavePlanet.Parameters.Add("@MoonOrbitRange", SqlDbType.Int).Value = planet.MoonOrbitRange;
            querySavePlanet.Parameters.Add("@SiderealDay", SqlDbType.Float).Value = planet.SiderealDay;
            querySavePlanet.Parameters.Add("@TidalForce", SqlDbType.Float).Value = planet.TidalForce;
            querySavePlanet.Parameters.Add("@AxialTilt", SqlDbType.Float).Value = planet.AxialTilt;
            querySavePlanet.Parameters.Add("@IsTidallyLocked", SqlDbType.Bit).Value = planet.IsTidallyLocked;
            querySavePlanet.Parameters.Add("@IsTidallyLockedWithMoon", SqlDbType.Bit).Value = planet.IsTidallyLockedWithMoon;
            querySavePlanet.Parameters.Add("@StarTidalEffect", SqlDbType.Float).Value = planet.StarTidalEffect;
            querySavePlanet.Parameters.Add("@MoonTidalEffect", SqlDbType.Float).Value = planet.MoonTidalEffect;
            querySavePlanet.Parameters.Add("@ResidualSeismicStress", SqlDbType.Float).Value = planet.ResidualSeismicStress;
            querySavePlanet.Parameters.Add("@TidalHeatingFactor", SqlDbType.Float).Value = planet.TidalHeatingFactor;
            querySavePlanet.Parameters.Add("@MajorTectonicPlates", SqlDbType.Int).Value = planet.MajorTectonicPlates;
            querySavePlanet.Parameters.Add("@BiomassRating", SqlDbType.Int).Value = planet.BiomassRating;
            querySavePlanet.Parameters.Add("@BiocomplexityRating", SqlDbType.Int).Value = planet.BiocomplexityRating;
            querySavePlanet.Parameters.Add("@CurrentSophontExists", SqlDbType.Bit).Value = planet.CurrentSophontExists;
            querySavePlanet.Parameters.Add("@ExtinctSophontExists", SqlDbType.Bit).Value = planet.ExtinctSophontExists;
            querySavePlanet.Parameters.Add("@BiodiversityRating", SqlDbType.Int).Value = planet.BiodiversityRating;
            querySavePlanet.Parameters.Add("@CompatibilityRating", SqlDbType.Int).Value = planet.CompatibilityRating;
            querySavePlanet.Parameters.Add("@HabitabilityRating", SqlDbType.Int).Value = planet.HabitabilityRating;
            querySavePlanet.Parameters.Add("@ResourceRating", SqlDbType.Int).Value = planet.ResourceRating;
            querySavePlanet.Parameters.Add("@Importance", SqlDbType.Int).Value = planet.Importance;
            querySavePlanet.Parameters.Add("@ResourceFactor", SqlDbType.Int).Value = planet.ResourceFactor;
            querySavePlanet.Parameters.Add("@InfrastructureFactor", SqlDbType.Int).Value = planet.InfrastructureFactor;
            querySavePlanet.Parameters.Add("@EfficiencyFactor", SqlDbType.Int).Value = planet.EfficiencyFactor;
            querySavePlanet.Parameters.Add("@ResourceUnits", SqlDbType.Int).Value = planet.ResourceUnits;
            querySavePlanet.Parameters.Add("@GWPPerCapita", SqlDbType.Int).Value = planet.GWPPerCapita;
            querySavePlanet.Parameters.Add("@WTNStarportModifier", SqlDbType.Int).Value = planet.WTNStarportModifier;
            querySavePlanet.Parameters.Add("@WorldTradeNumber", SqlDbType.Int).Value = planet.WorldTradeNumber;
            querySavePlanet.Parameters.Add("@InequalityRating", SqlDbType.Int).Value = planet.InequalityRating;
            querySavePlanet.Parameters.Add("@TariffRateId", SqlDbType.Int).Value = planet.TariffRates;
            querySavePlanet.Parameters.Add("@TariffPercentage", SqlDbType.Int).Value = planet.TariffPercentage;
            querySavePlanet.Parameters.Add("@Population", SqlDbType.Int).Value = planet.Population;
            querySavePlanet.Parameters.Add("@PValue", SqlDbType.Int).Value = planet.PValue;
            querySavePlanet.Parameters.Add("@PopulationConcentrationRating", SqlDbType.Int).Value = planet.PopulationConcentrationRating;
            querySavePlanet.Parameters.Add("@UrbanizationPercentage", SqlDbType.Int).Value = planet.UrbanizationPercentage;
            querySavePlanet.Parameters.Add("@MajorCityPopulation", SqlDbType.BigInt).Value = planet.MajorCityPopulation;
            querySavePlanet.Parameters.Add("@Government", SqlDbType.Int).Value = planet.Government;
            querySavePlanet.Parameters.Add("@TechLevel", SqlDbType.Int).Value = planet.TechLevel;
            querySavePlanet.Parameters.Add("@LowCommonTechLevel", SqlDbType.Int).Value = planet.LowCommonTechLevel;
            querySavePlanet.Parameters.Add("@EnergyTechLevel", SqlDbType.Int).Value = planet.EnergyTechLevel;
            querySavePlanet.Parameters.Add("@ElectronicsTechLevel", SqlDbType.Int).Value = planet.ElectronicsTechLevel;
            querySavePlanet.Parameters.Add("@ManufacturingTechLevel", SqlDbType.Int).Value = planet.ManufacturingTechLevel;
            querySavePlanet.Parameters.Add("@MedicalTechLevel", SqlDbType.Int).Value = planet.MedicalTechLevel;
            querySavePlanet.Parameters.Add("@EnvironmentalTechLevel", SqlDbType.Int).Value = planet.EnvironmentalTechLevel;
            querySavePlanet.Parameters.Add("@LandTransportTechLevel", SqlDbType.Int).Value = planet.LandTransportTechLevel;
            querySavePlanet.Parameters.Add("@WaterTransportTechLevel", SqlDbType.Int).Value = planet.WaterTransportTechLevel;
            querySavePlanet.Parameters.Add("@AirTransportTechLevel", SqlDbType.Int).Value = planet.AirTransportTechLevel;
            querySavePlanet.Parameters.Add("@PersonalMilitaryTechLevel", SqlDbType.Int).Value = planet.PersonalMilitaryTechLevel;
            querySavePlanet.Parameters.Add("@HeavyMilitaryTechLevel", SqlDbType.Int).Value = planet.HeavyMilitaryTechLevel;
            querySavePlanet.Parameters.Add("@Diversity", SqlDbType.Int).Value = planet.Diversity;
            querySavePlanet.Parameters.Add("@Xenophilia", SqlDbType.Int).Value = planet.Xenophilia;
            querySavePlanet.Parameters.Add("@Uniqueness", SqlDbType.Int).Value = planet.Uniqueness;
            querySavePlanet.Parameters.Add("@Symbology", SqlDbType.Int).Value = planet.Symbology;
            querySavePlanet.Parameters.Add("@Cohesion", SqlDbType.Int).Value = planet.Cohesion;
            querySavePlanet.Parameters.Add("@Progressiveness", SqlDbType.Int).Value = planet.Progressiveness;
            querySavePlanet.Parameters.Add("@Expansionism", SqlDbType.Int).Value = planet.Expansionism;
            querySavePlanet.Parameters.Add("@Militancy", SqlDbType.Int).Value = planet.Militancy;
            querySavePlanet.Parameters.Add("@MilitaryBudgetPercent", SqlDbType.Float).Value = planet.MilitaryBudgetPercent;
            querySavePlanet.Parameters.Add("@TravelZone", SqlDbType.Char, 1).Value = (char) planet.TravelZone;
            
            openCon.Open();
            querySavePlanet.ExecuteNonQuery();
            openCon.Close();
        }
    }

    private static void AddStarToDB(Guid starSystemId, WorldBuilderStar star, SqlConnection openCon)
    {
        var saveStar =
            "INSERT INTO dbo.Star (Id,Name,StarSystemId,Component,SpectralTypeId,SpectralSubclass,LuminosityClassId,SpecialTypeId,StarTypeId,OrbitNum,Period,Eccentricity,Age,Mass,Diameter,Temperature,MAO,HZCO)" +
            "VALUES (@Id,@Name,@StarSystemId,@Component,@SpectralTypeId,@SpectralSubclass,@LuminosityClassId,@SpecialTypeId,@StarTypeId,@OrbitNum,@Period,@Eccentricity,@Age,@Mass,@Diameter,@Temperature,@MAO,@HZCO)";

        using SqlCommand querySaveStar = new SqlCommand(saveStar);
        querySaveStar.Connection = openCon;
        querySaveStar.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = star.Id;
        querySaveStar.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = star.Name;
        querySaveStar.Parameters.Add("@StarSystemId", SqlDbType.UniqueIdentifier).Value = starSystemId;
        querySaveStar.Parameters.Add("@Component", SqlDbType.NVarChar, 10).Value = star.Component;
        querySaveStar.Parameters.Add("@SpectralTypeId", SqlDbType.Int).Value = star.SpectralType;
        querySaveStar.Parameters.Add("@SpectralSubclass", SqlDbType.Int).Value = star.SpectralSubclass;
        querySaveStar.Parameters.Add("@LuminosityClassId", SqlDbType.Int).Value = star.LuminosityClass;
        querySaveStar.Parameters.Add("@SpecialTypeId", SqlDbType.Int).Value = star.SpecialType;
        querySaveStar.Parameters.Add("@StarTypeId", SqlDbType.Int).Value = star.StarType;
        querySaveStar.Parameters.Add("@OrbitNum", SqlDbType.Float).Value = star.OrbitNumber;
        querySaveStar.Parameters.Add("@Period", SqlDbType.Float).Value = star.Period;
        querySaveStar.Parameters.Add("@Eccentricity", SqlDbType.Float).Value = star.Eccentricity;
        querySaveStar.Parameters.Add("@Age", SqlDbType.Float).Value = star.Age;
        querySaveStar.Parameters.Add("@Mass", SqlDbType.Float).Value = star.Mass;
        querySaveStar.Parameters.Add("@Diameter", SqlDbType.Int).Value = star.Diameter;
        querySaveStar.Parameters.Add("@Temperature", SqlDbType.Int).Value = star.Temperature;
        querySaveStar.Parameters.Add("@MAO", SqlDbType.Float).Value = star.MAO;
        querySaveStar.Parameters.Add("@HZCO", SqlDbType.Float).Value = star.HZCO;
        
        openCon.Open();
        querySaveStar.ExecuteNonQuery();
        openCon.Close();

        if (star.CompanionStar != null) {
            AddStarToDB(starSystemId, star.CompanionStar, openCon);
        }
    }

    private static void AddMainSystemToHex(Guid hexId, Guid starSystemId, SqlConnection openCon)
    {
        var saveMainStarSystemToHex = $"UPDATE dbo.Hex SET MainSystemId = @MainSystemId WHERE Id = @HexId";
        
        using SqlCommand querySaveMainStarSystemToHex = new SqlCommand(saveMainStarSystemToHex);
        querySaveMainStarSystemToHex.Connection = openCon;
        querySaveMainStarSystemToHex.Parameters.Add("@MainSystemId", SqlDbType.UniqueIdentifier).Value = starSystemId;
        querySaveMainStarSystemToHex.Parameters.Add("@HexId", SqlDbType.UniqueIdentifier).Value = hexId;
        
        openCon.Open();
        querySaveMainStarSystemToHex.ExecuteNonQuery();
        openCon.Close();
    }

    public static List<WorldBuilderSubsector> GetSubsectors(Guid sectorId)
    {
        var subsectors = new List<WorldBuilderSubsector>();
        
        using SqlConnection openCon = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=WorldBuilderSectorGeneration;Trusted_Connection=True;");
        var getSubsectors = "SELECT * FROM dbo.Subsector WHERE SectorId = @SectorId";

        using SqlCommand queryGetSubsectors = new SqlCommand(getSubsectors);
        queryGetSubsectors.Connection = openCon;
        queryGetSubsectors.Parameters.Add("@SectorId", SqlDbType.UniqueIdentifier).Value = sectorId;
        
        openCon.Open();
        using SqlDataReader reader = queryGetSubsectors.ExecuteReader();
        while (reader.Read()) {
            var subsector = new WorldBuilderSubsector {
                Id = (Guid)reader[0],
                Coordinates = new Coordinates {
                    X = (int) reader[2],
                    Y = (int) reader[3]
                }
            };
            
            subsectors.Add(subsector);
        }

        return subsectors.OrderBy(x => x.Coordinates.X).ThenBy(x => x.Coordinates.Y).ToList();
    }

    public static List<WorldBuilderHex> GetHexes(Guid subsectorId)
    {
        var hexes = new List<WorldBuilderHex>();
        
        using SqlConnection openCon = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=WorldBuilderSectorGeneration;Trusted_Connection=True;");
        var getSubsectors = "SELECT * FROM dbo.Hex WHERE SubsectorId = @SubsectorId";

        using SqlCommand queryGetSubsectors = new SqlCommand(getSubsectors);
        queryGetSubsectors.Connection = openCon;
        queryGetSubsectors.Parameters.Add("@SubsectorId", SqlDbType.UniqueIdentifier).Value = subsectorId;
        
        openCon.Open();
        using SqlDataReader reader = queryGetSubsectors.ExecuteReader();
        while (reader.Read()) {
            var hex = new WorldBuilderHex {
                Id = (Guid) reader[0],
                Coordinates = {
                    X = (int) reader [3], 
                    Y = (int) reader[4]
                }
            };
            
            hexes.Add(hex);
        }

        return hexes.OrderBy(x => x.Coordinates.X).ThenBy(x => x.Coordinates.Y).ToList();
    }
}