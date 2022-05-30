using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PluggableAI;
[CreateAssetMenu(menuName = "PluggableAI/Decision/Ressurected Look")]
public class Ressurected_LookDecision : Decision
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
            Character target;
            for(int i = 0; i < colls.Length; i++) {

                target = colls[i].GetComponent<Character>();
                if (target.IsDead) continue;
                else {
                    controller.SetTarget(target.transform);
                    return true;
                }
            }
        }
        else
        {
            if(controller.Target != null)
                controller.SetTarget(null);
            return false;
        }

        return false;
    }
}
