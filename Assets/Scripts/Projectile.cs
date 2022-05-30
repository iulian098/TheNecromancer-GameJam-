using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] ParticleSystem vfx;
    [SerializeField] GameObject hitEffect;
    [SerializeField] string hitSFX;
    [SerializeField] float force;

    bool hit = false;
    public float Damage { get; set; }
    void Start()
    {
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other) {

        if (hit || other.CompareTag("Projectile")) return;

        hit = true;

        GameObject hitEffect = Instantiate(this.hitEffect, transform.position, Quaternion.identity);


        rb.isKinematic = true;

        Debug.Log($"Hit: {other.name}");

        if (!other.CompareTag("Player")) {
            IDamageable target;
            if(other.TryGetComponent(out target))
                target.ReceiveDamage(Damage);

        }

        SFXPlayer.SFXManager.PlaySFX(hitSFX, transform);

        vfx.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        Destroy(hitEffect, 1f);
        Destroy(this.gameObject, 1f);
    }

}
