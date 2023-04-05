using TravellerCharacterCreatorBL.SectorCreator.Planet;
using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.SectorCreator.Enums;
using TravellerCreatorModels.SectorCreator.Interfaces;
using TravellerCreatorModels.SectorCreator.Mongoose;
using TravellerCreatorModels.SectorCreator.RTTWorldgen;
using TravellerCreatorModels.SectorCreator.StarFrontiers;
using TravellerCreatorServices;

namespace TravellerCharacterCreatorBL.SectorCreator;

public class PlanetGenerator
{
    private readonly ITradeCodesService _tradeCodesService;
    private readonly MongoosePlanetGenerator _mongoosePlanetGenerator;
    private readonly StarFrontiersPlanetGenerator _starFrontiersPlanetGenerator;
    private readonly RTTWorldgenPlanetGenerator _rttWorldgenPlanetGenerator;

    public PlanetGenerator()
    {
        _tradeCodesService = new TradeCodeService();
        _mongoosePlanetGenerator = new MongoosePlanetGenerator();
        _starFrontiersPlanetGenerator = new StarFrontiersPlanetGenerator();
        _rttWorldgenPlanetGenerator = new RTTWorldgenPlanetGenerator();
    }

    public IPlanet GenerateMongoosePlanet(SectorType sectorType)
    {
        MongoosePlanet planet = _mongoosePlanetGenerator.GeneratePlanet(sectorType);
        planet.TradeCodes = _tradeCodesService.GetTradeCodes(planet);

        return planet;
    }

    public IPlanet GenerateStarFrontiersPlanet(bool habitable, int habitableBase, int planetNum)
    {
        StarFrontiersPlanet planet = _starFrontiersPlanetGenerator.GeneratePlanet(habitable, habitableBase, planetNum);
        planet.TradeCodes = _tradeCodesService.GetTradeCodes(planet);

        return planet;
    }

    public List<IPlanet> GenerateRTTWorldgenPlanets(List<IStar> starSystemStars)
    {
        var planets = new List<IPlanet>();
        List<RTTWorldgenStar> rttWorldgenStars = starSystemStars.Cast<RTTWorldgenStar>().ToList();
        
        planets.AddRange(GenerateEpistellarPlanets(rttWorldgenStars[0]));
        planets.AddRange(GenerateInnerPlanets(rttWorldgenStars, planets.Count + 1));
        planets.AddRange(GenerateOuterPlanets(rttWorldgenStars, planets.Count + 1));

        return planets;
    }

    private List<RTTWorldgenPlanet> GenerateEpistellarPlanets(RTTWorldgenStar primaryStar)
    {
        var planets = new List<RTTWorldgenPlanet>();

        var roll = Roll.D6(1) - 3;

        if (primaryStar.SpectralType == SpectralType.M && primaryStar.Luminosity == Luminosity.V) {
            roll--;
        } else if (primaryStar.Luminosity == Luminosity.III
                   || primaryStar.SpectralType is SpectralType.D or SpectralType.L) {
            roll = 0;
        }

        roll = Math.Min(roll, 2);

        for (var i = 0; i < roll; i++) {
            planets.Add(_rttWorldgenPlanetGenerator.GeneratePlanet(PlanetOrbit.Epistellar, primaryStar, i + 1));
        }

        return planets;
    }

    private List<RTTWorldgenPlanet> GenerateInnerPlanets(List<RTTWorldgenStar> starSystemStars, int orbitNum)
    {
        var planets = new List<RTTWorldgenPlanet>();

        int roll = Roll.D6(1) - 1;

        if (starSystemStars[0].SpectralType == SpectralType.M && starSystemStars[0].Luminosity == Luminosity.V) {
            roll--;
        } else if (starSystemStars.Exists(x => x.CompanionOrbit == CompanionOrbit.Close)) {
            roll = 0;
        } else if (starSystemStars[0].SpectralType == SpectralType.L) {
            roll = Roll.D3(1) - 1;
        }

        for (var i = 0; i < roll; i++) {
            planets.Add(_rttWorldgenPlanetGenerator.GeneratePlanet(PlanetOrbit.Inner, starSystemStars[0], orbitNum + i));
        }

        return planets;
    }

    private List<RTTWorldgenPlanet> GenerateOuterPlanets(List<RTTWorldgenStar> starSystemStars, int orbitNum)
    {
        var planets = new List<RTTWorldgenPlanet>();

        int roll = Roll.D6(1) - 1;

        if ((starSystemStars[0].SpectralType == SpectralType.M && starSystemStars[0].Luminosity == Luminosity.V)
            || starSystemStars[0].SpectralType == SpectralType.L) {
            roll--;
        } else if (starSystemStars.Exists(x => x.CompanionOrbit == CompanionOrbit.Moderate)) {
            roll = 0;
        }

        for (var i = 0; i < roll; i++) {
            planets.Add(_rttWorldgenPlanetGenerator.GeneratePlanet(PlanetOrbit.Outer, starSystemStars[0], orbitNum + i));
        }

        return planets;
    }

    public RTTWorldgenPlanet GenerateSettlement(RTTWorldgenPlanet planet)
    {
        if (planet.Population >= 1) {
            return planet;
        }
        
        planet = _rttWorldgenPlanetGenerator.GenerateSettlement(planet);

        _tradeCodesService.GetTradeCodes(planet);
        
        return planet;
    }
}