using UnityEngine;
using Photon.Pun;
using System.IO;


public class PlayerManager : MonoBehaviour
{
    
    private GameObject controller;

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
        controller = PhotonNetwork.Instantiate(Path.Combine("PlayerController"),
            point.position, point.rotation,0, new object[] { photonView.ViewID});
    }

    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        CreateController();
    }
}
