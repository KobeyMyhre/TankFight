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
    
    public List<GameObject> Players;
    TankHealth[] playerSearch;
    TankPowerGun powerGun;
    public float reactionDelay;
    float reactionTimer;
    public float shootReactionDelay;
    float shootReactionTimer;
    public Statics.GameMode gameMode;

    KOTHCenter controlPoint;



    Vector3[] path;
    int targetIdx = 0;
    



	// Use this for initialization
	void Start ()
    {
        reactionDelay = .1f;
        reactionTimer = 0;
        shootReactionDelay = 1.8f;
        shootReactionTimer = 0;
        powerGun = GetComponent<TankPowerGun>();
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
        if(gameMode == Statics.GameMode.KingOfTheHill)
        {
            controlPoint = FindObjectOfType<KOTHCenter>();
        }

        
       
	}


    public GameObject moveToDecision()
    {
        if(controlPoint != null)
        {
            return controlPoint.gameObject;
        }
        float lowestDistance = Mathf.Infinity;
        GameObject retval = null;
        foreach(var G in Players)
        {
            var gHealth = G.GetComponent<TankHealth>();
            if(gHealth != null)
            {
                if (gHealth.isAlive)
                {
                    float distance = Vector2.Distance(transform.position, G.transform.position);
                    if (distance <= lowestDistance)
                    {
                        lowestDistance = distance;
                        retval = G;
                    }
                }
            }
            else
            {
                float distance = Vector2.Distance(transform.position, G.transform.position);
                if (distance <= lowestDistance)
                {
                    lowestDistance = distance;
                    retval = G;
                }
            }
            
            
        }
        return retval;
    }
    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIdx; i < path.Length; i++)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawSphere(path[i], .2f);
                if (i == targetIdx)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful && gameObject.activeInHierarchy)
        {
            path = newPath;
            
            StopCoroutine("followPath");
            StartCoroutine("followPath");
        }
    }

    bool closeEnoughVector(Vector2 posA, Vector2 posB, float acurracy)
    {
        float acceptablePosX = posB.x + acurracy;
        float acceptableNegX = posB.x - acurracy;
        float acceptablePosY = posB.y + acurracy;
        float acceptableNegY = posB.y - acurracy;

        if(posA.x > acceptableNegX && posA.x < acceptablePosX && posA.y < acceptablePosY && posA.y > acceptableNegY)
        {
            return true;
        }
        return false;
    }

    IEnumerator followPath()
    {
        Vector3 currentWaypoint = path[0];
        
        while (true)
        {
            
            if (closeEnoughVector(transform.position, currentWaypoint, .02f))
            {
                targetIdx++;
                if (targetIdx >= path.Length)
                {
                    
                    yield break;
                }
                currentWaypoint = path[targetIdx];
            }
            Vector2 dir = (Vector2)(transform.position - currentWaypoint);
            dir += (Vector2)transform.position;
            body.transform.up = Vector3.Slerp(body.transform.up, ((Vector2)body.transform.position - dir).normalized, bodyRotSpeed * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);

            //rb.velocity = ((Vector2)body.transform.up) * speed;

            if(!gameObject.activeInHierarchy)
            {
                StopCoroutine("followPath");
            }

            yield return null;
        }
    }

    public void moveToTank(GameObject target)
    {
      
        if(target != null)
        {
            //Obstacle Avoidance
            Vector2 avoidanceForce = Vector2.zero;
            RaycastHit2D hit = Physics2D.CircleCast(body.position, .5f, body.up, .5f, LayerMask.GetMask("AIAvoidance"));
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Obstacle" || hit.collider.tag == "Cover")
                {
                    //Debug.Log(hit.collider.name);
                    avoidanceForce = hit.normal;
                }
            }

            //Direction to Target
           
            Vector2 dir = (Vector2)(transform.position - target.transform.position) + avoidanceForce;
            dir += (Vector2)transform.position;

            //Turn towards Avoidance.Normal or Target
            if (avoidanceForce != Vector2.zero)
            {
                body.transform.up = Vector3.Slerp(body.transform.up, ((Vector2)body.transform.position - avoidanceForce).normalized, bodyRotSpeed * Time.deltaTime);

            }
            else
            {
                body.transform.up = Vector3.Slerp(body.transform.up, ((Vector2)body.transform.position - dir).normalized, bodyRotSpeed * Time.deltaTime);

            }



            //Move towards facing direction
            rb.velocity = ((Vector2)body.transform.up) * speed;
            
        }


    }


    public GameObject aimAtTank()
    {
        float highestScore = Mathf.NegativeInfinity;
        float lowestHealth = Mathf.Infinity;
        float lowestDistance = 5;
        int lowestHealthModifier = 2;
        GameObject retval = null;
        foreach (var p in Players)
        {
            int score = 0;
            TankHealth pHealth = p.GetComponent<TankHealth>();
            if(pHealth != null)
            {
                if (pHealth.isAlive)
                {
                    float distance = Vector2.Distance(transform.position, p.transform.position);
                    if (distance <= lowestDistance)
                    {
                        score++;
                        if (pHealth.currentHealth <= lowestHealth)
                        {
                            score += lowestHealthModifier;
                            lowestHealthModifier++;
                            lowestHealth = pHealth.currentHealth;
                        }
                        // lowestDistance = distance;
                    }

                   
                }
            }
            if (score > highestScore)
            {
                retval = p.gameObject;
                highestScore = score;
            }

        }
        return retval;
    }

    public void aimBarrel(GameObject target)
    {
        if(target != null)
        {
            Vector2 dir = barrel.transform.position - target.transform.position;
            dir += (Vector2)barrel.transform.position;
            barrel.transform.up = Vector3.Slerp(barrel.transform.up, ((Vector2)barrel.transform.position - dir).normalized, rotSpeed * Time.deltaTime);

        }

    }
	
    public bool hasPowerUp()
    {
        return powerGun.myPowerUp.Count > 0;
        
    }

    public bool shouldFire(GameObject target)
    {
       
        if (target != null)
        {
            RaycastHit2D hit = Physics2D.Linecast(barrel.position, target.transform.position, LayerMask.GetMask("AIAvoidance"));
            if (hit.collider != null)
            {
                return false;
            }
            Vector2 dir = barrel.transform.position - target.transform.position;
            float angle = Vector2.Angle(dir.normalized, barrel.transform.up);


            if (angle >= 160 && angle <= 180)
            {
                return true;
            }
        }


        
        return false;
    }

    void updateList()
    {
        for(int i =0; i < Players.Count; i++)
        {
            var isPlayer = Players[i].GetComponent<TankHealth>();
            if(isPlayer == null)
            {
                if (Players[i] == null || !Players[i].activeInHierarchy)
                {
                    Players.RemoveAt(i);
                }
            }
            
        }
        Players.TrimExcess();
    }
    public GameObject moveToTarget;
    public GameObject aimAtTarget;
	// Update is called once per frame
	void Update ()
    {
        reactionTimer -= Time.deltaTime;
        shootReactionTimer -= Time.deltaTime;
        if(reactionTimer <= 0)
        {
            moveToTarget = moveToDecision();
            aimAtTarget = aimAtTank();
            reactionTimer = reactionDelay;
        }
        if(moveToTarget != null && moveToTarget.activeInHierarchy)
        {
           // PathRequestManager.RequestPath(transform.position, moveToTarget.transform.position, OnPathFound);
            moveToTank(moveToTarget);
        }
        else
        {
            moveToTarget = moveToDecision();
        }
        if(aimAtTarget != null && aimAtTarget.activeInHierarchy)
        {
            aimBarrel(aimAtTarget);
        }
        else
        {
            aimAtTarget = aimAtTank();
        }
        
        
        
        if(shouldFire(aimAtTank()))
        {
            if(hasPowerUp())
            {
                autoFire = true;
            } else { fire = true; }
            shootReactionTimer = shootReactionDelay;
        }
        else
        {
            autoFire = false;
            fire = false;
        }
        updateList();
	}
}
