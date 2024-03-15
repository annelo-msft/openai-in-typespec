// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;
using OpenAI.Emitted;

namespace OpenAI.Models
{
    /// <summary> The CreateSpeechRequest. </summary>
    public partial class CreateSpeechRequest
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

        /// <summary> Initializes a new instance of <see cref="CreateSpeechRequest"/>. </summary>
        /// <param name="model"> One of the available [TTS models](/docs/models/tts): `tts-1` or `tts-1-hd`. </param>
        /// <param name="input"> The text to generate audio for. The maximum length is 4096 characters. </param>
        /// <param name="voice">
        /// The voice to use when generating the audio. Supported voices are `alloy`, `echo`, `fable`,
        /// `onyx`, `nova`, and `shimmer`. Previews of the voices are available in the
        /// [Text to speech guide](/docs/guides/text-to-speech/voice-options).
        /// </param>
        /// <exception cref="ArgumentNullException"> <paramref name="input"/> is null. </exception>
        public CreateSpeechRequest(CreateSpeechRequestModel model, string input, CreateSpeechRequestVoice voice)
        {
            Argument.AssertNotNull(input, nameof(input));

            Model = model;
            Input = input;
            Voice = voice;
        }

        /// <summary> Initializes a new instance of <see cref="CreateSpeechRequest"/>. </summary>
        /// <param name="model"> One of the available [TTS models](/docs/models/tts): `tts-1` or `tts-1-hd`. </param>
        /// <param name="input"> The text to generate audio for. The maximum length is 4096 characters. </param>
        /// <param name="voice">
        /// The voice to use when generating the audio. Supported voices are `alloy`, `echo`, `fable`,
        /// `onyx`, `nova`, and `shimmer`. Previews of the voices are available in the
        /// [Text to speech guide](/docs/guides/text-to-speech/voice-options).
        /// </param>
        /// <param name="responseFormat"> The format to audio in. Supported formats are `mp3`, `opus`, `aac`, and `flac`. </param>
        /// <param name="speed"> The speed of the generated audio. Select a value from `0.25` to `4.0`. `1.0` is the default. </param>
        /// <param name="serializedAdditionalRawData"> Keeps track of any properties unknown to the library. </param>
        internal CreateSpeechRequest(CreateSpeechRequestModel model, string input, CreateSpeechRequestVoice voice, CreateSpeechRequestResponseFormat? responseFormat, double? speed, IDictionary<string, BinaryData> serializedAdditionalRawData)
        {
            Model = model;
            Input = input;
            Voice = voice;
            ResponseFormat = responseFormat;
            Speed = speed;
            _serializedAdditionalRawData = serializedAdditionalRawData;
        }

        /// <summary> Initializes a new instance of <see cref="CreateSpeechRequest"/> for deserialization. </summary>
        internal CreateSpeechRequest()
        {
        }

        /// <summary> One of the available [TTS models](/docs/models/tts): `tts-1` or `tts-1-hd`. </summary>
        public CreateSpeechRequestModel Model { get; }
        /// <summary> The text to generate audio for. The maximum length is 4096 characters. </summary>
        public string Input { get; }
        /// <summary>
        /// The voice to use when generating the audio. Supported voices are `alloy`, `echo`, `fable`,
        /// `onyx`, `nova`, and `shimmer`. Previews of the voices are available in the
        /// [Text to speech guide](/docs/guides/text-to-speech/voice-options).
        /// </summary>
        public CreateSpeechRequestVoice Voice { get; }
        /// <summary> The format to audio in. Supported formats are `mp3`, `opus`, `aac`, and `flac`. </summary>
        public CreateSpeechRequestResponseFormat? ResponseFormat { get; set; }
        /// <summary> The speed of the generated audio. Select a value from `0.25` to `4.0`. `1.0` is the default. </summary>
        public double? Speed { get; set; }
    }
}
