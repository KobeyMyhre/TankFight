using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGun : MonoBehaviour {

    [HideInInspector]
    public ObjectPool bulletPool;
    public ObjectPool automaticPool;
    TankController controller;
    TankScore score;
    public GameObject barrel;
    public float bulletSpeed;
    public Statics.Player playerNum;
    public float reloadTime;
    private float reloadingTimer;
    public float autoDelay;
    private float autoTimer;
    public int automaticAmmo;

    public GameObject fireEffect;
    public Sprite bulletSprite;
    public GameObject automaticEffect;
    public TextMesh outOfAmmoText;
    AudioSource shootSound;
	// Use this for initialization
	void Start ()
    {
        shootSound = GetComponent<AudioSource>();
        score = GetComponent<TankScore>();
        bulletPool = Statics.gunPool;
        automaticPool = Statics.automaticBulletPool;
        controller = GetComponent<TankController>();
        reloadingTimer = 0;
        autoTimer = 0;
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
    void turnOffAutoEffect()
    {
        automaticEffect.SetActive(false);
    }
    void shoot()
    {
        shootSound.Play();
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


    void autoShoot()
    {
        automaticEffect.SetActive(true);
        Invoke("turnOffAutoEffect", autoDelay);
        var bullet = automaticPool.GetObject();
        bullet.SetActive(true);
        bullet.transform.position = barrel.transform.position;
        bullet.transform.up = barrel.transform.up;
        Vector2 vel = bullet.transform.up * bulletSpeed;
        bullet.GetComponent<Rigidbody2D>().velocity = vel;
        TankBullet bulletProperties = bullet.GetComponent<TankBullet>();
        bulletProperties.playerNum = playerNum;
        bulletProperties.score = score;
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

        if(controller.autoFire && autoTimer <= 0 && automaticAmmo > 0)
        {
            autoTimer = autoDelay;
            autoShoot();
            automaticAmmo--;
        }
        else if(controller.autoFire && automaticAmmo <= 0)
        {
            outOfAmmoText.gameObject.SetActive(true);

        }
        else if(autoTimer > 0)
        {
            outOfAmmoText.gameObject.SetActive(false);
            autoTimer -= Time.deltaTime;
        }
        else
        {
            outOfAmmoText.gameObject.SetActive(false);
        }

	}
}
