// <auto-generated/>

#nullable disable

using System;
using System.ComponentModel;

namespace OpenAI.Assistants
{
    internal readonly partial struct InternalThreadMessageRole : IEquatable<InternalThreadMessageRole>
    {
        private readonly string _value;

        public InternalThreadMessageRole(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private const string UserValue = "user";
        private const string AssistantValue = "assistant";

        public static InternalThreadMessageRole User { get; } = new InternalThreadMessageRole(UserValue);
        public static InternalThreadMessageRole Assistant { get; } = new InternalThreadMessageRole(AssistantValue);
        public static bool operator ==(InternalThreadMessageRole left, InternalThreadMessageRole right) => left.Equals(right);
        public static bool operator !=(InternalThreadMessageRole left, InternalThreadMessageRole right) => !left.Equals(right);
        public static implicit operator InternalThreadMessageRole(string value) => new InternalThreadMessageRole(value);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is InternalThreadMessageRole other && Equals(other);
        public bool Equals(InternalThreadMessageRole other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;
        public override string ToString() => _value;
    }
}
