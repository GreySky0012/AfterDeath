using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Feature {

    public float _attack;
    public Vector3 _repelForce;

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Death();
            return;
        }

        StartCoroutine(Blink(1.0f));
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == Tags.player)
        {
            collision.gameObject.GetComponent<PlayerController>().Hurt(_attack , 0.8f);
            collision.gameObject.GetComponent<PlayerController>().Repel(transform.position, _repelForce);
        }
    }

    override public void Death()
    {
        Destroy(gameObject);
    }
}
