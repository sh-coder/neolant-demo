using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeolantDemo.DAL.Entities;
using NeolantDemo.DAL.Interfaces;
using NeolantDemo.DAL.MSSQL;
using NeolantDemo.DAL.Repositories;

namespace NeolantDemo.DAL.Tests
{
    [TestClass]
    public sealed class UnitTestBase : IDisposable
    {
        private const int Min = 100;
        private const int Max = Int32.MaxValue;
        private readonly IUnitOfWork _database;
        private readonly Random _random = new Random(Guid.NewGuid().GetHashCode());

        public UnitTestBase()
        {
            const string connectionStringName = @"neolant";
            string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

            _database = new UnitOfWork(connectionString);
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        //[TestMethod]
        public void DeleteAll()
        {
            _database.PropertyRepository.DeleteAll();
            IEnumerable<Property> result0 = _database.PropertyRepository.Get();
            Assert.AreEqual(result0.Count(), 0);

            _database.CommonUniversalPropertyRepository.DeleteAll();
            IEnumerable<CommonUniversalProperty> result1 = _database.CommonUniversalPropertyRepository.Get();
            Assert.AreEqual(result1.Count(), 0);

            _database.FacilityClassRepository.DeleteAll();
            IEnumerable<FacilityClass> result2 = _database.FacilityClassRepository.Get();
            Assert.AreEqual(result2.Count(), 0);

            _database.PropertyKindRepository.DeleteAll();
            IEnumerable<PropertyKind> result3 = _database.PropertyKindRepository.Get();
            Assert.AreEqual(result3.Count(), 0);
        }

        [TestMethod]
        public void FacilityClassCreateFindDelete()
        {
            var itemRoot = new FacilityClass
            {
                InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                Identifier = "Test_root_identifier_" + _random.Next(Min, Max),
            };

            var itemChild = new FacilityClass
            {
                InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                ParentInstanceS = itemRoot.InstanceS,
                Identifier = "Test_child_identifier_" + _random.Next(Min, Max),
            };

            _database.FacilityClassRepository.Create(itemRoot);
            _database.FacilityClassRepository.Create(itemChild);

            FacilityClass itemCreated = _database.FacilityClassRepository.Get(itemRoot.InstanceS);
            Assert.AreEqual(itemRoot, itemCreated);

            itemCreated = _database.FacilityClassRepository.Find(x => x.Equals(itemChild)).FirstOrDefault();
            Assert.AreEqual(itemChild, itemCreated);

            _database.FacilityClassRepository.Delete(itemChild.InstanceS);
            _database.FacilityClassRepository.Delete(itemRoot.InstanceS);

            itemCreated = _database.FacilityClassRepository.Get(itemChild.InstanceS);
            Assert.IsNull(itemCreated);
        }

        [TestMethod]
        public void PropertyKindCreateFindDelete()
        {
            var itemRoot = new PropertyKind
            {
                InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                Identifier = "Test_root_identifier_" + _random.Next(Min, Max),
                NdtDataType = NamedDefinedTypeDataType.Int
            };

            var itemChild = new PropertyKind
            {
                InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                ParentInstanceS = itemRoot.InstanceS,
                Identifier = "Test_child_identifier_" + _random.Next(Min, Max),
                NdtDataType = NamedDefinedTypeDataType.Decimal
            };

            _database.PropertyKindRepository.Create(itemRoot);
            _database.PropertyKindRepository.Create(itemChild);

            PropertyKind itemCreated = _database.PropertyKindRepository.Get(itemRoot.InstanceS);
            Assert.AreEqual(itemRoot, itemCreated);

            itemCreated = _database.PropertyKindRepository.Find(x => x.Equals(itemChild)).FirstOrDefault();
            Assert.AreEqual(itemChild, itemCreated);

            _database.PropertyKindRepository.Delete(itemChild.InstanceS);
            _database.PropertyKindRepository.Delete(itemRoot.InstanceS);

            itemCreated = _database.PropertyKindRepository.Get(itemChild.InstanceS);
            Assert.IsNull(itemCreated);
        }

        [TestMethod]
        public void CommonUniversalPropertyCreateFindDelete()
        {
            var facilityClass = new FacilityClass
            {
                InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                Identifier = "Test_identifier_" + _random.Next(Min, Max),
            };
            var propertyKind = new PropertyKind
            {
                InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                Identifier = "Test_identifier_" + _random.Next(Min, Max),
                NdtDataType = NamedDefinedTypeDataType.String
            };
            var item1 = new CommonUniversalProperty
            {
                InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                UniversalClassS = facilityClass.InstanceS,
                PropertyKindS = propertyKind.InstanceS,
            };

            _database.FacilityClassRepository.Create(facilityClass);
            _database.PropertyKindRepository.Create(propertyKind);
            _database.CommonUniversalPropertyRepository.Create(item1);
            CommonUniversalProperty item1Created = _database.CommonUniversalPropertyRepository.Get(item1.InstanceS);
            Assert.AreEqual(item1, item1Created);

            item1Created = _database.CommonUniversalPropertyRepository.Find(x => x.Equals(item1)).First();
            Assert.AreEqual(item1, item1Created);

            _database.CommonUniversalPropertyRepository.Delete(item1.InstanceS);
            item1Created = _database.CommonUniversalPropertyRepository.Get(item1.InstanceS);
            Assert.IsNull(item1Created);

            _database.FacilityClassRepository.Delete(facilityClass.InstanceS);
            _database.PropertyKindRepository.Delete(propertyKind.InstanceS);
        }

        //[TestMethod]
        public void FillFacilityClassAndPropertyKinds()
        {
            var fc1 = new FacilityClass
            {
                InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                Identifier = "Оборудование"
            };

            var fc2 = new FacilityClass
            {
                InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                Identifier = "Труба",
                ParentInstanceS = fc1.InstanceS
            };

            var fc3 = new FacilityClass
            {
                InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                Identifier = "Насос",
                ParentInstanceS = fc1.InstanceS
            };

            var fc4 = new FacilityClass
            {
                InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                Identifier = "Фонтанная арматура",
                ParentInstanceS = fc1.InstanceS
            };

            var createdFCs = new HashSet<FacilityClass> {fc1, fc2, fc3, fc4};
            var beforeCreateFCs = new HashSet<FacilityClass>(_database.FacilityClassRepository.Get());

            _database.FacilityClassRepository.Create(fc1);
            _database.FacilityClassRepository.Create(fc2);
            _database.FacilityClassRepository.Create(fc3);
            _database.FacilityClassRepository.Create(fc4);

            var afterCreateFks = new HashSet<FacilityClass>(_database.FacilityClassRepository.Get());

            createdFCs.UnionWith(beforeCreateFCs);
            Assert.AreEqual(afterCreateFks.Count, createdFCs.Count);

            var pk1 = new PropertyKind
            {
                InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                Identifier = "Марка",
                NdtDataType = NamedDefinedTypeDataType.String
            };

            var pk2 = new PropertyKind
            {
                InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                Identifier = "Напор",
                NdtDataType = NamedDefinedTypeDataType.Double
            };

            var pk3 = new PropertyKind
            {
                InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                Identifier = "Масса",
                NdtDataType = NamedDefinedTypeDataType.Double
            };

            var pk4 = new PropertyKind
            {
                InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                Identifier = "Дата",
                NdtDataType = NamedDefinedTypeDataType.DateTime
            };

            var pk5 = new PropertyKind
            {
                InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                ParentInstanceS = pk4.InstanceS,
                Identifier = "Дата установки",
                NdtDataType = NamedDefinedTypeDataType.DateTime
            };

            var pk6 = new PropertyKind
            {
                InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                ParentInstanceS = pk1.InstanceS,
                Identifier = "Марка стали",
                NdtDataType = NamedDefinedTypeDataType.String
            };

            var pk7 = new PropertyKind
            {
                InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                Identifier = "Толщина стенки",
                NdtDataType = NamedDefinedTypeDataType.Double
            };

            var pk8 = new PropertyKind
            {
                InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                Identifier = "Год выпуска",
                NdtDataType = NamedDefinedTypeDataType.Int
            };

            var pk9 = new PropertyKind
            {
                InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                Identifier = "Инвентарный номер",
                NdtDataType = NamedDefinedTypeDataType.String
            };

            var pk10 = new PropertyKind
            {
                InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                Identifier = "Завод изготовить",
                NdtDataType = NamedDefinedTypeDataType.String
            };

            var pk11 = new PropertyKind
            {
                InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                Identifier = "Внешний диаметр",
                NdtDataType = NamedDefinedTypeDataType.Int
            };


            var createPKs = new HashSet<PropertyKind>
            {
                pk1,
                pk2,
                pk3,
                pk4,
                pk5,
                pk6,
                pk7,
                pk8,
                pk9,
                pk10,
                pk11
            };
            var beforeCreatePKs = new HashSet<PropertyKind>(_database.PropertyKindRepository.Get());

            foreach (PropertyKind pk in createPKs)
            {
                _database.PropertyKindRepository.Create(pk);
            }

            var afterCreatePKs = new HashSet<PropertyKind>(_database.PropertyKindRepository.Get());

            createPKs.UnionWith(beforeCreatePKs);
            Assert.AreEqual(afterCreatePKs.Count, createPKs.Count);

            // Свойства трубы
            var tubeProperties = new HashSet<PropertyKind>
            {
                pk6,
                pk7,
                pk11
            };

            // Свойства насоса
            var pumpProperties = new HashSet<PropertyKind>
            {
                pk1,
                pk2,
                pk3,
                pk5
            };

            // Свойства Фонтанной арматуры
            var faProperties = new HashSet<PropertyKind>
            {
                pk8,
                pk9,
                pk10
            };

            var dictFcWithPks = new Dictionary<FacilityClass, HashSet<PropertyKind>>
            {
                {fc2, tubeProperties},
                {fc3, pumpProperties},
                {fc4, faProperties}
            };
            var fcWithPkConnection = new HashSet<CommonUniversalProperty>();
            foreach (var fc in dictFcWithPks)
            {
                foreach (PropertyKind pk in fc.Value)
                {
                    fcWithPkConnection.Add(new CommonUniversalProperty
                    {
                        InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                        UniversalClassS = fc.Key.InstanceS,
                        PropertyKindS = pk.InstanceS,
                        Description = string.Format("{0} - {1}", fc.Key.Identifier, pk.Identifier)
                    });
                }
            }
            // todo: add bulk insert
            foreach (CommonUniversalProperty item in fcWithPkConnection)
            {
                _database.CommonUniversalPropertyRepository.Create(item);
            }
        }

        //[TestMethod]
        public void FillCommonUniversalProperty()
        {
            IEnumerable<FacilityClass> facilityClasses = _database.FacilityClassRepository.Get();
            IEnumerable<PropertyKind> propertyKinds = _database.PropertyKindRepository.Get().ToList();

            var createItems = new HashSet<CommonUniversalProperty>();
            int i = 0;
            foreach (FacilityClass fc in facilityClasses)
            {
                IEnumerable<PropertyKind> propertyKindsSkiped = propertyKinds.Skip(i++);
                foreach (PropertyKind pk in propertyKindsSkiped)
                {
                    createItems.Add(new CommonUniversalProperty
                    {
                        InstanceS = MSSQLCustomContext.GenerateInstanceS(),
                        UniversalClassS = fc.InstanceS,
                        PropertyKindS = pk.InstanceS,
                        Description = string.Format("{0} - {1}", fc.Identifier, pk.Identifier)
                    });
                }
            }

            var beforeCreate = new HashSet<CommonUniversalProperty>(_database.CommonUniversalPropertyRepository.Get());

            foreach (CommonUniversalProperty item in createItems)
            {
                _database.CommonUniversalPropertyRepository.Create(item);
            }

            var afterCreate = new HashSet<CommonUniversalProperty>(_database.CommonUniversalPropertyRepository.Get());

            createItems.UnionWith(beforeCreate);
            Assert.AreEqual(afterCreate.Count, createItems.Count);
        }
    }
}