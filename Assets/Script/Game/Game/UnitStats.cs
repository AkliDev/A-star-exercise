using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Unit/Stats", order = 1)]
public class UnitStats : ScriptableObject
{
    [SerializeField] private int m_MoveEnergy;
    public int MoveEnergy { get { return m_MoveEnergy; } }
    [SerializeField] private int m_HitPoints;
    public int HitPoints { get { return m_HitPoints; } }
    [SerializeField] private int m_DamagePoints;
    public int DamagePoints { get { return m_DamagePoints; } }
    [SerializeField] private int m_DamageRange;
    public int DamageRange { get { return m_DamageRange; } }
}
