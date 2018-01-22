﻿using Lidgren.Network;
using LunaCommon.Message.Base;
using LunaCommon.Message.Types;

namespace LunaCommon.Message.Data.MasterServer
{
    public class MsRequestServersMsgData : MsBaseMsgData
    {
        /// <inheritdoc />
        internal MsRequestServersMsgData() { }
        public override MasterServerMessageSubType MasterServerMessageSubType => MasterServerMessageSubType.RequestServers;

        public string CurrentVersion;

        public override string ClassName { get; } = nameof(MsRequestServersMsgData);

        internal override void InternalSerialize(NetOutgoingMessage lidgrenMsg, bool compressData)
        {
            base.InternalSerialize(lidgrenMsg, compressData);

            lidgrenMsg.Write(CurrentVersion);
        }

        internal override void InternalDeserialize(NetIncomingMessage lidgrenMsg, bool dataCompressed)
        {
            base.InternalDeserialize(lidgrenMsg, dataCompressed);

            CurrentVersion = lidgrenMsg.ReadString();
        }

        internal override int InternalGetMessageSize(bool dataCompressed)
        {
            return base.InternalGetMessageSize(dataCompressed) + CurrentVersion.GetByteCount();
        }
    }
}
