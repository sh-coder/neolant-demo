using System;

namespace NeolantDemo.Core.ExtensionMethods
{
    /// <summary>
    /// Класс для работы с перечислениями.
    /// </summary>
    public static class EnumerationExtensions
    {
        /// <summary>
        /// Преобразует строковое представление параметра<paramref name="value" />в эквивалент объекта перечисления типа
        /// <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">Тип перечисления.</typeparam>
        /// <param name="value">Строка, содержащая имя или значение для преобразования.</param>
        /// <param name="ignoreCase">
        /// Значение <c>true</c>, чтобы игнорировать регистр; в противном случае — значение <c>false</c>.
        /// Значение по умолчанию: <c>true</c>.
        /// </param>
        /// <returns>Объект перечисления типа <typeparamref name="T" /> или <c>null</c>.</returns>
        public static T ToEnum<T>(this string value, bool ignoreCase = true)
        {
            return (T) Enum.Parse(typeof (T), value, ignoreCase);
        }
    }
}