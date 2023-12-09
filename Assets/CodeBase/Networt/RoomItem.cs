using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomItem : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text roomName;
    private RoomInfo info;
    public void SetUp(RoomInfo info)
    {
        this.info = info;
        roomName.text = info.Name;
    }

    public void OnClick()
    {
        ConnectionToServer.Instance.OnJoinRoom(this.info);
    }
}
