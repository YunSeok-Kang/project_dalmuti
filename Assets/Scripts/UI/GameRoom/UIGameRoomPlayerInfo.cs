using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameRoomPlayerInfo : MonoBehaviour
{
    [SerializeField]
    private Text _nicknameText;

    [SerializeField]
    private Image _readyImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetReady(bool isReady)
    {
        _readyImage.gameObject.SetActive(isReady);
    }

    public void SetNickname(string nickname)
    {
        _nicknameText.text = nickname;
    }
}
