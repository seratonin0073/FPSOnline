using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class NetworkStatistic : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text PlayersCouner;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdatePlayers", 0f, 5f);
    }

    private void UpdatePlayers()
    {
        PlayersCouner.text = "Players online: " + PhotonNetwork.CountOfPlayers.ToString();
    }
}
