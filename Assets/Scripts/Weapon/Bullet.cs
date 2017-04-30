using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public GameObject _flarePrefab;
    [HideInInspector]
    public float _damage;

    virtual public void Init(float damage, Vector2 vel)
    {
        _damage = damage;
        GetComponent<Rigidbody2D>().velocity = vel;
    }

    public void ShowParticleEffect()
    {
        GameObject go = Instantiate(_flarePrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(go, 1f);
        Destroy(gameObject);
    }
}
