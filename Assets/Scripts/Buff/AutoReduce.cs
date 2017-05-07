using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AutoReduce : MonoBehaviour {

    public Slider _slider;
    private PlayerController _player;
    private SceneManagerFight _scene;
    public float _restTime;

    public Buff _buff;

    public Image _backImage;
    public Image _forntImage;

    void Start()
    {
        _slider = GetComponentInChildren<Slider>();
        _scene = GameManager.Instance._scene as SceneManagerFight;
        _player = _scene._player;
    }

    void Update()
    {
        _restTime -= Time.deltaTime;
        if (_restTime <= 0)
        {
            DestroyBuff();
        }
        else
            _slider.value = _restTime / _buff._existTime*_slider.maxValue;
    }

    public void Init(Buff buff)
    {
        _buff = buff;
        _restTime = buff._existTime;
        _backImage.overrideSprite = _forntImage.overrideSprite = _buff._iconSprite;
    }

    public void DestroyBuff()
    {
        _player._buffs.RemoveBuff(this);
        Destroy(gameObject);
    }
}
