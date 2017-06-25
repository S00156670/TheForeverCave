using UnityEngine;
using System.Collections;

public interface IProjectileWeapon {

    Transform ProjectileSpawn { get; set; }

    void CastProjectile();

}
