using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomItem : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform transformRoomList;
    [SerializeField] private GameObject RoomItemPref;
    [SerializeField] private TMP_Text roomName;
    private RoomInfo info;

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

    public void SetUp(RoomInfo info)
    {
        this.info = info;
        roomName.text = info.Name;
    }
}
