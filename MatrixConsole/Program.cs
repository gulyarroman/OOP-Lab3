using System.Globalization;
using System.Text;
using ArrayLib;

Console.OutputEncoding = Encoding.UTF8;
Console.Title = "Двовимірний масив (варіант 2)";
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

Console.WriteLine("Робота з двовимірним масивом (варіант 2)");
Console.WriteLine($"Елементи - випадкові числа з діапазону [{MatrixTasks.RangeMin}; {MatrixTasks.RangeMax}], 2 дробових знаки.");
Console.WriteLine();

int rows = ReadCount("Введіть кількість рядків n = ", 20);
int columns = ReadCount("Введіть кількість стовпців m = ", 10);
double[,] matrix = MatrixTasks.Generate(rows, columns, new Random());
List<int> rowsWithoutPositive = MatrixTasks.RowsWithoutPositive(matrix);

Console.WriteLine();
Console.WriteLine("Згенерована матриця:");
PrintMatrix(matrix, Enumerable.Range(0, columns).ToArray(), rowsWithoutPositive);

Console.WriteLine();
Console.Write("1) Кількість рядків, які не містять жодного додатного елемента: ");
Console.ForegroundColor = ConsoleColor.Cyan;
Console.Write(rowsWithoutPositive.Count);
Console.ResetColor();
Console.WriteLine(rowsWithoutPositive.Count > 0 ? $" (номери рядків: {string.Join(", ", rowsWithoutPositive)})" : "");

Console.WriteLine();
Console.WriteLine("2) Матриця після перестановки стовпців за спаданням сум модулів додатних елементів");
Console.WriteLine("   (у заголовку - номер стовпця у початковій матриці):");
int[] origins = MatrixTasks.SortColumnsByPositiveAbsSumDescending(matrix);
PrintMatrix(matrix, origins, rowsWithoutPositive);

Console.WriteLine();
Console.Write("Натисніть будь-яку клавішу для завершення...");
Console.ReadKey();

static int ReadCount(string prompt, int max)
{
    while (true)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out int value) && value >= 1 && value <= max)
            return value;

        Console.WriteLine($"  Помилка введення! Потрібне ціле число від 1 до {max}.");
    }
}

static void PrintMatrix(double[,] matrix, int[] columnOrigins, List<int> highlightRows)
{
    const int width = 9;
    int rows = matrix.GetLength(0);
    int columns = matrix.GetLength(1);

    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.Write(new string(' ', 7));
    foreach (int origin in columnOrigins)
        Console.Write($"[{origin}]".PadLeft(width));
    Console.WriteLine();

    for (int i = 0; i < rows; i++)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write($"[{i}]".PadLeft(7));
        Console.ForegroundColor = highlightRows.Contains(i) ? ConsoleColor.Cyan : ConsoleColor.Gray;
        for (int j = 0; j < columns; j++)
            Console.Write(matrix[i, j].ToString("F2").PadLeft(width));
        Console.WriteLine();
    }

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("сума+".PadLeft(7));
    foreach (double sum in MatrixTasks.PositiveAbsSums(matrix))
        Console.Write(sum.ToString("F2").PadLeft(width));
    Console.ResetColor();
    Console.WriteLine("   <- сума модулів додатних елементів стовпця");
}
