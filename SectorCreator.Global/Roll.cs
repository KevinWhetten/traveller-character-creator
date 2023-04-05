namespace SectorCreator.Global;

public static class Roll
{
    private static Random Random => new();

    public static int D3(int i)
    {
        var sum = 0;

        for (var x = 0; x < i; x++) {
            sum += Random.Next(1, 4);
        }

        return sum;
    }

    public static int D6(int i)
    {
        var sum = 0;

        for (var x = 0; x < i; x++) {
            sum += Random.Next(1, 7);
        }

        return sum;
    }

    public static int D10(int i)
    {
        var sum = 0;

        for (var x = 0; x < i; x++) {
            sum += Random.Next(1, 11);
        }

        return sum;
    }

    public static int D(int max, int i)
    {
        var sum = 0;

        for (var x = 0; x < i; x++) {
            sum += Random.Next(1, max + 1);
        }

        return sum;
    }
}