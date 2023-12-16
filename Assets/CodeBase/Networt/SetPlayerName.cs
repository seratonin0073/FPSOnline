using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class SetPlayerName : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField nickNameInput;

    public override void OnConnectedToMaster()
    {
        LoadNickName();
    }

    private void LoadNickName()
    {
        string playerName = PlayerPrefs.GetString("SaveNickName");
        if(string.IsNullOrEmpty(playerName))
        {
            playerName = "Player" + Random.Range(0, 1000);
        }

        PhotonNetwork.NickName = playerName;
        nickNameInput.text = playerName;
    }

    public void ChangeName()
    {
        PlayerPrefs.SetString("SaveNickName", nickNameInput.text);
        LoadNickName();
    }
}
