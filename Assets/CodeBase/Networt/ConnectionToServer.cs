using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ConnectionToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        WindowManager.Layout.OpenLayout("MainMenu");
        Debug.Log("Connected to Lobby!");
    }
}
