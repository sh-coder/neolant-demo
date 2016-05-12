using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using NeolantDemo.Core;

namespace NeolantDemo.WEB.Models
{
    /// <summary>
    /// Свойство/атрибут.
    /// </summary>
    [DataContract]
    public class Property
    {
        private readonly string _dynamicPropertyName = Nameof<Property>.Property(i => i.DynamicValue);
        private Dictionary<string, dynamic> _dynamicProperties;
        private dynamic _dynamicValue;

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
        public dynamic DynamicValue
        {
            get { return _dynamicValue; }
            set
            {
                _dynamicValue = value;

                string camelCase = char.ToLower(_dynamicPropertyName[0]) + _dynamicPropertyName.Substring(1);
                _dynamicProperties = new Dictionary<string, dynamic> {{camelCase, _dynamicValue}};
            }
        }

        /// <summary>
        /// Контейнер для свойств, тип которых dynamic либо определяется в runtime.
        /// </summary>
        /// <value>Динамичные свойства.</value>
        public Dictionary<string, dynamic> DynamicProperties
        {
            get
            {
                if (_dynamicProperties != null)
                {
                    _dynamicValue = _dynamicProperties.Values.FirstOrDefault();
                }
                return _dynamicProperties;
            }
            set { _dynamicProperties = value; }
        }


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
    }
}