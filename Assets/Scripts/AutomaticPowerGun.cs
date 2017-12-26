using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticPowerGun : PowerUpGun {

   
   
    public float bulletSpeed;


    // Use this for initialization
    void Start ()
    {
        PowerUpPool = Statics.automaticBulletPool;
        fireDelay = .2f;
        maxSpecialAmmo = currentSpecialAmmo;
    }
   
    
    public override void shoot(TankScore score, Statics.Player playerNum, Transform spawnTrans)
    {
        currentSpecialAmmo--;
       

        //Bullet Spawning
        var bullet = PowerUpPool.GetObject();
        bullet.SetActive(true);
        bullet.transform.position = spawnTrans.transform.position;
        bullet.transform.up = spawnTrans.transform.up;
        Vector2 vel = bullet.transform.up * bulletSpeed;
        bullet.GetComponent<Rigidbody2D>().velocity = vel;

        //Score and Owner
        TankBullet bulletProperties = bullet.GetComponent<TankBullet>();
        bulletProperties.frontalDamage = frontDamage;
        bulletProperties.normalDamage = normalDamage;
        bulletProperties.playerNum = playerNum;
        bulletProperties.score = score;
    }
    // Update is called once per frame
    void Update ()
    {
       
    }
}
