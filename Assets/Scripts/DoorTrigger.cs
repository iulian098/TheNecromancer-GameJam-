using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] Enemy boss;
    [SerializeField] Animator anim;
    // Start is called before the first frame update

    private void OnDisable() {
        boss.OnDie -= PlayAnim;
    }

    void Start()
    {
        boss.OnDie += PlayAnim;
    }

    private void PlayAnim() {
        anim.SetBool("Open", true);
    }
}
