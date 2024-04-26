//using OpenAI;
//using System.ClientModel;
//using System.ClientModel.Primitives;

//namespace AzureOpenAI;

//public static class ChatClientExtensions
//{
//    // Expose overloads of protocol methods as extensions to the base client
//    public static ClientResult CreateChatCompletion(this Chat client, string model, BinaryContent content, RequestOptions options = default)
//    {
//        if (client is not AzureChatClient azureClient)
//        {
//            throw new NotSupportedException("Cannot call CreateChatCompletion with 'model' parameter when not using the Azure OpeAI client.");
//        }

//        return azureClient.CreateChatCompletion(model, content, options);
//    }

//    public static async Task<ClientResult> CreateChatCompletionAsync(this Chat client, string model, BinaryContent content, RequestOptions options = default)
//    {
//        if (client is not AzureChatClient azureClient)
//        {
//            throw new NotSupportedException("Cannot call CreateChatCompletionAsync with 'model' parameter when not using the Azure OpeAI client.");
//        }

//        return await azureClient.CreateChatCompletionAsync(model, content, options).ConfigureAwait(false);
//    }
//}
