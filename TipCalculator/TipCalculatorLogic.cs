using System;

namespace TipCalculator
{
    /// Статический класс логики калькулятора чаевых. Методы принимают простые типы (double, int) и возвращают результат. При некорректных данных выбрасывают ArgumentException.
    public static class TipCalculatorLogic
    {
        /// Вычисляет сумму чаевых.
        /// <param name="billAmount">Сумма счёта в рублях. Должна быть больше 0.</param>
        /// <param name="tipPercent">Процент чаевых. Допустимые значения: 0, 5, 10, 15.</param>
        /// <returns>Сумма чаевых, округлённая до 2 знаков.</returns>
        /// <exception cref="ArgumentException">
        /// Выбрасывается, если billAmount &lt;= 0 или tipPercent не входит в {0, 5, 10, 15}.
        /// </exception>
        public static double CalculateTip(double billAmount, int tipPercent)
        {
            if (billAmount <= 0)
                throw new ArgumentException("Сумма счёта должна быть больше нуля.");

            if (tipPercent != 0 && tipPercent != 5 && tipPercent != 10 && tipPercent != 15)
                throw new ArgumentException("Процент чаевых должен быть 0, 5, 10 или 15.");

            return Math.Round(billAmount * tipPercent / 100.0, 2);
        }

        /// Вычисляет итоговую сумму счёта с учётом чаевых.
        /// <param name="billAmount">Сумма счёта в рублях. Должна быть больше 0.</param>
        /// <param name="tipPercent">Процент чаевых. Допустимые значения: 0, 5, 10, 15.</param>
        /// <returns>Итоговая сумма (счёт + чаевые), округлённая до 2 знаков.</returns>
        public static double CalculateTotal(double billAmount, int tipPercent)
        {
            double tip = CalculateTip(billAmount, tipPercent);
            return Math.Round(billAmount + tip, 2);
        }

        /// Вычисляет сумму, которую платит каждый гость.
        /// <param name="totalAmount">Итоговая сумма счёта с чаевыми.</param>
        /// <param name="guestCount">Количество гостей. Должно быть не менее 1.</param>
        /// <returns>Сумма на одного гостя, округлённая до 2 знаков.</returns>
        /// <exception cref="ArgumentException">
        /// Выбрасывается, если guestCount меньше 1.
        /// </exception>
        public static double CalculatePerPerson(double totalAmount, int guestCount)
        {
            if (guestCount < 1)
                throw new ArgumentException("Количество гостей должно быть не менее 1.");

            return Math.Round(totalAmount / guestCount, 2);
        }
    }
}
