using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMine : MonoBehaviour
{

    // Use this for initialization
    public int Damage;
    ObjectPool explosion;
    pooledObject me;
    public Statics.Player playerNum;

    public TankScore score;
    public float blinkInterval;
    float blinkTimer;
    public GameObject blinker;
    bool onOff;
	void Start ()
    {
        onOff = true;
        explosion = Statics.explosionPool[0];
        me = GetComponent<pooledObject>();
        blinker = transform.GetChild(0).gameObject;
	}

    void spawnBoom()
    {
        var explosionSpawn = explosion.GetObject();
        explosionSpawn.transform.position = transform.position;
        explosionSpawn.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            var hitable = collision.transform.parent.GetComponent<IDamageable>();
            var tankGun = collision.transform.parent.GetComponent<TankGun>();
            if (hitable != null && tankGun.playerNum != playerNum)
            {
                if (hitable.takeDamage(Damage))
                {
                    score.updateScore();
                }
                spawnBoom();
                me.returnToPool();
            }

        }
        
    }


    // Update is called once per frame
    void Update ()
    {
        blinkTimer -= Time.deltaTime;
        if(blinkTimer <= 0)
        {
            onOff = !onOff;
            blinker.SetActive(onOff);
            blinkTimer = blinkInterval;
        }
	}
}
