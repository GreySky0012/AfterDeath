using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager_1 : SceneManagerFight {

    override protected void InitScene()
    {
        _bossPostion = new Vector3(0f, 3f, 0f);
        _checkPointId = 1;

        base.InitScene();
    }
}
