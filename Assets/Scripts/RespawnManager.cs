using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour {

    TankHealth tanks;
    TextMesh respawnTexts;
    public float respawnTime;
    private float timer;
    
    // Use this for initialization
    void Start ()
    {
        Statics.scoreManager.players.Add(this);
        timer = respawnTime;
        respawnTexts = GetComponent<TextMesh>();
        tanks = GetComponentInChildren<TankHealth>();
        tanks.respawn = this;
        respawnTexts.text = "";
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!tanks.isAlive)
        {
            timer -= Time.deltaTime;
            respawnTexts.text = ((int)timer).ToString();
            if (timer <= 0)
            {
                tanks.reset();
                respawnTexts.text = "";
                timer = respawnTime;
            }
        }
        
	}
}
