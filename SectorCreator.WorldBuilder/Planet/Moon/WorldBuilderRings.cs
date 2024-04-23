using SectorCreator.WorldBuilder.Planet.Planet;

namespace SectorCreator.WorldBuilder.Planet.Moon;

public class WorldBuilderRings : WorldBuilderMoon
{
    public double RingSpan { get; set; }
    public double InnerLimit => OrbitNumber - RingSpan / 2.0;
    public double OuterLimit => OrbitNumber + RingSpan / 2.0;

    public WorldBuilderRings()
    {
        Size = 25;
    }

    public void Generate(WorldBuilderPlanet parent)
    {
        OrbitNumber = 0.4 + (_rollingService.D6(2) / 8.0);
        RingSpan = _rollingService.D6(3) / 100.0 + 0.07;

        if (parent.Moons.Count(x => x.Size == 25) > 2) {
            var outermostLimit = parent.Moons.Where(x => x.Size == 25).Max(x => ((WorldBuilderRings) x).OuterLimit);
            OrbitNumber = outermostLimit + RingSpan / 2.0;
        } else {
            foreach (var rings in from moon in parent.Moons
                     where moon.Size == 25
                     select (WorldBuilderRings) moon
                     into rings
                     where OrbitNumber > 0
                     where (rings.InnerLimit < InnerLimit && rings.OuterLimit > InnerLimit)
                     select rings) {
                OrbitNumber = rings.OuterLimit + RingSpan / 2.0;
            }
        }
    }
}