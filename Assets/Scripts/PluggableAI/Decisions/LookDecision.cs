using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PluggableAI;
[CreateAssetMenu(menuName = "PluggableAI/Decision/Look")]
public class LookDecision : Decision
{
    [SerializeField] LayerMask attackingLayers;
    public override bool Decide(Enemy controller)
    {
        return Look(controller);
    }

    private bool Look(Enemy controller)
    {
        Collider[] colls = Physics.OverlapSphere(controller.transform.position, controller.AgroRange, attackingLayers);

        if (colls.Length > 0)
        {
            if(controller.Target == null)
                controller.SetTarget(colls[0].transform);
            return true;
        }
        else
        {
            if(controller.Target)
                controller.SetTarget(null);
            return false;
        }
    }
}
