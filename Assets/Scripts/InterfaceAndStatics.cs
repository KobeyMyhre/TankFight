using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceAndStatics : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


public static class Statics
{
    public static ObjectPool gunPool;
    public static ObjectPool automaticBulletPool;
    public static ObjectPool[] explosionPool;
    public static ObjectPool minePool;
    public static ObjectPool grenadePool;
    public static ScoreManager scoreManager;
    public enum Player
    {
        one = 1,two = 2,three = 3,four = 4
    }
    public enum GameMode
    {
        DeathMatch, KingOfTheHill
    }
}

public interface IDamageable
{
    bool takeDamage(int damageTaken);
    bool die();
}


