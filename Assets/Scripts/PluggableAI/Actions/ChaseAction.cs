using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PluggableAI;
[CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : Action
{
    [SerializeField] LayerMask attackingLayers;
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

        Collider[] colls = Physics.OverlapSphere(controller.transform.position, controller.AgroRange, attackingLayers);

        if (colls.Length > 1 && controller.Target != null) {
            float currentDistace = Vector3.Distance(controller.transform.position, controller.Target.position);
            for (int i = 0; i < colls.Length; i++) {
                float nextDistance = Vector3.Distance(controller.transform.position, colls[i].transform.position);
                if (nextDistance < currentDistace) {
                    currentDistace = nextDistance;
                    controller.SetTarget(colls[i].transform);
                }
            }
        }
        //Debug.Log("Set destination to " + controller.target.name);

        if (controller.Anim.GetBool("Attack"))
            controller.Anim.SetBool("Attack", false);
    }
}
