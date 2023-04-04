using TravellerCreatorModels.Enums;

namespace TravellerCreatorModels.Interfaces;

public interface IStar
{
    SpectralType SpectralType { get; set; }
    int SpectralSubclass { get; set; }
}