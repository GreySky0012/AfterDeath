using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Feature : MonoBehaviour {

    [HideInInspector]
    public float _health;
    [HideInInspector]
    public float _maxHealth;

    public void Start()
    {
        _health = _maxHealth;
    }

    /// <summary>
    /// be hurt
    /// </summary>
    /// <param name="damage"></param>
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

    /// <summary>
    /// show the feature a blink effect
    /// </summary>
    /// <param name="blinkTime"></param>
    /// <returns></returns>
    public IEnumerator Blink(float blinkTime)
    {
        int count = (int)(blinkTime / 0.1f);
        SpriteRenderer renderer = transform.GetComponent<SpriteRenderer>();

        for (int i = 0; i < count; i++)
        {
            renderer.enabled = false;
            yield return new WaitForSeconds(0.05f);
            renderer.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }
    }

    abstract public void Death();
}
