﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : MonoBehaviour {

    public int frontalDamage;
    public int normalDamage;
    ObjectPool explosion;
    ObjectPool weakExplosion;
    pooledObject me;
    [HideInInspector]
    public Statics.Player playerNum;
    [HideInInspector]
    public TankScore score;
    // Use this for initialization
    void Start ()
    {
        explosion = Statics.explosionPool[0];
        weakExplosion = Statics.explosionPool[1];
        me = GetComponent<pooledObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void spawnBoom()
    {
        var explosionSpawn = explosion.GetObject();
        explosionSpawn.transform.position = transform.position;
        explosionSpawn.SetActive(true); 
    }
    void spawnLilBoom()
    {
        var explosionSpawn = weakExplosion.GetObject();
        explosionSpawn.transform.position = transform.position;
        explosionSpawn.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {
            var hitable = collision.transform.parent.GetComponent<IDamageable>();
            var tankGun = collision.transform.parent.GetComponent<TankGun>();
            if(hitable != null && tankGun.playerNum != playerNum )
            {
                Vector2 dir = collision.transform.position - transform.position;

                float angle = Vector2.Angle(dir.normalized, collision.transform.up);
                float topBottom = Mathf.Sign(collision.transform.position.y - transform.position.y);
                //Debug.Log(angle);

                if(topBottom == -1 && angle >= 125 && angle <= 180)
                {
                    spawnLilBoom();
                    if(hitable.takeDamage(frontalDamage))
                    {
                        score.updateScore();
                    }
                }
                else
                {
                    spawnBoom();
                    if (hitable.takeDamage(normalDamage))
                    {
                        score.updateScore();
                    }
                    //Debug.Log("side/back");
                }


                //if (angle >= 40 && angle <= 130)
                //{
                //    hitable.takeDamage(sideDamage);
                //    Debug.Log("Side Hit");
                //}
                //else
                //{
                //    hitable.takeDamage(frontalDamage);
                //    Debug.Log("Front/back Hit");
                //}
                
                me.returnToPool();
            }
                       
        }
        if(collision.tag == "Obstacle" || collision.tag == "Bullet")
        {
            spawnBoom();
            me.returnToPool();
        }
    }
}
