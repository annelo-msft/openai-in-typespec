using OpenAI;
using OpenAI.Chat;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureOpenAI;

internal class AzureChatClient : ChatClient
{
    public AzureChatClient(string model, ApiKeyCredential credential = null, OpenAIClientOptions options = null) : base(model, credential, options)
    {
    }
}
