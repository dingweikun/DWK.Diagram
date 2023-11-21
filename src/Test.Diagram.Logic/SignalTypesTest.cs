using DWK.Diagram.Logic;
using System.Text.Json;

namespace Test.Diagram.Logic;

public class SignalTypesTest
{
    [Fact]
    public void JsonSerializeTest()
    {
        Assert.Equal($"\"{nameof(SignalTypes.Any)}\"", JsonSerializer.Serialize(SignalTypes.Any));
        Assert.Equal($"\"{nameof(SignalTypes.Analog)}\"", JsonSerializer.Serialize(SignalTypes.Analog));
        Assert.Equal($"\"{nameof(SignalTypes.Digital)}\"", JsonSerializer.Serialize(SignalTypes.Digital));
    }

    [Fact]
    public void JsonDeserializeTest()
    {
        Assert.Equal(SignalTypes.Any, JsonSerializer.Deserialize<SignalTypes>($"\"{SignalTypes.Any}\""));
        Assert.Equal(SignalTypes.Analog, JsonSerializer.Deserialize<SignalTypes>($"\"{SignalTypes.Analog}\""));
        Assert.Equal(SignalTypes.Digital, JsonSerializer.Deserialize<SignalTypes>($"\"{SignalTypes.Digital}\""));
    }
}