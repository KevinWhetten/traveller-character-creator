using SectorCreator.Global;

namespace SectorCreator.Models.Basic;

public class Starport
{
    private readonly IRollingService _rollingService;

    public Starport(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }

    public StarportClass Class { get; set; }
    public StarportSpecialFeature SpecialFeature;

    public Starport()
    { }

    public StarportSpecialistType SpecialistType { get; set; }
    public StarportEvent Event { get; set; }
    public List<StarportInstallation> Installations { get; set; } = new();
    public StarportEnforcement Enforcement { get; set; }
    public StarportDefenses Defenses { get; set; }
    public bool Highport { get; set; }


    public void GenerateSpecialFeature()
    {
        if (_rollingService.D6(2) < 8 || Class is StarportClass.X) {
            return;
        }

        SpecialFeature = _rollingService.D6(2) switch {
            2 => StarportSpecialFeature.Ruins,
            3 => StarportSpecialFeature.AlienQuarter,
            4 => StarportSpecialFeature.LacksCapability,
            5 => StarportSpecialFeature.NoDownport,
            6 => StarportSpecialFeature.SpecialistType,
            7 => StarportSpecialFeature.Installation,
            8 => StarportSpecialFeature.Independent,
            9 => StarportSpecialFeature.RunByEntity,
            10 => StarportSpecialFeature.MajorTradeNexus,
            11 => StarportSpecialFeature.Orbital,
            12 => StarportSpecialFeature.UnusualConstruction
        };

        if (SpecialFeature == StarportSpecialFeature.Installation) {
            var installation = _rollingService.D10(1) switch {
                1 => StarportInstallation.ArmyBase,
                2 => StarportInstallation.DefenseBase,
                3 => StarportInstallation.MaintenanceFacility,
                4 => StarportInstallation.NavalBase,
                5 => StarportInstallation.NavalDepot,
                6 => StarportInstallation.ResearchFacility,
                7 => StarportInstallation.ScoutBase,
                8 => StarportInstallation.ExplorerBase,
                9 => StarportInstallation.Shipyard,
                10 => StarportInstallation.WayStation
            };
            Installations.Add(installation);
        }

        if (SpecialFeature == StarportSpecialFeature.SpecialistType) {
            SpecialistType = _rollingService.D6(1) switch {
                1 => StarportSpecialistType.Industrial,
                2 => StarportSpecialistType.LaunchFacilities,
                3 => StarportSpecialistType.Makeshift,
                4 => StarportSpecialistType.Military,
                5 => StarportSpecialistType.Passenger,
                6 => StarportSpecialistType.Private
            };
        }
    }

    public void GenerateEvent()
    {
        if (_rollingService.D6(2) < 6 || Class is StarportClass.X) {
            return;
        }

        Event = _rollingService.D6(2) switch {
            2 => StarportEvent.Embargo,
            3 => StarportEvent.Neglected,
            4 => StarportEvent.Secure,
            5 => StarportEvent.RunDown,
            6 => StarportEvent.Slowdown,
            7 => StarportEvent.VibrantMarket,
            8 => StarportEvent.TrafficSurge,
            9 => StarportEvent.Investment,
            10 => StarportEvent.NewMarket,
            11 => StarportEvent.Smuggling,
            12 => StarportEvent.Expansion
        };
    }

    public void GenerateDefenses(bool navalOrMilitaryBasePresent, bool navalDepotPresent)
    {
        if (Class is StarportClass.X) {
            return;
        }

        var roll = _rollingService.D6(2) + Class switch {
            StarportClass.A => 4,
            StarportClass.B => 2,
            StarportClass.C => 0,
            StarportClass.D => -3,
            StarportClass.E => -6,
            _ => 0
        };
        roll += navalOrMilitaryBasePresent ? 4 : 0;
        roll += navalDepotPresent ? 8 : 0;

        Defenses = roll switch {
            <= 2 => StarportDefenses.None,
            <= 5 => StarportDefenses.Minimal,
            <= 8 => StarportDefenses.Light,
            <= 11 => StarportDefenses.Standard,
            <= 15 => StarportDefenses.Heavy,
            <= 19 => StarportDefenses.VeryHeavy,
            _ => StarportDefenses.Fortress
        };
    }

    public void GenerateEnforcement(int hostLawLevel)
    {
        if (Class is StarportClass.X) {
            return;
        }

        var roll = hostLawLevel - _rollingService.D6(2) + Class switch {
            StarportClass.A => 7,
            StarportClass.B => 5,
            StarportClass.C => 3,
            StarportClass.D => 1,
            _ => 0
        };

        Enforcement = roll switch {
            <= 0 => StarportEnforcement.Violent,
            <= 2 => StarportEnforcement.Harmonious,
            <= 4 => StarportEnforcement.Minimal,
            <= 6 => StarportEnforcement.Small,
            <= 8 => StarportEnforcement.Average,
            <= 10 => StarportEnforcement.WellEquipped,
            <= 12 => StarportEnforcement.Paramilitary,
            <= 14 => StarportEnforcement.Military,
            _ => StarportEnforcement.PrivateArmy
        };
    }
}