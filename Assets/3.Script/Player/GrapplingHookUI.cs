using UnityEngine;
using UnityEngine.UI;

public class GrapplingHookUI : MonoBehaviour
{
    [Header("Settings")]
    public float maxDistance = 25f;
    public LayerMask hookLayer;
    public Transform hookOrigin; // 손이나 발사 지점
    public LineRenderer aimLine; // 미리보기 라인렌더러

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        if (aimLine != null)
        {
            aimLine.positionCount = 0;
        }
    }

    private void Update()
    {
        ShowGrapplePreview();
    }

    private void ShowGrapplePreview()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, hookLayer))
        {
            if (aimLine != null)
            {
                aimLine.positionCount = 2;
                aimLine.SetPosition(0, hookOrigin.position);
                aimLine.SetPosition(1, hit.point);
            }
        }
        else
        {
            if (aimLine != null)
            {
                aimLine.positionCount = 0;
            }
        }
    }
}