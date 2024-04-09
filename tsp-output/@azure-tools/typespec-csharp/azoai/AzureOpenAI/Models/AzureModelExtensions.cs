using OpenAI.Models;
using System.Text.Json;

namespace AzureOpenAI.Models;

internal static class AzureModelExtensions
{
    // TODO: return type for collection?
    public static IList<AzureChatExtensionConfiguration>? GetDataSources(this CreateChatCompletionRequest request)
    {
        if (!request.AzureProperties.TryGetValue("data_sources", out BinaryData? value))
        {
            return null;
        }

        // TODO: Notice that the generator would create this using
        // the same serialization/deserialization code they would typically
        // put into a model.

        if (property.NameEquals("data_sources"u8))
        {
            if (property.Value.ValueKind == JsonValueKind.Null)
            {
                continue;
            }
            List<AzureChatExtensionConfiguration> array = new List<AzureChatExtensionConfiguration>();
            foreach (var item in property.Value.EnumerateArray())
            {
                array.Add(AzureChatExtensionConfiguration.DeserializeAzureChatExtensionConfiguration(item, options));
            }
            dataSources = array;
            continue;
        }
    }
}
