namespace TravellerCreatorGlobalMethods;

public static class Roll
{
    private static Random _random => new();
    public static int D6(int i)
    {
        var sum = 0;
        
        for (var x = 0; x < i; x++) {
            sum += _random.Next(1, 7);
        }

        return sum;
    }
}