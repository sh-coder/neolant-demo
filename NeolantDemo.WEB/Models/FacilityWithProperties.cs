using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace NeolantDemo.WEB.Models
{
    /// <summary>
    /// Объект.
    /// </summary>
    [DataContract]
    public class FacilityWithProperties
    {
        /// <summary>
        /// Получает или задаёт идентификатор объекта.
        /// </summary>
        /// <value>Идентификатор объекта.</value>
        [Key]
        [DataMember(Order = 1, IsRequired = true)]
        public long InstanceS { get; set; }

        /// <summary>
        /// Получает или задаёт идентификатор родителя объекта.
        /// </summary>
        [DataMember(Order = 2)]
        public long? ParentInstanceS { get; set; }

        /// <summary>
        /// Получает или задаёт идентификатор типа/класса объекта.
        /// </summary>
        [Required]
        [DataMember(Order = 3, IsRequired = true)]
        public long KindS { get; set; }

        /// <summary>
        /// Получает или задаёт наименование объекта.
        /// </summary>
        [Required]
        [DataMember(Order = 4, IsRequired = true)]
        public string Identifier { get; set; }

        /// <summary>
        /// Получает или задаёт список свойств.
        /// </summary>
        /// <value>Список свойств.</value>
        [DataMember(Order = 5)]
        public List<Property> Properties { get; set; }

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

            var other = (FacilityWithProperties) obj;

            return InstanceS == other.InstanceS
                   && ParentInstanceS == other.ParentInstanceS
                   && KindS == other.KindS
                   && string.Equals(Identifier, other.Identifier)
                   && Properties.OrderBy(f => f.PropertyKindS)
                            .SequenceEqual(other.Properties.OrderBy(s => s.PropertyKindS));
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
                hashCode = (hashCode * 397) ^ ParentInstanceS.GetHashCode();
                hashCode = (hashCode * 397) ^ KindS.GetHashCode();
                hashCode = (hashCode * 397) ^ (Identifier != null ? Identifier.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Properties != null ? Properties.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}