using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HaxCoordinate
{
    [SerializeField] private Vector2Int m_OffsetCoordinate;
    public Vector2Int OffsetCoordinate { get { return m_OffsetCoordinate; } set { m_OffsetCoordinate = value; } }

    [SerializeField] private Vector2Int m_AxialCoordinate;
    public Vector2Int AxialCoordinate
    {
        get
        {
            if (m_AxialCoordinate == Vector2Int.zero && OffsetCoordinate != new Vector2Int(0, 0))
                m_AxialCoordinate = HaxCoordAlgorithms.CubeToAxial(CubeCoordinate);
            return m_AxialCoordinate;
        }
        set
        {
            m_AxialCoordinate = value;
        }
    }

    [SerializeField] private Vector3Int m_CubeCoordinate;
    public Vector3Int CubeCoordinate
    {
        get
        {
            if (m_CubeCoordinate == Vector3Int.zero && OffsetCoordinate != new Vector2Int(0, 0))
                CubeCoordinate = HaxCoordAlgorithms.OddrToCube(OffsetCoordinate);
            return m_CubeCoordinate;
        }

        set
        {
            m_CubeCoordinate = value;
        }
    }
}
