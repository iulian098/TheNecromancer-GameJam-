using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PluggableAI;
[CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : Action
{
    public override void Act(Enemy controller)
    {
        Chase(controller);
    }

    void Chase(Enemy controller)
    {
        if (controller.Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            controller.Agent.isStopped = true;
        else if(controller.Agent.isStopped && !controller.IsDead)
            controller.Agent.isStopped = false;

        if(controller.Target && !controller.TargetComponent.IsDead)
            controller.Agent.SetDestination(controller.Target.position);

        //Debug.Log("Set destination to " + controller.target.name);

        if (controller.Anim.GetBool("Attack"))
            controller.Anim.SetBool("Attack", false);
    }
}
