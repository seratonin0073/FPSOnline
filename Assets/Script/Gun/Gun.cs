using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : Item
{
    public GameObject bulletPrefab;
    public Animator gunAnimator;
    public GameObject shootFX;
    public Transform muzzle;

    public abstract override void Use();
}
