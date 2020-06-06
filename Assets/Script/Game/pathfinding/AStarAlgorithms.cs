using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class AStarAlgorithms
{
    static public HaxNode[] GetPath(HaxNode start, HaxNode destination)
    {
        List<HaxNode> open = new List<HaxNode>();
        List<HaxNode> closed = new List<HaxNode>();

        start.AstarProperties.GCost = 0;
        start.AstarProperties.GCost = Grid.instance.GetDistanceHax(start, destination);

        open.Add(start);

        do
        {
            int currentNodeIndex = 0;
            int lowestFCost = int.MaxValue;
            HaxNode currentNode = null;

            for (int i = 0; i < open.Count; i++)
            {
                if (open[i].AstarProperties.FCost < lowestFCost)
                {
                    lowestFCost = open[i].AstarProperties.FCost;
                    currentNodeIndex = i;
                    currentNode = open[i];
                }
            }

            open.RemoveAt(currentNodeIndex);
            closed.Add(currentNode);

            foreach (HaxNode neighbor in Grid.instance.GetNeighborsHax(currentNode).ToArray())
            {
                if (!neighbor.IsWalkable || closed.Contains(neighbor))
                    continue;

                neighbor.AstarProperties.HCost = Grid.instance.GetDistanceHax(neighbor, destination);
                int newCostToNeighbour = currentNode.AstarProperties.GCost + neighbor.MoveCost;

                if (newCostToNeighbour < neighbor.AstarProperties.GCost || !open.Contains(neighbor) /*|| currentNode.AstarProperties.GCost + neighbor.MoveCost < neighbor.AstarProperties.GCost*/)
                {
                    neighbor.AstarProperties.Parent = currentNode;
                    neighbor.AstarProperties.GCost = currentNode.AstarProperties.GCost + neighbor.MoveCost;
                    if (!open.Contains(neighbor))
                        open.Add(neighbor);
                }
            }

            if (currentNode == destination)
            {
                List<HaxNode> path = new List<HaxNode>();
                path.Add(currentNode);
                CreatPath(path, currentNode);
                ClearNodes();
                return path.ToArray();
            }
        }
        while (open.Count > 0);
        ClearNodes();
        return new HaxNode[0];
    }

    static private void CreatPath(List<HaxNode> path, HaxNode nodetogetparant)
    {
        if (nodetogetparant.AstarProperties.Parent == null)
            return;
        else
            path.Add(nodetogetparant.AstarProperties.Parent);

        CreatPath(path, nodetogetparant.AstarProperties.Parent);
    }

    static private void ClearNodes()
    {
        for (int x = 0; x < Grid.instance.m_Nodes.GetLength(0); x++)
        {
            for (int y = 0; y < Grid.instance.m_Nodes.GetLength(1); y++)
            {
                Grid.instance.m_Nodes[x, y].AstarProperties.Clear();
            }
        }
    }

    static public HaxNode[] GetPatsh(HaxNode start, HaxNode destination)
    {
        //G = is the movement cost from the start point A to the current square.
        //H = is the estimated movement cost from the current square to the destination point (heuristic)
        //F = G+H

        List<HaxNode> open = new List<HaxNode>();
        List<HaxNode> closed = new List<HaxNode>();

        open.Add(start);

        while (open.Count > 0)
        {
            HaxNode currentNode = open[0];

            for (int i = 1; i < open.Count; i++)
            {
                if (open[i].AstarProperties.FCost < currentNode.AstarProperties.FCost || open[i].AstarProperties.FCost == currentNode.AstarProperties.FCost)
                {
                    if (open[i].AstarProperties.HCost < currentNode.AstarProperties.HCost)
                        currentNode = open[i];
                }
            }

            open.Remove(currentNode);
            closed.Add(currentNode);

            if (currentNode == destination)
            {
                return RetracePath(start, destination);
            }

            foreach (HaxNode neighbour in Grid.instance.GetNeighborsHax(currentNode))
            {
                if (closed.Contains(neighbour) || !neighbour.IsWalkable)
                {
                    continue;
                }

                int newCostToNeighbour = currentNode.AstarProperties.GCost + Grid.instance.GetDistanceHax(currentNode, neighbour);

                if (newCostToNeighbour >= neighbour.AstarProperties.GCost && open.Contains(neighbour)) continue;

                neighbour.AstarProperties.GCost = newCostToNeighbour;
                neighbour.AstarProperties.HCost = Grid.instance.GetDistanceHax(neighbour, destination);
                neighbour.AstarProperties.Parent = currentNode;

                if (!open.Contains(neighbour))
                    open.Add(neighbour);
            }
        }



        return null;
    }

    static private HaxNode[] RetracePath(HaxNode start, HaxNode destination)
    {
        List<HaxNode> path = new List<HaxNode>();

        HaxNode currentNode = destination;

        while (currentNode != start)
        {
            path.Add(currentNode);
            currentNode = currentNode.AstarProperties.Parent;
        }

        path.Reverse();

        return path.ToArray();
    }
}

public class AStarProperties
{
    public int GCost = 99999;
    public int HCost;
    public int FCost => GCost + HCost;
    public HaxNode Parent = null;

    public void Clear()
    {
        GCost = int.MaxValue;
        HCost = 0;
        Parent = null;
    }
}
