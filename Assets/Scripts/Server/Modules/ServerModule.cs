using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Nettention.Proud;

namespace Networking
{
    public class ServerModule
    {
        public NetClient TargetClient
        {
            get; private set;
        }

        // general 한 proxy, stub은 ServerModule(최상단)에서 보유.
        public CommonC2S.Proxy C2SProxy
        {
            get; private set;
        }

        public CommonS2C.Stub S2CStub
        {
            get; private set;
        }

        public ServerModule(NetClient target, CommonC2S.Proxy proxy, CommonS2C.Stub stub)
        {
            TargetClient = target;
            C2SProxy = proxy;
            S2CStub = stub;
        }

        protected virtual bool Init()
        {
            return true;
        }
    }
}