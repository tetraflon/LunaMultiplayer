﻿using Lidgren.Network;
using LunaCommon.Message.Interface;

namespace LunaCommon.Message.Base
{
    public abstract class MessageData : IMessageData
    {
        /// <summary>
        /// Make constructor internal so they have to use the factory.
        /// This is made this way as the factory use a cache system to avoid the generation of garbage
        /// </summary>
        internal MessageData() { }

        /// <inheritdoc />
        public virtual ushort MajorVersion { get; set; } = LmpVersioning.MajorVersion;

        /// <inheritdoc />
        public virtual ushort MinorVersion { get; set; } = LmpVersioning.MinorVersion;

        /// <inheritdoc />
        public virtual ushort BuildVersion { get; set; } = LmpVersioning.BuildVersion;

        /// <inheritdoc />
        public long ReceiveTime { get; set; }

        /// <inheritdoc />
        public long SentTime { get; set; }

        /// <inheritdoc />
        public virtual ushort SubType => 0;

        /// <inheritdoc />
        public abstract string ClassName { get; }

        public void Serialize(NetOutgoingMessage lidgrenMsg, bool compressData)
        {
            lidgrenMsg.Write(SentTime);
            lidgrenMsg.Write(MajorVersion);
            lidgrenMsg.Write(MinorVersion);
            lidgrenMsg.Write(BuildVersion);
            InternalSerialize(lidgrenMsg, compressData);
        }

        internal abstract void InternalSerialize(NetOutgoingMessage lidgrenMsg, bool compressData);

        public void Deserialize(NetIncomingMessage lidgrenMsg, bool dataCompressed)
        {
            SentTime = lidgrenMsg.ReadInt64();
            MajorVersion = lidgrenMsg.ReadUInt16();
            MinorVersion = lidgrenMsg.ReadUInt16();
            BuildVersion = lidgrenMsg.ReadUInt16();
            InternalDeserialize(lidgrenMsg, dataCompressed);
        }

        internal abstract void InternalDeserialize(NetIncomingMessage lidgrenMsg, bool dataCompressed);
        
        public int GetMessageSize(bool dataCompressed)
        {
            return sizeof(long) + sizeof(ushort) * 3 + InternalGetMessageSize(dataCompressed);
        }

        internal abstract int InternalGetMessageSize(bool dataCompressed);
    }
}