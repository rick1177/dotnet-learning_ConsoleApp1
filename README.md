# Проблема работы с объектом типа List, наполненным объектами собственного класса при попытке отправки List в функцию

Столкнулс с такой ситуацией, что когда создаёлся свой класс (для примера сразу 2 класса):
```c#
class Factories //Завод
{
    public string name = "Без названия";    // наименование
    public string description = "";         // описание

    public void Print()
    {
        Console.WriteLine($"Наименование: {name}  Описание: {description}");
    }
}

class Units // Установка
{
    public string name = "Без названия";    // наименование
    public int factoryId;                   // производитель

    public void Print()
    {
        Console.WriteLine($"Наименование: {name}  Производитель: {factoryId} ");
    }
}
```
после чего в коде создаётся список с объектами каждого классеа:
```c#
List<Factories> Factories_list = new List<Factories>();
Factories_list.Add(new Factories { name = "НПЗ#1", description = "Первый нефтеперерабатывающий завод" });
Factories_list.Add(new Factories { name = "НПЗ#2", description = "Второй нефтеперерабатывающий завод" });

List<Units> Units_list = new List<Units>();
Units_list.Add(new Units { name = "ГФУ-2", factoryId = 1 });
Units_list.Add(new Units { name = "АВТ-6", factoryId = 1 });
Units_list.Add(new Units { name = "АВТ-10", factoryId = 2 });
```

Поскольку таких списков много, хочется создать функцию, куда будет отправляться нужный список, а она будет вызывать метод объектов для этого класса в цикле.
Вот тут и возникла проблема:
```c#
public static void GetList<T>(T m_list) where T: IList
    {
        if (m_list is null) return;
        if ((m_list is List<Factories> || m_list is List<Units> || m_list is List<Tanks>) /*&& m_list is not null*/)
        {
            foreach (T i_list in m_list)
                m_list.Print();


        }
    }
```
Внутри функции не удаётся добравть до метода Print экземпляра класса, который сидит в листе.
