using PluggableAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/Ressurected Follow")]
public class Ressurected_FollowAction : Action {
    public override void Act(Enemy controller) {
        if (controller.Following) {
            controller.Agent.SetDestination(controller.Following.position);
            if (controller.Agent.isStopped || !controller.Agent.updateRotation) {
                controller.Agent.isStopped = false;
                controller.Agent.updateRotation = true;
            }
        }
        else
            controller.Following = GameManager.Instance.Player.transform;
    }
}
