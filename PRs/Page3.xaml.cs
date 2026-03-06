using System;
using System.Globalization; // Обязательно добавь это в начало файла!
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;

namespace PRs
{
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
            // Инициализация (проверь, что MyChart совпадает с x:Name в XAML)
            if (MyChart.ChartAreas.Count == 0)
            {
                MyChart.ChartAreas.Add(new ChartArea("MainArea"));
                var series = new Series("y = f(x)") { ChartType = SeriesChartType.Line, BorderWidth = 3 };
                MyChart.Series.Add(series);
            }
        }

        private void Calc_Click(object sender, RoutedEventArgs e)
        {
            // Сообщение для проверки: если оно не вылетает, кнопка ВООБЩЕ не подключена к коду
            // MessageBox.Show("Метод Calc_Click запущен!"); 

            // Используем CultureInfo.InvariantCulture, чтобы точка "." работала как разделитель
            bool isX0 = double.TryParse(txtX0.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double x0);
            bool isXk = double.TryParse(txtXk.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double xk);
            bool isDx = double.TryParse(txtDx.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double dx);
            bool isB = double.TryParse(txtB.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double b);

            if (isX0 && isXk && isDx && isB)
            {
                if (dx <= 0)
                {
                    MessageBox.Show("Шаг (dx) должен быть больше нуля!");
                    return;
                }

                txtResults.Clear();
                MyChart.Series[0].Points.Clear();

                int pointsCount = 0;
                // Идем циклом от x0 до xk
                for (double x = x0; x <= xk; x = Math.Round(x + dx, 2))
                {
                    // Формула 7 варианта
                    double val = Math.Pow(x, 3) + Math.Pow(b, 3);
                    // Корень кубический (работает и для отрицательных чисел)
                    double cbrt = Math.Sign(val) * Math.Pow(Math.Abs(val), 1.0 / 3.0);
                    double y = 9 * (x + 15 * cbrt);

                    // Добавляем в текстовое поле
                    txtResults.AppendText($"x: {x:F2} | y: {y:F4}\r\n");
                    // Добавляем на график
                    MyChart.Series[0].Points.AddXY(x, y);
                    pointsCount++;
                }

                if (pointsCount == 0)
                {
                    MessageBox.Show("Цикл не выполнился. Проверьте: x0 должно быть меньше xk!");
                }
            }
            else
            {
                MessageBox.Show("Ошибка ввода! Убедитесь, что все поля заполнены числами.");
            }
        }
    }
}
