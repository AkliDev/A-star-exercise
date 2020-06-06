using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendToggle : MonoBehaviour {

    [SerializeField] private EHaxType m_HaxType;
    private Toggle m_Toggle;

    private void Awake()
    {
        m_Toggle = GetComponent<Toggle>();
    }
    void Start () {
		
	}
	
	void Update () {
		
	}

    public void OnToggle()
    {
        if(m_Toggle.isOn)
        TileChanger.instance.ToggleChange(m_Toggle, m_HaxType);      
    }
}
