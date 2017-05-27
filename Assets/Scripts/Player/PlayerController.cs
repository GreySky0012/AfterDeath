using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// player logic
/// </summary>
/// author:Alex
public class PlayerController : Feature {

    public struct Data
    {
        public float _defense;
        public float _moveSpeed;
        public float _jumpForce;
        public float _repulseRate;
        public float _collectionTime;//The time of each collection
        public float _invicibleTimeRate;

        public void Init()
        {
            _defense = PlayerData.Instance._defense_origin;
            _moveSpeed = PlayerData.Instance._moveSpeed_origin;
            _jumpForce = PlayerData.Instance._jumpForce_origin;
            _repulseRate = PlayerData.Instance._repulseRate_origin;
            _collectionTime = PlayerData.Instance._collectionTime_origin;
            _invicibleTimeRate = PlayerData.Instance._invicibleTimeRate_origin;
        }

        public void Set(Data date)
        {
            _defense = date._defense;
            _moveSpeed = date._moveSpeed;
            _jumpForce = date._jumpForce;
            _repulseRate = date._repulseRate;
            _collectionTime = date._collectionTime;
            _invicibleTimeRate = date._invicibleTimeRate;
        }
    }

    #region state of the hero
    public PlayerState _state;//now state of hero
    [HideInInspector]
	public bool _isFacingLeft;//is the hero facing left
    [HideInInspector]
	public bool _controllable = true;//is the hero controllable
    [HideInInspector]
    public bool _fireable = true;//is the player fireable in this scene
	[HideInInspector]
    public bool _isTouchingResource = false;//is the hero touching some resources
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
    public PlayerInfo _info;
    public BuffManager _buffs;
    public TechManager _techs;
    [HideInInspector]
    public Weapon _weapon;//The controller of the gun
    #endregion

    #region the basic function of Monobehaviour
    void Awake()
    {
        _playerData = new Data();
        _origin_data = new Data();
        _state = new StayState(this);
        _buffs = new BuffManager();
    }

	// Use this for initialization
	void Start ()
    {
        _techs = new TechManager(_info._techs);
        _scale = transform.localScale;
        _playerData.Init();
        _origin_data.Init();
        _techs.CalPlayerDate(_origin_data, _playerData);
        _maxHealth = 100f;
        _health = _maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (_state._type == PlayerState.Type.picking) {
            _collectionTime += Time.deltaTime;
            if (_collectionTime > _playerData._collectionTime)
            {
                PickResource();
                _collectionTime = 0;
            }
        }

        //techs must be calculated before than buffs
        _techs.TechsUpdate(this);
        _buffs.CheckBuffs();
        _buffs.BuffsUpdate(_playerData);
        _buffs.CalWeapon(_weapon);

        ListenKeyboard();
        ListenMouse();
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

    public void StartPickResource()
    {
        _state.ActionStartPick();
    }

    public void StopPickResource()
    {
        _state.ActionStopPick();
    }

    /// <summary>
    /// Exits the resource.
    /// </summary>
    public void ExitResource()
    {
        _state.ActionExitResource();
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

        _health -= CalDamage(damage);
        _hpText.text = _health.ToString() + "/" + _maxHealth;

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

    /// <summary>
    /// Start to pick up resource.
    /// </summary>
    public void startPickResource()
    {
        _collectionTime = 0;
    }

    /// <summary>
    /// Exits the resource.
    /// </summary>
    public void exitResource()
    {
        _state.ActionStopPick();
        _isTouchingResource = false;
        _nowTouching = null;
    }
    #endregion

    #region resource operation
    /// <summary>
	/// Touch the resource.
	/// </summary>
	/// <param name="resource">the touching resource.</param>
	private void TouchResource(ResourceController resource)
	{
		_isTouchingResource = true;
		_nowTouching = resource;
	}

	/// <summary>
	/// Picking the resource.
	/// </summary>
	private void PickResource()
	{
        CommonData.ResourceType type = _nowTouching._type;
		int num = _nowTouching.Collect (this);
        _info._bag.AddResource(type,num);
        ((SceneManagerFight)GameManager.Instance._scene).GetItem((int)type, num);
    }
    #endregion

    #region listen to the player input
    /// <summary>
    /// Listen to keyboard and handle it
    /// </summary>
    void ListenKeyboard()
	{
        if (Input.GetAxis("Horizontal") < 0)
        {
            _state.ActionMove(true);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            _state.ActionMove(false);
        }
        if (Input.GetButtonDown("Jump"))
        {
            _state.ActionJump();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
			StartPickResource();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            StopPickResource();
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
        if (!_fireable || _state._type == PlayerState.Type.picking)
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

    public void AddBuff(Buff.BuffType type,float existTime)
    {
        _buffs.CreateBuff(type, existTime);
    }

    public override void Death()
    {
        Destroy(this);
    }

    void SetGetControlToTrue()
    {
		_controllable = true;
    }

    /// <summary>
    /// calculate the real damage with the defense
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    float CalDamage(float damage)
    {
        float real_damage = damage * _playerData._defense;
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