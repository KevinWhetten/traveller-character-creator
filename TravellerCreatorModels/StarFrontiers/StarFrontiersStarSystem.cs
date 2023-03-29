﻿using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;

namespace TravellerCreatorModels.StarFrontiers;

public class StarFrontiersStarSystem : IStarSystem
{
    public List<IStar> Stars { get; set; } = new();
    public StarSystemType Type { get; set; }
    public bool GasGiant { get; set; }
    public IPlanet? Planet { get; set; }
    public List<IPlanet> Planets { get; set; } = new();
    
    public void GetGasGiant()
    {
        throw new NotImplementedException();
    }
}