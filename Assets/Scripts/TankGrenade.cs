using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGrenade : MonoBehaviour {

    public int Damage;
    ObjectPool explosion;
    pooledObject me;
    [HideInInspector]
    public Statics.Player playerNum;
    [HideInInspector]
    public TankScore score;
    [HideInInspector]
    public Vector3 startPos;
    public float travelDistance;
    public float rotSpeed;

	// Use this for initialization
	void Start ()
    {
        
        explosion = Statics.explosionPool[1];
        me = GetComponent<pooledObject>();
	}
    void spawnBoom()
    {
        var explosionSpawn = explosion.GetObject();
        explosionSpawn.transform.position = transform.position;
        explosionSpawn.SetActive(true);
    }

    void Explode()
    {
        Collider2D[] hood = Physics2D.OverlapCircleAll(transform.position, 1);
        foreach(Collider2D obj in hood)
        {
            if(obj.tag == "Player")
            {
                var tankGun = obj.transform.parent.GetComponent<TankGun>();
                if(tankGun.playerNum != playerNum)
                {
                    if(obj.gameObject.transform.parent.GetComponent<IDamageable>().takeDamage(Damage))
                    {
                        score.updateScore();
                    }
                }
                
            }
        }
    }

 

    // Update is called once per frame
    void Update ()
    {
        transform.Rotate(0,0,rotSpeed);
        if(Vector3.Distance(transform.position,startPos) >= travelDistance)
        {
            spawnBoom();
            Explode();
            me.returnToPool();
        }
	}
}
