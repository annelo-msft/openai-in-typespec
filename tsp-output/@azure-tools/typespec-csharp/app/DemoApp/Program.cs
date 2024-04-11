using OpenAI;
using OpenAI.Models;
using System.ClientModel;

using AzureOpenAI.Models;
using AzureOpenAI;

Console.WriteLine("Hello, World!");

CallUnbrandedService();
CallAzureService();

void CallUnbrandedService()
{
    // <Third party>
    string apiKey = Environment.GetEnvironmentVariable("OPENAI_KEY")!;

    OpenAIClient client = new(new ApiKeyCredential(apiKey));
    // </Third party>

    Chat chatClient = client.GetChatClient();

    List<BinaryData> messages = new();
    var message = new
    {
        role = "user",
        content = "running over the same old ground"
    };
    messages.Add(BinaryData.FromObjectAsJson(message));

    CreateChatCompletionRequest request = new(messages, CreateChatCompletionRequestModel.Gpt35Turbo);

    ClientResult<CreateChatCompletionResponse> result = chatClient.CreateChatCompletion(request);
    CreateChatCompletionResponse completion = result.Value;

    // TODO: Do something with output
}

void CallAzureService()
{
    // <Azure>
    Uri endpoint = new("https://annelo-openai-01.openai.azure.com/");
    string apiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_KEY")!;

    AzureOpenAIClient client = new(endpoint, new ApiKeyCredential(apiKey));
    // </Azure>

    Chat chatClient = client.GetChatClient();

    List<BinaryData> messages = new();
    var message = new
    {
        role = "user",
        content = "running over the same old ground"
    };
    messages.Add(BinaryData.FromObjectAsJson(message));

    CreateChatCompletionRequest request = new(messages, CreateChatCompletionRequestModel.Gpt35Turbo);

    // <Azure>
    // Add Azure input property via extension methods
    AzureSearchChatExtensionParameters searchParams = new(new Uri("https://azure.search.com"), "MySearchIndex");
    AzureSearchChatExtensionConfiguration dataSource = new(searchParams);
    request.SetAzureDataSource(dataSource);
    // </Azure>

    ClientResult<CreateChatCompletionResponse> result = chatClient.CreateChatCompletion(request);
    CreateChatCompletionResponse completion = result.Value;

    // <Azure>
    // Use Azure output property via extension methods
    AzureChatExtensionsMessageContext? azureContext = completion.Choices[0].Message.GetAzureExtensionsContext();

    if (azureContext is not null && azureContext.Citations.Count > 0)
    {
        // TODO: Do something with output
    }
    // </Azure>
}
