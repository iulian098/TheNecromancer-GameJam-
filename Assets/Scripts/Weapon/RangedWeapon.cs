using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField] Projectile projectile;
    [SerializeField] Transform spawnPoint;
    [SerializeField] string shootSfx;

    public override void Attack() {
        Projectile proj = Instantiate(projectile, spawnPoint.position, parent.transform.rotation);
        proj.Damage = damage;
        SFXPlayer.SFXManager.PlaySFX(shootSfx, transform);
    }
}
