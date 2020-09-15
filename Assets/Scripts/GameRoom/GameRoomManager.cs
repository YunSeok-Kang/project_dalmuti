using Networking;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using Voxellers;

/// <summary>
/// 우선은 ServerManager를 직접 참조하는 형태로 작성함. ServerManager와의 직접적인 결합을 끊으면 좋겠다.
/// </summary>
public class GameRoomManager : MonoSingleton<GameRoomManager> 
{
    private Dictionary<string, GameRoomClient> _gameRoomDict = new Dictionary<string, GameRoomClient>();
    private string _myNickname;

    // Start is called before the first frame update
    void Start()
    {
        ServerManager.Instance.ModuleGameRoom.OnUserConnected.AddListener(OnUserConnect);
        ServerManager.Instance.ModuleGameRoom.OnUserDIsconnected.AddListener(OnUserDisconnect);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 클라이언트 정보를 GameRoomManager에 추가.
    /// </summary>
    /// <param name="nickName"></param>
    /// <param name="client"> client 속성 중 isMine이 true일 경우, 내부의 myNickname 정보를 업데이트.</param>
    public void AddClientInfo(string nickName, GameRoomClient client)
    {
        Debug.Log("AddClientInfo: " + nickName + ", isMine: " + client.isMine);

        if (client.isMine)
        {
            _myNickname = nickName;
        }

        _gameRoomDict.Add(nickName, client);
    }

    private void OnUserConnect(string nickName)
    {
        Debug.Log("OnUserConnect");

        GameRoomClient gameClient;
        if (!_gameRoomDict.TryGetValue(nickName, out gameClient))
        {
            gameClient = new GameRoomClient();
            _gameRoomDict.Add(nickName, gameClient);
        }

        DebugUserInfo();
    }

    private void OnUserDisconnect(string nickName)
    {
        GameRoomClient gameClient;
        if (_gameRoomDict.TryGetValue(nickName, out gameClient))
        {
            _gameRoomDict.Remove(nickName);
        }

        DebugUserInfo();
    }

    private void DebugUserInfo()
    {
        StringBuilder builder = new StringBuilder();
        foreach (KeyValuePair<string, GameRoomClient> client in _gameRoomDict)
        {
            builder.Append(client.Key);
            builder.Append(", ");
        }

        builder.Append("총: ");
        builder.Append(_gameRoomDict.Count);

        Debug.Log(builder.ToString());
    }
}
