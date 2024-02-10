using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
        }
    }

    [PunRPC]
    void RPC_Shoot(Vector3 hitPoint, Vector3 hinNormal)
    {
        Collider[] coll = Physics.OverlapSphere(hitPoint, 0.1f);
        if(coll.Length != 0)
        {
            GameObject bulletImp = 
                Instantiate(bulletPrefab, hitPoint, Quaternion.LookRotation(hitPoint, Vector3.up) * bulletPrefab.transform.rotation);
            bulletImp.transform.SetParent(coll[0].transform);
        }
    }
}
