namespace NeolantDemo.DAL.Entities
{
    /// <summary>
    /// Базовый объект справочника.
    /// </summary>
    public abstract class BaseHierarchicalFacility : BaseHierarchicalEntity
    {
        //public string Identifier { get; set; }
        /// <summary>
        /// Получает или задаёт наименование объекта.
        /// </summary>
        /// <value>Наименование объекта.</value>
        public string Identifier { get; set; }

        /// <summary>
        /// Получает или задаёт описание.
        /// </summary>
        /// <value>Описание.</value>
        public string Description { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            var other = (BaseHierarchicalFacility) obj;
            return base.Equals(other)
                   && string.Equals(Identifier, other.Identifier)
                   && string.Equals(Description, other.Description);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode*397) ^ (Identifier != null ? Identifier.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}