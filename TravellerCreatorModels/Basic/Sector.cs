namespace TravellerCreatorModels.Basic;

public class Sector
{
    public List<Subsector> Subsectors { get; set; }

    public Sector()
    {
        Subsectors = new List<Subsector>();
    }
}