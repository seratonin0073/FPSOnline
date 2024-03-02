using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class ShootGun : Gun
{
    private PhotonView myPv;
    [SerializeField] Camera myCam;

    private void Awake()
    {
        myPv = GetComponent<PhotonView>();
        gunAnimator = GetComponentInChildren<Animator>();

    }

  

    public override void Use()
    {
        Shoot();
    }

    private void Shoot()
    {
        Ray ray = myCam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = myCam.transform.position;
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            hit.collider.GetComponent<IDamagable>()?.TakeDamage(((GunInfo)itemInfo).Damage);
            //myPv.RPC("RPC_Shoot", RpcTarget.All, hit.point, hit.normal);
            ShootEffect(hit.point, hit.normal);
        }
    }

    //[PunRPC]
    void ShootEffect(Vector3 hitPoint, Vector3 hitNormal)
    {
        Collider[] coll = Physics.OverlapSphere(hitPoint, 0.1f);
        if(coll.Length != 0)
        {
            GameObject bulletImp = 
                PhotonNetwork.Instantiate(Path.Combine("PistolBulletImpact"), hitPoint, Quaternion.LookRotation(hitNormal, Vector3.up) * bulletPrefab.transform.rotation);
            bulletImp.transform.SetParent(coll[0].transform);
            bulletImp.SetActive(true);
        }
    }
}
