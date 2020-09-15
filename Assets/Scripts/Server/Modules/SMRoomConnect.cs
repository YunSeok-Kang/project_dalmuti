using Nettention.Proud;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Networking
{
    public class SMRoomConnect : ServerModule
    {
        public class ConnectionInfo
        {
            public string nickname;
        }

        public SMRoomConnect(NetClient target, CommonC2S.Proxy proxy, CommonS2C.Stub stub) : base(target, proxy, stub)
        {
            Init();
        }

        public ConnectionInfo LastConnectionInfo
        {
            get; private set;
        }

        protected override bool Init()
        {
            LastConnectionInfo = new ConnectionInfo();

            S2CStub.ReplyLogon = (HostID remote, RmiContext rmiContext, int result, System.String comment) =>
            {
                //this.groupID = (HostID)groupID;

                if (result == 0) // ok
                {
                    //m_state = State.InVille;
                    Debug.Log("서버 접속 성공");

                    // 여기에 있는게 맞는지 모르겠음.
                    // 이벤트로 얘 떨궈내자.
                    UnityEngine.SceneManagement.SceneManager.LoadScene("GameLobby");
                }
                else
                {
                    //m_state = State.Failed;
                    //_failMessage = "Logon failed. Error=" + comment;
                    Debug.Log("서버 접속 실패");
                }

                return true;
            };

            return true;
        }

        public void Connect(string ip, ushort port, string nickName, string roomName)
        {
            TargetClient.JoinServerCompleteHandler = (ErrorInfo info, ByteArray replyFromServer) =>
            {
                if (info.errorType == ErrorType.Ok)
                {
                    //m_state = State.LoggingOn;
                    LastConnectionInfo.nickname = nickName;

                    // try to join the specified ville by name given by the user.
                    C2SProxy.RequestLogon(HostID.HostID_Server, RmiContext.ReliableSend, nickName, roomName);
                }
                else
                {
                    //m_state = State.Failed;
                    Debug.Log(info.comment);

                    Application.Quit();
                }
            };

            // fill parameters and go
            NetConnectionParam cp = new NetConnectionParam();
            cp.serverIP = ip;
            cp.serverPort = port;
            cp.protocolVersion = new Guid("{0x863f7b4e,0xe26e,0x4102,{0x8b,0x33,0xbd,0x64,0x5a,0xfb,0xec,0xe}}");

            TargetClient.Connect(cp); // 실행하면 바로 반환되고, 비동기로 실행됨.
        }
    }
}