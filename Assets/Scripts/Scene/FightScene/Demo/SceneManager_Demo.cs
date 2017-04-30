using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager_Demo : SceneManagerFight {

    override protected void InitScene()
    {
        _bossPostion = new Vector3(5.69f, 2.04f, 0f);
        _checkPointId = 0;

        base.InitScene();
    }
}
