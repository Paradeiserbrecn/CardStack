using System;
using System.Text;
using Unity.Netcode;

public struct CardObject : IEquatable<CardObject>, IEquatable<CardTypes>, INetworkSerializable
{
    CardTypes cardType;
    bool VisibleToEveryone;

    public CardObject(CardTypes cardType, bool visibleToEveryone = false)
    {
        this.cardType = cardType;
        this.VisibleToEveryone = visibleToEveryone;
    }

    public readonly bool Equals(CardObject other)
    {
        return cardType == other.cardType && VisibleToEveryone == other.VisibleToEveryone;
    }

    public readonly bool Equals(CardTypes other)
    {
        return cardType == other;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        if (serializer.IsReader)
        {
            var reader = serializer.GetFastBufferReader();
            reader.ReadValueSafe(out cardType);
            reader.ReadValueSafe(out VisibleToEveryone);
        } 
        else
        {
            var writer = serializer.GetFastBufferWriter();
            writer.WriteValueSafe(cardType);
            writer.WriteValueSafe(VisibleToEveryone);
        }
    }

    public override readonly string ToString()
    {
        StringBuilder sb = new("{");
        sb.Append(cardType);
        sb.Append(", VisibleToEveryone: ");
        sb.Append(VisibleToEveryone);
        sb.Append("}");
        return sb.ToString();
    }
}
