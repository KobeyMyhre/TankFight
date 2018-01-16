using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public bool drawGridGizmos;
    
    Node[,] grid;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public LayerMask unWalkableMask;
    public TerrainType[] walkableRegions;
    LayerMask walkableMask;
    Dictionary<int, int> walkableRegionsDictionary = new Dictionary<int, int>();

    float nodeDiameter;
    int gridxSizeX, grideSizeY;


   

    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));
        if(grid != null && drawGridGizmos)
        {
            foreach(Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
           
        }
    }


    private void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridxSizeX = Mathf.RoundToInt( gridWorldSize.x / nodeDiameter);
        grideSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        foreach(TerrainType region in walkableRegions)
        {
            walkableMask.value |= region.terrainMask.value;
            walkableRegionsDictionary.Add( (int)Mathf.Log(region.terrainMask.value,2), region.terrainPenalty);
        }

        createdGrid();
    }

    public int maxSize
    {
        get
        {
            return gridxSizeX * grideSizeY;
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridxSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((grideSizeY - 1) * percentY);

        return grid[x, y];
    }

    void createdGrid()
    {
        Vector3 worldBottomLeft = transform.position - Vector3.right * (gridWorldSize.x/2) - Vector3.up * (gridWorldSize.y /2);
        grid = new Node[gridxSizeX, grideSizeY];
        for(int x =0; x < gridxSizeX; x++)
        {
            for(int y = 0; y < grideSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius,unWalkableMask));

                int movementPenalty = 0;

                if(walkable)
                {
                    Ray ray = new Ray(worldPoint + Vector3.up * 50, Vector3.down);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, walkableMask))
                    {
                        walkableRegionsDictionary.TryGetValue(hit.collider.gameObject.layer, out movementPenalty);
                    }
                }
                if(movementPenalty > 0)
                {
                    Debug.Log(movementPenalty);
                }
                grid[x, y] = new Node(walkable, worldPoint, x, y, movementPenalty);
            }
        }
    }


   public List<Node> getNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for(int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if(x == 0 && y == 0)
                {
                    continue;
                }
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if(checkX >= 0 && checkX < gridxSizeX && checkY >= 0 && checkY < grideSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    [System.Serializable]
    public class TerrainType
    {
        public LayerMask terrainMask;
        public int terrainPenalty;
    }

}
