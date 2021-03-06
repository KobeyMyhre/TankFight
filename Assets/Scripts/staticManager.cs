﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticManager : MonoBehaviour {

	// Use this for initialization
	void Awake ()
    {
        Statics.gunPool = transform.GetChild(0).GetComponent<ObjectPool>(); ;
        Statics.explosionPool = transform.GetChild(1).GetComponents<ObjectPool>();
        
        Statics.automaticBulletPool = transform.GetChild(2).GetComponent<ObjectPool>();
        Statics.minePool = transform.GetChild(3).GetComponent<ObjectPool>();
        Statics.grenadePool = transform.GetChild(4).GetComponent<ObjectPool>();
        Statics.scoreManager = GetComponentInChildren<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
