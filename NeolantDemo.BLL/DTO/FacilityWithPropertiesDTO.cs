using System.Collections.Generic;

namespace NeolantDemo.BLL.DTO
{
    /// <summary>
    /// ������ �� ����������.
    /// </summary>
    public class FacilityWithPropertiesDTO : FacilityDTO
    {
        /// <summary>
        /// �������� ��� ����� ������ �������.
        /// </summary>
        /// <value>������ �������.</value>
        public List<PropertyDTO> Properties { get; set; }
    }
}