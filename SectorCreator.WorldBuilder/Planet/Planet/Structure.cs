namespace SectorCreator.WorldBuilder.Planet.Planet;

public class Structure
{
    public string Name { get; set; }
    public char Code { get; set; }
    public static List<Structure> Structures => new() {
        new Structure {
            Name = "Demos",
            Code = 'D'
        },
        new Structure {
            Name = "Single Council",
            Code = 'S'
        },
        new Structure {
            Name = "Multiple Councils",
            Code = 'M'
        },
        new Structure {
            Name = "Ruler",
            Code = 'R'
        }
    };
}