using Nettention.Proud;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Networking
{
    public class SMGameRoom : ServerModule
    {
        private GameRoomC2S.Proxy _gameRoomC2SProxy = new GameRoomC2S.Proxy();
        private GameRoomS2C.Stub _gameRoomS2CStub = new GameRoomS2C.Stub();

        public SMGameRoom(NetClient target, CommonC2S.Proxy proxy, CommonS2C.Stub stub) : base(target, proxy, stub)
        {
            _gameRoomS2CStub.NotifyUserConnected = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, String nickname) =>
            {
                Debug.Log("접속자: " + nickname);
                return true;
            };
        }

        protected override bool Init()
        {

            return true;
        }
    }
}


