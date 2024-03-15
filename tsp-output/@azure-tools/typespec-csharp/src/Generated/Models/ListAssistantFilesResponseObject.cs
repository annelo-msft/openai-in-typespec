// <auto-generated/>

#nullable disable

using System;
using System.ComponentModel;

namespace OpenAI.Models
{
    /// <summary> The ListAssistantFilesResponse_object. </summary>
    public readonly partial struct ListAssistantFilesResponseObject : IEquatable<ListAssistantFilesResponseObject>
    {
        private readonly string _value;

        /// <summary> Initializes a new instance of <see cref="ListAssistantFilesResponseObject"/>. </summary>
        /// <exception cref="ArgumentNullException"> <paramref name="value"/> is null. </exception>
        public ListAssistantFilesResponseObject(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private const string ListValue = "list";

        /// <summary> list. </summary>
        public static ListAssistantFilesResponseObject List { get; } = new ListAssistantFilesResponseObject(ListValue);
        /// <summary> Determines if two <see cref="ListAssistantFilesResponseObject"/> values are the same. </summary>
        public static bool operator ==(ListAssistantFilesResponseObject left, ListAssistantFilesResponseObject right) => left.Equals(right);
        /// <summary> Determines if two <see cref="ListAssistantFilesResponseObject"/> values are not the same. </summary>
        public static bool operator !=(ListAssistantFilesResponseObject left, ListAssistantFilesResponseObject right) => !left.Equals(right);
        /// <summary> Converts a string to a <see cref="ListAssistantFilesResponseObject"/>. </summary>
        public static implicit operator ListAssistantFilesResponseObject(string value) => new ListAssistantFilesResponseObject(value);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is ListAssistantFilesResponseObject other && Equals(other);
        /// <inheritdoc />
        public bool Equals(ListAssistantFilesResponseObject other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;
        /// <inheritdoc />
        public override string ToString() => _value;
    }
}
