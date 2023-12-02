using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class ConnectionToServer : MonoBehaviourPunCallbacks
{
    public static ConnectionToServer Instance;
    [SerializeField] private TMP_InputField inputRoomName;
    [SerializeField] private TMP_Text roomName;

    void Awake()
    {
        Instance = this;
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateNewRoom()
    {
        if(string.IsNullOrEmpty(inputRoomName.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(inputRoomName.text);
    }

    public override void OnJoinedRoom()
    {
        WindowManager.Layout.OpenLayout("GameRoom");
        roomName.text = PhotonNetwork.CurrentRoom.Name;
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        WindowManager.Layout.OpenLayout("MainMenu");
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
