using System;
using UnityEngine;
using TMPro;

public class CubeController : MonoBehaviour
{
    public int value;
    public bool isSelected = false;

    private TextMeshPro valueText;

    private void Start()
    {
        valueText = GetComponentInChildren<TextMeshPro>();

        UpdateText();
        UpdateTextColor();
    }

    public void Init(int val)
    {
        value = val;

        if (valueText == null)
            valueText = GetComponentInChildren<TextMeshPro>();

        UpdateText();
        UpdateTextColor();
    }

    private void UpdateText()
    {
        if (valueText != null)
            valueText.text = value.ToString();
    }

    public void UpdateTextColor()
    {
        if (valueText != null)
            valueText.color = isSelected ? Color.black : Color.white;
    }

    public void ResetColor()
    {
        isSelected = false;
        UpdateTextColor();
    }
}