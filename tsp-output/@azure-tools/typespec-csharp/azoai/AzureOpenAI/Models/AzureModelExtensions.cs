using OpenAI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureOpenAI.Models;

internal static class AzureModelExtensions
{
    public static List<BinaryData>? GetDataSources(this CreateChatCompletionRequest request)
    {
        if (!request.AzureProperties.TryGetValue("data_sources", out BinaryData? value))
        {
            return null;
        }

        // TODO: Do something real
    }
}
