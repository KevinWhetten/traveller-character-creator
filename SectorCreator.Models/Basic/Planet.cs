﻿using SectorCreator.Global.Enums;

namespace SectorCreator.Models.Basic;

public class Planet
{
  public string Name { get; set; } = "";
  public int Size { get; set; }
  public int Atmosphere { get; set; }
  public int Temperature { get; set; }
  public int Hydrographics { get; set; }
  public int Population { get; set; }
  public int Government { get; set; }
  public int LawLevel { get; set; }
  public char Starport { get; set; }
  public int TechLevel { get; set; }
  public List<TradeCode> TradeCodes { get; set; } = new();
  public List<Base> Bases { get; set; } = new();
  public TravelCode TravelCode { get; set; }
  public PlanetType PlanetType { get; set; }
  public List<Planet> Satellites { get; set; } = new();

  public Planet()
  {
  }
  
  public Planet(Planet planet)
  {
    Name = planet.Name;
    Size = planet.Size;
    Atmosphere = planet.Atmosphere;
    Temperature = planet.Temperature;
    Hydrographics = planet.Hydrographics;
    Population = planet.Population;
    Government = planet.Government;
    LawLevel = planet.LawLevel;
    Starport = planet.Starport;
    TechLevel = planet.TechLevel;
    TradeCodes = planet.TradeCodes;
    Bases = planet.Bases;
    TravelCode = planet.TravelCode;
    PlanetType = planet.PlanetType;
    Satellites = planet.Satellites;
  }
}
