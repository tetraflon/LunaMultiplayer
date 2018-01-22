﻿using Lidgren.Network;
using LunaCommon.Locks;
using LunaCommon.Message.Types;

namespace LunaCommon.Message.Data.Lock
{
    public class LockAcquireMsgData : LockBaseMsgData
    {
        /// <inheritdoc />
        internal LockAcquireMsgData() { }
        public override LockMessageType LockMessageType => LockMessageType.Acquire;

        public LockDefinition Lock = new LockDefinition();
        public bool Force;

        public override string ClassName { get; } = nameof(LockAcquireMsgData);

        internal override void InternalSerialize(NetOutgoingMessage lidgrenMsg, bool compressData)
        {
            base.InternalSerialize(lidgrenMsg, compressData);

            lidgrenMsg.Write(Force);
            Lock.Serialize(lidgrenMsg, compressData);
        }

        internal override void InternalDeserialize(NetIncomingMessage lidgrenMsg, bool dataCompressed)
        {
            base.InternalDeserialize(lidgrenMsg, dataCompressed);

            Force = lidgrenMsg.ReadBoolean();
            Lock.Deserialize(lidgrenMsg, dataCompressed);
        }

        internal override int InternalGetMessageSize(bool dataCompressed)
        {
            return base.InternalGetMessageSize(dataCompressed) + Lock.GetByteSize(dataCompressed) + sizeof(bool);
        }
    }
}