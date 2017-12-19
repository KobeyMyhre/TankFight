using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticManager : MonoBehaviour {

	// Use this for initialization
	void Awake ()
    {
        Statics.gunPool = transform.GetChild(0).GetComponent<ObjectPool>(); ;
        Statics.explosionPool = transform.GetChild(1).GetComponent<ObjectPool>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
