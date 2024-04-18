// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;

namespace OpenAI.Models
{
    /// <summary> The CreateChatCompletionResponseChoice. </summary>
    public partial class CreateChatCompletionResponseChoice
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

        /// <summary> Initializes a new instance of <see cref="CreateChatCompletionResponseChoice"/>. </summary>
        /// <param name="finishReason">
        /// The reason the model stopped generating tokens. This will be `stop` if the model hit a
        /// natural stop point or a provided stop sequence, `length` if the maximum number of tokens
        /// specified in the request was reached, `content_filter` if content was omitted due to a flag
        /// from our content filters, `tool_calls` if the model called a tool, or `function_call`
        /// (deprecated) if the model called a function.
        /// </param>
        /// <param name="index"> The index of the choice in the list of choices. </param>
        /// <param name="message"></param>
        /// <param name="logprobs"> Log probability information for the choice. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="message"/> is null. </exception>
        internal CreateChatCompletionResponseChoice(CreateChatCompletionResponseChoiceFinishReason finishReason, long index, ChatCompletionResponseMessage message, CreateChatCompletionResponseChoiceLogprobs logprobs)
        {
            Argument.AssertNotNull(message, nameof(message));

            FinishReason = finishReason;
            Index = index;
            Message = message;
            Logprobs = logprobs;
        }

        /// <summary> Initializes a new instance of <see cref="CreateChatCompletionResponseChoice"/>. </summary>
        /// <param name="finishReason">
        /// The reason the model stopped generating tokens. This will be `stop` if the model hit a
        /// natural stop point or a provided stop sequence, `length` if the maximum number of tokens
        /// specified in the request was reached, `content_filter` if content was omitted due to a flag
        /// from our content filters, `tool_calls` if the model called a tool, or `function_call`
        /// (deprecated) if the model called a function.
        /// </param>
        /// <param name="index"> The index of the choice in the list of choices. </param>
        /// <param name="message"></param>
        /// <param name="logprobs"> Log probability information for the choice. </param>
        /// <param name="serializedAdditionalRawData"> Keeps track of any properties unknown to the library. </param>
        internal CreateChatCompletionResponseChoice(CreateChatCompletionResponseChoiceFinishReason finishReason, long index, ChatCompletionResponseMessage message, CreateChatCompletionResponseChoiceLogprobs logprobs, IDictionary<string, BinaryData> serializedAdditionalRawData)
        {
            FinishReason = finishReason;
            Index = index;
            Message = message;
            Logprobs = logprobs;
            _serializedAdditionalRawData = serializedAdditionalRawData;
        }

        /// <summary> Initializes a new instance of <see cref="CreateChatCompletionResponseChoice"/> for deserialization. </summary>
        public CreateChatCompletionResponseChoice()
        {
        }

        /// <summary>
        /// The reason the model stopped generating tokens. This will be `stop` if the model hit a
        /// natural stop point or a provided stop sequence, `length` if the maximum number of tokens
        /// specified in the request was reached, `content_filter` if content was omitted due to a flag
        /// from our content filters, `tool_calls` if the model called a tool, or `function_call`
        /// (deprecated) if the model called a function.
        /// </summary>
        public CreateChatCompletionResponseChoiceFinishReason FinishReason { get; }
        /// <summary> The index of the choice in the list of choices. </summary>
        public long Index { get; }
        /// <summary> Gets the message. </summary>
        public ChatCompletionResponseMessage Message { get; }
        /// <summary> Log probability information for the choice. </summary>
        public CreateChatCompletionResponseChoiceLogprobs Logprobs { get; }
    }
}
