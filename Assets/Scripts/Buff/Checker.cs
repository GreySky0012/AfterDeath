using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface Checker{
    bool CheckMove();
    bool CheckControl();
    void CalPlayerData(PlayerController.Data playerData_origin,PlayerController.Data player);
    void CalBullet(PlayerBullet bullet);
}
