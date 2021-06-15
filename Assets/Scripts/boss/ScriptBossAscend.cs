using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBossAscend : MonoBehaviour
{
    public GameObject bossGameObject;

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("BossPhaseChange"))
        {
            bossGameObject.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
