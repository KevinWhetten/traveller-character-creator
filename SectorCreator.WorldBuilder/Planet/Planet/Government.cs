using SectorCreator.WorldBuilder.Services;

namespace SectorCreator.WorldBuilder.Planet.Planet;

public class Government
{
    public string Profile => $"{Code}-{Centralization.Code}{GetAuthorityProfiles()}";
    public string FactionProfile => $"{RomanNumeralService.values[Id + 1]}-{Code}-{FactionStrength.Code}";
    public int Code { get; set; }
    public List<int> Relationships { get; set; } = new();
    public Centralization Centralization { get; set; }
    public List<GovernmentAuthority> Authorities { get; set; } = new();
    public FactionStrength FactionStrength { get; set; }
    public int LawLevel { get; set; }
    public JusticeSystem JusticeSystem { get; set; }
    public Uniformity UniformityOfLaw { get; set; }
    public bool PresumptionOfInnocence { get; set; }
    public bool DeathPenalty { get; set; }
    public int EconomicLawLevel { get; set; }
    public int CriminalLawLevel { get; set; }
    public int PrivateLawLevel { get; set; }
    public int PersonalRightsLevel { get; set; }
    public int TechLevel { get; set; }
    public int LowCommonTechLevel { get; set; }
    public int Id { get; set; }
    public int Enforcement { get; set; }
    public int Militia { get; set; }
    public int Army { get; set; }
    public int WetNavy { get; set; }
    public int AirForce { get; set; }
    public int SystemDefense { get; set; }
    public int Navy { get; set; }
    public int Marine { get; set; }
    private string GetAuthorityProfiles()
    {
        List<string> profiles = new();
        foreach (var authority in Authorities) {
            profiles.Add(authority.Profile);
        }

        return string.Join("-", profiles);
    }
}