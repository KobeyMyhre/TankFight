using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public Transform target;
    public float speed;
    Vector3[] path;
    int targetIdx;


	// Use this for initialization
	void Start ()
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
	}
	
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if(pathSuccessful)
        {
            path = newPath;
            StopCoroutine("followPath");
            StartCoroutine("followPath");
        }
    }


    IEnumerator followPath()
    {
        Vector3 currentWaypoint = path[0];
        while(true)
        {
            if(transform.position == currentWaypoint)
            {
                targetIdx++;
                if(targetIdx >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIdx];
            }
            transform.position = Vector3.MoveTowards(transform.position,currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if(path != null)
        {
            for(int i = targetIdx; i < path.Length; i++)
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawSphere(path[i], 1);
                if(i == targetIdx)
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
}
