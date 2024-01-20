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
        Transform point = SpawnManager.Instance.GetSpawnPoint();
        PhotonNetwork.Instantiate(Path.Combine("PlayerController"),
            point.position, point.rotation,0, new object[] { photonView.ViewID});
    }
}
