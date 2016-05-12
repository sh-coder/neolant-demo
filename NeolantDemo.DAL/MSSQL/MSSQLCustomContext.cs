using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NeolantDemo.Core.ExtensionMethods;
using NeolantDemo.DAL.Entities;

namespace NeolantDemo.DAL.MSSQL
{
    /// <summary>
    /// Контекст доступа к базе данных.
    /// </summary>
    public sealed class MSSQLCustomContext : IDisposable
    {
        private readonly string _connectionString;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="connectionString">Строка подключения к базе данных.</param>
        public MSSQLCustomContext(string connectionString)
        {
            _connectionString = connectionString;
        }


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Выбрать данные из таблицы.
        /// </summary>
        /// <typeparam name="T">Тип данных.</typeparam>
        /// <returns></returns>
        public IEnumerable<T> SelectTableValues<T>(long? instanceS = null) where T : BaseEntity, new()
        {
            string tableName = GetTableName<T>();
            if (string.IsNullOrWhiteSpace(tableName))
            {
                yield return null;
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("spSelectTable", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TableName", tableName);
                    command.Parameters.AddWithValue("@InstanceS",
                        instanceS.HasValue ? (object) instanceS.Value : DBNull.Value);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            var item = ReadTableItem<T>(reader);
                            yield return item;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Выборка данных с учётом фильтра.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="targetClasses">The target classes.</param>
        /// <returns></returns>
        public IEnumerable<T> FindTableValues<T>(IEnumerable<long> targetClasses) where T : Property, new()
        {
            var targetClassesTable = new DataTable();
            targetClassesTable.Columns.Add("INSTANCE_S", typeof (long));
            foreach (long targetClassInstanceS in targetClasses)
            {
                DataRow row = targetClassesTable.NewRow();
                row[0] = targetClassInstanceS;
                targetClassesTable.Rows.Add(row);
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("spSelectProperty", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TargetClasses", targetClassesTable).SqlDbType =
                        SqlDbType.Structured;

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            var item = ReadTableItem<T>(reader);
                            yield return item;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Выбрать иерархию данных из таблицы.
        /// </summary>
        /// <typeparam name="T">Тип данных.</typeparam>
        /// <param name="instanceS">Идентификатор данных.</param>
        /// <returns></returns>
        public IEnumerable<T> SelectTableHierarchy<T>(long? instanceS) where T : BaseHierarchicalEntity, new()
        {
            string tableName = GetTableName<T>();
            if (string.IsNullOrWhiteSpace(tableName))
            {
                yield return null;
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("spSelectTableHierarchy", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TableName", tableName);
                    command.Parameters.AddWithValue("@InstanceS",
                        instanceS.HasValue ? (object) instanceS.Value : DBNull.Value);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            var item = ReadTableItem<T>(reader);
                            yield return item;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Удалить данные из таблицы.
        /// </summary>
        /// <typeparam name="T">Тип данных.</typeparam>
        /// <param name="instanceS">Идентификатор данных.</param>
        /// <returns></returns>
        public void DeleteTableValue<T>(long instanceS) where T : BaseEntity, new()
        {
            string tableName = GetTableName<T>();
            if (string.IsNullOrWhiteSpace(tableName))
            {
                return;
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("spDelete", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TableName", tableName);
                    command.Parameters.AddWithValue("@InstanceS", instanceS);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Удалить все данные из таблицы.
        /// </summary>
        /// <typeparam name="T">Тип данных.</typeparam>
        /// <returns></returns>
        public void DeleteTableValue<T>() where T : BaseEntity, new()
        {
            string tableName = GetTableName<T>();
            if (string.IsNullOrWhiteSpace(tableName))
            {
                return;
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("spDeleteAll", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TableName", tableName);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Добавляет данные в таблицу.
        /// </summary>
        /// <typeparam name="T">Тип данных.</typeparam>
        /// <param name="entity">Экземпляр данных.</param>
        /// <returns></returns>
        public void InserTableValue<T>(BaseEntity entity) where T : BaseEntity
        {
            string storedProcName = GetInsertStoredProcName<T>();
            if (string.IsNullOrWhiteSpace(storedProcName))
            {
                return;
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(storedProcName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@InstanceS", (entity.InstanceS != 0)
                        ? entity.InstanceS
                        : GenerateInstanceS());

                    var entityAsHierarchical = entity as BaseHierarchicalEntity;
                    if (entityAsHierarchical != null)
                    {
                        command.Parameters.AddWithValue("@ParentInstanceS",
                            entityAsHierarchical.ParentInstanceS ?? (object) DBNull.Value);
                    }

                    var entityAsHierarchicalFacility = entity as BaseHierarchicalFacility;
                    if (entityAsHierarchicalFacility != null)
                    {
                        command.Parameters.AddWithValue("@Identifier", entityAsHierarchicalFacility.Identifier);
                        command.Parameters.AddWithValue("@Description", entityAsHierarchicalFacility.Description);
                    }

                    var entityAsPropertyKind = entity as PropertyKind;
                    if (entityAsPropertyKind != null)
                    {
                        command.Parameters.AddWithValue("@NdtDataType", entityAsPropertyKind.NdtDataType.ToString());
                    }

                    var entityAsFacility = entity as Facility;
                    if (entityAsFacility != null)
                    {
                        command.Parameters.AddWithValue("@KindS", entityAsFacility.KindS);
                    }

                    var entityAsCommonUniversalProperty = entity as CommonUniversalProperty;
                    if (entityAsCommonUniversalProperty != null)
                    {
                        command.Parameters.AddWithValue("@UniversalClassS",
                            entityAsCommonUniversalProperty.UniversalClassS);
                        command.Parameters.AddWithValue("@PropertyKindS", entityAsCommonUniversalProperty.PropertyKindS);
                        command.Parameters.AddWithValue("@Description", entityAsCommonUniversalProperty.Description);
                        command.Parameters.AddWithValue("@Sequence", entityAsCommonUniversalProperty.Sequence);
                    }

                    var entityAsProperty = entity as Property;
                    if (entityAsProperty != null)
                    {
                        command.Parameters.AddWithValue("@PropertyKindS", entityAsProperty.PropertyKindS);
                        command.Parameters.AddWithValue("@TargetClassS", entityAsProperty.TargetClassS);
                        command.Parameters.AddWithValue("@NdtDataType", entityAsProperty.EntityType.ToString());
                        command.Parameters.AddWithValue("@BigintValue", entityAsProperty.BigintValue);
                        //command.Parameters.AddWithValue("@BinaryValue", entityAsProperty.BinaryValue);
                        command.Parameters.AddWithValue("@BooleanValue", entityAsProperty.BooleanValue);
                        command.Parameters.AddWithValue("@DateTimeValue", entityAsProperty.DateTimeValue);
                        command.Parameters.AddWithValue("@DecimalValue", entityAsProperty.DecimalValue);
                        command.Parameters.AddWithValue("@FloatValue", entityAsProperty.FloatValue);
                        command.Parameters.AddWithValue("@IntegerValue", entityAsProperty.IntegerValue);
                        command.Parameters.AddWithValue("@SmallIntegerValue", entityAsProperty.SmallIntegerValue);
                        command.Parameters.AddWithValue("@StringValue", entityAsProperty.StringValue);
                    }

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private static T ReadTableItem<T>(SqlDataReader reader, bool readOnlyHierarchy = false)
            where T : BaseEntity, new()
        {
            int instanceS = reader.GetOrdinal("INSTANCE_S");
            var entity = new T
            {
                InstanceS = reader.GetInt64(instanceS),
            };

            var itemAsBaseHierarchicalEntity = entity as BaseHierarchicalEntity;
            if (itemAsBaseHierarchicalEntity != null)
            {
                int parentClassS = reader.GetOrdinal("PARENT_INSTANCE_S");
                itemAsBaseHierarchicalEntity.ParentInstanceS = reader.IsDBNull(parentClassS)
                    ? null
                    : (long?) reader.GetInt64(parentClassS);
            }

            if (readOnlyHierarchy)
            {
                return entity;
            }

            var itemAsBaseHierarchicalFacility = entity as BaseHierarchicalFacility;
            if (itemAsBaseHierarchicalFacility != null)
            {
                int ordinalIdentifier = reader.GetOrdinal("IDENTIFIER");
                itemAsBaseHierarchicalFacility.Identifier = reader.IsDBNull(ordinalIdentifier)
                    ? null
                    : reader.GetString(ordinalIdentifier);
            }

            var itemAsPropertyKind = entity as PropertyKind;
            if (itemAsPropertyKind != null)
            {
                int ordinalNdtDataType = reader.GetOrdinal("NDT_DATA_TYPE");
                string namedDefinedTypeString = reader.GetString(ordinalNdtDataType);
                itemAsPropertyKind.NdtDataType = namedDefinedTypeString.ToEnum<NamedDefinedTypeDataType>();
            }

            var entityAsFacility = entity as Facility;
            if (entityAsFacility != null)
            {
                int ordinalKindS = reader.GetOrdinal("KIND_S");
                entityAsFacility.KindS = reader.GetInt64(ordinalKindS);
            }

            var itemAsCommonUniversalProperty = entity as CommonUniversalProperty;
            if (itemAsCommonUniversalProperty != null)
            {
                int ordinalUniversalClassS = reader.GetOrdinal("UNIVERSAL_CLASS_S");
                int ordinalPropertyKindS = reader.GetOrdinal("PROPERTY_KIND_S");
                int ordinalDescription = reader.GetOrdinal("DESCRIPTION");
                int ordinalSequence = reader.GetOrdinal("SEQUENCE");

                itemAsCommonUniversalProperty.UniversalClassS = reader.GetInt64(ordinalUniversalClassS);
                itemAsCommonUniversalProperty.PropertyKindS = reader.GetInt64(ordinalPropertyKindS);
                itemAsCommonUniversalProperty.Description = reader.IsDBNull(ordinalDescription)
                    ? null
                    : reader.GetString(ordinalDescription);
                itemAsCommonUniversalProperty.Sequence = reader.IsDBNull(ordinalSequence)
                    ? (int?) null
                    : reader.GetInt32(ordinalSequence);
            }

            var entityAsProperty = entity as Property;
            if (entityAsProperty != null)
            {
                int ordinalPropertyKindS = reader.GetOrdinal("PROPERTY_KIND_S");
                int ordinalTargetClassS = reader.GetOrdinal("TARGET_CLASS_S");
                int ordinalNdtDataType = reader.GetOrdinal("NDT_DATA_TYPE");
                int ordinalBigintValue = reader.GetOrdinal("BIGINT_VALUE");
                int ordinalBinaryValue = reader.GetOrdinal("BINARY_VALUE");
                int ordinalBooleanValue = reader.GetOrdinal("BOOLEAN_VALUE");
                int ordinalDateTimeValue = reader.GetOrdinal("DATETIME_VALUE");
                int ordinalDecimalValue = reader.GetOrdinal("DECIMAL_VALUE");
                int ordinalFloatValue = reader.GetOrdinal("FLOAT_VALUE");
                int ordinalIntegerValue = reader.GetOrdinal("INTEGER_VALUE");
                int ordinalSmallIntegerValue = reader.GetOrdinal("SMALLINTEGER_VALUE");
                int ordinalStringValue = reader.GetOrdinal("STRING_VALUE");

                entityAsProperty.PropertyKindS = reader.GetFieldValue<long>(ordinalPropertyKindS);
                entityAsProperty.TargetClassS = reader.GetFieldValue<long>(ordinalTargetClassS);
                entityAsProperty.EntityType =
                    reader.GetFieldValue<string>(ordinalNdtDataType).ToEnum<NamedDefinedTypeDataType>();

                entityAsProperty.BigintValue = reader.IsDBNull(ordinalBigintValue)
                    ? null
                    : reader.GetFieldValue<long?>(ordinalBigintValue);
                entityAsProperty.BinaryValue = reader.IsDBNull(ordinalBinaryValue)
                    ? null
                    : reader.GetFieldValue<byte?[]>(ordinalBinaryValue);
                entityAsProperty.BooleanValue = reader.IsDBNull(ordinalBooleanValue)
                    ? null
                    : reader.GetFieldValue<Boolean?>(ordinalBooleanValue);
                entityAsProperty.DateTimeValue = reader.IsDBNull(ordinalDateTimeValue)
                    ? null
                    : reader.GetFieldValue<DateTime?>(ordinalDateTimeValue);
                entityAsProperty.DecimalValue = reader.IsDBNull(ordinalDecimalValue)
                    ? null
                    : reader.GetFieldValue<decimal?>(ordinalDecimalValue);
                entityAsProperty.FloatValue = reader.IsDBNull(ordinalFloatValue)
                    ? null
                    : reader.GetFieldValue<double?>(ordinalFloatValue);
                entityAsProperty.IntegerValue = reader.IsDBNull(ordinalIntegerValue)
                    ? null
                    : reader.GetFieldValue<int?>(ordinalIntegerValue);
                entityAsProperty.SmallIntegerValue = reader.IsDBNull(ordinalSmallIntegerValue)
                    ? null
                    : reader.GetFieldValue<Int16?>(ordinalSmallIntegerValue);
                entityAsProperty.StringValue = reader.IsDBNull(ordinalStringValue)
                    ? null
                    : reader.GetFieldValue<string>(ordinalStringValue);
            }

            return entity;
        }

        private static string GetTableName<T>() where T : BaseEntity
        {
            Type type = typeof (T);
            if (type == typeof (Facility))
            {
                return "FACILITY";
            }
            if (type == typeof (FacilityClass))
            {
                return "FACILITY_CLASS";
            }
            if (type == typeof (PropertyKind))
            {
                return "PROPERTY_KIND";
            }
            if (type == typeof (CommonUniversalProperty))
            {
                return "COMMON_UNIVERSAL_PROPERTY";
            }
            if (type == typeof (Property))
            {
                return "PROPERTY";
            }
            throw new NotSupportedException();
        }

        private static string GetInsertStoredProcName<T>() where T : BaseEntity
        {
            Type type = typeof (T);
            return "spInsert" + type.Name;
        }

        /// <summary>
        /// Тестовая заглушка.
        /// </summary>
        /// <returns>System.Int64.</returns>
        public static long GenerateInstanceS()
        {
            // todo: Реализовать генератор последовательных идентификаторов типа  long
            long generateInstanceS;
            do
            {
                generateInstanceS = 1;
                foreach (byte b in Guid.NewGuid().ToByteArray())
                {
                    generateInstanceS *= (b + 1);
                }
            } while (generateInstanceS < 0);

            return generateInstanceS;
        }
    }
}