using ConsoleApp1;
using System.Text.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        var def = new PortDef();
        var json = JsonSerializer.Serialize(def);
        Console.WriteLine(json);

        var dd = JsonSerializer.Deserialize<PortDef>(json);
        Console.WriteLine(dd == def);

        //--------------------------

        var dc = File.ReadAllText("NodeDef.json");
        var df = JsonSerializer.Deserialize<LogicNodeDef>(dc);

        var doc = File.ReadAllText("NodeDefs.json");
        var list = JsonSerializer.Deserialize<List<LogicNodeDef>>(doc);

        Console.WriteLine(list);
        Console.WriteLine("Hello, World!");

        Console.WriteLine("Hello, World!");
    }
}