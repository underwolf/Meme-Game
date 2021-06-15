using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBossProjectileCloner : MonoBehaviour
{
    public GameObject bossProjectile;

    public void ShootProjectile()
    {
        Instantiate(bossProjectile, transform.position, Quaternion.identity);
    }
}
