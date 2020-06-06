using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileChanger : MonoBehaviour
{
    public static TileChanger instance;

    [SerializeField] private Toggle[] m_Toggles;
    //int m_OnToggle;
    EHaxType m_HaxType = EHaxType.Free;

    HaxNode A;
    HaxNode B;
    [SerializeField] int ReachableAmount;
    [SerializeField] float m_SelectFactor = 0.75f;


    private void Awake()
    {
        CreateInstance();
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

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //    if (Grid.instance.GetHaxClicked() != null)
        //        A = Grid.instance.GetHaxClicked().GetComponent<HaxNode>();
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //    if(Grid.instance.GetHaxClicked()!= null)
        //    B = Grid.instance.GetHaxClicked().GetComponent<HaxNode>();

        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    if (A != null && B != null)
        //    {
        //        HaxNode[] path = AStarAlgorithms.GetPath(A, B);
        //        Grid.instance.ShowPath(A,B);
        //    }
        //}

        if (Input.GetMouseButton(0))
        {
            GameObject hax = Grid.instance.GetHaxClicked();
            if (hax != null)
            {
                HaxNode haxNode = hax.GetComponent<HaxNode>();
                haxNode.ChangeWalkableState(m_HaxType);
            }
        }

        //if (Input.GetMouseButtonDown(1))
        //{
        //    GameObject hax = Grid.instance.GetHaxClicked();
        //    if (hax != null)
        //    {
        //        HaxNode haxNode = hax.GetComponent<HaxNode>();
        //        Grid.instance.ShowPath(Grid.instance.m_Nodes[0, 0], haxNode);
        //    }
        //}

        //if (Input.GetMouseButtonDown(2))
        //{
        //    GameObject hax = Grid.instance.GetHaxClicked();
        //    if (hax != null)
        //    {
        //        HaxNode haxNode = hax.GetComponent<HaxNode>();
        //        Grid.instance.ShowReachable(haxNode, ReachableAmount);
        //    }
        //}
    }

    public void ToggleChange(Toggle Toggle, EHaxType Type)
    {
        if (Toggle.isOn == true)
        {
            {
                for (int i = 0; i < m_Toggles.Length; i++)
                {
                    if (Toggle != m_Toggles[i])
                    {
                        m_Toggles[i].isOn = false;
                    }
                    m_HaxType = Type;

                }
            }
        }
    }
}

