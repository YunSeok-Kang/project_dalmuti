using Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRGameReadyRequester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// GameLobby -> ReadyBtn의 OnClick() 이벤트에서 해당 함수 호출.
    /// </summary>
    public void RequestReady()
    {
        ServerManager.Instance.ModuleGameRoom.RequestReady();
    }
}
