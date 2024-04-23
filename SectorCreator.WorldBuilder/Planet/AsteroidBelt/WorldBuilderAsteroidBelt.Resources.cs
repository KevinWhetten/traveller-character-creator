namespace SectorCreator.WorldBuilder.Planet.AsteroidBelt;

public partial class WorldBuilderAsteroidBelt
{
    public void GenerateResources()
    {
        ResourceRating = _rollingService.Flux() + GetResourceDMs();

        ResourceRating = Math.Max(ResourceRating, 2);
        ResourceRating = Math.Min(ResourceRating, 12);
    }

    private int GetResourceDMs()
    {
        var dm = 0;

        dm += Bulk;
        dm += (int) Math.Floor(Composition.m / 10m);
        dm -= (int) Math.Floor(Composition.c / 10m);

        return dm;
    }
}