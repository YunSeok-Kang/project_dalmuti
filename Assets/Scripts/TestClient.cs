using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Nettention.Proud;

namespace DalmutiClient
{
    public class TestClient : MonoBehaviour
    {
        public string serverAddr = "localhost";

        // world name. you may consider it as user name.
        public string villeName = "Ville";

        // uses while the scene is 'error mode'
        string _failMessage = "";

        public HostID groupID = HostID.HostID_None;

        private NetClient _netClient = new NetClient();

        // for sending client-to-server messages
        // and the vice versa.
        SocialGameC2S.Proxy _c2sProxy = new SocialGameC2S.Proxy();
        SocialGameS2C.Stub _s2cStub = new SocialGameS2C.Stub();

        public enum State
        {
            Standby,
            Connecting,
            LoggingOn, // After connecting done. only connecting to server does net say login completion.
            InVille, // after logon successful. in main game mode.
            Failed,
        }

        public State m_state = State.Standby;

        // Start is called before the first frame update
        void Start()
        {
            Connect();
            //Invoke("Connect", 1f);

            _s2cStub.ReplyLogon = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, int groupID, int result, System.String comment) =>
            {
                this.groupID = (HostID)groupID;

                if (result == 0) // ok
                {
                    m_state = State.InVille;
                }
                else
                {
                    m_state = State.Failed;
                    _failMessage = "Logon failed. Error=" + comment;
                }

                return true;
            };

        }

        // Update is called once per frame
        void Update()
        {
            _netClient.FrameMove(); // Client는 단일 스레드로 동작. 모든 이벤트 통지는 FrameMove 함수에 의해 콜백됨.
        }

        private void Connect()
        {
            if (m_state == State.Standby)
            {
                m_state = State.Connecting;
                IssueConnect(); // attemp to connect and logon
            }
        }



        private void IssueConnect()
        {
            _netClient.AttachProxy(_c2sProxy);
            _netClient.AttachStub(_s2cStub);

            _netClient.JoinServerCompleteHandler = (ErrorInfo info, ByteArray replyFromServer) =>
            {
                if (info.errorType == ErrorType.Ok)
                {
                    m_state = State.LoggingOn;

                    // try to join the specified ville by name given by the user.
                    _c2sProxy.RequestLogon(HostID.HostID_Server, RmiContext.ReliableSend, villeName, false);
                }
                else
                {
                    m_state = State.Failed;
                    Debug.Log(info.comment);

                    Application.Quit();
                }
            };

            // if the server connection is down, we should prepare for exit.
            _netClient.LeaveServerHandler = (ErrorInfo info) =>
            {
                m_state = State.Failed;
            };

            // fill parameters and go
            NetConnectionParam cp = new NetConnectionParam();
            cp.serverIP = serverAddr;
            cp.serverPort = 15001;
            cp.protocolVersion = new Guid("{0x863f7b4e,0xe26e,0x4102,{0x8b,0x33,0xbd,0x64,0x5a,0xfb,0xec,0xe}}");

            _netClient.Connect(cp); // 실행하면 바로 반환되고, 비동기로 실행됨.
        }

        private void OnDestroy()
        {
            _netClient.Dispose(); // 클라이언트 객체가 파괴될 때 NetClient 객체의 메모리도 정리
        }
    }
}