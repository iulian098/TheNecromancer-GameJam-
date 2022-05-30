using PluggableAI;
using SFXPlayer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(EnemyState))]

public class Enemy : Character {

    [SerializeField] NavMeshAgent agent;
    [SerializeField] float agroRange;
    [SerializeField] float attackRange;
    [SerializeField] float faceTargetDistance;
    [SerializeField] Transform[] waypoints;
    [SerializeField] EnemyState enemyState;
    [SerializeField] Weapon currentWeapon;
    [SerializeField] CustomVFX vfxPlayer;
    [SerializeField] Vector3 healthBarOffset;
    [SerializeField] bool canBeRessurected = true;
    [SerializeField] State ressurectedState;
    [SerializeField] ParticleSystem ressurectedParticles;

    Character targetComponent;
    GameObject healthBar;
    Transform target;
    Transform following;
    Collider coll;
    bool ressurected;

    public NavMeshAgent Agent => agent;
    public float AgroRange => agroRange;
    public float AttackRange => attackRange;
    public Transform[] Waypoints => waypoints;
    public EnemyState EnemyState => enemyState;
    public int NextWaypointIndex { get; set; }
    public Transform Target {
        get => target; 
        set {
            if (value != target) {
                target = value;
                if (target != null)
                    targetComponent = target.GetComponent<Character>();
                else
                    targetComponent = null;
            }
        }
    }
    public Transform Following { get { return following; } set { following = value; } }
    public Character TargetComponent => targetComponent;
    public bool Ressurected => ressurected;
    public bool CanBeRessurected => canBeRessurected;

    private void OnDestroy() {
        Destroy(healthBar);
    }

    public override void Start() {
        base.Start();

        healthBar = Instantiate(GameManager.Instance.GameData.EnemyHealthBar, GameManager.Instance.EnemyHealthBarParent);
        healthBar.GetComponent<EnemyHealthUI>().SetTarget(this);

        coll = GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate() {
        if (isDead) return;

        anim.SetFloat("Speed", agent.velocity.magnitude / agent.speed);

        if (ressurected && !isDead) {
            Health -= Time.deltaTime;
        }

        if(target != null) {
            if (Vector3.Distance(transform.position, target.position) < faceTargetDistance) {
                agent.updateRotation = false;
                FaceTarget();
            }
            else
                agent.updateRotation = true;
        }

        if (healthBar && healthBar.activeSelf)
            healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, coll.bounds.size.y, 0)) + healthBarOffset;
    }


    public override void ReceiveDamage(float damage) {
        if (Health <= 0) return;
        base.ReceiveDamage(damage);

    }

    public override void Die() {
        agent.enabled = false;
        isDead = true;
        coll.isTrigger = true;
        enemyState.SetCurrentState(null);
        healthBar.gameObject.SetActive(false);
        anim.SetTrigger("Die");
        if(vfxPlayer != null)
            vfxPlayer.StopAll();
        if(ressurectedParticles != null)
            ressurectedParticles.gameObject.SetActive(false);
    }

    public void SetTarget(Transform target) {
        this.Target = target;
        if (target == null)
            agent.SetDestination(transform.position);
        else
            agent.SetDestination(target.position);
    }

    void FaceTarget() {
        Vector3 dir = (target.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0f, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 10f);
    }

    public void Attack() {
        currentWeapon.Attack();
    }

    public void Ressurect() {
        enemyState.SetCurrentState(ressurectedState);
        anim.ResetTrigger("Attack");
        anim.ResetTrigger("Die");
        anim.SetTrigger("Ressurect");
        Health = maxHealth;

        isDead = false;
        ressurected = true;

        coll.isTrigger = false;
        agent.enabled = true;
        agent.updateRotation = true;

        following = GameManager.Instance.Player.transform;

        currentWeapon.UsedBy = Weapon.UsedByType.Player;
        ressurectedParticles.gameObject.SetActive(true);

        tag = "Player";
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, agroRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
    }
}
