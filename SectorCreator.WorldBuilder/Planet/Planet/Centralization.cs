namespace SectorCreator.WorldBuilder.Planet.Planet;

public class Centralization
{
    public string Name { get; set; }
    public char Code { get; set; }
    public string Description { get; set; }

    public static List<Centralization> Centralizations => new() {
        new Centralization {
            Name = "Confederal",
            Code = 'C',
            Description = "Sub-states considered sovereign, more powerful than central government."
        },
        new Centralization {
            Name = "Federal",
            Code = 'F',
            Description = "Powers shared between sub-states and central government."
        },
        new Centralization {
            Name = "Unitary",
            Code = 'U',
            Description = "Central government dominant"
        }
    };
}