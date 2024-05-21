// <auto-generated/>

#nullable disable

using System;
using System.ComponentModel;

namespace OpenAI.Images
{
    /// <summary> The CreateImageEditRequestModel. </summary>
    internal readonly partial struct InternalCreateImageEditRequestModel : IEquatable<InternalCreateImageEditRequestModel>
    {
        private readonly string _value;

        /// <summary> Initializes a new instance of <see cref="InternalCreateImageEditRequestModel"/>. </summary>
        /// <exception cref="ArgumentNullException"> <paramref name="value"/> is null. </exception>
        public InternalCreateImageEditRequestModel(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private const string DallE2Value = "dall-e-2";

        /// <summary> dall-e-2. </summary>
        public static InternalCreateImageEditRequestModel DallE2 { get; } = new InternalCreateImageEditRequestModel(DallE2Value);
        /// <summary> Determines if two <see cref="InternalCreateImageEditRequestModel"/> values are the same. </summary>
        public static bool operator ==(InternalCreateImageEditRequestModel left, InternalCreateImageEditRequestModel right) => left.Equals(right);
        /// <summary> Determines if two <see cref="InternalCreateImageEditRequestModel"/> values are not the same. </summary>
        public static bool operator !=(InternalCreateImageEditRequestModel left, InternalCreateImageEditRequestModel right) => !left.Equals(right);
        /// <summary> Converts a string to a <see cref="InternalCreateImageEditRequestModel"/>. </summary>
        public static implicit operator InternalCreateImageEditRequestModel(string value) => new InternalCreateImageEditRequestModel(value);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is InternalCreateImageEditRequestModel other && Equals(other);
        /// <inheritdoc />
        public bool Equals(InternalCreateImageEditRequestModel other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;
        /// <inheritdoc />
        public override string ToString() => _value;
    }
}
