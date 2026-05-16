using System;
using System.Windows;

namespace TipCalculator
{
    /// Code-Behind главного окна. Отвечает только за UI: считывает ввод, вызывает логику, отображает результат. 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// Обработчик нажатия кнопки "Рассчитать". Валидирует ввод, вызывает TipCalculatorLogic, показывает результат.
        private void buttonCalculate_Click(object sender, RoutedEventArgs e)
        {
            // Валидация суммы счёта 
            // Заменяем точку на запятую для поддержки обоих разделителей
            string billText = textBoxBill.Text.Trim().Replace('.', ',');
            if (!double.TryParse(billText, out double bill) || bill <= 0)
            {
                MessageBox.Show(
                    "Введите корректную сумму счёта (положительное число).",
                    "Ошибка ввода",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            // Валидация количества гостей
            if (!int.TryParse(textBoxGuests.Text.Trim(), out int guests) || guests < 1)
            {
                MessageBox.Show(
                    "Введите корректное количество гостей (целое число, не менее 1).",
                    "Ошибка ввода",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            // Определение выбранного процента чаевых 
            int tipPercent = 0;
            if (radioTip5.IsChecked == true)       tipPercent = 5;
            else if (radioTip10.IsChecked == true) tipPercent = 10;
            else if (radioTip15.IsChecked == true) tipPercent = 15;

            try
            {
                // Вызов методов класса логики 
                double tip       = TipCalculatorLogic.CalculateTip(bill, tipPercent);
                double total     = TipCalculatorLogic.CalculateTotal(bill, tipPercent);
                double perPerson = TipCalculatorLogic.CalculatePerPerson(total, guests);

                // Формирование строки результата 
                string result =
                    $"Сумма счёта:      {bill:F2} руб.\n" +
                    $"Чаевые ({tipPercent,2}%):     {tip:F2} руб.\n" +
                    $"Итого:            {total:F2} руб.";

                // Строка разбивки по гостям отображается только если гостей > 1
                if (guests > 1)
                    result += $"\nНа каждого гостя ({guests} чел.): {perPerson:F2} руб.";

                // Отображение результата 
                textBlockResult.Text = result;
                borderResult.Visibility = Visibility.Visible;
            }
            catch (ArgumentException ex)
            {
                // Приложение не вылетает — показываем понятное сообщение пользователю
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
