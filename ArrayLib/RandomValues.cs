namespace ArrayLib;

public static class RandomValues
{
    // Integer grid instead of NextDouble(), so both ends of [min; max] are reachable.
    public static double Next(Random rnd, double min, double max, int digits)
    {
        double scale = Math.Pow(10, digits);
        int low = (int)Math.Round(min * scale);
        int high = (int)Math.Round(max * scale);
        return rnd.Next(low, high + 1) / scale;
    }
}
