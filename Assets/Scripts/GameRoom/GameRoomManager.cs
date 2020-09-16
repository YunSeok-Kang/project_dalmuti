using Networking;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Voxellers;

/// <summary>
/// 우선은 ServerManager를 직접 참조하는 형태로 작성함. ServerManager와의 직접적인 결합을 끊으면 좋겠다.
/// </summary>
public class GameRoomManager : MonoSingleton<GameRoomManager> 
{
    public class GameRoomEventWithClient : UnityEvent<GameRoomClient> { }

    private Dictionary<string, GameRoomClient> _gameRoomDict = new Dictionary<string, GameRoomClient>();
    private Dictionary<string, bool> _gameReadyStateDict = new Dictionary<string, bool>();
    public string myNickname = null;

    private bool _activateEvents = false;
    private Queue<GameRoomClient> _userConnectedEventQueue = new Queue<GameRoomClient>();

    // 이벤트를 이렇게 직접적으로 밖으로 드러내놓으면, 외부에서 호출할 수 있는 위험이 있음.
    public GameRoomEventWithClient OnUserConnected = new GameRoomEventWithClient();
    public GameRoomEventWithClient OnUserDisconnected = new GameRoomEventWithClient();
    public GameRoomEventWithClient OnUserReady = new GameRoomEventWithClient();

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Start is called before the first frame update
    private void Start()
    {
        ServerManager.Instance.ModuleGameRoom.OnUserConnected.AddListener(OnUserConnect);
        ServerManager.Instance.ModuleGameRoom.OnUserDIsconnected.AddListener(OnUserDisconnect);
        ServerManager.Instance.ModuleGameRoom.OnUserReady.AddListener(OnUserGameReady);
        ServerManager.Instance.ModuleGameRoom.OnGameStarted.AddListener(OnGameStarted);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_activateEvents && _userConnectedEventQueue.Count > 0)
        {
            while (_userConnectedEventQueue.Count > 0)
            {
                GameRoomClient client = _userConnectedEventQueue.Dequeue();

                Debug.Log("이벤트 호출: " + client.nickname);
                OnUserConnected.Invoke(client);
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameLobby")
        {
            _activateEvents = true;
        }
    }

    private void OnGameStarted()
    {
        // temp
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay_PC");
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
            myNickname = nickName;
        }

        _gameRoomDict.Add(nickName, client);
        _userConnectedEventQueue.Enqueue(client);

        //OnUserConnected.Invoke(client);
    }

    //public void AddReadyInfo(string nickname)
    //{
    //    OnUserGameReady(nickname);
    //}

    private void OnUserConnect(string nickName, bool isReady)
    {
        Debug.Log("OnUserConnect");

        GameRoomClient gameClient;
        if (!_gameRoomDict.TryGetValue(nickName, out gameClient))
        {
            gameClient = new GameRoomClient(nickName);
            gameClient.isReady = isReady;

            _gameRoomDict.Add(nickName, gameClient);
            _userConnectedEventQueue.Enqueue(gameClient);

            //OnUserConnected.Invoke(gameClient);
        }

        DebugUserInfo();
    }

    private void OnUserDisconnect(string nickName)
    {
        GameRoomClient gameClient;
        if (_gameRoomDict.TryGetValue(nickName, out gameClient))
        {
            _gameRoomDict.Remove(nickName);

            OnUserDisconnected.Invoke(gameClient);
        }

        DebugUserInfo();
    }

    private void OnUserGameReady(string nickname)
    {
        Debug.Log("OnUserGameReady: " + nickname);

        bool isReady;
        bool hasValue = _gameReadyStateDict.TryGetValue(nickname, out isReady);
        if (!hasValue)
        {
            _gameReadyStateDict.Add(nickname, true);
            Debug.Log("!hasValue");
        }
        else if (!isReady)
        {
            _gameReadyStateDict[nickname] = true;
            Debug.Log("!IsReady");
        }
        else
        {
            return;
        }

        _gameRoomDict[nickname].isReady = true;
        Debug.Log("GameRoomDict: " + _gameRoomDict[nickname].isReady);

        // Ready 이벤트가 호출되는 순서 상, _gameRoomDict에 정보가 등록된 후일 확률이 매우 높음(99.9%)
        // 따라서 Dictionary에서 다른 검사 없이 그냥 가지고 옴.
        // 또한 씬이 로딩된 후에 해당 이벤트가 실행될 것이기에, 따로 Queue에 데이터를 담지 않음.
        OnUserReady.Invoke(_gameRoomDict[nickname]);
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
