using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PluggableAI;
[CreateAssetMenu(menuName = "PluggableAI/Decision/Follow")]
public class FollowDecision : Decision
{
    [SerializeField] LayerMask attackingLayers;
    public override bool Decide(Enemy controller)
    {
        return Look(controller);
    }

    private bool Look(Enemy controller)
    {
        if(controller.Target == null) {
            return false;
        }
        else {
            return true;
        }
    }
}
