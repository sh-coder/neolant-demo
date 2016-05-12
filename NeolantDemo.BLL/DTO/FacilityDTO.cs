using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NeolantDemo.BLL.DTO
{
    /// <summary>
    /// Объект.
    /// </summary>
    public class FacilityDTO
    {
        /// <summary>
        /// Получает или задаёт идентификатор объекта.
        /// </summary>
        /// <value>Идентификатор сущности.</value>
        public long InstanceS { get; set; }

        /// <summary>
        /// Получает или задаёт идентификатор родителя объекта.
        /// </summary>
        public long? ParentInstanceS { get; set; }

        /// <summary>
        /// Получает или задаёт наименование объекта.
        /// </summary>
        [Required]
        public string Identifier { get; set; }

        /// <summary>
        /// Получает или задаёт идентификатор типа/класса объекта.
        /// </summary>
        [Required]
        public long KindS { get; set; }

        /// <summary>
        /// Получает или задаёт потомков объекта.
        /// </summary>
        /// <value>Потомки сущности.</value>
        public List<FacilityDTO> Children { get; set; }
    }
}