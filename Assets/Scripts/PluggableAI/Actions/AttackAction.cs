using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PluggableAI;
[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Action
{
    public override void Act(Enemy controller)
    {
        Attack(controller);
    }

    void Attack(Enemy controller)
    {
        controller.Anim.SetBool("Attack", true);
        controller.Agent.SetDestination(controller.transform.position);
        controller.Agent.isStopped = true;
        //controller.anim.Play("Attack");
    }
}
