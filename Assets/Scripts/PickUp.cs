using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {


    PowerUpGun specialGun;


    public virtual bool isPickUpAble()
    {
        return true;
    }
    public virtual void doInteraction()
    {

    }


    public void destroyPickup()
    {
        gameObject.SetActive(false);
    }


	// Use this for initialization
	void Start ()
    {
        specialGun = GetComponent<PowerUpGun>();
	}


    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            var powerGun = other.gameObject.GetComponent<TankPowerGun>();
            if(powerGun != null && specialGun != null)
            {
                if(powerGun.myPowerUp.Count > 0)
                {
                    powerGun.myPowerUp.Pop();
                }
                powerGun.myPowerUp.Push(specialGun);
                gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
