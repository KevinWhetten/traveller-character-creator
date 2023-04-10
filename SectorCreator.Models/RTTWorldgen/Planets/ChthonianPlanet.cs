namespace SectorCreator.Models.RTTWorldgen.Planets;

public interface IChthonianPlanet
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class ChthonianPlanet : IChthonianPlanet
{
    private readonly IPlanetValidation _planetValidation;

    public ChthonianPlanet(IPlanetValidation planetValidation)
    {
        _planetValidation = planetValidation;
    }
    
    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.Size = 16;
        planet.Atmosphere = 1;
        planet.Hydrographics = 0;
        planet.Biosphere = 0;
        planet = _planetValidation.ValidatePlanet(planet);
        return planet;
    }
}