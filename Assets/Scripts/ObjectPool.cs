﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {


    public GameObject pooledObject;
    public Stack<GameObject> pool;

    public static ObjectPool gunPool;
    

    public int pooledAmount;
	// Use this for initialization
	void Start ()
    {
        pool = new Stack<GameObject>();
        for(int i =0; i < pooledAmount; i++)
        {
            var spawnedObj = Instantiate(pooledObject);
            spawnedObj.GetComponent<pooledObject>().myPool = this;
            spawnedObj.transform.parent = gameObject.transform;
            spawnedObj.SetActive(false);
            pool.Push(spawnedObj);
        }
	}


    public GameObject GetObject()
    {
        if(pool.Count > 0)
        {
            return pool.Pop();
        }
<<<<<<< HEAD
        for (int i = 0; i < pooledAmount; i++)
        {
            var spawnedObj = Instantiate(pooledObject);
            spawnedObj.GetComponent<pooledObject>().myPool = this;
            spawnedObj.SetActive(false);
            pool.Push(spawnedObj);
        }
        return pool.Pop();
=======
        else
        {
            for (int i = 0; i < pooledAmount; i++)
            {
                var spawnedObj = Instantiate(pooledObject);
                spawnedObj.GetComponent<pooledObject>().myPool = this;
                spawnedObj.transform.parent = gameObject.transform;
                spawnedObj.SetActive(false);
                pool.Push(spawnedObj);
            }
            return pool.Pop();
        }
>>>>>>> aa944e66af524fbc5131dd83a418301a81b049e4
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
