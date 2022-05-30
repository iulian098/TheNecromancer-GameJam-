using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable {
    [SerializeField] float maxHealth;
    float health;

    public Action OnDie;
    public Action OnHealthChanged;

    private void Awake() {
        health = maxHealth;
    }

    public void ReceiveDamage(float damage) {
        health -= damage;

        OnHealthChanged?.Invoke();

        if (health <= 0)
            OnDie?.Invoke();
    }

    public void Die() {
        throw new NotImplementedException();
    }
}
