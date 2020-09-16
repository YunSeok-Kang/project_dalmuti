using Nettention.Proud;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Networking
{
    public class SMGameRoom : ServerModule
    {
        public class GameRoomEvent : UnityEvent<string> { }
        public class GameRoomEventWithBool : UnityEvent<string, bool> { }

        private NetClient _targetClient;

        private GameRoomC2S.Proxy _gameRoomC2SProxy = new GameRoomC2S.Proxy();
        private GameRoomS2C.Stub _gameRoomS2CStub = new GameRoomS2C.Stub();

        public GameRoomEventWithBool OnUserConnected = new GameRoomEventWithBool();
        public GameRoomEvent OnUserDIsconnected = new GameRoomEvent();
        public GameRoomEvent OnUserReady = new GameRoomEvent();
        public UnityEvent OnGameStarted = new UnityEvent();

        public SMGameRoom(NetClient target, CommonC2S.Proxy proxy, CommonS2C.Stub stub) : base(target, proxy, stub)
        {
            _targetClient = target;

            Init();
        }

        protected override bool Init()
        {
            _targetClient.AttachProxy(_gameRoomC2SProxy);
            _targetClient.AttachStub(_gameRoomS2CStub);

            _gameRoomS2CStub.NotifyUserConnected = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, String nickname, bool isReady) =>
            {
                Debug.Log("접속자: " + nickname);
                OnUserConnected.Invoke(nickname, isReady);
                return true;
            };

            _gameRoomS2CStub.NotifyUserDisconnected = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, String nickname) =>
            {
                Debug.Log("나간 사람: " + nickname);
                OnUserDIsconnected.Invoke(nickname);
                return true;
            };

            _gameRoomS2CStub.NotifyUserReady = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, String nickname) =>
            {
                Debug.Log("플레이어 " + nickname + "가 준비를 마쳤습니다.");
                OnUserReady.Invoke(nickname);
                return true;
            };

            _gameRoomS2CStub.NotifyGameStarted = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext) =>
            {
                Debug.Log("게임이 시작됩니다.");
                OnGameStarted.Invoke();
                return true;
            };

            return true;
        }

        public void RequestReady()
        {
            _gameRoomC2SProxy.RequestUserReady(HostID.HostID_Server, RmiContext.ReliableSend);
        }
    }
}


