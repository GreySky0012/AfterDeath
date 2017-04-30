using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDemo : BossController {

    public Vector3 _fromPos;
    public Vector3 _toPos;
    [HideInInspector]
    public Transform _player;
    public float _range;
    public float _rate;
    public float _shootRate;
    public bool _bossMove = false;

    public float _bullet_damage;

    public GameObject _bulletPrefab;

    private float circleRate;
    private float timer = 0;
    private float shootTimer = 0f;
    public Transform spawnPos;

	// Use this for initialization
    void Start()
    {
        base.Start();
        _player = GameManager.Instance._scene._player.transform;
        circleRate = _range;
	}
	
	// Update is called once per frame
	void Update () {
        shootTimer += Time.deltaTime; // 射击计时器

        MoveSphere();    // 弧线移动
        LookAtPlayer();  // 朝向角色

        if (shootTimer > Random.Range(_shootRate * 0.5f, _shootRate * 1.5f))
            Shoot();

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ShootLazer());
        }
	}
    void MoveSphere()
    {
        if (_bossMove)
        {
            timer += Time.deltaTime;

            // 弧线的中心
            Vector3 center = (_fromPos + _toPos) * 0.5f;

            // 向下移动中心，垂直于弧线
            center -= new Vector3(0, circleRate, 0);

            // 相对于中心在弧线上插值

            Vector3 riseRelCenter = _fromPos - center;

            Vector3 setRelCenter = _toPos - center;

            transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, timer * _rate);

            transform.position += center;

            if ((transform.position - _toPos).magnitude < 0.1f)
            {
                Vector3 temp = new Vector3(_toPos.x,_toPos.y,_toPos.z);
                _toPos = _fromPos;
                _fromPos = temp;
                timer = 0;
                circleRate = _range;
            }
        }

    }

    void LookAtPlayer()
    {
        float angle = Vector3.Angle(Vector3.right, _player.position - transform.position);
        transform.rotation = Quaternion.Euler(0, 0, transform.position.y > _player.position.y ? 360 - angle : angle);
    }

    void Shoot()
    {
        shootTimer = 0f;

        GameObject bullet = Instantiate(_bulletPrefab, spawnPos.position, Quaternion.identity) as GameObject;

        Vector2 vel = new Vector2(15f, 0);
        vel = spawnPos.rotation * vel;

        bullet.GetComponent<EnemyBullet>().Init(_bullet_damage, _repelForce, vel);
    }

    public void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (_health <= 400)
        {
            Vector3 tempPos = transform.position;
            GetComponent<SpriteRenderer>().sprite = Resources.Load("Pictures/boss_demo_2", typeof(Sprite)) as Sprite;
            _rate = 0.9f;
            _shootRate = 0.6f;
            transform.position = tempPos;
        }
    }

    IEnumerator ShootLazer()
    {
        RaycastHit2D hit;
        int count = 0;
        Vector3 direction = _player.position - spawnPos.position;
        direction.z = 0f;
        while (count <= 60)
        {
            hit = Physics2D.Raycast(spawnPos.position, direction + new Vector3(count * 0.2f, 0, 0), 10000, (1 << 8));

            if (hit.collider == null)
                break;

            count++;

            Debug.DrawLine(spawnPos.position, hit.point, Color.red);

            yield return new WaitForSeconds(0.01f);

        }
    }
}
