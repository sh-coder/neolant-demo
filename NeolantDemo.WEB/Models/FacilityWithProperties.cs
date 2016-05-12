using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace NeolantDemo.WEB.Models
{
    /// <summary>
    /// Объект.
    /// </summary>
    [DataContract]
    public class FacilityWithProperties
    {
        /// <summary>
        /// Получает или задаёт идентификатор объекта.
        /// </summary>
        /// <value>Идентификатор объекта.</value>
        [Key]
        [DataMember(Order = 1, IsRequired = true)]
        public long InstanceS { get; set; }

        /// <summary>
        /// Получает или задаёт идентификатор родителя объекта.
        /// </summary>
        [DataMember(Order = 2)]
        public long? ParentInstanceS { get; set; }

        /// <summary>
        /// Получает или задаёт идентификатор типа/класса объекта.
        /// </summary>
        [Required]
        [DataMember(Order = 3, IsRequired = true)]
        public long KindS { get; set; }

        /// <summary>
        /// Получает или задаёт наименование объекта.
        /// </summary>
        [Required]
        [DataMember(Order = 4, IsRequired = true)]
        public string Identifier { get; set; }

        /// <summary>
        /// Получает или задаёт список свойств.
        /// </summary>
        /// <value>Список свойств.</value>
        [DataMember(Order = 5)]
        public List<Property> Properties { get; set; }
    }
}