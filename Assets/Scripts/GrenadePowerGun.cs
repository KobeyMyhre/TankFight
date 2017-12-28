using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePowerGun : PowerUpGun {


    public float grenadeSpeed;

	// Use this for initialization
	void Start ()
    {
        PowerUpPool = Statics.grenadePool;
        fireDelay = .6f;
        maxSpecialAmmo = currentSpecialAmmo;
	}

    public override void shoot(TankScore score, Statics.Player playerNum, Transform spawnTrans)
    {
        currentSpecialAmmo--;

        var grenade = PowerUpPool.GetObject();
        grenade.SetActive(true);
        grenade.transform.position = spawnTrans.transform.position;
        grenade.transform.up = spawnTrans.transform.up;
        Vector2 vel = grenade.transform.up * grenadeSpeed;
        grenade.GetComponent<Rigidbody2D>().velocity = vel;

        TankGrenade grenadeProperties = grenade.GetComponent<TankGrenade>();
        grenadeProperties.startPos = spawnTrans.position;
        grenadeProperties.playerNum = playerNum;
        grenadeProperties.score = score;

    }
    // Update is called once per frame
    void Update () {
		
	}
}
