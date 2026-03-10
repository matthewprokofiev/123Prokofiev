using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;

namespace PR4
{
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
            if (MyChart.ChartAreas.Count == 0)
            {
                MyChart.ChartAreas.Add(new ChartArea("MainArea"));
                var series = new Series("y = f(x)") { ChartType = SeriesChartType.Line, BorderWidth = 3 };
                MyChart.Series.Add(series);
            }
        }

        private void Calc_Click(object sender, RoutedEventArgs e)
        {
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
                for (double x = x0; x <= xk; x = Math.Round(x + dx, 2))
                {
                    double val = Math.Pow(x, 3) + Math.Pow(b, 3);

                    double cbrt = Math.Sign(val) * Math.Pow(Math.Abs(val), 1.0 / 3.0);
                    double y = 9 * (x + 15 * cbrt);

                    txtResults.AppendText($"x: {x:F2} | y: {y:F4}\r\n");
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
