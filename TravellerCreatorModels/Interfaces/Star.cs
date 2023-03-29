using TravellerCreatorModels.Enums;

namespace TravellerCreatorModels.Interfaces;

public interface IStar
{
    Luminosity Luminosity { get; set; }
    int SpectralSubclass { get; set; }
}