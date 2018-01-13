using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : IHeapItem<Node>
{
    public bool walkable;
    public Vector3 worldPosition;
    public int movementPenalty;
    public int gridX;
    public int gridY;
    public Node parent;
    int HeapIndex;
    public int gCost;
    public int hCost;
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
    public Node(bool _walkable, Vector3 _worldPos, int _gridx, int _gridy, int _penalty)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridx;
        gridY = _gridy;
        movementPenalty = _penalty;
    }

    public int heapIndex
    {
        get
        {
            return HeapIndex;
        }
        set
        {
            HeapIndex = value;
        }
    }
    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }

}
