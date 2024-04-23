using SectorCreator.Global;

namespace SectorCreator.WorldBuilder.Services;

public class CompositionService
{
    private static IRollingService _rollingService = new RollingService();
    
    public static List<Element> Elements = new() {
        new Element {
            Name = "Hydrogen Ion",
            Code = "H-",
            EscapeValue = 24.00,
            AtomicMass = 1,
            BoilingPoint = 20,
            MeltingPoint = 14,
            RelativeAbundance = 0
        },
        new Element {
            Name = "Hydrogen",
            Code = "H\u2082",
            EscapeValue = 12.00,
            AtomicMass = 2,
            BoilingPoint = 20,
            MeltingPoint = 14,
            RelativeAbundance = 1200
        },
        new Element {
            Name = "Helium",
            Code = "He",
            EscapeValue = 6.00,
            AtomicMass = 4,
            BoilingPoint = 4,
            MeltingPoint = 0,
            RelativeAbundance = 400
        },
        new Element {
            Name = "Methane",
            Code = "CH\u2084",
            EscapeValue = 1.50,
            AtomicMass = 16,
            BoilingPoint = 113,
            MeltingPoint = 91,
            RelativeAbundance = 70
        },
        new Element {
            Name = "Ammonia",
            Code = "NH\u2083",
            EscapeValue = 1.42,
            AtomicMass = 17,
            BoilingPoint = 240,
            MeltingPoint = 195,
            RelativeAbundance = 30
        },
        new Element {
            Name = "Water",
            Code = "H\u20820",
            EscapeValue = 1.33,
            AtomicMass = 18,
            BoilingPoint = 373,
            MeltingPoint = 273,
            RelativeAbundance = 100
        },
        new Element {
            Name = "Hydroflouric Acid",
            Code = "HF",
            EscapeValue = 1.20,
            AtomicMass = 20,
            BoilingPoint = 293,
            MeltingPoint = 190,
            RelativeAbundance = 2
        },
        new Element {
            Name = "Neon",
            Code = "Ne",
            EscapeValue = 1.20,
            AtomicMass = 20,
            BoilingPoint = 27,
            MeltingPoint = 25,
            RelativeAbundance = 50
        },
        new Element {
            Name = "Sodium",
            Code = "Na",
            EscapeValue = 1.04,
            AtomicMass = 23,
            BoilingPoint = 1156,
            MeltingPoint = 371,
            RelativeAbundance = 40
        },
        new Element {
            Name = "Nitrogen",
            Code = "N\u2082",
            EscapeValue = 0.86,
            AtomicMass = 28,
            BoilingPoint = 77,
            MeltingPoint = 63,
            RelativeAbundance = 60
        },
        new Element {
            Name = "Carbon Monoxide",
            Code = "CO",
            EscapeValue = 0.86,
            AtomicMass = 28,
            BoilingPoint = 82,
            MeltingPoint = 68,
            RelativeAbundance = 70
        },
        new Element {
            Name = "Hydrogen Cyanide",
            Code = "HCN",
            EscapeValue = 0.86,
            AtomicMass = 28,
            BoilingPoint = 299,
            MeltingPoint = 260,
            RelativeAbundance = 30
        },
        new Element {
            Name = "Ethane",
            Code = "C\u2082H\u2086",
            EscapeValue = 0.80,
            AtomicMass = 30,
            BoilingPoint = 184,
            MeltingPoint = 90,
            RelativeAbundance = 70
        },
        new Element {
            Name = "Oxygen",
            Code = "O\u2082",
            EscapeValue = 0.75,
            AtomicMass = 32,
            BoilingPoint = 90,
            MeltingPoint = 54,
            RelativeAbundance = 50
        },
        new Element {
            Name = "Hydrochloric Acid",
            Code = "HCI",
            EscapeValue = 0.67,
            AtomicMass = 36,
            BoilingPoint = 321,
            MeltingPoint = 247,
            RelativeAbundance = 1
        },
        new Element {
            Name = "Flourine",
            Code = "F\u2082",
            EscapeValue = 0.63,
            AtomicMass = 38,
            BoilingPoint = 85,
            MeltingPoint = 53,
            RelativeAbundance = 2
        },
        new Element {
            Name = "Argon",
            Code = "Ar",
            EscapeValue = 0.60,
            AtomicMass = 40,
            BoilingPoint = 87,
            MeltingPoint = 83,
            RelativeAbundance = 20
        },
        new Element {
            Name = "Carbon Dioxide",
            Code = "CO\u2082",
            EscapeValue = 0.55,
            AtomicMass = 44,
            BoilingPoint = 216,
            MeltingPoint = 194,
            RelativeAbundance = 70
        },
        new Element {
            Name = "Formamide",
            Code = "CH\u2083NO",
            EscapeValue = 0.53,
            AtomicMass = 45,
            BoilingPoint = 483,
            MeltingPoint = 275,
            RelativeAbundance = 15
        },
        new Element {
            Name = "Formic Acid",
            Code = "CH\u2082O\u2082",
            EscapeValue = 0.52,
            AtomicMass = 46,
            BoilingPoint = 374,
            MeltingPoint = 281,
            RelativeAbundance = 15
        },
        new Element {
            Name = "Sulphur Dioxide",
            Code = "SO\u2082",
            EscapeValue = 0.38,
            AtomicMass = 64,
            BoilingPoint = 263,
            MeltingPoint = 201,
            RelativeAbundance = 20
        },
        new Element {
            Name = "Chlorine",
            Code = "Cl\u2082",
            EscapeValue = 0.34,
            AtomicMass = 70,
            BoilingPoint = 239,
            MeltingPoint = 171,
            RelativeAbundance = 1
        },
        new Element {
            Name = "Krypton",
            Code = "Kr",
            EscapeValue = 0.29,
            AtomicMass = 84,
            BoilingPoint = 120,
            MeltingPoint = 115,
            RelativeAbundance = 2
        },
        new Element {
            Name = "Sulphuric Acid",
            Code = "H\u2082SO\u2084",
            EscapeValue = 0.24,
            AtomicMass = 98,
            BoilingPoint = 718,
            MeltingPoint = 388,
            RelativeAbundance = 20
        }
    };

    public static Element GetRandomElement()
    {
        return Elements.ToArray()[_rollingService.D(Elements.Count - 1, 1)];
    }
}

public class Element
{
    public string Name { get; set; }
    public string Code { get; set; }
    public double EscapeValue { get; set; }
    public int AtomicMass { get; set; }
    public int BoilingPoint { get; set; }
    public int MeltingPoint { get; set; }
    public int RelativeAbundance { get; set; }
}