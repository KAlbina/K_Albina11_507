//Вариант 6
//(7 баллов) Напишите TypeFactory. Метод CreateAndFill(Type type, Dictionary<string, object> values) создает объект и заполняет его свойства значениями из словаря (по совпадению имен).
//(7 баллов) Напишите TaskDispatcher: создайте список из 50 задач (Action). Запустите их так, чтобы в любой момент времени выполнялось не более 5 задач (используйте обычный Semaphore(5, 5)).
//(1 балл) Объедините: каждое Action в TaskDispatcher должно вызывать TypeFactory для создания объекта с предопределенными данными, а затем добавлять этот объект в общий потокобезопасный List.
using Kozlova_Albina;


// номер 1 

Console.WriteLine("Номер 1");

var values = new Dictionary<string, object>
{
    ["Name"] = "Альбина",
    ["Age"] = 21,
    ["Email"] = "albina@example.com",
};

object created = TypeFactory.CreateAndFill(typeof(Person), values);
Console.WriteLine(created);
Console.WriteLine();


// Номер 2 и 3

Console.WriteLine("Номер 2 и 3");


var results = new List<Person>();
var listLock = new object();

var dispatcher = new TaskDispatcher();

for (int i = 0; i < 50; i++)
{
    int index = i; 
    dispatcher.AddTask(() =>
    {
        var data = new Dictionary<string, object>
        {
            ["Name"] = $"Person #{index}",
            ["Age"] = 20 + index % 10,
            ["Email"] = $"person{index}@example.com",
        };

        var person = (Person)TypeFactory.CreateAndFill(typeof(Person), data);

       
        Thread.Sleep(50);

        lock (listLock)
        {
            results.Add(person);
        }
    });
}

dispatcher.Run();

Console.WriteLine($"Создано кол-во объектов: {results.Count}");
Console.WriteLine($"Максимум одновременно выполнявшихся задач: {dispatcher.MaxObservedConcurrency}");
Console.WriteLine("Примеры рез-ов:");
foreach (Person person in results.OrderBy(p => p.Name).Take(3))
    Console.WriteLine($"  {person}");
