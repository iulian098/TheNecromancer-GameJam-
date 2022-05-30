using System;
using UnityEngine;

public class PlayerController : Character
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Weapon[] weapons;
    [SerializeField] Weapon currentWeapon;
    [SerializeField] float specialSpellCooldown;
    

    bool isGrounded;
    bool isAttacking;
    float specialSpellTimer;

    SpellsManager spellsManager;
    Vector3 movementVector;
    Vector3 targetVector;
    Vector3 mousePos;

    bool specialSpellUnlocked = false;

    public Action<int> OnWeaponChange;

    public override float Health { get => base.Health;
        set {
            base.Health = value;
            GameManager.Instance.PlayerHealthBar.fillAmount = health / maxHealth;
        }
    }

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        spellsManager = GameManager.Instance.SpellsManager;

        specialSpellUnlocked = Convert.ToBoolean(PlayerPrefs.GetInt("SpecialUnlocked", 0));
    }

    void Update()
    {
        movementVector.x = Input.GetAxis("Horizontal");
        isAttacking = Input.GetMouseButton(0);
        movementVector.z = Input.GetAxis("Vertical");
        //movementVector.x = pi.actions["Movement"].ReadValue<Vector2>().x;
        //movementVector.z = pi.actions["Movement"].ReadValue<Vector2>().y;
        //isAttacking = pi.actions["Attack"].IsPressed();
        if (Input.GetKeyDown(KeyCode.Q) && specialSpellUnlocked)
            TriggerSpecialSpell();

        if (specialSpellTimer > 0) {
            specialSpellTimer -= Time.deltaTime;
            spellsManager.SpecialSpellCooldown.fillAmount = Mathf.Lerp(0, 1, specialSpellTimer / specialSpellCooldown);
        }
    }

    void FixedUpdate() {
        targetVector = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * movementVector;

        anim.SetFloat("Speed", rb.velocity.magnitude / speed);
        anim.SetBool("Attack", isAttacking);

        if (!isAttacking && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Spell"))
            Movement();
        else {
            Rotation();
            rb.velocity = Vector3.zero;
        }
    }

    void Movement() {

        if (isDead) return;

        //Move
        if (movementVector.magnitude != 0) {
            if (targetVector.magnitude > 1)
                targetVector.Normalize();
            Vector3 mov = Vector3.ClampMagnitude(targetVector, 1.0f) * speed;
            mov.y = rb.velocity.y;

            rb.velocity = mov;
            rb.isKinematic = false;
        }
        else {
            if (!isGrounded) {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                rb.isKinematic = false;
            }
            else {
                rb.isKinematic = true;
            }
        }

        Rotation();
    }

    void Rotation() {
        if (isAttacking) {
            mousePos = Input.mousePosition;//pi.actions["MousePosition"].ReadValue<Vector2>();
            RaycastHit hit;
            Ray r = Camera.main.ScreenPointToRay(mousePos);

            Vector3 worldPos = Vector3.zero;

            if (Physics.Raycast(r, out hit))
                worldPos = hit.point;

            Vector3 pos = worldPos - transform.position;
            pos.y = 0;
            Quaternion rot = Quaternion.LookRotation(pos, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 35f);
        }else if (movementVector.magnitude != 0) {
            Quaternion rot = Quaternion.LookRotation(targetVector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 22.5f);
        }


    }

    public void Attack() {
        currentWeapon.Attack();
    }

    public override void PlayFootstep() {
        if(rb.velocity.magnitude / speed > 0.1f)
            base.PlayFootstep();

    }

    public override void ReceiveDamage(float damage) {
        base.ReceiveDamage(damage);

        if (Health > 0) return;

        Die();
    }

    public override void Die() {
        anim.SetTrigger("Die");
        isDead = true;
        GameManager.Instance.ShowEndScreen();
    }

    public void ChangeSpell(int index) {
        currentWeapon.gameObject.SetActive(false);
        weapons[index].gameObject.SetActive(true);
        currentWeapon = weapons[index];
    }

    void TriggerSpecialSpell() {
        if (specialSpellTimer > 0) return;

        Collider[] colls = Physics.OverlapSphere(transform.position, 5f, GameManager.Instance.GameData.EnemyMask);

        //if (colls.Length == 0) return;

        specialSpellTimer = specialSpellCooldown;
        anim.SetTrigger("Spell");

        if (colls.Length > 0) {
            for (int i = 0; i < colls.Length; i++) {
                Enemy target = colls[i].GetComponent<Enemy>();
                if (!target.Ressurected && target.IsDead && target.CanBeRessurected) {
                    target.gameObject.layer = LayerMask.NameToLayer("Player");
                    target.Ressurect();
                }
            }
        }

    }
}
