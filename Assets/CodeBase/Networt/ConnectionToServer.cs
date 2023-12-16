using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class ConnectionToServer : MonoBehaviourPunCallbacks
{
    public static ConnectionToServer Instance;
    [SerializeField] private TMP_InputField inputRoomName;
    [SerializeField] private TMP_Text roomName;

    [SerializeField] private Transform transformRoomList;
    [SerializeField] private GameObject RoomItemPref;

    [SerializeField] private GameObject playerListItem;
    [SerializeField] private Transform transformPlayerList;

    [SerializeField] private GameObject startGameButton;

    
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

        if (PhotonNetwork.IsMasterClient) startGameButton.SetActive(true);
        else startGameButton.SetActive(false);

        roomName.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;
        foreach(Transform trans in transformPlayerList)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerListItem, transformPlayerList).GetComponent<PlayerListItem>().SetUp(players[i]);
        }


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
        Debug.Log("Count of players on master: " + PhotonNetwork.CountOfPlayersOnMaster);
        PhotonNetwork.NickName = "Gamer " + Random.Range(0, 1000);
    }

    public override void OnJoinedLobby()
    {
        WindowManager.Layout.OpenLayout("MainMenu");
        Debug.Log("Connected to Lobby!");


    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in transformRoomList)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            Instantiate(RoomItemPref, transformRoomList).GetComponent<RoomItem>().SetUp(roomList[i]);
        }
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        Debug.Log("JoinRoom");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItem, transformPlayerList).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void ConnectedToRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

}
