namespace TravellerCreatorModels.Basic;

public class Sector
{
    public List<SubSector> SubSectors { get; set; }

    public Sector()
    {
        SubSectors = new List<SubSector>();
    }
}