using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class HaxMesh : MonoBehaviour
{
    public float m_CellSize;
    public float m_Height;

    float m_Width;
    float m_Lenght;

    public Mesh m_Mesh;
    public Vector3[] m_Vertices;
    int[] m_Triangles;
    Vector2[] m_UVs;

    MeshFilter m_MeshFilter;
    MeshRenderer m_MeshRenderer;

    Material m_Mat;

    bool done;

    private void Awake()
    {
        m_Width = Mathf.Sqrt(3);
        m_Lenght = 2;

        //m_Height = Random.Range(0, 500) * 0.001f;
        m_Height = 0.5f;
        m_Mesh = new Mesh();
        m_MeshFilter = GetComponent<MeshFilter>();
        m_MeshRenderer = GetComponent<MeshRenderer>();
        m_Vertices = new Vector3[12];
        m_UVs = new Vector2[m_Vertices.Length];
        m_Triangles = new int[48];

        SetVetecies();
        SetTriangles();
        SetUVs();
        SetMesh();
    }

    private void Start()
    {
        m_Mat = Resources.Load<Material>("Wall");
       
    }

    void Update()
    {
        if (!done)
        {
            SetVetecies();
            SetTriangles();
            SetUVs();
            SetMesh();
            m_MeshRenderer.material = m_Mat;
            done = true;
        }

    }

    void SetVetecies()
    {
        float angleDeg;
        float angleRad;

        for (int i = 0; i <= 5; i++)
        {
            angleDeg = 60 * i - 30;
            angleRad = Mathf.PI / 180 * angleDeg;
            m_Vertices[i] = new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad));
            m_Vertices[i + 6] = new Vector3(Mathf.Cos(angleRad), m_Height, Mathf.Sin(angleRad));
        }
    }

    void SetTriangles()
    {
        m_Triangles[0] = 0;
        m_Triangles[1] = 6;
        m_Triangles[2] = 1;

        m_Triangles[3] = 1;
        m_Triangles[4] = 6;
        m_Triangles[5] = 7;

        m_Triangles[6] = 1;
        m_Triangles[7] = 7;
        m_Triangles[8] = 2;

        m_Triangles[9] = 2;
        m_Triangles[10] = 7;
        m_Triangles[11] = 8;

        m_Triangles[12] = 2;
        m_Triangles[13] = 8;
        m_Triangles[14] = 3;

        m_Triangles[15] = 3;
        m_Triangles[16] = 8;
        m_Triangles[17] = 9;

        m_Triangles[18] = 3;
        m_Triangles[19] = 9;
        m_Triangles[20] = 4;

        m_Triangles[21] = 4;
        m_Triangles[22] = 9;
        m_Triangles[23] = 10;

        m_Triangles[24] = 4;
        m_Triangles[25] = 10;
        m_Triangles[26] = 5;

        m_Triangles[27] = 5;
        m_Triangles[28] = 10;
        m_Triangles[29] = 11;

        m_Triangles[30] = 5;
        m_Triangles[31] = 11;
        m_Triangles[32] = 0;

        m_Triangles[33] = 0;
        m_Triangles[34] = 11;
        m_Triangles[35] = 6;

        m_Triangles[36] = 6;
        m_Triangles[37] = 11;
        m_Triangles[38] = 7;

        m_Triangles[39] = 7;
        m_Triangles[40] = 11;
        m_Triangles[41] = 10;

        m_Triangles[42] = 7;
        m_Triangles[43] = 10;
        m_Triangles[44] = 8;

        m_Triangles[45] = 8;
        m_Triangles[46] = 10;
        m_Triangles[47] = 9;
    }

    void SetUVs()
    {
        
        for (int i = 0; i < m_Vertices.Length; i++)
        {
            m_UVs[i] = new Vector2(
              (m_Vertices[i].x + m_Width * 0.5f) * 0.5f,
               (m_Vertices[i].z + m_Lenght * 0.5f) * 0.5f);

            m_Vertices[i] = m_Vertices[i] * m_CellSize;
        }
    }

    void SetMesh()
    {
        m_Mesh.vertices = m_Vertices;
        m_Mesh.triangles = m_Triangles;
        m_Mesh.uv = m_UVs;
        m_Mesh.RecalculateNormals();
    }
}