// <auto-generated/>

using System;
using System.Collections.Generic;
using OpenAI;

namespace OpenAI.Internal.Models
{
    /// <summary> A list of [Files](/docs/api-reference/files) attached to an `assistant`. </summary>
    internal partial class AssistantFileObject
    {
        /// <summary>
        /// Keeps track of any properties unknown to the library.
        /// <para>
        /// To assign an object to the value of this property use <see cref="BinaryData.FromObjectAsJson{T}(T, System.Text.Json.JsonSerializerOptions?)"/>.
        /// </para>
        /// <para>
        /// To assign an already formatted json string to this property use <see cref="BinaryData.FromString(string)"/>.
        /// </para>
        /// <para>
        /// Examples:
        /// <list type="bullet">
        /// <item>
        /// <term>BinaryData.FromObjectAsJson("foo")</term>
        /// <description>Creates a payload of "foo".</description>
        /// </item>
        /// <item>
        /// <term>BinaryData.FromString("\"foo\"")</term>
        /// <description>Creates a payload of "foo".</description>
        /// </item>
        /// <item>
        /// <term>BinaryData.FromObjectAsJson(new { key = "value" })</term>
        /// <description>Creates a payload of { "key": "value" }.</description>
        /// </item>
        /// <item>
        /// <term>BinaryData.FromString("{\"key\": \"value\"}")</term>
        /// <description>Creates a payload of { "key": "value" }.</description>
        /// </item>
        /// </list>
        /// </para>
        /// </summary>
        private IDictionary<string, BinaryData> _serializedAdditionalRawData;

        /// <summary> Initializes a new instance of <see cref="AssistantFileObject"/>. </summary>
        /// <param name="id"> The identifier, which can be referenced in API endpoints. </param>
        /// <param name="createdAt"> The Unix timestamp (in seconds) for when the assistant file was created. </param>
        /// <param name="assistantId"> The assistant ID that the file is attached to. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="id"/> or <paramref name="assistantId"/> is null. </exception>
        internal AssistantFileObject(string id, DateTimeOffset createdAt, string assistantId)
        {
            Argument.AssertNotNull(id, nameof(id));
            Argument.AssertNotNull(assistantId, nameof(assistantId));

            Id = id;
            CreatedAt = createdAt;
            AssistantId = assistantId;
        }

        /// <summary> Initializes a new instance of <see cref="AssistantFileObject"/>. </summary>
        /// <param name="id"> The identifier, which can be referenced in API endpoints. </param>
        /// <param name="object"> The object type, which is always `assistant.file`. </param>
        /// <param name="createdAt"> The Unix timestamp (in seconds) for when the assistant file was created. </param>
        /// <param name="assistantId"> The assistant ID that the file is attached to. </param>
        /// <param name="serializedAdditionalRawData"> Keeps track of any properties unknown to the library. </param>
        internal AssistantFileObject(string id, AssistantFileObjectObject @object, DateTimeOffset createdAt, string assistantId, IDictionary<string, BinaryData> serializedAdditionalRawData)
        {
            Id = id;
            Object = @object;
            CreatedAt = createdAt;
            AssistantId = assistantId;
            _serializedAdditionalRawData = serializedAdditionalRawData;
        }

        /// <summary> Initializes a new instance of <see cref="AssistantFileObject"/> for deserialization. </summary>
        internal AssistantFileObject()
        {
        }

        /// <summary> The identifier, which can be referenced in API endpoints. </summary>
        public string Id { get; }
        /// <summary> The object type, which is always `assistant.file`. </summary>
        public AssistantFileObjectObject Object { get; } = AssistantFileObjectObject.AssistantFile;

        /// <summary> The Unix timestamp (in seconds) for when the assistant file was created. </summary>
        public DateTimeOffset CreatedAt { get; }
        /// <summary> The assistant ID that the file is attached to. </summary>
        public string AssistantId { get; }
    }
}
