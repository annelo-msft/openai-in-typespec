// <auto-generated/>

#nullable disable

using System;
using System.ComponentModel;

namespace OpenAI.Assistants
{
    internal readonly partial struct InternalMessageRequestContentTextObjectType : IEquatable<InternalMessageRequestContentTextObjectType>
    {
        private readonly string _value;

        public InternalMessageRequestContentTextObjectType(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private const string TextValue = "text";

        public static InternalMessageRequestContentTextObjectType Text { get; } = new InternalMessageRequestContentTextObjectType(TextValue);
        public static bool operator ==(InternalMessageRequestContentTextObjectType left, InternalMessageRequestContentTextObjectType right) => left.Equals(right);
        public static bool operator !=(InternalMessageRequestContentTextObjectType left, InternalMessageRequestContentTextObjectType right) => !left.Equals(right);
        public static implicit operator InternalMessageRequestContentTextObjectType(string value) => new InternalMessageRequestContentTextObjectType(value);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is InternalMessageRequestContentTextObjectType other && Equals(other);
        public bool Equals(InternalMessageRequestContentTextObjectType other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;
        public override string ToString() => _value;
    }
}
