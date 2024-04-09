using OpenAI.Models;
using System.Diagnostics;

namespace AzureOpenAI.Models;

public static class AzureModelExtensions
{
    public static void SetAzureDataSource(this CreateChatCompletionRequest request, AzureChatExtensionConfiguration dataSource)
    {
        Argument.AssertNotNull(dataSource, nameof(dataSource));

        // TODO: we can do it slow via MRW.Write to BinaryData;
        // can we do it fast via MRW.Write to Utf8JsonWriter?

        // TODO: Does it matter to pass MRW Options here?
        //BinaryData serializedValue = ModelReaderWriter.Write(dataSource);

        // Note: One observation here is - working in the "BinaryData space" is 
        // different depending on input/output models.  We could make it all 
        // strongly-typed for input, but we'd have a lot of properties to [EBN].

        // If the BinaryData represents JSON, then we need to mutate the JSON
        // to add a collection, and this is expensive.

        // We could use MutableJsonDocument 😮, JsonNode, etc.
        // We could keep separate property bags for input and output types, i.e.
        // for different scenarios.
        // We could do something like PipelineMessage and hold an ArrayBackedPropertyBag

        // For now: Let's have serialized raw data and strongly-typed additional properties.
        // This lets us wait to serialize them until we can do it in an optimal fashion.

        // Add the list if it doesn't already exist
        JsonModelList<AzureChatExtensionConfiguration> dataSources;

        if (request.AdditionalTypedProperties.TryGetValue("data_sources", out object? value))
        {
            Debug.Assert(value is JsonModelList<AzureChatExtensionConfiguration>);

            dataSources = (value as JsonModelList<AzureChatExtensionConfiguration>)!;
        }
        else
        {
            dataSources = [];
            request.AdditionalTypedProperties.Add("data_sources", dataSources);
        }

        dataSources.Add(dataSource);
    }

    //// TODO: return type for collection?
    //public static IList<AzureChatExtensionConfiguration>? GetDataSources(this CreateChatCompletionRequest request)
    //{
    //    if (!request.AzureProperties.TryGetValue("data_sources", out BinaryData? value))
    //    {
    //        return null;
    //    }

    //    // TODO: Notice that the generator would create this using
    //    // the same serialization/deserialization code they would typically
    //    // put into a model.

    //    if (property.NameEquals("data_sources"u8))
    //    {
    //        if (property.Value.ValueKind == JsonValueKind.Null)
    //        {
    //            continue;
    //        }
    //        List<AzureChatExtensionConfiguration> array = new List<AzureChatExtensionConfiguration>();
    //        foreach (var item in property.Value.EnumerateArray())
    //        {
    //            array.Add(AzureChatExtensionConfiguration.DeserializeAzureChatExtensionConfiguration(item, options));
    //        }
    //        dataSources = array;
    //        continue;
    //    }
    //}
}
