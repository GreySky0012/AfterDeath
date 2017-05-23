using UnityEngine;
using System.Collections;

public class DoubleJump : FootTech
{
    private bool _doubleJumpable = true;
    public DoubleJump() { _name = FootTech.FootTechs.doubleJump.ToString(); }

    public override void TechUpdate(PlayerController player)
    {
        //落地检测有问题，暂时没想到好的处理方法
        if (!_doubleJumpable && player._state._type != PlayerState.Type.jump)
        {
            _doubleJumpable = true;
        }
        if (_doubleJumpable && player._state._type == PlayerState.Type.jump)
        {
            if (Input.GetButtonDown("Jump"))
            {
                player.jump();
                _doubleJumpable = false;
            }
        }
    }

    public override void CalPlayerData(PlayerController.Data originData, PlayerController.Data data)
    {
        data._moveSpeed = originData._moveSpeed * 1.01f;
    }
}