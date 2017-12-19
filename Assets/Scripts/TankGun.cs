using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGun : MonoBehaviour {

    [HideInInspector]
    public ObjectPool bulletPool;
    TankController controller;
    TankScore score;
    public GameObject barrel;
    public float bulletSpeed;
    public Statics.Player playerNum;
    public float reloadTime;
    private float reloadingTimer;
    public GameObject fireEffect;
    public Sprite bulletSprite;
	// Use this for initialization
	void Start ()
    {
        score = GetComponent<TankScore>();
        bulletPool = Statics.gunPool;
        controller = GetComponent<TankController>();
        reloadingTimer = reloadTime;
        switch (controller.playerNum)
        {
            case 1:
                playerNum = Statics.Player.one;
                break;
            case 2:
                playerNum = Statics.Player.two;
                break;
            case 3:
                playerNum = Statics.Player.three;
                break;
            case 4:
                playerNum = Statics.Player.four;
                break;
        }
    }
	
    void turnOffEffect()
    {
        fireEffect.SetActive(false);
    }
    void shoot()
    {
        fireEffect.SetActive(true);
        Invoke("turnOffEffect", .1f);
        var bullet = bulletPool.GetObject();
        bullet.SetActive(true);
        bullet.transform.position = barrel.transform.position;
        bullet.transform.up = barrel.transform.up;
        Vector2 vel = bullet.transform.up * bulletSpeed;
        bullet.GetComponent<Rigidbody2D>().velocity = vel;
        TankBullet bulletProperties = bullet.GetComponent<TankBullet>();
        bulletProperties.playerNum = playerNum;
        bulletProperties.score = score;
        bullet.GetComponent<SpriteRenderer>().sprite = bulletSprite;
    }

	// Update is called once per frame
	void Update ()
    {
		if(controller.fire && reloadingTimer <= 0)
        {
            shoot();
            reloadingTimer = reloadTime;
        }
        else
        {
            reloadingTimer -= Time.deltaTime;
        }
	}
}
