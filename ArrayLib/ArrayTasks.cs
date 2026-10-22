using System.Numerics;

namespace ArrayLib;

/// <summary>Variant 2, task 3: one-dimensional array.</summary>
public static class ArrayTasks
{
    public const double RangeMin = -2.9;
    public const double RangeMax = 60.3;
    public const int Digits = 1;

    public static double[] Generate(int n, Random rnd)
    {
        var array = new double[n];
        for (int i = 0; i < n; i++)
            array[i] = RandomValues.Next(rnd, RangeMin, RangeMax, Digits);
        return array;
    }

    /// <summary>Indices of the first minimum and the first maximum element.</summary>
    public static (int MinIndex, int MaxIndex) FindMinMax(double[] array)
    {
        int minIndex = 0, maxIndex = 0;
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] < array[minIndex]) minIndex = i;
            if (array[i] > array[maxIndex]) maxIndex = i;
        }
        return (minIndex, maxIndex);
    }

    /// <summary>Index range of the elements strictly between min and max; From > To when it is empty.</summary>
    public static (int From, int To) RangeBetweenMinMax(double[] array)
    {
        var (minIndex, maxIndex) = FindMinMax(array);
        return (Math.Min(minIndex, maxIndex) + 1, Math.Max(minIndex, maxIndex) - 1);
    }

    /// <summary>Action 1: product of indices of the elements between min and max, or null if there are none.</summary>
    public static BigInteger? ProductOfIndicesBetweenMinMax(double[] array)
    {
        var (from, to) = RangeBetweenMinMax(array);
        if (from > to)
            return null;

        BigInteger product = BigInteger.One;
        for (int i = from; i <= to; i++)
            product *= i;
        return product;
    }

    /// <summary>Action 2: sorts the elements between min and max in descending order (in place).</summary>
    public static void SortBetweenMinMaxDescending(double[] array)
    {
        var (from, to) = RangeBetweenMinMax(array);
        if (from > to)
            return;

        int length = to - from + 1;
        Array.Sort(array, from, length);
        Array.Reverse(array, from, length);
    }
}
