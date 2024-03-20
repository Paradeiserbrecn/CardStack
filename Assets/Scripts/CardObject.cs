using System;
using System.Text;
using Unity.Netcode;
using UnityEngine;

public struct CardObject : IEquatable<CardObject>, IEquatable<CardTypes>, INetworkSerializable
{
    internal CardTypes cardType;


    public CardObject(CardTypes cardType)
    {
        this.cardType = cardType;
    }

    public readonly bool Equals(CardObject other)
    {
        return cardType == other.cardType;
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
        } 
        else
        {
            var writer = serializer.GetFastBufferWriter();
            writer.WriteValueSafe(cardType);
        }
    }

    public override readonly string ToString()
    {
        StringBuilder sb = new("{");
        sb.Append(cardType);
        sb.Append("}");
        return sb.ToString();
    }
}
