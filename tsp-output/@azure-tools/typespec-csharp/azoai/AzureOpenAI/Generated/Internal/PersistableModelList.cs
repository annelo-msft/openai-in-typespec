using System.ClientModel.Primitives;
using System.Text.Json;

namespace AzureOpenAI;

internal class PersistableModelList<TModel> : List<IPersistableModel<TModel>>, IPersistableModel<PersistableModelList<TModel>>
{
    public PersistableModelList<TModel> Create(BinaryData data, ModelReaderWriterOptions options)
    {
        throw new NotImplementedException();
    }

    public string GetFormatFromOptions(ModelReaderWriterOptions options)
    {
        throw new NotImplementedException();
    }

    public BinaryData Write(ModelReaderWriterOptions options)
    {
        throw new NotImplementedException();
    }
}

internal class JsonModelList<TModel> : List<IJsonModel<TModel>>, IJsonModel<JsonModelList<TModel>>
{
    public JsonModelList<TModel> Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
    {
        throw new NotImplementedException();
    }

    public JsonModelList<TModel> Create(BinaryData data, ModelReaderWriterOptions options)
    {
        throw new NotImplementedException();
    }

    public string GetFormatFromOptions(ModelReaderWriterOptions options)
    {
        throw new NotImplementedException();
    }

    public void Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
    {
        writer.WriteStartArray();

        foreach (IJsonModel<TModel> item in this)
        {
            item.Write(writer, options);
        }

        writer.WriteEndArray();
    }

    public BinaryData Write(ModelReaderWriterOptions options)
    {
        throw new NotImplementedException();
    }
}