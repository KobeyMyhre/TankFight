using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : PickUp
{

    public int healAmount;




    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            var healthOfTank = other.gameObject.GetComponent<TankHealth>();
            if(healthOfTank != null)
            {
                healthOfTank.healTank(healAmount);
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
