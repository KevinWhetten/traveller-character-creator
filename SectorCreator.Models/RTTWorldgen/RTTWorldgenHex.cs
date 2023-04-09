using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.RTTWorldgen;

public class RttWorldgenHex : Hex
{
    private void Generate()
    {
        var newStarSystem = new RttWorldgenStarSystem();
        
        if (Roll.D6(1) >= 4)
        {
            newStarSystem.Generate(StarSystemType.BrownDwarf);
            StarSystems.Add(newStarSystem);
        }

        if (Roll.D6(1) >= 4)
        {
            newStarSystem.Generate(StarSystemType.Regular);
            StarSystems.Add(newStarSystem);
        }

        foreach (var starSystem in StarSystems.Cast<RttWorldgenStarSystem>()) {
            foreach (var star in starSystem.Stars.Cast<RttWorldgenStar>()
                         .Where(star => star.CompanionOrbit == CompanionOrbit.Distant)) {
                starSystem.Stars.Remove(star);
                newStarSystem = new RttWorldgenStarSystem();
                newStarSystem.Generate(star);
                StarSystems.Add(newStarSystem);
            }
        }
    }

    public void Generate(Coordinates subsectorCoordinates, Coordinates hexCoordinates)
    {
        SetCoordinates(subsectorCoordinates, hexCoordinates);
        Generate();
    }
}