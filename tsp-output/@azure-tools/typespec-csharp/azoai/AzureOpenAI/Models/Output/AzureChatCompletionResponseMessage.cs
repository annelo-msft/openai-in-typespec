using OpenAI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureOpenAI.Models;

internal partial class AzureChatCompletionResponseMessage : ChatCompletionResponseMessage
{
    /// <summary>
    /// If Azure OpenAI chat extensions are configured, this array represents the incremental steps performed by those
    /// extensions while processing the chat completions request.
    /// </summary>
    public AzureChatExtensionsMessageContext? AzureExtensionsContext { get; }
}
