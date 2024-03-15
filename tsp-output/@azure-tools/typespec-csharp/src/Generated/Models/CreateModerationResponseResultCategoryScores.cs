// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;

namespace OpenAI.Models
{
    /// <summary> The CreateModerationResponseResultCategoryScores. </summary>
    public partial class CreateModerationResponseResultCategoryScores
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

        /// <summary> Initializes a new instance of <see cref="CreateModerationResponseResultCategoryScores"/>. </summary>
        /// <param name="hate"> The score for the category 'hate'. </param>
        /// <param name="hateThreatening"> The score for the category 'hate/threatening'. </param>
        /// <param name="harassment"> The score for the category 'harassment'. </param>
        /// <param name="harassmentThreatening"> The score for the category 'harassment/threatening'. </param>
        /// <param name="selfHarm"> The score for the category 'self-harm'. </param>
        /// <param name="selfHarmIntent"> The score for the category 'self-harm/intent'. </param>
        /// <param name="selfHarmInstructions"> The score for the category 'self-harm/instructive'. </param>
        /// <param name="sexual"> The score for the category 'sexual'. </param>
        /// <param name="sexualMinors"> The score for the category 'sexual/minors'. </param>
        /// <param name="violence"> The score for the category 'violence'. </param>
        /// <param name="violenceGraphic"> The score for the category 'violence/graphic'. </param>
        internal CreateModerationResponseResultCategoryScores(double hate, double hateThreatening, double harassment, double harassmentThreatening, double selfHarm, double selfHarmIntent, double selfHarmInstructions, double sexual, double sexualMinors, double violence, double violenceGraphic)
        {
            Hate = hate;
            HateThreatening = hateThreatening;
            Harassment = harassment;
            HarassmentThreatening = harassmentThreatening;
            SelfHarm = selfHarm;
            SelfHarmIntent = selfHarmIntent;
            SelfHarmInstructions = selfHarmInstructions;
            Sexual = sexual;
            SexualMinors = sexualMinors;
            Violence = violence;
            ViolenceGraphic = violenceGraphic;
        }

        /// <summary> Initializes a new instance of <see cref="CreateModerationResponseResultCategoryScores"/>. </summary>
        /// <param name="hate"> The score for the category 'hate'. </param>
        /// <param name="hateThreatening"> The score for the category 'hate/threatening'. </param>
        /// <param name="harassment"> The score for the category 'harassment'. </param>
        /// <param name="harassmentThreatening"> The score for the category 'harassment/threatening'. </param>
        /// <param name="selfHarm"> The score for the category 'self-harm'. </param>
        /// <param name="selfHarmIntent"> The score for the category 'self-harm/intent'. </param>
        /// <param name="selfHarmInstructions"> The score for the category 'self-harm/instructive'. </param>
        /// <param name="sexual"> The score for the category 'sexual'. </param>
        /// <param name="sexualMinors"> The score for the category 'sexual/minors'. </param>
        /// <param name="violence"> The score for the category 'violence'. </param>
        /// <param name="violenceGraphic"> The score for the category 'violence/graphic'. </param>
        /// <param name="serializedAdditionalRawData"> Keeps track of any properties unknown to the library. </param>
        internal CreateModerationResponseResultCategoryScores(double hate, double hateThreatening, double harassment, double harassmentThreatening, double selfHarm, double selfHarmIntent, double selfHarmInstructions, double sexual, double sexualMinors, double violence, double violenceGraphic, IDictionary<string, BinaryData> serializedAdditionalRawData)
        {
            Hate = hate;
            HateThreatening = hateThreatening;
            Harassment = harassment;
            HarassmentThreatening = harassmentThreatening;
            SelfHarm = selfHarm;
            SelfHarmIntent = selfHarmIntent;
            SelfHarmInstructions = selfHarmInstructions;
            Sexual = sexual;
            SexualMinors = sexualMinors;
            Violence = violence;
            ViolenceGraphic = violenceGraphic;
            _serializedAdditionalRawData = serializedAdditionalRawData;
        }

        /// <summary> Initializes a new instance of <see cref="CreateModerationResponseResultCategoryScores"/> for deserialization. </summary>
        internal CreateModerationResponseResultCategoryScores()
        {
        }

        /// <summary> The score for the category 'hate'. </summary>
        public double Hate { get; }
        /// <summary> The score for the category 'hate/threatening'. </summary>
        public double HateThreatening { get; }
        /// <summary> The score for the category 'harassment'. </summary>
        public double Harassment { get; }
        /// <summary> The score for the category 'harassment/threatening'. </summary>
        public double HarassmentThreatening { get; }
        /// <summary> The score for the category 'self-harm'. </summary>
        public double SelfHarm { get; }
        /// <summary> The score for the category 'self-harm/intent'. </summary>
        public double SelfHarmIntent { get; }
        /// <summary> The score for the category 'self-harm/instructive'. </summary>
        public double SelfHarmInstructions { get; }
        /// <summary> The score for the category 'sexual'. </summary>
        public double Sexual { get; }
        /// <summary> The score for the category 'sexual/minors'. </summary>
        public double SexualMinors { get; }
        /// <summary> The score for the category 'violence'. </summary>
        public double Violence { get; }
        /// <summary> The score for the category 'violence/graphic'. </summary>
        public double ViolenceGraphic { get; }
    }
}
