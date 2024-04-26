//using OpenAI;
//using System.ClientModel;

//namespace AzureOpenAI;

//public class AzureOpenAIClient : OpenAIClient
//{
//    private readonly string _apiVersion;
//    private readonly ApiKeyCredential _apiKeyCredential;
//    private readonly Uri _endpoint;

//    public AzureOpenAIClient(Uri endpoint, ApiKeyCredential credential, AzureOpenAIClientOptions? options = default)
//        : base(endpoint, credential, options)
//    {
//        options ??= new AzureOpenAIClientOptions();

//        _apiVersion = options.ApiVersion;
//        _endpoint = endpoint;
//        _apiKeyCredential = credential;
//    }

//    public override Chat GetChatClient()
//    {
//        return new AzureChatClient(Pipeline, _apiKeyCredential, _endpoint, _apiVersion);
//    }
//}
