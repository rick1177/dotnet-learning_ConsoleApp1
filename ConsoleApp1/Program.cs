﻿
using System.Collections;

internal class Program
{
    static void Main(string[] args)
    {

        // See https://aka.ms/new-console-template for more information
        List<Factories> Factories_list = new List<Factories>();
        Factories_list.Add(new Factories { name = "НПЗ#1", description = "Первый нефтеперерабатывающий завод" });
        Factories_list.Add(new Factories { name = "НПЗ#2", description = "Второй нефтеперерабатывающий завод" });

        List<Units> Units_list = new List<Units>();
        Units_list.Add(new Units { name = "ГФУ-2", factoryId = 1 });
        Units_list.Add(new Units { name = "АВТ-6", factoryId = 1 });
        Units_list.Add(new Units { name = "АВТ-10", factoryId = 2 });


        List<Tanks> Tanks_list = new List<Tanks>();
        Tanks_list.Add(new Tanks { name = "Резервуар 1", volume = 1500, maxvolume = 2000, unitId = 1 });
        Tanks_list.Add(new Tanks { name = "Резервуар 2", volume = 2500, maxvolume = 3000, unitId = 1 });
        Tanks_list.Add(new Tanks { name = "Дополнительный резервуар 24", volume = 3000, maxvolume = 3000, unitId = 2 });
        Tanks_list.Add(new Tanks { name = "Резервуар 35", volume = 3000, maxvolume = 3000, unitId = 2 });
        Tanks_list.Add(new Tanks { name = "Резервуар 47", volume = 4000, maxvolume = 5000, unitId = 3 });
        Tanks_list.Add(new Tanks { name = "Резервуар 256", volume = 500, maxvolume = 2000, unitId = 3 });


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
        if ((list is List<Factories> || list is List<Units> || list is List<Tanks>) /*&& m_list is not null*/)
        {
            foreach (T item in list)
                item.Print();
        }
    }
}


class Factories : IPrintForClasses//Завод
{
    public string name = "Без названия";    // наименование
    public string description = "";         // описание

    public void Print()
    {
        Console.WriteLine($"Наименование: {name}  Описание: {description}");
    }
}

class Units : IPrintForClasses // Установка
{
    public string name = "Без названия";    // наименование
    public int factoryId;                   // производитель

    public void Print()
    {
        Console.WriteLine($"Наименование: {name}  Производитель: {factoryId} ");
    }
}

class Tanks : IPrintForClasses //Резервуар
{
    public string name = "Без названия";    // наименование
    public int volume;                      // объём
    public int maxvolume;                   // максимальный объём
    public int unitId;                      // товар

    public void Print()
    {
        Console.WriteLine(
            $"Наименование: {name}  " +
            $"Объём: {volume}   " +
            $"Максимальный объём: {maxvolume}   "  +
            $"Номер товара: {unitId}");
    }
}

interface IPrintForClasses
{
    void Print();
}