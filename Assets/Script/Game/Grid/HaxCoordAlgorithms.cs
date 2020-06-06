using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HaxCoordAlgorithms
{
    static public Vector3Int OddrToCube(Vector2Int OffsetPos)
    {
        int x = OffsetPos.x - (OffsetPos.y - (OffsetPos.y & 1)) / 2;
        int z = OffsetPos.y;
        int y = -x - z;

        return new Vector3Int(x, y, z);
    }

    static public Vector2Int CubeToOddr(Vector3Int CubePos)
    {
        int col = CubePos.x + (CubePos.z - (CubePos.z & 1)) / 2;
        int row = CubePos.z;
        return new Vector2Int(col, row);
    }

    static public Vector3Int AxialToCube(Vector2Int AxialPos)
    {
        int x = AxialPos.x;
        int z = AxialPos.y;
        int y = -x - z;
        return new Vector3Int(x, y, z);
    }

    static public Vector2Int CubeToAxial(Vector3Int CubePos)
    {
        int x = CubePos.x;
        int y = CubePos.z;
        return new Vector2Int(x, y);
    }
}

