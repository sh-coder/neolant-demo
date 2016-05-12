using System.Collections.Generic;

namespace NeolantDemo.BLL.DTO
{
    /// <summary>
    /// Объект со свойствами.
    /// </summary>
    public class FacilityWithPropertiesDTO : FacilityDTO
    {
        /// <summary>
        /// Получает или задаёт список свойств.
        /// </summary>
        /// <value>Список свойств.</value>
        public List<PropertyDTO> Properties { get; set; }
    }
}