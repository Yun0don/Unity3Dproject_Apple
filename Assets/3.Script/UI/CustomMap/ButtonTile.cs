using UnityEngine;
using UnityEngine.UI;

public class ButtonTile : MonoBehaviour
{
    private Button button;
    private Image image;

    private bool isFilled = false;

    private Color filledColor = Color.green;
    private Color emptyColor = Color.white;

    public bool IsFilled => isFilled;

    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        button.onClick.AddListener(ToggleState);
        SetColor();
    }
    private void Start()
    {
        // 자동으로 매니저에 등록
        MapEditorManager manager = FindObjectOfType<MapEditorManager>();
        if (manager != null)
            manager.RegisterTile(this);
    }
    private void ToggleState()
    {
        isFilled = !isFilled;
        SetColor();
    }

    private void SetColor()
    {
        if (image != null)
            image.color = isFilled ? filledColor : emptyColor;
    }

    public void ResetTile()
    {
        isFilled = false;
        SetColor();
    }
}