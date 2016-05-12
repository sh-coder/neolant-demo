using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NeolantDemo.BLL.DTO
{
    /// <summary>
    /// Вид объекта.
    /// </summary>
    public class FacilityClassDTO
    {
        /// <summary>
        /// Получает или задаёт идентификатор сущности.
        /// </summary>
        /// <value>Идентификатор сущности.</value>
        [Key]
        public long InstanceS { get; set; }

        /// <summary>
        /// Получает или задаёт идентификатор родителя сущности.
        /// </summary>
        /// <value>Идентификатор родителя сущности.</value>
        public long? ParentInstanceS { get; set; }

        /// <summary>
        /// Получает или задаёт наименование сущности.
        /// </summary>
        /// <value>Наименование сущности.</value>
        [Required]
        public string Identifier { get; set; }

        /// <summary>
        /// Получает или задаёт потомков сущности.
        /// </summary>
        /// <value>Потомки сущности.</value>
        public List<FacilityClassDTO> Children { get; set; }
    }
}