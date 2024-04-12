using AzureOpenAI;
using AzureOpenAI.Models;
using ClientModel.Tests.Mocks;
using OpenAI;
using OpenAI.Models;
using System.ClientModel;
using System.ClientModel.Primitives;

Console.WriteLine("Hello, World!");

//CallUnbrandedService();
CallAzureService();

void CallUnbrandedService()
{
    // <Third party>
    string apiKey = Environment.GetEnvironmentVariable("OPENAI_KEY")!;

    OpenAIClient client = new(new Uri("https://www.mock-oai.com"), new ApiKeyCredential(apiKey), GetUnbrandedClientOptions());
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

OpenAIClientOptions GetUnbrandedClientOptions()
{
    OpenAIClientOptions options = new()
    {
        Transport = new MockPipelineTransport("Transport", i => (200, BinaryData.FromString(
            """
            {
              "id": "chatcmpl-7R1nGnsXO8n4oi9UPz2f3UHdgAYMn",
              "created": 1686676106,
              "choices": [
                {
                  "index": 0,
                  "finish_reason": "stop",
                  "message": {
                    "role": "assistant",
                    "content": "Ahoy matey! So ye be wantin' to care for a fine squawkin' parrot, eh? Well, shiver me timbers, let ol' Cap'n Assistant share some wisdom with ye! Here be the steps to keepin' yer parrot happy 'n healthy:\n\n1. Secure a sturdy cage: Yer parrot be needin' a comfortable place to lay anchor! Be sure ye get a sturdy cage, at least double the size of the bird's wingspan, with enough space to spread their wings, yarrrr!\n\n2. Perches 'n toys: Aye, parrots need perches of different sizes, shapes, 'n textures to keep their feet healthy. Also, a few toys be helpin' to keep them entertained 'n their minds stimulated, arrrh!\n\n3. Proper grub: Feed yer feathered friend a balanced diet of high-quality pellets, fruits, 'n veggies to keep 'em strong 'n healthy. Give 'em fresh water every day, or ye’ll have a scurvy bird on yer hands!\n\n4. Cleanliness: Swab their cage deck! Clean their cage on a regular basis: fresh water 'n food daily, the floor every couple of days, 'n a thorough scrubbing ev'ry few weeks, so the bird be livin' in a tidy haven, arrhh!\n\n5. Socialize 'n train: Parrots be a sociable lot, arrr! Exercise 'n interact with 'em daily to create a bond 'n maintain their mental 'n physical health. Train 'em with positive reinforcement, treat 'em kindly, yarrr!\n\n6. Proper rest: Yer parrot be needin' ’bout 10-12 hours o' sleep each night. Cover their cage 'n let them slumber in a dim, quiet quarter for a proper night's rest, ye scallywag!\n\n7. Keep a weather eye open for illness: Birds be hidin' their ailments, arrr! Be watchful for signs of sickness, such as lethargy, loss of appetite, puffin' up, or change in droppings, and make haste to a vet if need be.\n\n8. Provide fresh air 'n avoid toxins: Parrots be sensitive to draft and pollutants. Keep yer quarters well ventilated, but no drafts, arrr! Be mindful of toxins like Teflon fumes, candles, or air fresheners.\n\nSo there ye have it, me hearty! With proper care 'n commitment, yer parrot will be squawkin' \"Yo-ho-ho\" for many years to come! Good luck, sailor, and may the wind be at yer back!"
                  },
                  "logprobs": null
                }
              ],
              "usage": {
                "completion_tokens": 557,
                "prompt_tokens": 33,
                "total_tokens": 590
              }
            }
            """)))
    };

    return options;
}

void CallAzureService()
{
    // <Azure>
    Uri endpoint = new("https://annelo-openai-01.openai.azure.com/");
    string apiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_KEY")!;

    AzureOpenAIClient client = new(endpoint, new ApiKeyCredential(apiKey), GetAzureClientOptions());
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
    request.GetDataSources().Add(GetAzureSearchDataSource());

    // </Azure>

    ClientResult<CreateChatCompletionResponse> result = chatClient.CreateChatCompletion(request);
    CreateChatCompletionResponse completion = result.Value;

    // <Azure>
    // Use Azure output property via extension methods
    AzureChatExtensionsMessageContext? azureContext = completion.Choices[0].Message.GetAzureExtensionsContext();

    if (azureContext is not null && azureContext.Citations.Count > 0)
    {
        int i = 0;
        foreach (var citation in azureContext.Citations)
        {
            Console.WriteLine($"Citation {i++}: Content={citation.Content}, Uri={citation.Url}, Title={citation.Title}");
        }
    }
    // </Azure>
}

AzureSearchChatExtensionConfiguration GetAzureSearchDataSource()
{
    AzureSearchChatExtensionParameters searchParams = new(new Uri("https://azure.search.com"), "MySearchIndex");
    return new(searchParams);
}

AzureOpenAIClientOptions GetAzureClientOptions()
{
    MockPipelineTransport mockTransport = new("Transport", i => (200, BinaryData.FromString(
            """
            {
              "id": "chatcmpl-7R1nGnsXO8n4oi9UPz2f3UHdgAYMn",
              "created": 1686676106,
              "choices": [
                {
                  "index": 0,
                  "finish_reason": "stop",
                  "message": {
                    "role": "assistant",
                    "content": "Ahoy matey! So ye be wantin' to care for a fine squawkin' parrot, eh? Well, shiver me timbers, let ol' Cap'n Assistant share some wisdom with ye! Here be the steps to keepin' yer parrot happy 'n healthy:\n\n1. Secure a sturdy cage: Yer parrot be needin' a comfortable place to lay anchor! Be sure ye get a sturdy cage, at least double the size of the bird's wingspan, with enough space to spread their wings, yarrrr!\n\n2. Perches 'n toys: Aye, parrots need perches of different sizes, shapes, 'n textures to keep their feet healthy. Also, a few toys be helpin' to keep them entertained 'n their minds stimulated, arrrh!\n\n3. Proper grub: Feed yer feathered friend a balanced diet of high-quality pellets, fruits, 'n veggies to keep 'em strong 'n healthy. Give 'em fresh water every day, or ye’ll have a scurvy bird on yer hands!\n\n4. Cleanliness: Swab their cage deck! Clean their cage on a regular basis: fresh water 'n food daily, the floor every couple of days, 'n a thorough scrubbing ev'ry few weeks, so the bird be livin' in a tidy haven, arrhh!\n\n5. Socialize 'n train: Parrots be a sociable lot, arrr! Exercise 'n interact with 'em daily to create a bond 'n maintain their mental 'n physical health. Train 'em with positive reinforcement, treat 'em kindly, yarrr!\n\n6. Proper rest: Yer parrot be needin' ’bout 10-12 hours o' sleep each night. Cover their cage 'n let them slumber in a dim, quiet quarter for a proper night's rest, ye scallywag!\n\n7. Keep a weather eye open for illness: Birds be hidin' their ailments, arrr! Be watchful for signs of sickness, such as lethargy, loss of appetite, puffin' up, or change in droppings, and make haste to a vet if need be.\n\n8. Provide fresh air 'n avoid toxins: Parrots be sensitive to draft and pollutants. Keep yer quarters well ventilated, but no drafts, arrr! Be mindful of toxins like Teflon fumes, candles, or air fresheners.\n\nSo there ye have it, me hearty! With proper care 'n commitment, yer parrot will be squawkin' \"Yo-ho-ho\" for many years to come! Good luck, sailor, and may the wind be at yer back!",
                    "context": {
                      "citations": [
                        {
                          "content": "Content of the citation",
                          "url": "https://www.example.com",
                          "title": "Title of the citation",
                          "filepath": "path/to/file",
                          "chunk_id": "chunk-id"
                        }
                      ]
                    }
                  },
                  "logprobs": null
                }
              ],
              "usage": {
                "completion_tokens": 557,
                "prompt_tokens": 33,
                "total_tokens": 590
              }
            }
            """)));

    mockTransport.OnSendingRequest = (i, m) =>
    {
        Console.WriteLine("Request:");
        Console.WriteLine($"  Uri='{m.Request.Uri}'");
        Console.WriteLine($"  Content='{WriteAsString(m.Request.Content!)}'");
    };

    AzureOpenAIClientOptions options = new()
    {
        //Transport = mockTransport
    };

    return options;
}

string WriteAsString(BinaryContent content)
{
    MemoryStream stream = new();
    content.WriteTo(stream);
    stream.Position = 0;
    return BinaryData.FromStream(stream).ToString();
}