using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Networking;

public class UIServerConnection : MonoBehaviour
{
    public InputField ipInput;
    public InputField nickNameInput;
    public InputField roomKeyField; // 게임 방을 의미하는 RoomKey.

    private void Awake()
    {
        ipInput.text = "192.168.0.28";
        roomKeyField.text = "Derung";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Connect()
    {
        ServerManager.Instance.ModuleRoomConnect.Connect(ipInput.text, 15001, nickNameInput.text, roomKeyField.text);
    }
}
