using System.ComponentModel.DataAnnotations;

namespace NeolantDemo.BLL.DTO
{
    /// <summary>
    /// ����� ������� �� ���������.
    /// </summary>
    public class CommonUniversalPropertyDTO
    {
        /// <summary>
        /// �������� ��� ����� ������������� ��������.
        /// </summary>
        /// <value>������������� ��������.</value>
        [Key]
        public long InstanceS { get; set; }

        /// <summary>
        /// �������� ��� ����� ������������� �������.
        /// </summary>
        /// <value>������������� �������.</value>
        [Required]
        public long UniversalClassS { get; set; }

        /// <summary>
        /// �������� ��� ����� ������������� ���� ��������/��������.
        /// </summary>
        /// <value>������������� ���� ��������/��������.</value>
        [Required]
        public long PropertyKindS { get; set; }

        /// <summary>
        /// �������� ��� ����� �������� �����.
        /// </summary>
        /// <value>�������� �����.</value>
        public string Description { get; set; }

        /// <summary>
        /// �������� ��� ����� ����� ������������������ �����.
        /// </summary>
        /// <value>����� ������������������ �����.</value>
        public int? Sequence { get; set; }
    }
}