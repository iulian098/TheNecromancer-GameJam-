using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PluggableAI;
[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action
{
    public override void Act(Enemy controller)
    {
        if (controller.Target == null)
        {
            Patrol(controller);
        }
    }

    void Patrol(Enemy controller)
    {
        if(controller.Agent.isOnNavMesh && controller.Agent.isStopped)
            controller.Agent.isStopped = false;

        if (controller.Waypoints.Length > 0)
        {
            controller.Agent.destination = controller.Waypoints[controller.NextWaypointIndex].position;

            if (controller.Agent.remainingDistance <= controller.Agent.stoppingDistance && !controller.Agent.pathPending)
                controller.NextWaypointIndex = (controller.NextWaypointIndex + 1) % controller.Waypoints.Length;
        }
    }
}
