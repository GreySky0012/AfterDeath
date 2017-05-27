using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet {
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == Tags.enemy)
        {
            collision.GetComponent<BossController>().TakeDamage(_damage);
            ShowParticleEffect();
        }

        if (collision.transform.tag == Tags.wall || collision.transform.tag == Tags.ground)
        {
            ShowParticleEffect();
        }
    }
}
