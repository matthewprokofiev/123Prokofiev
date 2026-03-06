using System;
using System.Windows;
using System.Windows.Controls;

namespace PRs
{
    public partial class Page2 : Page
    {
        public Page2() => InitializeComponent();

        private void Calc_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtX.Text, out double x) && double.TryParse(txtB.Text, out double b))
            {
                double fx = rbSh.IsChecked == true ? Math.Sinh(x) : (rbSq.IsChecked == true ? x * x : Math.Exp(x));
                double xb = x * b;
                double s;

                if (xb > 1 && xb < 10) s = Math.Exp(fx);
                else if (xb > 12 && xb < 40) s = Math.Sqrt(Math.Abs(fx + 4 * b));
                else s = b * fx * fx;

                txtResult.Text = Math.Round(s, 5).ToString();
            }
            else MessageBox.Show("Проверьте ввод чисел!");
        }
    }
}