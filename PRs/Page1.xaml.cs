using System;
using System.Windows;
using System.Windows.Controls;

namespace PR4
{
    public partial class Page1 : Page
    {
        public Page1() => InitializeComponent();

        private void Calc_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtX.Text) || string.IsNullOrEmpty(txtY.Text) || string.IsNullOrEmpty(txtZ.Text))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (double.TryParse(txtX.Text, out double x) && double.TryParse(txtY.Text, out double y) && double.TryParse(txtZ.Text, out double z))
            {
                if (x < -1 || x > 1)
                {
                    MessageBox.Show("x для arccos должен быть в диапазоне [-1, 1]", "Ошибка ОДЗ");
                    return;
                }

                double znam = Math.Abs(x - y) * z + Math.Pow(x, 2);
                if (znam == 0)
                {
                    MessageBox.Show("Деление на ноль!", "Ошибка");
                    return;
                }

                double chisl = x + 3 * Math.Abs(x - y) + Math.Pow(x, 2);
                double res = 5 * Math.Atan(x) - 0.25 * Math.Acos(x) * (chisl / znam);

                txtResult.Text = Math.Round(res, 6).ToString();
            }
            else MessageBox.Show("Введите числовые значения!");
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            txtX.Clear(); txtY.Clear(); txtZ.Clear(); txtResult.Clear();
        }
    }
}