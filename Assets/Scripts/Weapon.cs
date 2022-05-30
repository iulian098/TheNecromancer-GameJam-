using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected UsedByType usedBy;
    [SerializeField] protected Character parent;
    [SerializeField] protected float damage;
    
    public UsedByType UsedBy { get => usedBy; set => usedBy = value; }

    public virtual void Attack() {
        Debug.Log($"[Weapon] {parent.name} attacl");
    }

    public enum UsedByType {
        Player,
        Enemy
    }
}
