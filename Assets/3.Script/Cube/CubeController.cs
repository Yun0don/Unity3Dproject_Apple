using System;
using UnityEngine;
using TMPro;

public class CubeController : MonoBehaviour // 큐브 상태관리
{
    public int value;
    public bool isSelected = false;

    private Renderer rend;
    private Material matInstance;            
    private TextMeshPro valueText;         
    private void Start()
    {
        rend = GetComponent<Renderer>();
        matInstance = rend.material;        
        valueText = GetComponentInChildren<TextMeshPro>(); 

        UpdateText();
        UpdateColor();
    }

    public void Init(int val)
    {
        value = val;
        if (valueText == null)
            valueText = GetComponentInChildren<TextMeshPro>();

        UpdateText();
    }

    private void UpdateText()
    {
        if (valueText != null)
            valueText.text = value.ToString();
    }

    public void UpdateColor()
    {
        if (matInstance != null)
            matInstance.color = isSelected ? Color.green : Color.grey;
    }
    public void ResetColor()
    {
        isSelected = false;
        UpdateColor();
    }
}