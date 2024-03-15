// <auto-generated/>

#nullable disable

using System;
using System.ComponentModel;

namespace OpenAI.Models
{
    /// <summary> The DeleteAssistantFileResponse_object. </summary>
    public readonly partial struct DeleteAssistantFileResponseObject : IEquatable<DeleteAssistantFileResponseObject>
    {
        private readonly string _value;

        /// <summary> Initializes a new instance of <see cref="DeleteAssistantFileResponseObject"/>. </summary>
        /// <exception cref="ArgumentNullException"> <paramref name="value"/> is null. </exception>
        public DeleteAssistantFileResponseObject(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private const string AssistantFileDeletedValue = "assistant.file.deleted";

        /// <summary> assistant.file.deleted. </summary>
        public static DeleteAssistantFileResponseObject AssistantFileDeleted { get; } = new DeleteAssistantFileResponseObject(AssistantFileDeletedValue);
        /// <summary> Determines if two <see cref="DeleteAssistantFileResponseObject"/> values are the same. </summary>
        public static bool operator ==(DeleteAssistantFileResponseObject left, DeleteAssistantFileResponseObject right) => left.Equals(right);
        /// <summary> Determines if two <see cref="DeleteAssistantFileResponseObject"/> values are not the same. </summary>
        public static bool operator !=(DeleteAssistantFileResponseObject left, DeleteAssistantFileResponseObject right) => !left.Equals(right);
        /// <summary> Converts a string to a <see cref="DeleteAssistantFileResponseObject"/>. </summary>
        public static implicit operator DeleteAssistantFileResponseObject(string value) => new DeleteAssistantFileResponseObject(value);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is DeleteAssistantFileResponseObject other && Equals(other);
        /// <inheritdoc />
        public bool Equals(DeleteAssistantFileResponseObject other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;
        /// <inheritdoc />
        public override string ToString() => _value;
    }
}
