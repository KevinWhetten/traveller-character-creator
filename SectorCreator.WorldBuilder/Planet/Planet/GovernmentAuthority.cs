using SectorCreator.Global;

namespace SectorCreator.WorldBuilder.Planet.Planet;

public class GovernmentAuthority
{
    private readonly IRollingService _rollingService = new RollingService();
    public bool IsMainAuthority { get; set; } = false;
    public Authority Authority { get; set; }
    public Structure FunctionalStructure { get; set; }
    public string Profile => $"{Authority.Code}{FunctionalStructure.Code}";

    public void GenerateStructure(Government government, bool mainFunctionIsRuler)
    {
        if (mainFunctionIsRuler) {
            FunctionalStructure = Structure.Structures[3];
            return;
        }

        FunctionalStructure = government.Code switch {
            2 => Structure.Structures[0],
            8 or 9 => Structure.Structures[2],
            3 or 12 or 15 => _rollingService.D6(1) switch {
                <= 4 => Structure.Structures[1],
                >= 5 => Structure.Structures[2]
            },
            10 or 11 or 13 or 14 => IsMainAuthority switch {
                true => _rollingService.D6(1) switch {
                    <= 5 => Structure.Structures[3],
                    >= 6 => Structure.Structures[1]
                },
                false => GetFunctionalStructure(2)
            },
            _ => GetFunctionalStructure(0)
        };
    }

    private Structure GetFunctionalStructure(int dm)
    {
        return (_rollingService.D6(2) + dm) switch {
            <= 3 => Structure.Structures[0],
            7 or 8 => Structure.Structures[3],
            5 or 6 or 9 or 11 => Structure.Structures[2],
            4 or 10 or >= 12 => Structure.Structures[1]
        };
    }
}