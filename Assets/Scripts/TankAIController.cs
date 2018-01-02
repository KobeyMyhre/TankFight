using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAIController : TankController {

    Rigidbody2D rb;
    public float speed;
    public float bodyRotSpeed;
    public float rotSpeed;
    public Transform body;
    public Transform barrel;
    List<GameObject> Players;
    TankHealth[] playerSearch;

    

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        Players = new List<GameObject>();
        playerSearch = FindObjectsOfType<TankHealth>();
        for(int i =0; i < playerSearch.Length;i++)
        {
            if(playerSearch[i].gameObject != this.gameObject)
            {
                Players.Add(playerSearch[i].gameObject);
            }
            
        }

       
	}


    public GameObject moveToDecision()
    {
        float lowestDistance = Mathf.Infinity;
        GameObject retval = null;
        foreach(var G in Players)
        {
            float distance = Vector2.Distance(transform.position, G.transform.position);
            if(distance <= lowestDistance)
            {
                lowestDistance = distance;
                retval = G;
            }
        }
        return retval;
    }
    public void moveToTank(GameObject target)
    {
        Vector2 dir = transform.position - target.transform.position;
        dir += (Vector2)transform.position;
        body.transform.up = Vector3.Slerp(body.transform.up, ((Vector2)body.transform.position - dir).normalized, bodyRotSpeed * Time.deltaTime);
        rb.AddForce(body.transform.up * speed);
    }

    public void aimBarrel(GameObject target)
    {
        Vector2 dir = barrel.transform.position - target.transform.position;
        dir += (Vector2)barrel.transform.position;
        barrel.transform.up = Vector3.Slerp(body.transform.up, ((Vector2)barrel.transform.position - dir).normalized, rotSpeed * Time.deltaTime);

    }
	
	// Update is called once per frame
	void Update ()
    {
        moveToTank(moveToDecision());
        aimBarrel(moveToDecision());
	}
}
