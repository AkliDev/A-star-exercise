using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal static class BreadthFirstAlgorithms
{
    static public HaxNode[] FindReachable(HaxNode start, int maxMovement)
    {
        List<HaxNode> open = new List<HaxNode>();
        List<HaxNode> visited = new List<HaxNode>();

        open.Add(start);
        visited.Add(start);
        start.BreadthFirstProperties.DistanceMoved = 0;

        int SavetyLoopCount = 0;

        while (open.Count > 0)
        {
            HaxNode[] neighbors = Grid.instance.GetNeighborsHax(open[0]).ToArray();
            for (int i = 0; i < neighbors.Length; i++)
            {
                if (open[0].BreadthFirstProperties.DistanceMoved + neighbors[i].MoveCost >= neighbors[i].BreadthFirstProperties.DistanceMoved ||
                    open[0].BreadthFirstProperties.DistanceMoved + neighbors[i].MoveCost > maxMovement) continue;

                neighbors[i].BreadthFirstProperties.DistanceMoved = open[0].BreadthFirstProperties.DistanceMoved + neighbors[i].MoveCost;
                neighbors[i].BreadthFirstProperties.Parant = open[0];

                if (neighbors[i].IsWalkable)
                {

                    if (!visited.Contains(neighbors[i]))
                        visited.Add(neighbors[i]);
                    if (!open.Contains(neighbors[i]))
                        open.Add(neighbors[i]);
                }

            }
            open.RemoveAt(0);
            SavetyLoopCount++;

            if (SavetyLoopCount > 1000)
                break;
        }

        HexNodeCleanUp(visited);

        return visited.ToArray();
    }

    static void HexNodeCleanUp(List<HaxNode> nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].BreadthFirstProperties.DistanceMoved = int.MaxValue;
            nodes[i].BreadthFirstProperties.Parant = null;
        }
    }
}
public class BreadthFirstProperties
{
    public int DistanceMoved = int.MaxValue;
    public HaxNode Parant;
}
