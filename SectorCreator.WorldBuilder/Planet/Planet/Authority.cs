namespace SectorCreator.WorldBuilder.Planet.Planet;

public class Authority
{
    public string Name { get; set; }
    public char Code { get; set; }

    public static List<Authority> Authorities => new() {
        new Authority {
            Name = "Legislative",
            Code = 'L'
        },
        new Authority {
            Name = "Executive",
            Code = 'E'
        },
        new Authority {
            Name = "Judicial",
            Code = 'J'
        },
        new Authority {
            Name = "Balanced",
            Code = 'B'
        }
    };
}