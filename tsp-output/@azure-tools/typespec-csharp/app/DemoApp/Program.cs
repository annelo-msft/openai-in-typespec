using OpenAI;
using OpenAI.Models;
using System.ClientModel;

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

CreateChatCompletionRequest request = new CreateChatCompletionRequest(messages, CreateChatCompletionRequestModel.Gpt35Turbo);

ClientResult<CreateChatCompletionResponse> result = chatClient.CreateChatCompletion(request);

//AzureOpenAIClient client = new AzureOpenAIClient(endpoint, new ApiKeyCredential(apiKey));
//Chat chatClient = client.GetChatClient("gpt-35-turbo-instruct");

// TODO: Here.  What is the convenience model story?
// Should the Azure client return Response<ChatCompletion>?
//ClientResult<ChatCompletion> result = await chatClient.CompleteChatAsync()

// TODO: How do we reconcile returning Response from Azure client protocol methods
// and ClientResult from unbranded client protocol methods?

// TODO: MRW story - input model and output models too.

//Client

//OpenAIClient client = new()