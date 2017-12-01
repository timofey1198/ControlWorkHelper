using System;


namespace ControlWorkHelper
{
    class Helper
    {
        /// <summary>
        /// Функция для ввода целого числа с проверкой данный из консоли
        /// </summary>
        /// <param name="welcome">
        /// Строка приглашения для ввода
        /// </param>
        /// <returns>
        /// Целое число, введенное с консоли
        /// </returns>
        public static int IntInput(string welcome)
        {
            int n = 0;
            Console.WriteLine(welcome);
            while (!int.TryParse(Console.ReadLine(), out n))
            {
                Console.WriteLine("Ошибка. Повторите ввод:");
            }
            return n;
        }

        /// <summary>
        /// Функция для ввода целого числа с проверкой данный из консоли
        /// </summary>
        /// <param name="welcome">
        /// Строка приглашения для ввода
        /// </param>
        /// <param name="minValue">
        /// Минимальное допустимое значение
        /// </param>
        /// <param name="maxValue">
        /// Максимальное допустимое значение
        /// </param>
        /// <returns>
        /// Целое число, введенное с консоли
        /// </returns>
        public static int IntInput(string welcome, int minValue, int maxValue)
        {
            int n = 0;
            Console.WriteLine(welcome);
            while (!int.TryParse(Console.ReadLine(), out n) || n > maxValue || n < minValue)
            {
                Console.WriteLine("Ошибка. Повторите ввод:");
            }
            return n;
        }
    }
}
