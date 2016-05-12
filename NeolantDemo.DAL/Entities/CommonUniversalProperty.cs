﻿namespace NeolantDemo.DAL.Entities
{
    /// <summary>
    /// Связь объекта со свойством.
    /// </summary>
    public class CommonUniversalProperty : BaseEntity
    {
        /// <summary>
        /// Получает или задаёт идентификатор объекта.
        /// </summary>
        /// <value>Идентификатор объекта.</value>
        public long UniversalClassS { get; set; }

        /// <summary>
        /// Получает или задаёт идентификатор вида атрибута/свойства.
        /// </summary>
        /// <value>Идентификатор вида атрибута/свойства.</value>
        public long PropertyKindS { get; set; }

        /// <summary>
        /// Получает или задаёт описание связи.
        /// </summary>
        /// <value>Описание связи.</value>
        public string Description { get; set; }

        /// <summary>
        /// Получает или задаёт номер последовательности связи.
        /// </summary>
        /// <value>Номер последовательности связи.</value>
        public int? Sequence { get; set; }
    }
}