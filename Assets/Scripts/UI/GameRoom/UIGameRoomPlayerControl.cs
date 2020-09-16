using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voxellers.ObjectPooling;

public class UIGameRoomPlayerControl : MonoBehaviour
{
    private Dictionary<GameRoomClient, UIGameRoomPlayerInfo> _playerInfoDict = new Dictionary<GameRoomClient, UIGameRoomPlayerInfo>();

    public Transform parent;

    private void Awake()
    {
        //GameRoomManager.Instance.OnUserConnected.AddListener((client) =>
        //{
        //    OnUserConnect(client.nickname);
        //});
        GameRoomManager.Instance.OnUserConnected.AddListener(OnUserConnect);

        //GameRoomManager.Instance.OnUserDisconnected.AddListener((client) =>
        //{
        //    OnUserDisconnect(client.nickname);
        //});
        GameRoomManager.Instance.OnUserDisconnected.AddListener(OnUserDisconnect);

        GameRoomManager.Instance.OnUserReady.AddListener(OnUserReady);
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnUserConnect(GameRoomClient client)
    {
        GameObject playerInfoObject = ObjectPoolingManager.Instance.GetObject("PlayerInfo");
        UIGameRoomPlayerInfo playerInfo = playerInfoObject.GetComponent<UIGameRoomPlayerInfo>();
        playerInfo.SetNickname(client.nickname);
        Debug.Log("client: " + client.isReady);
        playerInfo.SetReady(client.isReady);

        playerInfoObject.transform.parent = parent;

        _playerInfoDict.Add(client, playerInfo);
    }

    private void OnUserDisconnect(GameRoomClient client)
    {
        UIGameRoomPlayerInfo playerInfo = _playerInfoDict[client];
        ObjectPoolingManager.Instance.ReturnObject("PlayerInfo", playerInfo.gameObject);

        _playerInfoDict.Remove(client);
    }

    private void OnUserReady(GameRoomClient client)
    {
        UIGameRoomPlayerInfo playerInfo = _playerInfoDict[client];
        playerInfo.SetReady(client.isReady);
    }
}
