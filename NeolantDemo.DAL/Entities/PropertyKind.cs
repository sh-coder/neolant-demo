namespace NeolantDemo.DAL.Entities
{
    /// <summary>
    /// Вид атрибута/свойства.
    /// </summary>
    public class PropertyKind : BaseHierarchicalFacility
    {
        /// <summary>
        /// Получает или задаёт тип данных атрибута/свойства.
        /// </summary>
        /// <value>Тип данных атрибута/свойства.</value>
        public NamedDefinedTypeDataType NdtDataType { get; set; }
    }
}