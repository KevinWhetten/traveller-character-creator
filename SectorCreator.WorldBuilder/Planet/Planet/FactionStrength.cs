namespace SectorCreator.WorldBuilder.Planet.Planet;

public class FactionStrength
{
    public string Name { get; set; }
    public char Code { get; set; }
    public string Description { get; set; }
    public int Power { get; set; }

    public static List<FactionStrength> FactionStrengths => new() {
        new FactionStrength {
            Name = "Obscure group",
            Code = 'O',
            Description = "Few have heard of them",
            Power = 0
        },
        new FactionStrength {
            Name = "Fringe group",
            Code = 'F',
            Description = "",
            Power = 1
        },
        new FactionStrength {
            Name = "Minor group",
            Code = 'M',
            Description = "",
            Power = 2
        },
        new FactionStrength {
            Name = "Notable group",
            Code = 'N',
            Description = "",
            Power = 3
        },
        new FactionStrength {
            Name = "Significant group",
            Code = 'S',
            Description = "Nearly as powerful as the government",
            Power = 4
        },
        new FactionStrength {
            Name = "Overwhelming popular support",
            Code = 'P',
            Description = "More powerful than government",
            Power = 5
        },
        new FactionStrength {
            Name = "Official Government",
            Code = 'G',
            Description = "",
            Power = 6
        }
    };
}