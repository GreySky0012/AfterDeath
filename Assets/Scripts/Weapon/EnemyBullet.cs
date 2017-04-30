using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet {

    [HideInInspector]
    public Vector3 _repelForce;

    public void Init(float damage, Vector3 repelForce,Vector3 vel)
    {
        base.Init(damage, vel);
        _repelForce = repelForce;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == Tags.player)
        {
            collision.GetComponent<PlayerController>().Hurt(_damage, 0.5f);
            collision.GetComponent<PlayerController>().Repel(transform.position, _repelForce);
            ShowParticleEffect();
        }

        if (collision.transform.tag == Tags.wall || collision.transform.tag == Tags.ground)
        {
            ShowParticleEffect();
        }
    }
}
