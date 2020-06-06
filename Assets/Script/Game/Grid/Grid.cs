using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Linq.Expressions;

public class Grid : MonoBehaviour
{
    public static Grid instance;

    public float m_CellSize;
    public Vector2Int m_GridSize;
    public Vector2 m_SpacingSize;

    public HaxNode[,] m_Nodes;
    public Vector2Int[] m_NeighborsPositions;
    public Vector2Int[] m_DiagonalPositions;

    private Camera m_Camera;

    float m_SelectFactor = 0.5f;


    private void Awake()
    {
        CreateInstance();
        CreateGrid();
    }

    private void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void CreateGrid()
    {
        m_Nodes = new HaxNode[m_GridSize.x, m_GridSize.y];
        m_SpacingSize = new Vector2(Mathf.Sqrt(3) * m_CellSize, 2 * m_CellSize);

        GameObject cell = new GameObject();
        cell.AddComponent<HaxNode>();
        cell.AddComponent<HaxMesh>();
        SphereCollider col = cell.AddComponent<SphereCollider>();
        col.radius = m_CellSize;
        cell.name = "Cell";

        for (int x = 0; x < m_GridSize.x; x++)
        {
            for (int y = 0; y < m_GridSize.y; y++)
            {
                m_Nodes[x, y] = Instantiate(cell).GetComponent<HaxNode>();
                m_Nodes[x, y].transform.parent = this.transform;
                m_Nodes[x, y].SetCoordinate(new Vector2Int(x, y));
                m_Nodes[x, y].gameObject.name = "Node" + x.ToString() + " " + y.ToString();

                HaxMesh haxCell = m_Nodes[x, y].GetComponent<HaxMesh>();
                haxCell.m_CellSize = m_CellSize;

                haxCell.transform.position = new Vector3(haxCell.transform.position.x, haxCell.transform.position.y, -(y * m_SpacingSize.y * 0.75F));
                if (y % 2 == 0)
                {
                    haxCell.transform.position = new Vector3(x * m_SpacingSize.x, haxCell.transform.position.y, haxCell.transform.position.z);
                }
                else
                {
                    haxCell.transform.position = new Vector3(m_SpacingSize.x * 0.5f + x * m_SpacingSize.x, haxCell.transform.position.y, haxCell.transform.position.z);
                }
            }
        }
        Destroy(cell);
    }

    private void Start()
    {
        m_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public HaxNode[] ShowReachable(HaxNode hax, int movePower)
    {
        ResetSelect();
        HaxNode[] reachable = BreadthFirstAlgorithms.FindReachable(hax, movePower);
        for (int i = 0; i < reachable.Length; i++)
        {
            reachable[i].m_SpriteRenderer.color = new Color(reachable[i].m_SpriteRenderer.color.r * m_SelectFactor, reachable[i].m_SpriteRenderer.color.g * m_SelectFactor, reachable[i].m_SpriteRenderer.color.b * m_SelectFactor);
        }

        return reachable;
    }

    public HaxNode[] ShowAttackable(HaxNode hax, int movePower)
    {
        ResetSelect();
        HaxNode[] reachable = BreadthFirstAlgorithms.FindReachable(hax, movePower);
        for (int i = 0; i < reachable.Length; i++)
        {
            reachable[i].m_SpriteRenderer.color = Color.red;
        }

        return reachable;
    }

    public HaxNode[] ShowPath(HaxNode Start, HaxNode Dest)
    {
        ResetSelect();
        HaxNode[] path = AStarAlgorithms.GetPath(Start, Dest);
        for (int i = 0; i < path.Length; i++)
        {
            path[i].m_SpriteRenderer.color = new Color(path[i].m_SpriteRenderer.color.r * m_SelectFactor, path[i].m_SpriteRenderer.color.g * m_SelectFactor, path[i].m_SpriteRenderer.color.b * m_SelectFactor);
        }
   
        return path;
    }

    public void ResetSelect()
    {
        for (int x = 0; x < m_Nodes.GetLength(0); x++)
        {
            for (int y = 0; y < m_Nodes.GetLength(1); y++)
            {
                m_Nodes[x, y].ChangeColor();
            }
        }
    }

    public Vector2Int[] GetNeighborsOffset(Vector3Int CubePos)
    {
        Vector2Int[] neighborsPositions =
        {
            HaxCoordAlgorithms.CubeToOddr(new Vector3Int(CubePos.x + 1,CubePos.y - 1,CubePos.z)),
            HaxCoordAlgorithms.CubeToOddr(new Vector3Int(CubePos.x    ,CubePos.y - 1,CubePos.z+ 1)),
            HaxCoordAlgorithms.CubeToOddr(new Vector3Int(CubePos.x - 1,CubePos.y    ,CubePos.z+ 1)),
            HaxCoordAlgorithms.CubeToOddr(new Vector3Int(CubePos.x - 1,CubePos.y + 1,CubePos.z)),
            HaxCoordAlgorithms.CubeToOddr(new Vector3Int(CubePos.x    ,CubePos.y + 1,CubePos.z - 1)),
            HaxCoordAlgorithms.CubeToOddr(new Vector3Int(CubePos.x + 1,CubePos.y    ,CubePos.z- 1))
        };
        return neighborsPositions;
    }

    public List<HaxNode> GetNeighborsHax(HaxNode haxnode)
    {
        Vector2Int[] neighborsPositions =
        {
            HaxCoordAlgorithms.CubeToOddr(new Vector3Int(haxnode.HaxCoordinate.CubeCoordinate.x + 1,haxnode.HaxCoordinate.CubeCoordinate.y - 1,haxnode.HaxCoordinate.CubeCoordinate.z)),
            HaxCoordAlgorithms.CubeToOddr(new Vector3Int(haxnode.HaxCoordinate.CubeCoordinate.x    ,haxnode.HaxCoordinate.CubeCoordinate.y - 1,haxnode.HaxCoordinate.CubeCoordinate.z+ 1)),
            HaxCoordAlgorithms.CubeToOddr(new Vector3Int(haxnode.HaxCoordinate.CubeCoordinate.x - 1,haxnode.HaxCoordinate.CubeCoordinate.y    ,haxnode.HaxCoordinate.CubeCoordinate.z+ 1)),
            HaxCoordAlgorithms.CubeToOddr(new Vector3Int(haxnode.HaxCoordinate.CubeCoordinate.x - 1,haxnode.HaxCoordinate.CubeCoordinate.y + 1,haxnode.HaxCoordinate.CubeCoordinate.z)),
            HaxCoordAlgorithms.CubeToOddr(new Vector3Int(haxnode.HaxCoordinate.CubeCoordinate.x    ,haxnode.HaxCoordinate.CubeCoordinate.y + 1,haxnode.HaxCoordinate.CubeCoordinate.z - 1)),
            HaxCoordAlgorithms.CubeToOddr(new Vector3Int(haxnode.HaxCoordinate.CubeCoordinate.x + 1,haxnode.HaxCoordinate.CubeCoordinate.y    ,haxnode.HaxCoordinate.CubeCoordinate.z- 1))
        };

        List<HaxNode> neighbors = new List<HaxNode>();
        for (int i = 0; i < neighborsPositions.Length; i++)
        {
            if (IsIndexValid(m_Nodes, neighborsPositions[i].x, neighborsPositions[i].y))
                neighbors.Add(m_Nodes[neighborsPositions[i].x, neighborsPositions[i].y]);
        }
        return neighbors;
    }

    public Vector3Int[] NeighborsCube(Vector3Int CubePos)
    {
        Vector3Int[] neighborsPositions =
        {
            new Vector3Int(CubePos.x + 1,CubePos.y - 1,CubePos.z),
            new Vector3Int(CubePos.x    ,CubePos.y - 1,CubePos.z+ 1),
            new Vector3Int(CubePos.x - 1,CubePos.y    ,CubePos.z+ 1),
            new Vector3Int(CubePos.x - 1,CubePos.y + 1,CubePos.z),
            new Vector3Int(CubePos.x    ,CubePos.y + 1,CubePos.z - 1),
            new Vector3Int(CubePos.x + 1,CubePos.y    ,CubePos.z- 1)
        };
        return neighborsPositions;
    }

    public Vector2Int[] Diagonals(Vector3Int CubePos)
    {
        Vector2Int[] Diagonal =
        {
            HaxCoordAlgorithms.CubeToOddr(new Vector3Int(CubePos.x + 1,CubePos.y - 2,CubePos.z + 1)),
            HaxCoordAlgorithms.CubeToOddr(new Vector3Int(CubePos.x - 1,CubePos.y - 1,CubePos.z + 2)),
            HaxCoordAlgorithms.CubeToOddr(new Vector3Int(CubePos.x - 2,CubePos.y + 1,CubePos.z + 1)),
            HaxCoordAlgorithms.CubeToOddr(new Vector3Int(CubePos.x - 1,CubePos.y + 2,CubePos.z - 1)),
            HaxCoordAlgorithms.CubeToOddr(new Vector3Int(CubePos.x + 1,CubePos.y + 1,CubePos.z - 2)),
            HaxCoordAlgorithms.CubeToOddr(new Vector3Int(CubePos.x + 2,CubePos.y - 1,CubePos.z - 1))
        };
        return Diagonal;
    }

    public int GetDistanceCube(Vector3 a, Vector3 b)
    {
        return Mathf.RoundToInt((Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z)) * .5f);
    }

    public int GetDistanceHax(HaxNode a, HaxNode b)
    {
        return Mathf.RoundToInt((Mathf.Abs(a.HaxCoordinate.CubeCoordinate.x - b.HaxCoordinate.CubeCoordinate.x) +
                Mathf.Abs(a.HaxCoordinate.CubeCoordinate.y - b.HaxCoordinate.CubeCoordinate.y) +
                Mathf.Abs(a.HaxCoordinate.CubeCoordinate.z - b.HaxCoordinate.CubeCoordinate.z)) * .5f);
    }

    public Vector3 CubeLerp(Vector3 a, Vector3 b, float t)
    {
        return new Vector3(Mathf.Lerp(a.x, b.x, t),
                           Mathf.Lerp(a.y, b.y, t),
                           Mathf.Lerp(a.z, b.z, t));
    }

    public Vector2Int[] CubeLineDraw(Vector3 a, Vector3 b)
    {
        float N = GetDistanceCube(a, b);
        List<Vector3Int> Pre = new List<Vector3Int>();
        Vector2Int[] result = new Vector2Int[(int)N];

        for (int i = 0; i < N; i++)
        {
            Pre.Add(CubeRound(CubeLerp(a, b, 1.0f / N * i)));

            result[i] = HaxCoordAlgorithms.CubeToOddr(new Vector3Int(Pre[i].x, Pre[i].y, Pre[i].z));
        }
        return result;
    }

    public Vector3Int CubeRound(Vector3 cube)
    {
        float rx = Mathf.RoundToInt(cube.x);
        float ry = Mathf.RoundToInt(cube.y);
        float rz = Mathf.RoundToInt(cube.z);

        float xDiff = Mathf.Abs(rx - cube.x);
        float yDiff = Mathf.Abs(ry - cube.y);
        float zDiff = Mathf.Abs(rz - cube.z);

        if (xDiff > yDiff && xDiff > zDiff)
        {
            rx = -ry - rz;
        }
        else if (yDiff > zDiff)
        {
            ry = -rx - rz;
        }
        else
        {
            rz = -rx - ry;
        }

        return new Vector3Int((int)rx, (int)ry, (int)rz);
    }

    public GameObject GetHaxClicked()
    {

        RaycastHit hit;
        if (Physics.Raycast(m_Camera.ScreenPointToRay(Input.mousePosition), out hit, 9999))
        {
            return hit.transform.gameObject;
        }

        return null;
    }

    public bool IsIndexValid<T>(T[,] Array, int x, int y)
    {
        if (x >= 0 && x < Array.GetLength(0))
        {
            if (y >= 0 && y < Array.GetLength(1))
            {
                return true;
            }
        }
        return false;
    }
}

