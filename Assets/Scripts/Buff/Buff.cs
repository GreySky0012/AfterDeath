using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// The abstract class of the buff logic
/// </summary>
public abstract class Buff
{
    /// <summary>
    /// the buff type enum
    /// </summary>
    public enum BuffType
    {
        slow, poison,
        dizzy,
        imprisoned
    }

    [HideInInspector]
    public BuffType _buffType { get; protected set; }

    public float _existTime;//the exist time of buff(never change)
    public Sprite _iconSprite;//the image of this type of buff,init in the child class

    public Buff(float existTime)
    {
        _existTime = existTime;
    }

    #region called by the same name function in buff manager
    public abstract bool CheckMove();

    public abstract bool CheckControl();

    /// <summary>
    /// called by the buff managers
    /// </summary>
    /// <param name="playerData_origin">the origin data of the player</param>
    /// <param name="playerData">the current data of the player</param>
    public abstract void CalPlayerData(PlayerController.Data playerData_origin,PlayerController.Data playerData);

    public abstract void CalWeapon(Weapon weapon);

    public abstract void BuffUpdate(PlayerController.Data player);
    #endregion
}