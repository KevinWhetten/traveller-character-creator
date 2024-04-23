using SectorCreator.WorldBuilder.Enums;
using SectorCreator.WorldBuilder.Planet.Moon;

namespace SectorCreator.WorldBuilder.Planet.AsteroidBelt;

public partial class WorldBuilderAsteroidBelt
{
    private double BeltSpan { get; set; }
    private int Bulk { get; set; }
    private AsteroidBeltComposition Composition { get; set; } = new();

    public void GenerateSignificantBodies(WorldBuilderStarSystem starSystem, double outermostOrbit)
    {
        var size1DM = 0;
        var size2DM = 0;

        if (OrbitNumber > starSystem.Star.HZCO + 3) size1DM += 2;
        if (BeltSpan < 0.1) size1DM -= 4;

        if (OrbitNumber >= starSystem.Star.HZCO + 2 && OrbitNumber <= starSystem.Star.HZCO + 3) size2DM++;
        if (OrbitNumber > starSystem.Star.HZCO + 3) size2DM += 3;
        if (BeltSpan > 1.0) size2DM++;

        var size1Bodies = Math.Max(_rollingService.D6(2) - 12 + Bulk + size1DM, 0);
        var sizeSBodies = Math.Max(_rollingService.D6(2) - 10 + (size2DM + 1) * (Bulk + 1), 0);

        if (BeltSpan < 0.1 && sizeSBodies > 0) {
            sizeSBodies = (int) Math.Ceiling(sizeSBodies / 2m);
        }

        if (sizeSBodies > 50 && Math.Abs(OrbitNumber - outermostOrbit) < .001) {
            sizeSBodies = (int) (sizeSBodies * ((double) _rollingService.D6(1) / _rollingService.D3(1)) + _rollingService.D6(1));
        }

        var createdBodies =
            CreateSignificantBodies(starSystem, (int) Math.Round(size1Bodies * Composition.m / 100m), 1, Enums.Composition.MostlyMetal);
        createdBodies += CreateSignificantBodies(starSystem, (int) Math.Round(size1Bodies * Composition.s / 100m), 1, Enums.Composition.MostlyRock);
        createdBodies += CreateSignificantBodies(starSystem, (int) Math.Round(size1Bodies * Composition.c / 100m), 1, Enums.Composition.MostlyIce);
        CreateSignificantBodies(starSystem, size1Bodies - createdBodies, 1, Enums.Composition.MostlyIce);

        createdBodies = CreateSignificantBodies(starSystem, (int) Math.Round(sizeSBodies * Composition.m / 100m), 26, Enums.Composition.MostlyMetal);
        createdBodies += CreateSignificantBodies(starSystem, (int) Math.Round(sizeSBodies * Composition.s / 100m), 26, Enums.Composition.MostlyRock);
        createdBodies += CreateSignificantBodies(starSystem, (int) Math.Round(sizeSBodies * Composition.c / 100m), 26, Enums.Composition.MostlyIce);
        CreateSignificantBodies(starSystem, sizeSBodies - createdBodies, 26, Enums.Composition.MostlyIce);
    }
    
    private int CreateSignificantBodies(WorldBuilderStarSystem starSystem, int num, int size, Composition composition = Enums.Composition.None)
    {
        var flag = composition == Enums.Composition.None;
        for (var i = 0; i < num; i++) {
            var moon = new WorldBuilderMoon {
                OrbitNumber = (_rollingService.Flux() * BeltSpan) / 8.0,
                Composition = composition,
                Size = size
            };
            if (flag) {
                moon.GenerateComposition(starSystem);
            } else {
                moon.GenerateComposition(starSystem, composition);
            }
        }
    
        return num;
    }
}