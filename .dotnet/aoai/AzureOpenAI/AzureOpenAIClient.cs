using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using OpenAI;
using OpenAI.Chat;

namespace AzureOpenAI;

public class AzureOpenAIClient : OpenAIClient
{
    private readonly Uri _endpoint;

    // TODO: Should we use Azure.Core pipeline here or ClientModel one?
    private readonly HttpPipeline _pipeline;

    private readonly AzureKeyCredential _keyCredential;
    private const string AuthorizationHeader = "api-key";

    private readonly OpenAIClientOptions _unbrandedClientOptions;

    public AzureOpenAIClient(Uri endpoint, AzureKeyCredential azureKeyCredential, AzureOpenAIClientOptions? options = default)
    {
        options ??= new();

        _endpoint = endpoint;
        _keyCredential = azureKeyCredential;
        _pipeline = HttpPipelineBuilder.Build(options, Array.Empty<HttpPipelinePolicy>(), new HttpPipelinePolicy[] { new AzureKeyCredentialPolicy(_keyCredential, AuthorizationHeader) }, new ResponseClassifier());

        // TODO: This is not the model for Azure clients because we can't freeze ClientOptions.
        // Should we just create the ClientPipelineOptions right away to freeze them?
        _unbrandedClientOptions = options.ToOpenAIClientOptions();
    }

    public override ChatClient GetChatClient(string model)
    {
        // TODO: Azure.Core 2.0: could we pass AzureKeyCredential for ApiKeyCredential?
        // TODO: Conversion across ClientOptions?
        return new AzureChatClient(model, _keyCredential.ToApiKeyCredential(), _unbrandedClientOptions);
    }
}
