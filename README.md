# neolant-demo
Тестовая справочная система, [описание тестового задания](Docs/TestTask.pdf)

## Описание API, моделей:
```
http://localhost:20449/swagger/ui/index
```

## Описание контроллеров:
* FacilityClasses - Контроллер справочника видов объектов.
* PropertyKinds - Контроллер справочника видов свойств.
* CommonUniversalProperties - Контроллер привязки видов объектов к видам свойств.
* Facility - Контроллер для работы с иерархией объектов.
* FacilityWithProperties - Контроллер для работы с объектами со свойствами.

## Примеры запросов:
* Создание объекта определённого класса в определённом месте дерева объектов с возможностью задания значений атрибутов:
```
curl -X POST --header 'Content-Type: application/json' --header 'Accept: application/json' -d '{
  "instanceS": 12345677890,
  "parentInstanceS": 4589285344679034880,
  "kindS": 7045466727029669888,
  "identifier": "Тестовый насос",
  "properties": [
    {
      "propertyKindS": 2048281056983318528,
      "value": "2016-02-10T01:02:18Z"
    },
    {
      "propertyKindS": 4539770964740669440,
      "value": 22.12345
    },
    {
      "propertyKindS": 4986085573657906176,
      "value": 12.0
    },
    {
      "propertyKindS": 8775751229292694144,
      "value": "ЦВ"
    }
  ]
}' 'http://localhost:20449/odata/v1/FacilityWithProperties'
```

* Получение поддерева объектов по заданному объекту(только наименования и иерархия):
```
curl -X GET --header 'Accept: application/json' 'http://localhost:20449/odata/v1/Facilities(5481173415354171392)/Function.Hierarchy?%24expand=children(%24levels%3Dmax%3B%24select%3Didentifier)&%24select=identifier'
```

* Получение заданного объекта (полностью, со значениями):
```
curl -X GET --header 'Accept: application/json' 'http://localhost:20449/odata/v1/FacilityWithProperties(12345677890)'
```

* Среди потомков заданного объекта (могут быть объекты разных классов) выбрать список насосов установленных до сегодняшнего дня с массой более 10 кг:
```
curl -X GET --header 'Accept: application/json' 'localhost:20449/odata/v1/FacilityWithProperties(59993433315328)/Function.Descendants(kindS=7045466727029669888)?$filter=properties/any(x: x/propertyKindS eq 2048281056983318528 and x/dateTimeValue lt 2016-05-12T00:00:00Z) and properties/any(y: y/propertyKindS eq 4539770964740669440 and y/floatValue gt 10)'
```

Ссылка на бекап базы:
https://box.delta.ge/owncloud/index.php/s/uAiDEarE70c4TGZ/download

Ссылка на спецификацию OData:
http://docs.oasis-open.org/odata/odata/v4.0/errata02/os/complete/part1-protocol/odata-v4.0-errata02-os-part1-protocol-complete.html
