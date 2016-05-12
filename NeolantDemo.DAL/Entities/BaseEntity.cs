using System;

namespace NeolantDemo.DAL.Entities
{
    /// <summary>
    /// Базовый объект.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Получает или задаёт идентификатор сущности.
        /// </summary>
        /// <value>Идентификатор сущности.</value>
        public long InstanceS { get; set; }

        /// <summary>
        /// Получает или задаёт дату создания экземпляра сущности.
        /// </summary>
        /// <value>Дата создания экземпляра.</value>
        public DateTime Created { get; set; }

        /// <summary>
        /// Получает или задаёт наименование создателя экземпляра сущности.
        /// </summary>
        /// <value>Наименование создателя экземпляра.</value>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Получает или задаёт дату обновления экземпляра сущности.
        /// </summary>
        /// <value>Дата обновления экземпляра.</value>
        public DateTime Updated { get; set; }

        /// <summary>
        /// Получает или задаёт наименование обновителя экземпляра сущности.
        /// </summary>
        /// <value>Наименование обновителя экземпляра.</value>
        public string UpdatedBy { get; set; }

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

            var other = (BaseEntity) obj;
            return InstanceS == other.InstanceS
                   && Created.Equals(other.Created)
                   && string.Equals(CreatedBy, other.CreatedBy)
                   && Updated.Equals(other.Updated)
                   && string.Equals(UpdatedBy, other.UpdatedBy);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = InstanceS.GetHashCode();
                hashCode = (hashCode*397) ^ Created.GetHashCode();
                hashCode = (hashCode*397) ^ (CreatedBy != null ? CreatedBy.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Updated.GetHashCode();
                hashCode = (hashCode*397) ^ (UpdatedBy != null ? UpdatedBy.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}