using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Feature : MonoBehaviour {

    [HideInInspector]
    public float _health;
    public float _maxHealth;

    public void Start()
    {
        _health = _maxHealth;
    }

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
