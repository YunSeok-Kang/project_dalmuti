using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Nettention.Proud;

using Voxellers;

namespace Networking
{
    public class ServerManager : MonoSingleton<ServerManager>
    {
        private NetClient _netClient = new NetClient();

        //SocialGameC2S.Proxy _c2sProxy = new SocialGameC2S.Proxy();
        //SocialGameS2C.Stub _s2cStub = new SocialGameS2C.Stub();

        // for sending client-to-server messages
        // and the vice versa.
        private CommonC2S.Proxy _c2sProxy = new CommonC2S.Proxy();
        private CommonS2C.Stub _s2cStub = new CommonS2C.Stub();

        public SMRoomConnect ModuleRoomConnect
        {
            get; private set;
        }

        // ----------------------------------------- Unity Events ----------------------------------------- //
        private void Awake()
        {
            _netClient.AttachProxy(_c2sProxy);
            _netClient.AttachStub(_s2cStub);

            ModuleRoomConnect = new SMRoomConnect(_netClient, _c2sProxy, _s2cStub);
        }

        // Start is called before the first frame update
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {
            _netClient.FrameMove();
        }

        private void OnDestroy()
        {
            _netClient.Dispose(); // 클라이언트 객체가 파괴될 때 NetClient 객체의 메모리도 정리
        }
    }
}


