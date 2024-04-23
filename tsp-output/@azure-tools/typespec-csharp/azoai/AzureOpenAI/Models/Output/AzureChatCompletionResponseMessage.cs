//using OpenAI.Models;
//using System.ClientModel.Primitives;
//using System.Diagnostics;
//using System.Text.Json;

//namespace AzureOpenAI.Models;

//internal class AzureChatCompletionResponseMessage : JsonModel<AzureChatCompletionResponseMessage>
//{
//    private readonly ChatCompletionResponseMessage _message;
//    private readonly AzureChatExtensionsMessageContext? _azureContext;

//    public AzureChatCompletionResponseMessage(ChatCompletionResponseMessage message)
//    {
//        _message = message;

//        if (message is not IJsonModel model)
//        {
//            throw new InvalidOperationException("TODO");
//        }

//        if (model.AdditionalProperties.TryGetValue("context", out object? value))
//        {
//            Debug.Assert(value is AzureChatExtensionsMessageContext);

//            _azureContext = value as AzureChatExtensionsMessageContext;
//        }
//    }

//    protected override AzureChatCompletionResponseMessage CreateCore(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
//    {
//    }

//    protected override void WriteCore(Utf8JsonWriter writer, ModelReaderWriterOptions options)
//    {
//        throw new NotSupportedException("TODO");
//    }
//}
