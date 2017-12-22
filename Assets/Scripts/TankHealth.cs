using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour, IDamageable
{
    public int maxHealth;
    public int currentHealth;
    public bool isAlive; 
    Vector3 respawnPos;
    Statics.Player playerNum;
    TankController controller;
    SpriteRenderer mySprite;
    
    [HideInInspector]
    public RespawnManager respawn;
	// Use this for initialization
	void Start ()
    {
        mySprite = GetComponentInChildren<SpriteRenderer>();
        controller = GetComponent<TankController>();
        switch (controller.playerNum)
        {
            case 1:
                playerNum = Statics.Player.one;
                break;
            case 2:
                playerNum = Statics.Player.two;
                break;
            case 3:
                playerNum = Statics.Player.three;
                break;
            case 4:
                playerNum = Statics.Player.four;
                break;
        }
        isAlive = true;
        respawnPos = transform.position;
        currentHealth = maxHealth;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void reset()
    {
        isAlive = true;
        currentHealth = maxHealth;
        transform.position = respawnPos;
        gameObject.SetActive(true);
    }

    void resetColor()
    {
        mySprite.color = Color.white;
    }

    public bool takeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        mySprite.color = Color.red;
        Invoke("resetColor", .2f);
        return die();
    }

    public bool die()
    {
        if(currentHealth <= 0)
        {
            isAlive = false;
            gameObject.SetActive(false);
            return true;
        } else { return false; }
    }
}
