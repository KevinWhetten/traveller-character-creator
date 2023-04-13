using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Factories;

public interface IHexFactory
{
    Hex GenerateMongooseHex(Coordinates subsectorCoordinates, Coordinates hexCoordinates, SectorType sectorType);
    Hex GenerateT5Hex(Coordinates subsectorCoordinates, Coordinates hexCoordinates);
    Hex GenerateRttWorldgenHex(Coordinates subsectorCoordinates, Coordinates hexCoordinates);
    Hex GenerateStarFrontiersHex(Coordinates subsectorCoordinates, Coordinates hexCoordinates);
}

public class HexFactory : IHexFactory
{
    private readonly IStarSystemFactory _starSystemFactory;
    private readonly IRollingService _rollingService;

    public HexFactory(IStarSystemFactory starSystemFactory, IRollingService rollingService)
    {
        _starSystemFactory = starSystemFactory;
        _rollingService = rollingService;
    }

    public Hex GenerateMongooseHex(Coordinates subsectorCoordinates, Coordinates hexCoordinates, SectorType sectorType)
    {
        var hex = new Hex {
            Coordinates = SetCoordinates(subsectorCoordinates, hexCoordinates)
        };

        if (_rollingService.D6(1) >= 4) {
            hex.StarSystems.Add(_starSystemFactory.GenerateMongooseStarSystem(sectorType));
        }

        return hex;
    }

    public Hex GenerateT5Hex(Coordinates subsectorCoordinates, Coordinates hexCoordinates)
    {
        var hex = new Hex {
            Coordinates = SetCoordinates(subsectorCoordinates, hexCoordinates)
        };

        hex.StarSystems.Add(_starSystemFactory.GenerateT5StarSystem());

        return hex;
    }

    public Hex GenerateRttWorldgenHex(Coordinates subsectorCoordinates, Coordinates hexCoordinates)
    {
        var hex = new Hex {
            Coordinates = SetCoordinates(subsectorCoordinates, hexCoordinates)
        };

        if (_rollingService.D6(1) >= 4) {
            hex.StarSystems.Add(_starSystemFactory.GenerateRttWorldgenStarSystem(StarSystemType.BrownDwarf));
        }

        if (_rollingService.D6(1) >= 4) {
            hex.StarSystems.Add(_starSystemFactory.GenerateRttWorldgenStarSystem(StarSystemType.Regular));
        }

        foreach (var starSystem in hex.StarSystems) {
            foreach (var star in starSystem.Stars.Cast<RttWorldgenStar>()
                         .Where(star => star.CompanionOrbit == CompanionOrbit.Distant)) {
                starSystem.Stars.Remove(star);
                hex.StarSystems.Add(_starSystemFactory.GenerateRttWorldgenStarSystem(star));
            }
        }

        return hex;
    }

    public Hex GenerateStarFrontiersHex(Coordinates subsectorCoordinates, Coordinates hexCoordinates)
    {
        var hex = new Hex {
            Coordinates = SetCoordinates(subsectorCoordinates, hexCoordinates)
        };

        if (_rollingService.D10(1) >= 5) {
            hex.StarSystems.Add(_starSystemFactory.GenerateStarFrontiersStarSystem());
        }

        return hex;
    }

    private Coordinates SetCoordinates(Coordinates subsectorCoordinates, Coordinates hexCoordinates)
    {
        return new Coordinates {
            X = hexCoordinates.X + 8 * ((subsectorCoordinates.X - 1) % 8),
            Y = hexCoordinates.Y + 10 * ((subsectorCoordinates.Y - 1) % 10)
        };
    }
}