// <auto-generated/>

#nullable disable

namespace OpenAI.Models
{
    public partial class CreateTranslationRequest
    {
        internal MultipartFormDataBinaryContent ToMultipartContent()
        {
            MultipartFormDataBinaryContent content = new();

            content.Add(File, "file", FileName);
            content.Add(Model.ToString(), "model");

            if (Prompt is not null)
            {
                content.Add(Prompt, "prompt");
            }

            if (ResponseFormat is not null)
            {
                content.Add(ResponseFormat.ToString(), "response_format");
            }

            if (Temperature is not null)
            {
                content.Add(Temperature.Value, "temperature");
            }

            return content;
        }
    }
}
