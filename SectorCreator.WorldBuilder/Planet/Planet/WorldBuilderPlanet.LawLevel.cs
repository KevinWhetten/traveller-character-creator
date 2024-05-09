namespace SectorCreator.WorldBuilder.Planet.Planet;

public partial class WorldBuilderPlanet
{
    public int LawLevel => Governments.Count > 0 ? Governments.First().LawLevel : 0;

    private void GenerateLawLevel(Government government)
    {
        government.LawLevel = Math.Max(_rollingService.Flux() + government.Code, 0);
        GenerateJusticeSystem(government);
        GenerateUniformityOfLaw(government);
        GeneratePresumptionOfInnocence(government);
        GenerateDeathPenalty(government);
        GenerateSubclassifications(government);
    }

    private void GenerateJusticeSystem(Government government)
    {
        var dm = government.Code switch {
            1 or (>= 8 and <= 12) or 15 => -2,
            13 or 14 => 4,
            _ => 0
        };

        if (LawLevel >= 10) dm -= 4;
        dm += TechLevel switch {
            <= 0 => 4,
            <= 2 => 2,
            _ => 0
        };

        if (Governments.First().Authorities.Where(x => x.IsMainAuthority).Select(x => x.Authority.Code).Contains('J')) dm -= 2;

        government.JusticeSystem = (_rollingService.D6(2) + dm) switch {
            <= 5 => JusticeSystem.Inquisitorial,
            <= 8 => JusticeSystem.Adversarial,
            >= 9 => JusticeSystem.Traditional
        };
    }

    private void GenerateUniformityOfLaw(Government government)
    {
        var dm = Government switch {
            3 or 5 or >= 10 => -1,
            2 => 1,
            _ => 0
        };

        government.UniformityOfLaw = (_rollingService.D6(1) + dm) switch {
            <= 2 => Uniformity.Personal,
            3 => Uniformity.Territorial,
            >= 4 => Uniformity.Universal
        };
    }

    private void GeneratePresumptionOfInnocence(Government government)
    {
        var dm = -LawLevel;
        if (government.JusticeSystem == JusticeSystem.Adversarial) dm += 2;

        government.PresumptionOfInnocence = _rollingService.D6(2) + dm > 0;
    }

    private void GenerateDeathPenalty(Government government)
    {
        var dm = 0;
        if (Government == 0) dm -= 4;
        if (TechLevel >= 9) dm += 4;

        government.DeathPenalty = _rollingService.D6(2) + dm >= 8;
    }

    private void GenerateSubclassifications(Government government)
    {
        GenerateEconomicLaw(government);
        GenerateCriminalLaw(government);
        GeneratePrivateLaw(government);
        GeneratePersonalRightsLevel(government);
    }

    private void GenerateEconomicLaw(Government government)
    {
        var dm = Government switch {
            0 => -2,
            1 => 2,
            2 => -1,
            9 => 1,
            _ => 0
        };

        government.EconomicLawLevel = Math.Max(LawLevel + _rollingService.D3(2) - 4 + dm, 0);
    }

    private void GenerateCriminalLaw(Government government)
    {
        var dm = government.JusticeSystem == JusticeSystem.Inquisitorial ? 1 : 0;

        government.CriminalLawLevel = Math.Max(LawLevel + _rollingService.D3(2) - 4 + dm, 0);
    }

    private void GeneratePrivateLaw(Government government)
    {
        var dm = Government is 3 or 5 or 12 ? -1 : 0;

        government.PrivateLawLevel = Math.Max(_rollingService.D3(2) - 4 + dm, 0);
    }

    private void GeneratePersonalRightsLevel(Government government)
    {
        var dm = Government switch {
            0 or 2 => -1,
            1 => 2,
            _ => 0
        };

        government.PersonalRightsLevel = Math.Max(LawLevel + _rollingService.D3(2) - 4 + dm, 0);
    }
}