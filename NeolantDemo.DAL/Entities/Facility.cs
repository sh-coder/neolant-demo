namespace NeolantDemo.DAL.Entities
{
    /// <summary>
    /// Объект.
    /// </summary>
    public class Facility : BaseHierarchicalFacility
    {
        /// <summary>
        /// Получает или задаёт идентификатор типа/класса объекта.
        /// </summary>
        public long KindS { get; set; }
    }
}