using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitState
{
    void OnEnter();
    void Update();
    void OnExit();
}

public class Unit : MonoBehaviour
{
    [SerializeField] ESide m_Side;
    public HaxNode m_Node;

    public UnitStats m_Stats;
    int m_HitPoints;

    public void ChangeTile(HaxNode hax)
    {
        if (m_Node != null)
        {
            m_Node.SetUnit(null);
        }
        m_Node = hax;
        m_Node.SetUnit(this);
        SetPosition();
    }

    public void SetPosition()
    {
        transform.position = m_Node.m_SpriteRenderer.transform.position;
    }
}

public enum ESide
{
    Good,
    Bad
}
