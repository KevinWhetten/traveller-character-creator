using SectorCreator.Global.Enums;
using SectorCreator.WorldBuilder.Planet.AsteroidBelt;
using SectorCreator.WorldBuilder.Planet.Moon;

namespace SectorCreator.WorldBuilder.Planet.Planet;

public partial class WorldBuilderPlanet
{
    public double HillSphereAU { get; set; }
    public double HillSpherePD => Diameter > 0 ? HillSphereAU * 149597870.9 / Diameter : 0;
    public int HillSphereMoonLimit => (int)Math.Floor(HillSpherePD / 2.0);
    public int MoonOrbitRange { get; set; }
    public List<WorldBuilderMoon> Moons { get; set; } = new();
    public int Sub => Moons.Count(x => x.Size != 25);

    public void GenerateMoons(WorldBuilderStarSystem starSystem)
    {
        if (PlanetType == PlanetType.AsteroidBelt) {
            ((WorldBuilderAsteroidBelt) this).GenerateSignificantBodies(starSystem, starSystem.Planets.Max(x => x.OrbitNumber));
        } else {
            HillSphereAU = OrbitDistance * (1 - Eccentricity) * Math.Pow((Mass * 0.000003) / (3 * starSystem.Mass), 1.0 / 3.0);
            MoonOrbitRange = Math.Min(HillSphereMoonLimit - 2, 200);

            var moonDM = GetMoonDM(HasMoonDMPenalty(starSystem));

            var moonQuantity = Size switch {
                <= 2 => _rollingService.D6(1) - 5 - moonDM,
                <= 9 => _rollingService.D6(2) - 8 - (moonDM * 2),
                <= 15 => _rollingService.D6(2) - 6 - (moonDM * 2),
                16 => _rollingService.D6(3) - 7 - (moonDM * 3),
                >= 17 => _rollingService.D6(4) - 6 - (moonDM * 4)
            };

            if (moonQuantity == 0) {
                GenerateMoon(starSystem, 25);
            }

            if (HillSpherePD < 1.5 && moonQuantity >= 1) {
                GenerateMoon(starSystem, 25);
                moonQuantity = 0;
            }

            for (var i = 0; i < moonQuantity; i++) {
                GenerateMoon(starSystem);
            }

            Moons = Moons.OrderBy(x => x.MoonOrbit).ToList();
        }
    }

    private bool HasMoonDMPenalty(WorldBuilderStarSystem starSystem)
    {
        if (starSystem.Star.CompanionStar != null) {
            if (OrbitNumber - starSystem.Spread <= starSystem.Star.AvailableOrbits.First()) {
                return true;
            }
        }

        if (OrbitNumber < 12) {
            if (!starSystem.OrbitIsAvailable(OrbitNumber - starSystem.Spread) ||
                !starSystem.OrbitIsAvailable(OrbitNumber + starSystem.Spread)) {
                return true;
            }
        }

        if (starSystem.Star.StarType != StarType.Primary) {
            if (OrbitNumber + starSystem.Spread > starSystem.Star.AvailableOrbits.Last()) {
                return true;
            }
        }

        return false;
    }

    private int GetMoonDM(bool moonDMPenalty)
    {
        if (OrbitNumber < 1) return -1;
        if (moonDMPenalty) return -1;
        return 0;
    }


    private void GenerateMoon(WorldBuilderStarSystem starSystem, int size = 0)
    {
        WorldBuilderMoon moon;
        moon = new WorldBuilderMoon();
        if (size == 0) {
            size = _rollingService.D6(1) switch {
                <= 3 => 26,
                <= 5 => _rollingService.D3(1) - 1,
                6 => Size - 1 - _rollingService.D6(1),
                _ => moon.Size
            };

            if (size == 0) {
                size = 25;
            } else if (size < 0) {
                size = 26;
            }
        }

        moon = size == 25
            ? new WorldBuilderRings()
            : new WorldBuilderMoon(size);

        if (moon.Size == 25) {
            ((WorldBuilderRings) moon).Generate(this);
        } else {
            moon.Generate(starSystem, this);
        }

        Moons.Add(moon);
    }
}