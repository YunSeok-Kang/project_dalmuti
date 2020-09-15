using Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRConnectionInfoLoader : MonoBehaviour
{
    private void Awake()
    {
        SMRoomConnect.ConnectionInfo connectionInfo = ServerManager.Instance.ModuleRoomConnect.LastConnectionInfo;

        GameRoomClient myClient = new GameRoomClient();
        myClient.isMine = true;

        GameRoomManager.Instance.AddClientInfo(connectionInfo.nickname, myClient);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
