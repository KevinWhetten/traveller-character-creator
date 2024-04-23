using SectorCreator.Global.Enums;
using SectorCreator.WorldBuilder.Star;

namespace SectorCreator.WorldBuilder.Planet.GasGiant;

public partial class WorldBuilderGasGiantPlanet
{
    public void GenerateSizeCharacteristics(WorldBuilderStarSystem starSystem)
    {
        switch (_rollingService.D6(1) + GetGasGiantSizeDM(starSystem.Star, starSystem.Spread)) {
            case <= 2:
                Size = 16;
                Diameter = _rollingService.D3(2) * 12742;
                Density = _rollingService.Between(0.08, 0.16);
                break;
            case <= 4:
                Size = 17;
                Diameter = (_rollingService.D6(1) + 6) * 12742;
                Density = _rollingService.Between(0.16, 0.20);
                break;
            case >= 5:
                Size = 18;
                Diameter = (_rollingService.D6(2) + 6) * 12742;
                Density = _rollingService.Between(0.20, 0.70);
                break;
        }
    }
    
    private int GetGasGiantSizeDM(WorldBuilderStar primaryStar, double systemSpread)
    {
        var dm = 0;
        if (primaryStar.SpectralType == SpectralType.BD
            || primaryStar is {SpectralType: SpectralType.M, LuminosityClass: LuminosityClass.V}
            || primaryStar.LuminosityClass == LuminosityClass.VI) {
            dm--;
        }
    
        if (systemSpread < .01) dm--;
    
        return dm;
    }
}