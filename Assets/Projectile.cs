using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    enum WeaponDamageCount
    {
        bullet = 10,
        cannon = 50,
        laser = 20,
        cheat = 100
    }

    int damage = (int)WeaponDamageCount.bullet;

    private void OnCollisionEnter(Collision col)
    {
        GameObject hit = col.gameObject;
        Health health = hit.GetComponent<Health>();

        if(health != null)
        {
            health.TakeDamage(damage);
        }

        Destroy(this.gameObject);

    }

    // x = 1.0f
    //condition   ? first_expression : second_expression; 
    //return x = 0.0 ? Math.Sin(x) / x  : 1.0;
}
 