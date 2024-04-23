namespace SectorCreator.Global;

public interface IRollingService
{
    int D3(int i);
    int D6(int i);
    int D10(int i);
    int D(int size, int i);
    int Flux();
    double Percent();
    int D66();
    double Variance(double value, int percent);
    double Between(double low, double high);
    double ValueWithVariance(double value, int percent);
}

public class RollingService : IRollingService
{
    private readonly Random Random = new();

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

    public int D(int size, int i)
    {
        var sum = 0;

        for (var x = 0; x < i; x++) {
            sum += Random.Next(1, size + 1);
        }

        return sum;
    }

    public int Flux()
    {
        return D6(1) - D6(1);
    }

    public double Percent()
    {
        return Random.NextDouble() * 100;
    }

    public int D66()
    {
        return D6(1) * 10 + D6(1);
    }

    public double Variance(double value, int percent)
    {
        return value * (Random.NextDouble() * percent / 100.0);
    }

    public double Between(double low, double high)
    {
        return Random.NextDouble() * (high - low) + low;
    }

    public double ValueWithVariance(double value, int percent)
    {
        return value + Variance(value, percent);
    }
}