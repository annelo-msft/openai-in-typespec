using Azure;
using System;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureOpenAI;

internal static class AzureCoreExtensions
{
    public static ApiKeyCredential ToApiKeyCredential(this AzureKeyCredential credential)
    {
        throw new NotImplementedException();
    }
}
