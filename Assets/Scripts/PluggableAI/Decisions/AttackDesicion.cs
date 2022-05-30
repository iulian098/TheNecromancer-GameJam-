using UnityEngine;
using PluggableAI;

[CreateAssetMenu(menuName = "PluggableAI/Decision/Attack")]
public class AttackDesicion : Decision
{
    public override bool Decide(Enemy controller)
    {
        return Attack(controller);
    }

    bool Attack(Enemy controller)
    {
        if (controller.Target == null || controller.TargetComponent.IsDead) {
            controller.Target = null;
            return false;
        }
        return Vector3.Distance(controller.transform.position, controller.Target.position) < controller.AttackRange;
    }
}
