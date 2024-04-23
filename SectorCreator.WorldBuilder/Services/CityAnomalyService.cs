using SectorCreator.WorldBuilder.Planet.Planet;

namespace SectorCreator.WorldBuilder.Services;

public static class CityAnomalyService
{
    public static List<Anomaly> Anomalies => new() {
        new Anomaly {
            CityType = "Arcology, sealed city",
            MinimumTechLevel = 8,
            CityCode = "Ar"
        },
        new Anomaly {
            CityType = "Flying, buoyant gas",
            MinimumTechLevel = 8,
            CityCode = "Fb"
        },
        new Anomaly {
            CityType = "Flying, grav hover",
            MinimumTechLevel = 10,
            CityCode = "Fg"
        },
        new Anomaly {
            CityType = "Flying, grav mobile",
            MinimumTechLevel = 14,
            CityCode = "Fm"
        },
        new Anomaly {
            CityType = "Mobile, rails",
            MinimumTechLevel = 6,
            CityCode = "Mr"
        },
        new Anomaly {
            CityType = "Mobile, tracked",
            MinimumTechLevel = 9,
            CityCode = "Mt"
        },
        new Anomaly {
            CityType = "Space, spin",
            MinimumTechLevel = 8,
            CityCode = "Ss"
        },
        new Anomaly {
            CityType = "Space, grav",
            MinimumTechLevel = 10,
            CityCode = "Sg"
        },
        new Anomaly {
            CityType = "Underground, benign environment",
            MinimumTechLevel = 6,
            CityCode = "Ub"
        },
        new Anomaly {
            CityType = "Underground, hostile environment",
            MinimumTechLevel = 8,
            CityCode = "Uh"
        },
        new Anomaly {
            CityType = "Water, shore floating adjacent",
            MinimumTechLevel = 0,
            CityCode = "Wa"
        },
        new Anomaly {
            CityType = "Water, static floating deep water",
            MinimumTechLevel = 6,
            CityCode = "Wd"
        },
        new Anomaly {
            CityType = "Water, free floating",
            MinimumTechLevel = 8,
            CityCode = "Wf"
        },
        new Anomaly {
            CityType = "Water, submerged",
            MinimumTechLevel = 9,
            CityCode = "Ws"
        },
        new Anomaly {
            CityType = "Water, deep ocean",
            MinimumTechLevel = 12,
            CityCode = "Wx"
        }
    };
}