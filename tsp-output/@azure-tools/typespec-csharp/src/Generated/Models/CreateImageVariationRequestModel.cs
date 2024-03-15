// <auto-generated/>

#nullable disable

using System;
using System.ComponentModel;

namespace OpenAI.Models
{
    /// <summary> Enum for model in CreateImageVariationRequest. </summary>
    public readonly partial struct CreateImageVariationRequestModel : IEquatable<CreateImageVariationRequestModel>
    {
        private readonly string _value;

        /// <summary> Initializes a new instance of <see cref="CreateImageVariationRequestModel"/>. </summary>
        /// <exception cref="ArgumentNullException"> <paramref name="value"/> is null. </exception>
        public CreateImageVariationRequestModel(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private const string DallE2Value = "dall-e-2";

        /// <summary> dall-e-2. </summary>
        public static CreateImageVariationRequestModel DallE2 { get; } = new CreateImageVariationRequestModel(DallE2Value);
        /// <summary> Determines if two <see cref="CreateImageVariationRequestModel"/> values are the same. </summary>
        public static bool operator ==(CreateImageVariationRequestModel left, CreateImageVariationRequestModel right) => left.Equals(right);
        /// <summary> Determines if two <see cref="CreateImageVariationRequestModel"/> values are not the same. </summary>
        public static bool operator !=(CreateImageVariationRequestModel left, CreateImageVariationRequestModel right) => !left.Equals(right);
        /// <summary> Converts a string to a <see cref="CreateImageVariationRequestModel"/>. </summary>
        public static implicit operator CreateImageVariationRequestModel(string value) => new CreateImageVariationRequestModel(value);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is CreateImageVariationRequestModel other && Equals(other);
        /// <inheritdoc />
        public bool Equals(CreateImageVariationRequestModel other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;
        /// <inheritdoc />
        public override string ToString() => _value;
    }
}
