using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Hero controller.
/// </summary>
/// author:Alex

public class PlayerController : Feature {

    public struct Data
    {
        public float _damage;
        public float _defence;
        public float _moveSpeed;
        public float _jumpForce;
        public float _repulseRate;
        public float _overHeatMax;
        public float _fireRate;
        public float _collectionTime;//The time of each collection
        public float _invicibleTimeRate;

        public void Init()
        {
            _defence = PlayerData.Instance._defence_origin;
            _moveSpeed = PlayerData.Instance._moveSpeed_origin;
            _jumpForce = PlayerData.Instance._jumpForce_origin;
            _collectionTime = PlayerData.Instance._collectionTime_origin;
            _invicibleTimeRate = PlayerData.Instance._invicibleTimeRate_origin;
        }
    }

	public enum state{ stay, move, jump, picking }

    #region state of the hero
    public PlayerState _state;//now state of hero
    [HideInInspector]
	public bool _isFacingLeft;//is the hero facing left
    [HideInInspector]
	public bool _controllable = true;//is the hero controllable
    [HideInInspector]
    public bool _fireable = true;
	[HideInInspector]
    public bool _isTouchingResource = false;//is the hero touching some resources
    [HideInInspector]
    public bool _picking = false;//is the hero picking resources
    [HideInInspector]
    public bool _invicible = false;
    #endregion

    #region the reference of the UI compenent
    [HideInInspector]
    public Slider _hpSlider;
    public Text _hpText;
    #endregion

    #region some data for calculation
    Vector3 _scale;//The inital scale of the hero
    ResourceController _nowTouching;//The resource the hero is touching
    float _collectionTime;
    #endregion

    #region the data of player
    public Data _playerData;
    public Data _origin_data { private set; get; }
    public Bag _bag;
    public BuffManager _buffs;
    [HideInInspector]
    public Weapon _weapon;//The controller of the gun
    #endregion

    #region the basic function of Monobehaviour
    void Awake()
    {
        _playerData = new Data();
        _origin_data = new Data();
        _state = new StayState(this);
    }

	// Use this for initialization
	void Start ()
    {
        _scale = transform.localScale;
        _playerData.Init();
        _maxHealth = PlayerData.Instance._maxHealth_origin;
        _health = _maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (_picking) {
            _collectionTime += Time.deltaTime;
            if (_collectionTime > _playerData._collectionTime)
            {
                PickResource();
                _collectionTime = 0;
            }
		}
		ListenKeyboard ();
        ListenMouse();
        _buffs.CheckBuffs();
        _hpText.text = _health.ToString() + "/" + _maxHealth;
	}
    #endregion

    #region actions called by extern
    /// <summary>
    /// try to make the hero dizzy
    /// </summary>
    /// <param name="time"></param>
    public void Dizzy(float time)
    {
        if (_invicible)
            return;
        _state.ActionDizzy(time);
    }

    public void Imprisoned(float time)
    {
        if (_invicible)
            return;
        _state.ActionImprisoned(time);
    }

    public void Shot()
    {
        if (IsControllable())
            _state.ActionShot();
    }

    public void Jump()
    {
        if (IsControllable())
            _state.ActionJump();
    }

    public void Move(bool isLeft)
    {
        if (IsControllable())
        {
            _state.ActionMove(isLeft);
        }
    }

    public void TurnTo(bool isLeft)
    {
        if (IsControllable())
        {
            _state.ActionTurnTo(isLeft);
        }
    }

	/// <summary>
	/// Hurt.
	/// </summary>
	/// <param name="damage">Amount of damage.</param>
	/// <param name="isToLeft">If set to <c>true</c> is towards left.</param>
	public void Hurt(float damage , float invicibleTime)
	{
        if (!_invicible)
            _state.ActionHurt(damage, invicibleTime);
	}

    public void Repel(Vector3 attackerPos, Vector3 repelForce)
    {
        if(!_invicible)
            _state.ActionRepel(attackerPos, repelForce);
    }

    /// <summary>
    /// The hero fall to the ground
    /// </summary>
    public void FallGround()
	{
        _state = new StayState(this);
	}
    #endregion

    #region actions called by player states
    /// <summary>
    /// make the hero dizzy(real function)
    /// </summary>
    public void dizzy(float time)
    {

    }

    public void imprisoned(float time)
    {

    }

    public void move(bool left)
    {
        transform.position += new Vector3((left ? -1 : 1) * _playerData._moveSpeed, 0, 0);
    }

    /// <summary>
    /// The hero jumps
    /// </summary>
    public void jump()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector3(0, _playerData._jumpForce, 0));
    }

    /// <summary>
    /// Shot.
    /// </summary>
    public void shot()
    {
        _weapon.Shot();
    }

    /// <summary>
    /// Let the hero turn to a direction
    /// </summary>
    /// <param name="isLeft">If set to <c>true</c> is left.</param>
    public void turnTo(bool isLeft)
    {
        if (_isFacingLeft == isLeft)
            return;
        else
        {
            transform.localScale = new Vector3((isLeft ? -1 : 1) * _scale.x, _scale.y, _scale.z);
            _isFacingLeft = isLeft;
        }
    }

    /// <summary>
    /// Handles the damage.
    /// </summary>
    /// <param name="damage">Amount of damage.</param>
    public void hurt(float damage, float invicibleTime)
    {
        _invicible = true;

        _health -= calcuDamage(damage);
        if (_health <= 0)
        {
            StartCoroutine(ReduceHp(0));
            Destroy(gameObject);
            return;
        }

        Invoke("SetInvicibleToFalse", invicibleTime * _playerData._invicibleTimeRate);

        StartCoroutine(Blink(invicibleTime));

        StartCoroutine(ReduceHp(_health));
    }

    public void repel(Vector3 attackerPos, Vector3 repelForce)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(((attackerPos.x - transform.position.x) > 0 ? -1 : 1) * repelForce.x * _playerData._repulseRate, repelForce.y * _playerData._repulseRate));

        _controllable = false;

        Invoke("SetGetControlToTrue", 0.2f);
    }
    #endregion

    #region resource operation
    /// <summary>
	/// Touchs the resource.
	/// </summary>
	/// <param name="resource">the touching resource.</param>
	void TouchResource(ResourceController resource)
	{
		_isTouchingResource = true;
		_nowTouching = resource;
	}

	/// <summary>
	/// Starts to pick up resource.
	/// </summary>
	public void startPickResource()
    {
        _picking = true;
        _collectionTime = 0;
    }

	/// <summary>
	/// Picking the resource.
	/// </summary>
	void PickResource()
	{
        CommonData.ResourceType type = _nowTouching._type;
		int num = _nowTouching.Collect (this);
        _bag.AddResource(type,num);
        ((SceneManagerFight)GameManager.Instance._scene).GetItem((int)type, num);
    }

	/// <summary>
	/// Stops picking resource.
	/// </summary>
	public void stopPickResource()
	{
        _picking = false;
        _controllable = true;
	}

	/// <summary>
	/// Exits the resource.
	/// </summary>
	public void ExitResource()
	{
		stopPickResource ();
		_isTouchingResource = false;
		_nowTouching = null;
	}
    #endregion

    #region listen to the player input
    /// <summary>
    /// Listen to keyboard and handle it
    /// </summary>
    void ListenKeyboard()
	{
        if (Input.GetKey(KeyCode.A))
        {
            _state.ActionMove(true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _state.ActionMove(false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _state.ActionJump();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
			if (_isTouchingResource)
            {
                _state.ActionStartPick();
            }
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            _state.ActionStopPick();
        }
	}

	/// <summary>
	/// Listen to mouse and transmit it.
	/// </summary>
	void ListenMouse()
	{
		HandleMousePosition (Camera.main.ScreenToWorldPoint(Input.mousePosition));
		if (Input.GetMouseButton (0))
        {
            _state.ActionShot();
		}
	}

	/// <summary>
	/// Handles the mouse position.
	/// </summary>
	void HandleMousePosition(Vector3 mousePosition)
	{
        if (_picking)
            return;
		_state.ActionTurnTo (((mousePosition.x - transform.position.x) < 0) ? true : false);
		_weapon.RotateToMouse (mousePosition);
	}
    #endregion

    #region some judge functions
    public bool IsControllable()
    {
        if (!_controllable)
            return false;
        if (!_buffs.CheckControl())
            return false;
        return true;
    }

    public bool IsMoveable()
    {
        if (!_buffs.CheckMove())
            return false;
        return true;
    }
    #endregion

    void SetGetControlToTrue()
    {
		_controllable = true;
    }

    /// <summary>
    /// calculate the real damage with the defence
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    float calcuDamage(float damage)
    {
        float real_damage = damage * _playerData._defence;
        return real_damage;
    }

    void SetInvicibleToFalse()
    {
        _invicible = false;
    }

    IEnumerator ReduceHp(float nowHp)
    {
        for(int i = 0;i < 10;i++)
        {
            _hpSlider.value -= (_hpSlider.value - nowHp) / (10 - i);
            yield return new WaitForSeconds(0.01f);
        }
        _hpSlider.value = nowHp;
    }

    public override void Death()
    {
        Destroy(this);
    }

    #region deal with the collision of the player
    /// <summary>
    /// Raises the collision enter2d event.
    /// </summary>
    /// <param name="coll">Coll.</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == Tags.ground)
        {
            FallGround();
        }
    }

    /// <summary>
    /// Raises the trigger enter2 d event.
    /// </summary>
    /// <param name="coll">Coll.</param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        switch (coll.gameObject.tag)
        {
            case Tags.resource:
                TouchResource(coll.gameObject.GetComponent<ResourceController>());
                break;
        }
    }

    /// <summary>
    /// Raises the collision exit2 d event.
    /// </summary>
    /// <param name="coll">Coll.</param>
    void OnTriggerExit2D(Collider2D coll)
    {
        switch (coll.gameObject.tag)
        {
            case Tags.resource:
                _state.ActionExitResource();
                break;
        }
    }
    #endregion
}