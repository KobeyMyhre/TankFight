using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesPowerGun : PowerUpGun {

	// Use this for initialization
	void Start ()
    {
        fireDelay = .5f;
        maxSpecialAmmo = currentSpecialAmmo;
        PowerUpPool = Statics.minePool;
	}


    public override void shoot(TankScore score, Statics.Player playerNum, Transform spawnTrans)
    {
        currentSpecialAmmo--;


        //Bullet Spawning
        var mine = PowerUpPool.GetObject();
        mine.SetActive(true);
        mine.transform.position = spawnTrans.transform.position;
        mine.transform.up = spawnTrans.transform.up;

        var mineProperties = mine.GetComponent<TankMine>();
        mineProperties.score = score;
        mineProperties.playerNum = playerNum;
        
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
