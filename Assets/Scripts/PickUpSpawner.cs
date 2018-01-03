using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour {

    public PickUp[] spawnAblePickUps;
    public float spawnRange;
    public float spawnInterval;
    private float spawnTimer;

    void spawnPickUp()
    {
        TankAIController[] AI = FindObjectsOfType<TankAIController>();
        int idx = Random.Range(0, spawnAblePickUps.Length);
        var spawnedPickUp = Instantiate(spawnAblePickUps[idx]);
        float x = Random.Range(-spawnRange, spawnRange);
        float y = Random.Range(-spawnRange, spawnRange);
        Vector2 spawnPos = new Vector2(x,y);
        spawnedPickUp.transform.position = spawnPos;
        spawnTimer = spawnInterval;
        foreach(TankAIController tank in AI)
        {
            tank.Players.Add(spawnedPickUp.gameObject);
        }
    }

	// Use this for initialization
	void Start ()
    {
        spawnTimer = spawnInterval;
	}
	
	// Update is called once per frame
	void Update ()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0)
        {
            
            spawnPickUp();
        }
	}
}
