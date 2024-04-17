using OpenAI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AzureOpenAI.Models;

public static class CreateChatCompletionRequestExtensions
{
    // Input model property
    // Note: on the input-side, we are unable to type-check that we're using an Azure
    // model, because the end-user created an instance of an unbranded model.  We'll
    // later convert this to an Azure model that can serialize to the Azure format,
    // but for now, we need to stash the values in a collection of objects that we'll
    // use to populate the Azure model properties later.
    public static IList<AzureChatExtensionConfiguration> GetDataSources(this CreateChatCompletionRequest request)
    {
        // TODO: How can we validate that this is being called in the right context,
        // e.g. user is using an Azure client instance and not an unbranded one?

        // TODO: What is the interplay of SerializedAdditionalRawData and 
        // AdditionalTypedProperties?  i.e. in a round-trip scenario, if we
        // wanted to mutate a SeriazliedAdditionalRawData, do we deserialize it
        // and stick it in AdditionalTypedProperties (are these "deserialized
        // additional properties, e.g.?).

        // TODO: what is our concurrency story for models in unbranded clients?
        // Are we happy with taking the Azure client stance of "models don't need
        // to be thread-safe because they are rarely shared between threads"? 
        JsonModelList<AzureChatExtensionConfiguration> dataSources;

        if (request.SerializedAdditionalRawData.TryGetValue("data_sources", out object? value))
        {
            Debug.Assert(value is JsonModelList<AzureChatExtensionConfiguration>);

            dataSources = (value as JsonModelList<AzureChatExtensionConfiguration>)!;
        }
        else
        {
            dataSources = [];
            request.SerializedAdditionalRawData.Add("data_sources", dataSources);
        }

        return dataSources;
    }
}
