﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace NeolantDemo.WEB.Models
{
    /// <summary>
    /// Свойство/атрибут.
    /// </summary>
    [DataContract]
    public class Property
    {
        /// <summary>
        /// Получает или задаёт идентификатор вида атрибута/свойства.
        /// </summary>
        /// <value>Идентификатор вида атрибута/свойства.</value>
        [Required]
        [DataMember(Order = 1, IsRequired = true)]
        public long PropertyKindS { get; set; }

        /// <summary>
        /// Получает или задаёт значение свойства.
        /// </summary>
        /// <value>Значение свойства.</value>
        [DataMember(Order = 2, IsRequired = true)]
        public dynamic DynamicValue { get; set; }

        /// <summary>
        /// Контейнер для свойств, тип которых dynamic либо определяется в runtime.
        /// </summary>
        /// <value>Динамичные свойства.</value>
        public Dictionary<string, dynamic> DynamicProperties =>
            new Dictionary<string, dynamic> { { "value", DynamicValue } };

        /// <summary>
        /// Нужно только для фильтрации. Для получения или установки данных использовать <see cref="DynamicValue" />
        /// </summary>
        [DataMember(Order = Int32.MaxValue)]
        public DateTime? DateTimeValue { get; set; }

        /// <summary>
        /// Нужно только для фильтрации. Для получения или установки данных использовать <see cref="DynamicValue" />
        /// </summary>
        [DataMember(Order = Int32.MaxValue)]
        public double? FloatValue { get; set; }

        /// <summary>
        /// Нужно только для фильтрации. Для получения или установки данных использовать <see cref="DynamicValue" />
        /// </summary>
        [DataMember(Order = Int32.MaxValue)]
        public int? IntegerValue { get; set; }

        /// <summary>
        /// Нужно только для фильтрации. Для получения или установки данных использовать <see cref="DynamicValue" />
        /// </summary>
        [DataMember(Order = Int32.MaxValue)]
        public string StringValue { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            var other = ((Property)obj);

            return PropertyKindS == other.PropertyKindS
                              && Equals(DynamicValue, other.DynamicValue);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (DynamicValue != null ? DynamicValue.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ PropertyKindS.GetHashCode();
                return hashCode;
            }
        }
    }
}