using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// The buffs icon interface script
/// </summary>
public class AutoReduce : MonoBehaviour {

    private PlayerController _player;
    private SceneManagerFight _scene;//the scene where the buff exist
    public float _restTime;//the rest time of this buff

    public Buff _buff;//the buff logic

    /// <summary>
    /// some references of the buff interface
    /// </summary>
    public Slider _slider;
    public Image _backImage;
    public Image _forntImage;

    void Awake()
    {
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
