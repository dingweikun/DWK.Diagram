using System.Text.Json;

namespace ConsoleApp1;

//-------------------------------------------------------------------------------------------------------------------------

public static class MockDatas
{
    public static IEnumerable<LogicNodeDef>? LoadNodeDefs()
    {
        var doc = File.ReadAllText("NodeDefs.json");
        var list = JsonSerializer.Deserialize<List<LogicNodeDef>>(doc);

        return list;
    }

}
