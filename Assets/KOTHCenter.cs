using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KOTHCenter : MonoBehaviour {


    ScoreManager scoreManager;

	// Use this for initialization
	void Start ()
    {
        scoreManager = Statics.scoreManager;
	}

    int numOnHill;
    TankScore guyInHill;
    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    numOnHill = 0;
    //    foreach(RespawnManager tank in scoreManager.players )
    //    {
    //        if(tank.gameObject == other.transform.parent.transform.parent.gameObject)
    //        {
    //            numOnHill++;
    //            guyInHill = tank.gameObject;
    //        }
    //    }
    //    if(numOnHill == 1)
    //    {

    //    }
    //}
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 2);
    }

    // Update is called once per frame
    void Update ()
    {
        Collider2D[] guysOnHill = Physics2D.OverlapCircleAll(transform.position, 2);
        numOnHill = 0;
        foreach(var guy in guysOnHill)
        {
            var isItTank = guy.GetComponentInParent<TankScore>();
            if(isItTank != null)
            {
                numOnHill++;
                guyInHill = isItTank;
            }
        }
        if(numOnHill == 1)
        {
            guyInHill.updateScore();
        }
       
	}
}
