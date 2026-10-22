namespace ArrayLib;

/// <summary>Variant 2, task 4: two-dimensional array.</summary>
public static class MatrixTasks
{
    public const double RangeMin = -15.62;
    public const double RangeMax = 53.36;
    public const int Digits = 2;

    public static double[,] Generate(int rows, int columns, Random rnd)
    {
        var matrix = new double[rows, columns];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
                matrix[i, j] = RandomValues.Next(rnd, RangeMin, RangeMax, Digits);
        return matrix;
    }

    /// <summary>Action 1: indices of rows that do not contain any positive element.</summary>
    public static List<int> RowsWithoutPositive(double[,] matrix)
    {
        var result = new List<int>();
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            bool hasPositive = false;
            for (int j = 0; j < matrix.GetLength(1) && !hasPositive; j++)
                hasPositive = matrix[i, j] > 0;
            if (!hasPositive)
                result.Add(i);
        }
        return result;
    }

    /// <summary>Sum of absolute values of the positive elements of every column.</summary>
    public static double[] PositiveAbsSums(double[,] matrix)
    {
        var sums = new double[matrix.GetLength(1)];
        for (int j = 0; j < sums.Length; j++)
            for (int i = 0; i < matrix.GetLength(0); i++)
                if (matrix[i, j] > 0)
                    sums[j] += Math.Abs(matrix[i, j]);
        return sums;
    }

    /// <summary>Action 2: sorts columns by positive abs sums (desc); returns original index per new position.</summary>
    public static int[] SortColumnsByPositiveAbsSumDescending(double[,] matrix)
    {
        double[] sums = PositiveAbsSums(matrix);
        int[] order = Enumerable.Range(0, sums.Length)
                                .OrderByDescending(j => sums[j])
                                .ToArray();

        var source = (double[,])matrix.Clone();
        for (int newJ = 0; newJ < order.Length; newJ++)
            for (int i = 0; i < matrix.GetLength(0); i++)
                matrix[i, newJ] = source[i, order[newJ]];
        return order;
    }
}
