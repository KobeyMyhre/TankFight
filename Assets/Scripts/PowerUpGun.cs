using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGun : MonoBehaviour {

    
    


    //What bullets are Spawned
    public ObjectPool PowerUpPool;

    
    public float fireDelay;


    public int frontDamage;
    public int normalDamage;
   
    public int currentSpecialAmmo;
    public int maxSpecialAmmo;
   
    public bool displayAmmoText()
    {
        return currentSpecialAmmo <= 0;
        
    }


    public virtual void shoot(TankScore score, Statics.Player playerNum, Transform spawnTrans)
    {

    }
    public void reload()
    {
        if (currentSpecialAmmo < maxSpecialAmmo)
        {
            currentSpecialAmmo = maxSpecialAmmo;
        }
    }
   
    // Use this for initialization
    void Start ()
    {
        
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
