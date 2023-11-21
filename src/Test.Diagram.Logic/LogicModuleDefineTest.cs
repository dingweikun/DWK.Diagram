using DWK.Diagram.Logic;
using System.Text.Json;

namespace Test.Diagram.Logic;

public class LogicModuleDefineTest
{
    [Fact]
    public void JsonSerializeTest()
    {
        LogicModuleDefine def = new()
        {
            Category = "Add",
            Description = "Addition Module"
        };

        var json = JsonSerializer.Serialize<LogicModuleDefine>(def);
        var exp = JsonSerializer.Deserialize<LogicModuleDefine>(json);

        Assert.NotNull(exp);
        Assert.Equal(def, exp);
    }
}
