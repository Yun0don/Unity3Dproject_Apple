using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class GrapplingHook : MonoBehaviour
{
    [Header("Grappling Settings")]
    public float maxDistance = 25f;
    public float spring = 250f;
    public float damper = 200f;
    public float massScale = 1f;
    public float pullForce = 40f; // 매달렸을 때 당기는 추가 힘

    public LayerMask hookLayer;
    public Transform hookOrigin;

    private SpringJoint springJoint;
    private LineRenderer lineRenderer;
    private Camera cam;
    private CharacterController charController;
    private Rigidbody rb;
    private bool isGrappling;

    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        charController = GetComponent<CharacterController>();
        lineRenderer = GetComponent<LineRenderer>();

        rb.isKinematic = true;
        lineRenderer.positionCount = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isGrappling)
        {
            StartCoroutine(GrappleRoutine());
        }

        if (Input.GetMouseButtonUp(0))
        {
            EndGrapple(preserveMomentum: true);
        }

        if (isGrappling)
        {
            lineRenderer.SetPosition(0, hookOrigin.position);
        }
    }

    private IEnumerator GrappleRoutine()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, hookLayer))
        {
            Vector3 targetPoint = hit.point;

            charController.enabled = false;
            rb.isKinematic = false;
            isGrappling = true;

            springJoint = gameObject.AddComponent<SpringJoint>();
            springJoint.autoConfigureConnectedAnchor = false;
            springJoint.connectedAnchor = targetPoint;
            springJoint.spring = spring;
            springJoint.damper = damper;
            springJoint.massScale = massScale;
            springJoint.maxDistance = Vector3.Distance(transform.position, targetPoint) * 0.8f;
            springJoint.minDistance = Vector3.Distance(transform.position, targetPoint) * 0.6f;

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(1, targetPoint);

            while (isGrappling)
            {
                lineRenderer.SetPosition(0, hookOrigin.position);

                // ✅ 줄 방향으로 지속적인 보조 가속
                Vector3 directionToAnchor = (springJoint.connectedAnchor - transform.position).normalized;
                rb.AddForce(directionToAnchor * pullForce, ForceMode.Acceleration);

                yield return null;
            }
        }
    }

    private void EndGrapple(bool preserveMomentum = false)
    {
        if (!isGrappling) return;

        // 추가적인 한 번의 밀어주기 효과를 원할 경우 아래 코드 유지
        if (springJoint != null)
        {
            Vector3 directionToAnchor = (springJoint.connectedAnchor - transform.position).normalized;
            if (preserveMomentum)
                rb.velocity += directionToAnchor * 20f + Vector3.up * 2f;

            Destroy(springJoint);
        }

        isGrappling = false;
        lineRenderer.positionCount = 0;

        // ✅ 딜레이 후 이동 방식 전환
        StartCoroutine(ReenableControllerDelayed());
    }

    private IEnumerator ReenableControllerDelayed()
    {
        yield return new WaitForSeconds(0.1f); // 가속 후 부드럽게 전환
        rb.isKinematic = true;
        charController.enabled = true;
    }
}
