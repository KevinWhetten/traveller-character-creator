using TravellerCreatorModels.SectorCreator.Enums;

namespace TravellerCreatorModels.SectorCreator.Interfaces;

public interface IStar
{
    SpectralType SpectralType { get; set; }
    int SpectralSubclass { get; set; }
}