using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pooledObject : MonoBehaviour {

    [HideInInspector]
    public ObjectPool myPool;

	public void returnToPool()
    {
        gameObject.SetActive(false);
        myPool.pool.Push(this.gameObject);
    }
}
