using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the monster feature
/// attack the player by collisions
/// </summary>
public class Enemy : Feature {

    public float _attack;
    public Vector3 _repelForce;

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
