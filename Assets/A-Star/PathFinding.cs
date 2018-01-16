using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathFinding : MonoBehaviour {




    PathRequestManager requestManager;

    

    Grid grid;
    private void Awake()
    {
        grid = GetComponent<Grid>();
        requestManager = GetComponent<PathRequestManager>();
    }

    public void startFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    int getDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        if(dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        return 14 * dstX + 10 * (dstY - dstX);
    }

    Vector3[] retracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] wayPoints = simplifyPath(path);
        System.Array.Reverse(wayPoints);
        return wayPoints;
        
    }

    Vector3[] simplifyPath(List<Node> path)
    {
        List<Vector3> wayPoints = new List<Vector3>();
        Vector2 dirOld = Vector2.zero;
        for(int i =1; i < path.Count; i++)
        {
            Vector2 dirNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
           // if(dirNew != dirOld)
           // {
                wayPoints.Add(path[i].worldPosition);
           // }
            dirOld = dirNew;
        }
        return wayPoints.ToArray();
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {

        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        if(startNode.walkable && targetNode.walkable)
        {
            Heap<Node> openSet = new Heap<Node>(grid.maxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);
            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    pathSuccess = true;

                    break;
                }
                List<Node> nodeTest = grid.getNeighbours(currentNode);
                foreach (Node neighbour in grid.getNeighbours(currentNode))
                {
                    
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }
                    if(neighbour.movementPenalty > 0) { Debug.Log("Tough Road"); }
                    int newMovementCost = currentNode.gCost + getDistance(currentNode, neighbour) + neighbour.movementPenalty;
                    if (newMovementCost < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCost;
                        neighbour.hCost = getDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;
                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                        else
                        {
                            openSet.UpdateItem(neighbour);
                        }

                    }
                }
            }
        }
        
        
        yield return null;
        if(pathSuccess)
        {
            waypoints = retracePath(startNode, targetNode);
        }
        requestManager.finishedProcessingPath(waypoints, pathSuccess);
    }
}
