using System.Collections.Generic;

namespace NeolantDemo.DAL.Entities
{
    /// <summary>
    /// Базовый объект с поддержкой иерархии.
    /// </summary>
    public abstract class BaseHierarchicalEntity : BaseEntity
    {
        /// <summary>
        /// Получает или задаёт идентификатор родителя сущности.
        /// </summary>
        /// <value>Идентификатор родителя сущности.</value>
        public long? ParentInstanceS { get; set; }

        /// <summary>
        /// Получает или задаёт потомков сущности.
        /// </summary>
        /// <value>Потомки сущности.</value>
        public List<BaseHierarchicalEntity> Children { get; set; }

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
            var other = (BaseHierarchicalEntity) obj;
            return base.Equals(other)
                   && ParentInstanceS == other.ParentInstanceS
                   && Equals(Children, other.Children);
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
                hashCode = (hashCode*397) ^ ParentInstanceS.GetHashCode();
                hashCode = (hashCode*397) ^ (Children != null ? Children.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}