// <auto-generated/>

#nullable disable

using System;
using System.ComponentModel;

namespace OpenAI.Models
{
    /// <summary> The DeleteThreadResponse_object. </summary>
    public readonly partial struct DeleteThreadResponseObject : IEquatable<DeleteThreadResponseObject>
    {
        private readonly string _value;

        /// <summary> Initializes a new instance of <see cref="DeleteThreadResponseObject"/>. </summary>
        /// <exception cref="ArgumentNullException"> <paramref name="value"/> is null. </exception>
        public DeleteThreadResponseObject(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private const string ThreadDeletedValue = "thread.deleted";

        /// <summary> thread.deleted. </summary>
        public static DeleteThreadResponseObject ThreadDeleted { get; } = new DeleteThreadResponseObject(ThreadDeletedValue);
        /// <summary> Determines if two <see cref="DeleteThreadResponseObject"/> values are the same. </summary>
        public static bool operator ==(DeleteThreadResponseObject left, DeleteThreadResponseObject right) => left.Equals(right);
        /// <summary> Determines if two <see cref="DeleteThreadResponseObject"/> values are not the same. </summary>
        public static bool operator !=(DeleteThreadResponseObject left, DeleteThreadResponseObject right) => !left.Equals(right);
        /// <summary> Converts a string to a <see cref="DeleteThreadResponseObject"/>. </summary>
        public static implicit operator DeleteThreadResponseObject(string value) => new DeleteThreadResponseObject(value);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is DeleteThreadResponseObject other && Equals(other);
        /// <inheritdoc />
        public bool Equals(DeleteThreadResponseObject other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;
        /// <inheritdoc />
        public override string ToString() => _value;
    }
}
