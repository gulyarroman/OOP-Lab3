using System.Globalization;
using System.Text;
using ArrayLib;

Console.OutputEncoding = Encoding.UTF8;
Console.Title = "Одновимірний масив (варіант 2)";
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

Console.WriteLine("Робота з одновимірним масивом (варіант 2)");
Console.WriteLine($"Елементи - випадкові числа з діапазону [{ArrayTasks.RangeMin}; {ArrayTasks.RangeMax}], 1 дробовий знак.");
Console.WriteLine("Індекси елементів нумеруються з 0.");
Console.WriteLine();

int n = ReadCount("Введіть кількість елементів масиву n = ");
double[] array = ArrayTasks.Generate(n, new Random());
var (minIndex, maxIndex) = ArrayTasks.FindMinMax(array);

Console.WriteLine();
Console.WriteLine("Згенерований масив:");
PrintArray(array, minIndex, maxIndex);
Console.WriteLine($"Мінімальний елемент: a[{minIndex}] = {array[minIndex]:F1};  максимальний елемент: a[{maxIndex}] = {array[maxIndex]:F1}");
PrintLegend();

Console.WriteLine();
Console.WriteLine("1) Добуток індексів елементів, розташованих між мінімальним і максимальним:");
var (from, to) = ArrayTasks.RangeBetweenMinMax(array);
var product = ArrayTasks.ProductOfIndicesBetweenMinMax(array);
if (product is null)
    Console.WriteLine("   між мінімальним і максимальним елементами немає інших елементів.");
else
    Console.WriteLine($"   {DescribeProduct(from, to)} = {product}");

Console.WriteLine();
Console.WriteLine("2) Масив після впорядкування елементів між мінімальним і максимальним за спаданням:");
ArrayTasks.SortBetweenMinMaxDescending(array);
PrintArray(array, minIndex, maxIndex);

Console.WriteLine();
Console.Write("Натисніть будь-яку клавішу для завершення...");
Console.ReadKey();

static int ReadCount(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out int value) && value >= 1 && value <= 100)
            return value;

        Console.WriteLine("  Помилка введення! Потрібне ціле число від 1 до 100.");
    }
}

static string DescribeProduct(int from, int to)
{
    int count = to - from + 1;
    return count <= 12
        ? string.Join(" * ", Enumerable.Range(from, count))
        : $"{from} * {from + 1} * ... * {to}";
}

static void PrintArray(double[] array, int minIndex, int maxIndex)
{
    const int perRow = 10;
    int left = Math.Min(minIndex, maxIndex);
    int right = Math.Max(minIndex, maxIndex);

    for (int start = 0; start < array.Length; start += perRow)
    {
        int end = Math.Min(start + perRow, array.Length);

        Console.ForegroundColor = ConsoleColor.DarkGray;
        for (int i = start; i < end; i++)
            Console.Write($"[{i}]".PadLeft(8));
        Console.WriteLine();

        for (int i = start; i < end; i++)
        {
            Console.ForegroundColor = i == minIndex ? ConsoleColor.Cyan
                : i == maxIndex ? ConsoleColor.Red
                : i > left && i < right ? ConsoleColor.Yellow
                : ConsoleColor.Gray;
            Console.Write(array[i].ToString("F1").PadLeft(8));
        }
        Console.ResetColor();
        Console.WriteLine();
    }
}

static void PrintLegend()
{
    Console.Write("Позначення: ");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write("мінімальний");
    Console.ResetColor();
    Console.Write(", ");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write("максимальний");
    Console.ResetColor();
    Console.Write(", ");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("елементи між ними");
    Console.ResetColor();
    Console.WriteLine();
}
