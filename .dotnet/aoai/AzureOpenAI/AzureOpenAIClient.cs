using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using OpenAI;
using OpenAI.Chat;

namespace AzureOpenAI;

public class AzureOpenAIClient : OpenAIClient
{
    private readonly Uri _endpoint;
    private readonly HttpPipeline _pipeline;
    private readonly AzureKeyCredential _keyCredential;
    private const string AuthorizationHeader = "api-key";

    public AzureOpenAIClient(Uri endpoint, AzureKeyCredential azureKeyCredential, AzureOpenAIClientOptions? options = default)
    {
        options ??= new();

        _endpoint = endpoint;
        _keyCredential = azureKeyCredential;
        _pipeline = HttpPipelineBuilder.Build(options, Array.Empty<HttpPipelinePolicy>(), new HttpPipelinePolicy[] { new AzureKeyCredentialPolicy(_keyCredential, AuthorizationHeader) }, new ResponseClassifier());
    }

    public override ChatClient GetChatClient(string model)
    {
        return base.GetChatClient(model);
    }
}
