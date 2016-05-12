using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NeolantDemo.DAL.Entities;

namespace NeolantDemo.BLL.DTO
{
    /// <summary>
    /// Вид атрибута/свойства.
    /// </summary>
    public class PropertyKindDTO
    {
        /// <summary>
        /// Получает или задаёт идентификатор вида свойства.
        /// </summary>
        /// <value>Идентификатор вида свойства.</value>
        [Key]
        public long InstanceS { get; set; }

        /// <summary>
        /// Получает или задаёт идентификатор родителя вида свойства.
        /// </summary>
        /// <value>Идентификатор родителя вида свойства.</value>
        public long? ParentInstanceS { get; set; }

        /// <summary>
        /// Получает или задаёт наименование вида свойства.
        /// </summary>
        /// <value>Наименование вида свойства.</value>
        [Required]
        public string Identifier { get; set; }

        /// <summary>
        /// Получает или задаёт тип данных атрибута/свойства.
        /// </summary>
        /// <value>Тип данных атрибута/свойства.</value>
        [Required]
        public NamedDefinedTypeDataType NdtDataType { get; set; }

        /// <summary>
        /// Получает или задаёт потомков вида свойства.
        /// </summary>
        /// <value>Потомки сущности.</value>
        public List<PropertyKindDTO> Children { get; set; }
    }
}