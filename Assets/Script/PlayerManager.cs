using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    private PhotonView photonView;
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if(photonView.IsMine)
        {
            CreateController();
        }
    }

    private void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("PlayerController"),
            Vector3.zero, Quaternion.identity);
    }
}
