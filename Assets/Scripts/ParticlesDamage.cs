using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesDamage : MonoBehaviour
{
    [SerializeField] Weapon.UsedByType usedBy;
    [SerializeField] float damage;
    private void OnParticleCollision(GameObject other) {
        if (other.CompareTag("Player") && usedBy == Weapon.UsedByType.Enemy)
            other.GetComponent<Character>().ReceiveDamage(damage);
        else if (other.CompareTag("Enemy") && usedBy == Weapon.UsedByType.Player)
            other.GetComponent<Character>().ReceiveDamage(damage);
    }
}
