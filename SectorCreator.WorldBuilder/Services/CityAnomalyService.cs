using SectorCreator.WorldBuilder.Planet.Planet;

namespace SectorCreator.WorldBuilder.Services;

public static class CityAnomalyService
{
    public static List<CityAnomaly> Anomalies => new() {
        new CityAnomaly {
            CityType = "Arcology, sealed city",
            MinimumTechLevel = 8,
            CityCode = "Ar"
        },
        new CityAnomaly {
            CityType = "Flying, buoyant gas",
            MinimumTechLevel = 8,
            CityCode = "Fb"
        },
        new CityAnomaly {
            CityType = "Flying, grav hover",
            MinimumTechLevel = 10,
            CityCode = "Fg"
        },
        new CityAnomaly {
            CityType = "Flying, grav mobile",
            MinimumTechLevel = 14,
            CityCode = "Fm"
        },
        new CityAnomaly {
            CityType = "Mobile, rails",
            MinimumTechLevel = 6,
            CityCode = "Mr"
        },
        new CityAnomaly {
            CityType = "Mobile, tracked",
            MinimumTechLevel = 9,
            CityCode = "Mt"
        },
        new CityAnomaly {
            CityType = "Space, spin",
            MinimumTechLevel = 8,
            CityCode = "Ss"
        },
        new CityAnomaly {
            CityType = "Space, grav",
            MinimumTechLevel = 10,
            CityCode = "Sg"
        },
        new CityAnomaly {
            CityType = "Underground, benign environment",
            MinimumTechLevel = 6,
            CityCode = "Ub"
        },
        new CityAnomaly {
            CityType = "Underground, hostile environment",
            MinimumTechLevel = 8,
            CityCode = "Uh"
        },
        new CityAnomaly {
            CityType = "Water, shore floating adjacent",
            MinimumTechLevel = 0,
            CityCode = "Wa"
        },
        new CityAnomaly {
            CityType = "Water, static floating deep water",
            MinimumTechLevel = 6,
            CityCode = "Wd"
        },
        new CityAnomaly {
            CityType = "Water, free floating",
            MinimumTechLevel = 8,
            CityCode = "Wf"
        },
        new CityAnomaly {
            CityType = "Water, submerged",
            MinimumTechLevel = 9,
            CityCode = "Ws"
        },
        new CityAnomaly {
            CityType = "Water, deep ocean",
            MinimumTechLevel = 12,
            CityCode = "Wx"
        }
    };
}