using SectorCreator.Models.CustomTypes;
using SectorCreator.WorldBuilder.Enums;

namespace SectorCreator.WorldBuilder.Planet.TerrestrialPlanet;

public partial class WorldBuilderTerrestrialPlanet
{
    public string GetDetailedReport(WorldBuilderStarSystem starSystem)
    {
        var reportString = $"WORLD\t{Name} ({starSystem.Name} {Primary} d) {SAH}-837\tSAH/UWP\t{SAH}\n";
        reportString += $"SECTOR | LOCATION\t??? ????\tInitialSurvey\t???\tLastUpdated\t???\n";
        reportString += $"PrimaryObject(s)\t{starSystem.Star.Name}\tSystem Age (Gyr)\t{starSystem.Age:N3}\tTravel Zone\t\n";
        reportString +=
            $"ORBIT\tO#\t{OrbitNumber:N1}\tAU\t{OrbitDistance:N2}\tEccentricity\t{Eccentricity:N3}\tPeriod\tSolar: {Period:N2}y ({Period * 365.25:N2} std d)\n";
        reportString += $"NOTES\t\n";
        reportString += $"SIZE\tDiameter(km)\tComposition\tDensity\tGravity\tMass\tEsc v (kps)\n";
        reportString += $"\t{Diameter:N0}\t{GetPlanetComposition()}\t{Density:N2}\t{Gravity:N2}\t{Mass:N2}\t{EscapeVelocity:N3}\n";
        reportString += $"Notes:\t\n";
        reportString +=
            $"ATMOSPHERE\tPressure (bar)\t{BAR:N3}\tComposition\t{GetAtmosphereComposition()}\tO\u2082 (bar)\t{PartialPressureOfOxygen:N3}\n";
        reportString += $"Taints\t{GetTaints()}\tScale Height\t{ScaleHeight:N2}\n";
        reportString += $"Notes\t\n";
        reportString +=
            $"HYDROGRAPHICS\tCoverage (%)\t{HydroPercent:N2}\tComposition\t{GetLiquidComposition()}\tDistribution\t{SurfaceDistribution}\n";
        reportString += $"Major bodies\t{MajorContinents} major continents\tMinor bodies\t{MinorContinents} minor continents\tOther\t\n";
        reportString += $"Notes:\t\n";
        reportString +=
            $"ROTATION\tSidereal\t{GetValueInTime(SiderealDay)} ({SiderealDay:N3})\tSolar\t{GetValueInTime(SolarDayInHours)} ({SolarDayInHours:N2})\tSolar days/year\t{SolarDaysInLocalYear:N2}\tAxial Tilt\t{AxialTilt:N3}\u00b0\n";
        reportString += $"\tTidal lock?\t{(IsTidallyLocked ? "Yes" : "No")}\tTides\tMoons {MoonTidalEffect:N2}m, Star {StarTidalEffect:N2}m\n";
        reportString += $"Notes:\t\n";
        reportString += $"TEMPERATURE\tHigh:\t{HighTemperature}K | {HighTemperature - 273}\u00b0C\tLuminosity\t{starSystem.Luminosity:N2}\tNotes:\n";
        reportString += $"\tMean:\t{Temperature}K | {Temperature - 273}\u00b0C\tAlbedo:\t{Albedo:N2}\t\n";
        reportString += $"\tLow:\t{LowTemperature}K | {LowTemperature - 273}\u00b0C\tGreenhouse:\t{GreenhouseFactor:N3}\t\n";
        reportString +=
            $"Seismic Stress:\t{TotalSeismicStress:N0}\tResidual Stress:\t{ResidualSeismicStress:N0}\tTidal Stress:\t{TidalStressFactor:N0}\tTidal Heating:\t{TidalHeatingFactor:N0}\tMajor Tectonic Plates:\t{MajorTectonicPlates:N0}\n";
        reportString +=
            $"LIFE\tBiomass\t{ExtendedHex.values[BiomassRating]}\tBiocomplexity:\t{ExtendedHex.values[BiocomplexityRating]}\tSophonts\t{(CurrentSophontExists ? "Yes" : "No")}\tBiodiversity:\t{ExtendedHex.values[BiodiversityRating]}\tCompatibility:\t{ExtendedHex.values[CompatibilityRating]}\n";
        reportString += $"Notes:\t\n";
        reportString += $"RESOURCES\tRating:\t{ExtendedHex.values[ResourceRating]}\tNotes:\t\n";
        reportString += $"HABITABILITY\tRating:\t{ExtendedHex.values[HabitabilityRating]}\tNotes:\t\n";
        reportString += $"SUBORDINATES\tSAH/UWP\tOrbit (PD)\tOrbit (km)\tEcc\tDiameter\tDensity\tMass\tPeriod (h)\tSize(\u00b0)\n";
        reportString += GetSubordinateInfo();
        reportString += $"Notes:\t\n";
        reportString += $"COMMENTS\t";

        return reportString;
    }

    private string GetPlanetComposition()
    {
        return Composition switch {
            Composition.None => "None",
            Composition.ExoticIce => "Exotic Ice",
            Composition.MostlyIce => "Mostly Ice",
            Composition.MostlyRock => "Mostly Rock",
            Composition.RockAndMetal => "Rock and Metal",
            Composition.MostlyMetal => "Mostly Metal",
            Composition.CompressedMetal => "Compressed Metal",
            _ => "Unknown"
        };
    }

    private string GetAtmosphereComposition()
    {
        List<string> compositionString = AtmosphereChemicals.Select(element => $"{element.Element.Name} {element.Percent:N2}%").ToList();

        return string.Join(", ", compositionString);
    }

    private string GetTaints()
    {
        List<string> taintStrings = AtmosphereTaints.Select(taint => taint.ToString()).ToList()!;

        return taintStrings.Count != 0
            ? string.Join(", ", taintStrings)
            : "none";
    }

    private string GetLiquidComposition()
    {
        List<string> compositionString = LiquidChemicals.Select(element => $"{element.Element.Code}").ToList();

        return string.Join(", ", compositionString);
    }

    private string GetValueInTime(double value)
    {
        var hours = $"{Math.Floor(SiderealDay):N0}";

        value -= Math.Floor(SiderealDay);
        var minutes = $"{Math.Floor(60.0 * value):N0}";

        value -= Math.Floor(60.0 * value) / 60.0;
        var seconds = $"{60.0 * 60.0 * value:N0}";

        if (seconds == "60") {
            seconds = "0";
            minutes = (int.Parse(minutes) + 1).ToString();
        }

        if (minutes == "60") {
            minutes = "0";
            hours = (int.Parse(hours) + 1).ToString();
        }

        var valueInTime = $"{hours}h{(minutes != "0" && seconds != "0" ? $" {minutes}m{(seconds != "0" ? $" {seconds}s" : "")}" : "")}";

        return valueInTime;
    }

    private string GetSubordinateInfo()
    {
        return Moons.Aggregate("",
            (current, moon) =>
                current +
                $"{moon.Name}\t{moon.SAH}\t{moon.OrbitDistanceInDiameters:N0}\t{moon.OrbitDistanceInKM:N0}\t{moon.Eccentricity:N3}\t{moon.Diameter:N0}\t{moon.Density:N3}\t{moon.Mass:N4}\t{moon.Period:N2}\t{moon.Size}\n");
    }
}