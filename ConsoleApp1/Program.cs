
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


        PrintList(Factories_list);
        var items = NumderOfListElements(Factories_list);
        Console.WriteLine($"ИТОГО количество предприятий: {items}");

        Console.WriteLine("\n");
        PrintList(Units_list);
        items = NumderOfListElements(Units_list);
        Console.WriteLine($"ИТОГО количество установок: {items}");
        Console.WriteLine("Попытка поиска 1");
        var listresult = FindByName(Units_list, "АВТ-6");
        Console.WriteLine("Попытка поиска 2");
        listresult = FindByName(Units_list, "АВТ");

        Console.WriteLine("\n");
        PrintList(Tanks_list);
        items = NumderOfListElements(Tanks_list);
        Console.WriteLine($"ИТОГО количество резервуаров: {items}");
        Console.WriteLine("\n");
        Console.WriteLine("\n");

        FindByRelatedElement(Units_list, "ГФУ-2", "FactoryId", Factories_list);

        //Tanks_list[0].Print();

    }

    /// <summary>
    ///  Данная функция выводит в консоль отправляемый список инстансов класса
    /// </summary>
    public static void PrintList<T>(List<T> list) where T: IPrintForClasses
    {
        if (list is null) return;
        {
            foreach (T item in list)
                item.Print();
        }
    }

    /// <summary>
    ///  Данная функция выводит количество элементов в каждом списке
    /// </summary>
    public static int NumderOfListElements<T>(List<T> list) 
    {
        return list.Count();
    }

    /// <summary>
    ///  Данная функция выводит элемент списка по имени
    /// </summary>
    public static List<T>? FindByName<T>(List<T> list, string name) where T : IPrintForClasses
    {
        if (list is null) return null;
        var searchresult = list.FindAll(p => p.Name == name);

        if (searchresult.Count>0)
        {
            foreach (T item in searchresult)
                item.Print();
        }
        else
        {
            Console.WriteLine("Не найдено ничего!");
        }

        return searchresult;
    }


    //!!!!!!!!!!!https://stackoverflow.com/questions/915795/pass-fieldname-as-a-parameter!!!!!!!!!
    /// <summary>
    ///  Данная функция выводит связанный по базе элемент
    /// </summary>
    public static List<R>? FindByRelatedElement<T, R>(List<T> list, string name, string relatedCikumnName, List<R> relatedList)
        where T : IPrintForClasses
        where R : IPrintForClasses
    {
        if (list is null) return null;
        List<R> Res_list = new List<R>();

        var propRelatedCikumnName = typeof(T).GetProperty(relatedCikumnName);

        var searchresult = list.FindAll(p => p.Name == name);

        if (searchresult.Count > 0)
        {
            foreach (T item in searchresult)
            {
                int value = (int)propRelatedCikumnName.GetValue(item, null) - 1;
                Res_list.Add(relatedList[value]);
                Console.WriteLine($"Наименование {item.Name} соотнесено с {Res_list[0].Name} ");
            }
        }
        else
        {
            Console.WriteLine("Не найдено ничего!");
        }

        /*public int Method(Bar bar, string propertyName)
        {
            var prop = typeof(Bar).GetProperty(propertyName);
            int value = (int)prop.GetValue(bar, null);
            return value * 2;
        }*/

        return Res_list;
    }

}


class Factories : IPrintForClasses//Завод
{
    private string name = "Без названия";
    public string Name { get; set; } = "Без названия";

    public string? Description { get; set; } = ""; // описание

    public string? TablePrint
    {
        get
        {
            if (Name!="")
            {
                return String.Format("Наименование: {0}  Описание: {1}", Name, Description);
            }
            else
            {
                return "";
            }
        }

        set
        {
            name = value;   
        }
    }

    public void Print()
    {
        Console.WriteLine($"Наименование: {Name}  Описание: {Description}");
    }
}

class Units : IPrintForClasses // Установка
{
    private string name = "Без названия";
    public string Name { get; set; } = "Без названия";
    public int? FactoryId { get; set; }      // производитель

    public string? TablePrint
    {
        get
        {
            if (Name != "")
            {
                return String.Format("Наименование: {0}  Производитель: {1}", Name, FactoryId);
            }
            else
            {
                return "";
            }
        }

        set
        {
            name = value;
        }
    }

    public void Print()
    {
        Console.WriteLine($"Наименование: {Name}  Производитель: {FactoryId} ");
    }
}

class Tanks : IPrintForClasses //Резервуар
{
    private string name = "Без названия";
    public string Name { get; set; } = "Без названия";

    public int? Volume;                      // объём
    public int? Maxvolume;                   // максимальный объём
    public int? UnitId;                      // товар

    public string? TablePrint
    {
        get
        {
            if (Name != "")
            {
                return String.Format(
                    "Наименование: {0}  " +
                    "Объём: {1}   " +
                    "Максимальный объём: {2}   " +
                    "Номер товара: {3}", Name, Volume, Maxvolume, UnitId);
            }
            else
            {
                return "";
            }
        }

        set
        {
            name = value;
        }
    }

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
    string Name { get; set; }
    string TablePrint { get; set; }
    void Print();
}