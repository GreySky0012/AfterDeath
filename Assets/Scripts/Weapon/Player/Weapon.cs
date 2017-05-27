using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {

    public enum Type { gun };

    /// <summary>
    /// some data for calculation.
    /// </summary>
    float _firingInterval;//The interval of firing
    float _shotCD;//the rest CD of shotting

    bool _overHeated = false;//is the weapon heated
    [HideInInspector]
    public Slider _overHeatSlider;

    public GameObject _bulletPrefab;
    public Transform _spawnPos;
    public AudioClip _shootClip;

    public float _heatAdd_origin;
    public float _bulletSpeed_origin;
    public float _repulseRate_origin;
    public float _overHeatMax_origin;
    public float _fireRate_origin;
    public float _damage_origin;

    [HideInInspector]
    public float _heatAdd;
    [HideInInspector]
    public float _damage;
    [HideInInspector]
    public float _repulseRate;
    [HideInInspector]
    public float _overHeatMax;
    [HideInInspector]
    public float _fireRate;
    [HideInInspector]
    public float _bulletSpeed;

	// Use this for initialization
    protected void Start()
    {
        Init();
        _overHeatSlider.maxValue = _overHeatMax;
	}
	
	// Update is called once per frame
	protected void Update ()
    {
        if (_shotCD > 0)
        {
            _shotCD -= Time.deltaTime;
        }

        ReduceHeat();
	}

    /// <summary>
    /// reset the date of the weapon
    /// </summary>
    public void Init()
    {
        _damage = _damage_origin;
        _bulletSpeed = _bulletSpeed_origin;
        _fireRate = _fireRate_origin;
        _repulseRate = _repulseRate_origin;
        _overHeatMax = _overHeatMax_origin;
        _heatAdd = _heatAdd_origin;
    }

    /// <summary>
    /// Shot.
    /// </summary>
    public bool Shot()
    {
        if (_shotCD > 0||_overHeated)
            return false;

        if (!_overHeated)
        {
            _firingInterval = 1 / _fireRate;
            _overHeatSlider.value += _heatAdd;
            _shotCD = _firingInterval;
            if (_overHeatSlider.value >= _overHeatSlider.maxValue)
            {
                _overHeated = true;
            }
        }

        GameObject bullet = Instantiate(_bulletPrefab, _spawnPos.position, Quaternion.identity) as GameObject;

        Vector2 vel = new Vector2();
        vel = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _spawnPos.position;
        vel.Normalize();
        vel *= _bulletSpeed;

        AudioSource.PlayClipAtPoint(_shootClip, transform.position);

        bullet.GetComponent<PlayerBullet>().Init(_damage, vel);

        return true;
    }

    /// <summary>
    /// Rotates towards the mouse position.
    /// </summary>
    /// <param name="mousePosition">Mouse position.</param>
    public void RotateToMouse(Vector3 mousePosition)
    {
        float angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);
        angle = mousePosition.y > transform.position.y ? angle : -angle;

        if (mousePosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, -1, 1);
            angle = -angle;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        //   angle += mousePosition.x > transform.position.x ? 0 : 180;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    void ReduceHeat()
    {
        if (_overHeated)
        {
            _overHeatSlider.value -= 0.4f;
        }
        else
        {
            _overHeatSlider.value -= 0.8f;
        }

        if (_overHeatSlider.value <= 0)
        {
            _overHeated = false;
        }
    }
}
