using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDrop : PickUp
{


    



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            var ammoOfTank = other.gameObject.GetComponent<TankGun>();
            if (ammoOfTank != null)
            {
                //ammoOfTank.reload();
                destroyPickup();
            }
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
