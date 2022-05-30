using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] float attackRadius;
    [SerializeField] bool useOverlapBox;
    [SerializeField] Vector3 overlapBoxSize;
    public override void Attack() {
        Collider[] colls;

        //Find Targets
        if (usedBy == UsedByType.Player) {
            colls = useOverlapBox ?
                Physics.OverlapBox(transform.position, overlapBoxSize, parent.transform.rotation, GameManager.Instance.GameData.EnemyMask) :
                Physics.OverlapSphere(transform.position, attackRadius, GameManager.Instance.GameData.EnemyMask);
        }
        else {
            colls = useOverlapBox ?
                Physics.OverlapBox(transform.position, overlapBoxSize, parent.transform.rotation, GameManager.Instance.GameData.PlayerMask) :
                Physics.OverlapSphere(transform.position, attackRadius, GameManager.Instance.GameData.PlayerMask);
        }


        if (colls.Length > 0) {
            for (int i = 0; i < colls.Length; i++) {
                IDamageable target = colls[i].GetComponent<IDamageable>();
                target.ReceiveDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected() {
        
        Gizmos.color = Color.red;
        if (useOverlapBox) {
            Gizmos.DrawWireCube(transform.position, overlapBoxSize * 2);
        }else
            Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
