using OpenAI;
using OpenAI.Models;
using System.ClientModel;

using AzureOpenAI.Models;

Console.WriteLine("Hello, World!");

//Uri endpoint = new("https://annelo-openai-01.openai.azure.com/");
//string apiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_KEY")!;

string apiKey = Environment.GetEnvironmentVariable("OPENAI_KEY")!;

OpenAIClient client = new OpenAIClient(new ApiKeyCredential(apiKey));
Chat chatClient = client.GetChatClient();

List<BinaryData> messages  = new List<BinaryData>();
var message = new
{
    role = "user",
    content = "running over the same old ground"
};
messages.Add(BinaryData.FromObjectAsJson(message));

// Add Azure property
AzureSearchChatExtensionParameters searchParams = new(new Uri("https://azure.search.com"), "MySearchIndex");
AzureSearchChatExtensionConfiguration dataSource = new(searchParams);

CreateChatCompletionRequest request = new CreateChatCompletionRequest(messages, CreateChatCompletionRequestModel.Gpt35Turbo);
request.SetAzureDataSource(dataSource);

ClientResult<CreateChatCompletionResponse> result = chatClient.CreateChatCompletion(request);

//AzureOpenAIClient client = new AzureOpenAIClient(endpoint, new ApiKeyCredential(apiKey));
//Chat chatClient = client.GetChatClient("gpt-35-turbo-instruct");

//ClientResult<ChatCompletion> result = await chatClient.CompleteChatAsync()

// TODO: MRW story - input model and output models too.
