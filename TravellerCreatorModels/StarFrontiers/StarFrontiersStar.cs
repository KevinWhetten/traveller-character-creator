using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;

namespace TravellerCreatorModels.StarFrontiers;

public class StarFrontiersStar : IStar
{
    public SpectralType SpectralType { get; set; }
    public int SpectralSubclass { get; set; }
    

    public StarFrontiersStar()
    {
        SpectralType = SpectralType.A;
        SpectralSubclass = 0;
    }
    public StarFrontiersStar(SpectralType spectralType, int spectralSubclass)
    {
        SpectralType = spectralType;
        SpectralSubclass = spectralSubclass;
    }
}