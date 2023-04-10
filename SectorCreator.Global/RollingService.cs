namespace SectorCreator.Global;

public interface IRollingService
{
    int D3(int i);
    int D6(int i);
    int D10(int i);
    int D(int max, int i);
}

public class RollingService : IRollingService
{
    private Random Random => new();

    public int D3(int i)
    {
        var sum = 0;

        for (var x = 0; x < i; x++) {
            sum += Random.Next(1, 4);
        }

        return sum;
    }

    public int D6(int i)
    {
        var sum = 0;

        for (var x = 0; x < i; x++) {
            sum += Random.Next(1, 7);
        }

        return sum;
    }

    public int D10(int i)
    {
        var sum = 0;

        for (var x = 0; x < i; x++) {
            sum += Random.Next(1, 11);
        }

        return sum;
    }

    public int D(int max, int i)
    {
        var sum = 0;

        for (var x = 0; x < i; x++) {
            sum += Random.Next(1, max + 1);
        }

        return sum;
    }
}