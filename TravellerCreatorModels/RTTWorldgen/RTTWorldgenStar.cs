﻿using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;

namespace TravellerCreatorModels.RTTWorldgen;

public class RTTWorldgenStar : IStar
{
    public Guid Id { get; } = Guid.NewGuid();
    public RTTWorldgenStarType RTTWorldgenStarType { get; set; }
    public SpectralType SpectralType { get; set; }
    public Luminosity Luminosity { get; set; }
    public int SpectralSubclass { get; set; }
    public StarOrbit StarOrbit { get; set; }
    public int ExpansionSize { get; set; }
    public int Age { get; set; }
}