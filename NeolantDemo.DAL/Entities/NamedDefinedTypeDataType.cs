namespace NeolantDemo.DAL.Entities
{
    /// <summary>
    /// Тип данных.
    /// </summary>
    public enum NamedDefinedTypeDataType
    {
        /// <summary>
        /// Строка <see cref="string" />.
        /// </summary>
        String,

        /// <summary>
        /// Дата и время <see cref="DateTime" />.
        /// </summary>
        DateTime,

        /// <summary>
        /// Число типа <see cref="double" />.
        /// </summary>
        Double,

        /// <summary>
        /// Число типа <see cref="long" />.
        /// </summary>
        Bigint,

        /// <summary>
        /// Число типа <see cref="int" />.
        /// </summary>
        Int,

        /// <summary>
        /// Число типа <see cref="Int16" />.
        /// </summary>
        Int16,

        /// <summary>
        /// Число типа <see cref="Single" />.
        /// </summary>
        Single,

        /// <summary>
        /// Бинарное <see cref="byte" />.
        /// </summary>
        Binary,

        /// <summary>
        /// Булевое <see cref="bool" />.
        /// </summary>
        Bool,

        /// <summary>
        /// Число типа <see cref="decimal" />.
        /// </summary>
        Decimal
    };
}