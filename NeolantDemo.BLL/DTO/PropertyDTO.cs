using System;
using System.ComponentModel.DataAnnotations;
using NeolantDemo.DAL.Entities;

namespace NeolantDemo.BLL.DTO
{
    /// <summary>
    /// Свойство/атрибут.
    /// </summary>
    public class PropertyDTO
    {
        /// <summary>
        /// Получает или задаёт идентификатор сущности.
        /// </summary>
        /// <value>Идентификатор сущности.</value>
        public long InstanceS { get; set; }

        /// <summary>
        /// Получает или задаёт идентификатор вида атрибута/свойства.
        /// </summary>
        /// <value>Идентификатор вида атрибута/свойства.</value>
        [Required]
        public long PropertyKindS { get; set; }

        /// <summary>
        /// Получает или задаёт идентификатор объекта которому соответствует данный экземпляр свойства.
        /// </summary>
        /// <value>Идентификатор объекта которому соответствует данный экземпляр свойства.</value>
        [Required]
        public long TargetClassS { get; set; }

        /// <summary>
        /// Получает или задаёт типа объекта <see cref="NamedDefinedTypeDataType" />.
        /// </summary>
        /// <value>Тип объекта.</value>
        [Required]
        public NamedDefinedTypeDataType EntityType { get; set; }

        /// <summary>
        /// Получает или задаёт описание.
        /// </summary>
        /// <value>Описание.</value>
        public string Description { get; set; }

        /// <summary>
        /// Получает или задаёт значение свойства.
        /// </summary>
        /// <value>Значение свойства.</value>
        [Required]
        public dynamic DynamicValue { get; set; }

        /// <summary>
        /// Получает или задаёт значение типа <see cref="long" />.
        /// </summary>
        /// <value>Значение <c>long</c> или <c>null</c>.</value>
        public long? BigintValue { get; set; }

        /// <summary>
        /// Получает или задаёт значение типа <see cref="T:byte[]" />.
        /// </summary>
        /// <value>Значение <c>byte[]</c> или <c>null</c>.</value>
        public byte?[] BinaryValue { get; set; }

        /// <summary>
        /// Получает или задаёт значение типа <see cref="bool" />.
        /// </summary>
        /// <value>Значение <c>bool</c> или <c>null</c>.</value>
        public bool? BooleanValue { get; set; }

        /// <summary>
        /// Получает или задаёт значение типа <see cref="DateTime" />.
        /// </summary>
        /// <value>Значение <c>DateTime</c> или <c>null</c>.</value>
        public DateTime? DateTimeValue { get; set; }

        /// <summary>
        /// Получает или задаёт значение типа <see cref="decimal" />.
        /// </summary>
        /// <value>Значение <c>decimal</c> или <c>null</c>.</value>
        public decimal? DecimalValue { get; set; }

        /// <summary>
        /// Получает или задаёт значение типа <see cref="double" />.
        /// </summary>
        /// <value>Значение <c>double</c> или <c>null</c>.</value>
        public double? FloatValue { get; set; }

        /// <summary>
        /// Получает или задаёт значение типа <see cref="int" />.
        /// </summary>
        /// <value>Значение <c>int</c> или <c>null</c>.</value>
        public int? IntegerValue { get; set; }

        /// <summary>
        /// Получает или задаёт значение типа <see cref="string" />.
        /// </summary>
        /// <value>Значение <c>string</c> или <c>null</c>.</value>
        public string StringValue { get; set; }

        /// <summary>
        /// Получает или задаёт значение типа <see cref="Int16" />.
        /// </summary>
        /// <value>Значение <c>Int16</c> или <c>null</c>.</value>
        public Int16? SmallIntegerValue { get; set; }
    }
}