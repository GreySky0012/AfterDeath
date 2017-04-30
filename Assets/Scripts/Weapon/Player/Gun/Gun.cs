using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon {

    void Start()
    {
        base.Start();
        _bulletSpeed = _bulletSpeed_origin = 15f;
        _fireRate = _fireRate_origin = 5f;
        _repulseRate_origin = 1f;
        _overHeatMax_origin = 100f;
    }

	// Update is called once per frame
	void Update () {
        base.Update();
	}

}