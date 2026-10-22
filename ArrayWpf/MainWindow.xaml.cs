using System.Globalization;
using System.Windows;
using System.Windows.Media;
using ArrayLib;

namespace ArrayWpf;

public record Cell(string IndexText, string ValueText, Brush Fill);

public partial class MainWindow : Window
{
    private static readonly Brush MinBrush = CreateBrush(0xBB, 0xDE, 0xFB);
    private static readonly Brush MaxBrush = CreateBrush(0xFF, 0xCD, 0xD2);
    private static readonly Brush BetweenBrush = CreateBrush(0xFF, 0xF5, 0x9D);

    private readonly Random rnd = new();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void GenerateButton_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(TextBoxN.Text, out int n) || n < 1 || n > 100)
        {
            ResultsPanel.Visibility = Visibility.Collapsed;
            MessageBox.Show("Некоректне значення n! Введіть ціле число від 1 до 100.",
                "Помилка вводу", MessageBoxButton.OK, MessageBoxImage.Error);
            TextBoxN.Focus();
            TextBoxN.SelectAll();
            return;
        }

        double[] array = ArrayTasks.Generate(n, rnd);
        var (minIndex, maxIndex) = ArrayTasks.FindMinMax(array);
        SourceArray.ItemsSource = ToCells(array, minIndex, maxIndex);
        MinMaxText.Text = $"Мінімальний елемент: a[{minIndex}] = {Format(array[minIndex])};   " +
                          $"максимальний елемент: a[{maxIndex}] = {Format(array[maxIndex])}";

        var (from, to) = ArrayTasks.RangeBetweenMinMax(array);
        var product = ArrayTasks.ProductOfIndicesBetweenMinMax(array);
        ProductText.Text = product is null ? "між мінімальним і максимальним елементами немає інших елементів"
            : from == to ? $"між ними лише один елемент з індексом {from}, добуток = {product}"
            : $"індекси з {from} по {to}, добуток = {product}";

        ArrayTasks.SortBetweenMinMaxDescending(array);
        SortedArray.ItemsSource = ToCells(array, minIndex, maxIndex);

        ResultsPanel.Visibility = Visibility.Visible;
    }

    private static List<Cell> ToCells(double[] array, int minIndex, int maxIndex)
    {
        int left = Math.Min(minIndex, maxIndex);
        int right = Math.Max(minIndex, maxIndex);
        var cells = new List<Cell>(array.Length);
        for (int i = 0; i < array.Length; i++)
        {
            Brush fill = i == minIndex ? MinBrush
                : i == maxIndex ? MaxBrush
                : i > left && i < right ? BetweenBrush
                : Brushes.White;
            cells.Add(new Cell($"[{i}]", Format(array[i]), fill));
        }
        return cells;
    }

    private static string Format(double value) => value.ToString("F1", CultureInfo.InvariantCulture);

    private static Brush CreateBrush(byte r, byte g, byte b)
    {
        var brush = new SolidColorBrush(Color.FromRgb(r, g, b));
        brush.Freeze();
        return brush;
    }
}
