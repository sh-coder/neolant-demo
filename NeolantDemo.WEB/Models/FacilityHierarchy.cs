using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace NeolantDemo.WEB.Models
{
    /// <summary>
    /// Иерархичный объект.
    /// </summary>
    [DataContract]
    public class FacilityHierarchy
    {
        /// <summary>
        /// Получает или задаёт идентификатор объекта.
        /// </summary>
        /// <value>Идентификатор объекта.</value>
        [Key]
        [DataMember(Order = 1)]
        public long InstanceS { get; set; }

        /// <summary>
        /// Получает или задаёт наименование объекта.
        /// </summary>
        [Required]
        [DataMember(Order = 2, IsRequired = true)]
        public string Identifier { get; set; }

        /// <summary>
        /// Получает или задаёт потомков объекта.
        /// </summary>
        /// <value>Потомки сущности.</value>
        [DataMember(Order = 3)]
        public List<FacilityHierarchy> Children { get; set; }
    }
}