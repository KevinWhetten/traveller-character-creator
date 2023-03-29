using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;

namespace TravellerCreatorModels.StarFrontiers;

public class StarFrontiersStar : IStar
{
    public Luminosity Luminosity { get; set; }
    public int SpectralSubclass { get; set; }
    

    public StarFrontiersStar()
    {
        Luminosity = Luminosity.A;
        SpectralSubclass = 0;
    }
    public StarFrontiersStar(Luminosity luminosity, int spectralSubclass)
    {
        Luminosity = luminosity;
        SpectralSubclass = spectralSubclass;
    }
}