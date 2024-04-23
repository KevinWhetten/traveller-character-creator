using SectorCreator.Global;

namespace SectorCreator.Models.Basic;

public static class Races
{
    private static readonly Race Human = new() {
        Name = "Human",
        HomeworldCoordinates = new Coordinates(18, 15),
        HomeSystemFilename = "../../../../Data/StarSystems/TerraSystem.tsv",
        Allegiance = "Humn",
        ExpansionRate = 8,
        Resilience = 1.25,
        MaxPopulation = 10
    };

    private static readonly Race Aslan = new() {
        Name = "Aslan",
        HomeworldCoordinates = new Coordinates(19, 6),
        HomeSystemFilename = "../../../../Data/StarSystems/KusyuSystem.tsv",
        Allegiance = "Asln",
        ExpansionRate = 8,
        Resilience = 1.25,
        MaxPopulation = 10
    };

    private static readonly Race Kekuu = new() {
        Name = "Kekuu",
        HomeworldCoordinates = new Coordinates(7, 32),
        HomeSystemFilename = "../../../../Data/StarSystems/KekuukiSystem.tsv",
        Allegiance = "Keku",
        ExpansionRate = 8,
        Resilience = .8,
        MaxPopulation = 9
    };

    private static readonly Race Ssitolusss = new() {
        Name = "Ssitolusss",
        HomeworldCoordinates = new Coordinates(13, 26),
        HomeSystemFilename = "../../../../Data/StarSystems/SsitolsssSystem.tsv",
        Allegiance = "Stls",
        ExpansionRate = 8,
        Resilience = .7,
        MaxPopulation = 9
    };

    private static readonly Race Tortosians = new() {
        Name = "Tortosians",
        HomeworldCoordinates = new Coordinates(19, 33),
        HomeSystemFilename = "../../../../Data/StarSystems/TortoriaSystem.tsv",
        Allegiance = "Tort",
        ExpansionRate = 7,
        Resilience = .85,
        MaxPopulation = 10
    };

    private static readonly Race Blubbus = new() {
        Name = "Blubbus",
        HomeworldCoordinates = new Coordinates(29, 29),
        HomeSystemFilename = "../../../../Data/StarSystems/BlubbusSystem.tsv",
        Allegiance = "Blbs",
        ExpansionRate = 10,
        Resilience = .65,
        MaxPopulation = 10
    };

    private static readonly Race Chrotos = new() {
        Name = "Chrotos",
        HomeworldCoordinates = new Coordinates(26, 14),
        HomeSystemFilename = "../../../../Data/StarSystems/ArianuilSystem.tsv",
        Allegiance = "Crts",
        ExpansionRate = 6,
        Resilience = .65,
        MaxPopulation = 8
    };

    private static readonly Race KaSara = new() {
        Name = "Ka'Sara",
        HomeworldCoordinates = new Coordinates(1, 10),
        HomeSystemFilename = "../../../../Data/StarSystems/KaTollisSystem.tsv",
        Allegiance = "KaSa",
        ExpansionRate = 16,
        Resilience = 3.5,
        MaxPopulation = 12
    };

    private static Race Vargr = new() {
        Name = "Vargr",
        HomeworldCoordinates = new Coordinates(26, 8),
        HomeSystemFilename = "../../../../Data/StarSystems/LairSystem.tsv",
        Allegiance = "Vrgr",
        ExpansionRate = 8,
        Resilience = 1.5,
        MaxPopulation = 10
    };

    public static readonly List<Race> races = new() {
        Human,
        Aslan,
        Kekuu,
        Ssitolusss,
        Tortosians,
        Blubbus,
        Chrotos,
        KaSara,
        Vargr
    };
}