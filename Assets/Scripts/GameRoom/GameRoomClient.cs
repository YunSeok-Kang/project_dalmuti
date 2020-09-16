using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomClient
{
    public string nickname;

    public bool isMine;

    public GameRoomClient() { }
    public GameRoomClient(string nickname) 
    {
        this.nickname = nickname;
    }
}
