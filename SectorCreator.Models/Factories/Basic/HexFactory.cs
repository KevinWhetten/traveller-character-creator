using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.Factories.RttWorldgen;
using SectorCreator.Models.Factories.t5;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Factories.Basic;

public interface IHexFactory
{
    Hex GenerateMongooseHex(Coordinates subsectorCoordinates, Coordinates hexCoordinates, SectorType sectorType);
    Hex GenerateT5Hex(Coordinates subsectorCoordinates, Coordinates hexCoordinates);
    Hex GenerateRttWorldgenHex(Coordinates subsectorCoordinates, Coordinates hexCoordinates);
    Hex GenerateStarFrontiersHex(Coordinates subsectorCoordinates, Coordinates hexCoordinates);
}

public class HexFactory : IHexFactory
{
    private readonly IRollingService _rollingService;
    private readonly IStarSystemFactory _starSystemFactory;
    private readonly IT5StarSystemFactory _t5StarSystemFactory;
    private readonly IRttWorldgenStarSystemFactory _rttWorldgenStarSystemFactory;

    public HexFactory(IRollingService rollingService, IStarSystemFactory starSystemFactory, IT5StarSystemFactory t5StarSystemFactory, IRttWorldgenStarSystemFactory rttWorldgenStarSystemFactory)
    {
        _rollingService = rollingService;
        _starSystemFactory = starSystemFactory;
        _t5StarSystemFactory = t5StarSystemFactory;
        _rttWorldgenStarSystemFactory = rttWorldgenStarSystemFactory;
    }

    public Hex GenerateMongooseHex(Coordinates subsectorCoordinates, Coordinates hexCoordinates, SectorType sectorType)
    {
        var hex = new Hex {
            Coordinates = SetCoordinates(subsectorCoordinates, hexCoordinates)
        };

        if (_rollingService.D6(1) >= 4) {
            hex.StarSystems.Add(_starSystemFactory.GenerateMongooseStarSystem(sectorType, hex.Coordinates));
        }

        return hex;
    }

    public Hex GenerateT5Hex(Coordinates subsectorCoordinates, Coordinates hexCoordinates)
    {
        var hex = new Hex {
            Coordinates = SetCoordinates(subsectorCoordinates, hexCoordinates)
        };

        hex.StarSystems.Add(_t5StarSystemFactory.Generate());

        return hex;
    }

    public Hex GenerateRttWorldgenHex(Coordinates subsectorCoordinates, Coordinates hexCoordinates)
    {
        var hex = new Hex {
            Coordinates = SetCoordinates(subsectorCoordinates, hexCoordinates)
        };

        if (_rollingService.D6(1) > 4) {
            hex.StarSystems.Add(_rttWorldgenStarSystemFactory.Generate(StarSystemType.BrownDwarf, hex.Coordinates));
        }

        if (_rollingService.D6(1) > 4) {
            hex.StarSystems.Add(_rttWorldgenStarSystemFactory.Generate(StarSystemType.Regular, hex.Coordinates));
        }

        for (var index = 0; index < hex.StarSystems.Count; index++) {
            var starSystem = hex.StarSystems[index];
            for (var i = 0; i < starSystem.CompanionStars.Count; i++) {
                var star = (RttWorldgenStar) starSystem.CompanionStars[i];
                if (star.CompanionOrbit == CompanionOrbit.Distant) {
                    starSystem.CompanionStars.Remove(star);
                    hex.StarSystems.Add(_rttWorldgenStarSystemFactory.Generate(star, hex.Coordinates));
                }
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
            hex.StarSystems.Add(_starSystemFactory.GenerateStarFrontiersStarSystem(hex.Coordinates));
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