using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class Buff : Checker {

    public enum BuffType
    {
        slow, poison,
        dizzy,
        imprisoned
    }

    [HideInInspector]
    public BuffType _buffType { get; protected set; }

    public float _existTime;
    public Sprite _iconSprite;

    public Buff(float existTime)
    {
        _existTime = existTime;
    }

    public abstract bool CheckMove();

    public abstract bool CheckControl();

    public abstract void CalPlayerData(PlayerController.Data playerData_origin,PlayerController.Data playerData);

    public abstract void CalBullet(PlayerBullet bullet);

    public abstract void BUffUpdate(PlayerController.Data player);
}
