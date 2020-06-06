using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaxNode : MonoBehaviour
{
    public HaxCoordinate HaxCoordinate { get; private set; }
    public AStarProperties AstarProperties { get; private set; }
    public BreadthFirstProperties BreadthFirstProperties { get; private set; }
    public bool IsWalkable { get; set; }
    public int MoveCost = 1;

    private HaxMesh m_Mesh;
    public SpriteRenderer m_SpriteRenderer;

    public EHaxType m_Type = EHaxType.Free;

    public Unit m_Unit;

    private void Awake()
    {
        IsWalkable = true;
        HaxCoordinate = new HaxCoordinate();
        AstarProperties = new AStarProperties();
        BreadthFirstProperties = new BreadthFirstProperties();
        m_Mesh = GetComponent<HaxMesh>();      
    }

    void CreateSprite()
    {
        GameObject sprite = new GameObject();
        m_SpriteRenderer = sprite.AddComponent<SpriteRenderer>();
        sprite.transform.position = transform.position + transform.up * 6;
        sprite.transform.parent = transform;
        sprite.transform.localScale = new Vector3(Grid.instance.m_CellSize, Grid.instance.m_CellSize, Grid.instance.m_CellSize) * 0.5f;
        sprite.transform.eulerAngles = new Vector3(90, m_SpriteRenderer.transform.eulerAngles.y, m_SpriteRenderer.transform.eulerAngles.z);
        m_SpriteRenderer.sprite = Resources.Load<Sprite>("Sprite");
        ChangeColor();
    }

    void CreateTextMesh()
    {
        GameObject textObject = new GameObject();
        TextMesh textMesh = textObject.AddComponent<TextMesh>();

        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        textMesh.text = HaxCoordinate.OffsetCoordinate.ToString()
                + "\n" + HaxCoordinate.AxialCoordinate.ToString()
                 + "\n" + HaxCoordinate.CubeCoordinate.ToString();
        textMesh.fontSize = (int)(Grid.instance.m_CellSize * 3);

        textObject.transform.parent = transform;
        textObject.transform.localPosition = Vector3.zero;
        textObject.transform.eulerAngles = new Vector3(90, textObject.transform.eulerAngles.y, textObject.transform.eulerAngles.z);
    }

    public void SetCoordinate(Vector2Int coord)
    {
        HaxCoordinate.OffsetCoordinate = coord;
        CreateSprite();
        CreateTextMesh();
    }

    public void SetUnit(Unit unit)
    {
        m_Unit = unit;
        ChangeWalkableState(m_Type);
    }

    public void ChangeWalkableState(EHaxType Type)
    {
        m_Type = Type;
        MoveCost = (int)m_Type;
        switch (m_Type)
        {
            case EHaxType.Free:
                IsWalkable = true;             
                break;
            case EHaxType.Wall:
                IsWalkable = false;
                break;
            case EHaxType.Water:
                IsWalkable = true;
                break;
            case EHaxType.Mountain:
                IsWalkable = true;
                break;
        }

        if(m_Unit != null)
            IsWalkable = false;

        ChangeColor();
    }

    public void ChangeColor()
    {
        switch (m_Type)
        {
        case EHaxType.Free:
            m_SpriteRenderer.color = Color.green;
            break;
        case EHaxType.Wall:
            m_SpriteRenderer.color = Color.gray;
            break;
        case EHaxType.Water:
            m_SpriteRenderer.color = new Color(0.378f, 0.747f, 0.902f);
            break;
        case EHaxType.Mountain:
            m_SpriteRenderer.color = new Color(0.988f, 0.392f, 0.12f);
            break;
        }      
    }
}

public enum EHaxType
{
    Free = 1,
    Wall = 0,
    Water = 3,
    Mountain = 5
}




