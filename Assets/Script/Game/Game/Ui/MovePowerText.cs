using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MovePowerText : MonoBehaviour
{
    TextMeshProUGUI m_Text;
    [SerializeField] private Slider m_MovementSlider;

    private void Start()
    {
        m_Text = GetComponent<TextMeshProUGUI>();
        m_MovementSlider.onValueChanged.AddListener(UpdateTextValue);
    }

    private void OnDestroy()
    {
        m_MovementSlider.onValueChanged.RemoveListener(UpdateTextValue);
    }

    private void UpdateTextValue(float value)
    {
        m_Text.text = "Move Power: " + (int)value;
    }
}
