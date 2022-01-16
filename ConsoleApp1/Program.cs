
using System.Collections;

internal class Program
{
    static void Main(string[] args)
    {

        // See https://aka.ms/new-console-template for more information
        List<Factories> Factories_list = new List<Factories>();
        Factories_list.Add(new Factories { Name = "НПЗ#1", Description = "Первый нефтеперерабатывающий завод" });
        Factories_list.Add(new Factories { Name = "НПЗ#2", Description = "Второй нефтеперерабатывающий завод" });

        List<Units> Units_list = new List<Units>();
        Units_list.Add(new Units { Name = "ГФУ-2", FactoryId = 1 });
        Units_list.Add(new Units { Name = "АВТ-6", FactoryId = 1 });
        Units_list.Add(new Units { Name = "АВТ-10", FactoryId = 2 });


        List<Tanks> Tanks_list = new List<Tanks>();
        Tanks_list.Add(new Tanks { Name = "Резервуар 1", Volume = 1500, Maxvolume = 2000, UnitId = 1 });
        Tanks_list.Add(new Tanks { Name = "Резервуар 2", Volume = 2500, Maxvolume = 3000, UnitId = 1 });
        Tanks_list.Add(new Tanks { Name = "Дополнительный резервуар 24", Volume = 3000, Maxvolume = 3000, UnitId = 2 });
        Tanks_list.Add(new Tanks { Name = "Резервуар 35", Volume = 3000, Maxvolume = 3000, UnitId = 2 });
        Tanks_list.Add(new Tanks { Name = "Резервуар 47", Volume = 4000, Maxvolume = 5000, UnitId = 3 });
        Tanks_list.Add(new Tanks { Name = "Резервуар 256", Volume = 500, Maxvolume = 2000, UnitId = 3 });


        GetList(Factories_list);
        Console.WriteLine("\n");
        GetList(Units_list);
        Console.WriteLine("\n");
        GetList(Tanks_list);

        //Tanks_list[0].Print();

    }

    public static void GetList<T>(List<T> list) where T: IPrintForClasses
    {
        if (list is null) return;
        {
            foreach (T item in list)
                item.Print();
        }
    }
}


class Factories : IPrintForClasses//Завод
{
    public string Name { get; set; } = "Без названия";

    public string? Description { get; set; } // описание
    public void Print()
    {
        Console.WriteLine($"Наименование: {Name}  Описание: {Description}");
    }
}

class Units : IPrintForClasses // Установка
{    
    public string Name { get; set; } = "Без названия";
    public int? FactoryId { get; set; }      // производитель

    public void Print()
    {
        Console.WriteLine($"Наименование: {Name}  Производитель: {FactoryId} ");
    }
}

class Tanks : IPrintForClasses //Резервуар
{
    public string Name { get; set; } = "Без названия";

    public int? Volume;                      // объём
    public int? Maxvolume;                   // максимальный объём
    public int? UnitId;                      // товар

    public void Print()
    {
        Console.WriteLine(
            $"Наименование: {Name}  " +
            $"Объём: {Volume}   " +
            $"Максимальный объём: {Maxvolume}   "  +
            $"Номер товара: {UnitId}");
    }
}

interface IPrintForClasses
{
    void Print();
}