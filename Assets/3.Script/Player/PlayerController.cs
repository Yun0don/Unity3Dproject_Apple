using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 4f;
    public float runSpeed = 7f;
    public float jumpHeight = 1.2f;
    public float gravity = -9.81f;

    [Header("Camera Settings")]
    public Transform cameraTransform;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 80f;

    private CharacterController controller;
    private Vector3 velocity;
    private float verticalLookRotation = 0f;
    private Quaternion initialCameraRotation;
    private float lastYPosition; // 이전 Y 위치 저장
    // bool 값으로 점프를 했나
    // 10체크

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        initialCameraRotation = cameraTransform.localRotation;
        lastYPosition = transform.position.y;
    }

    private void Update()
    {
        if (controller.enabled)
        {
            UpdateMovement();
        }
        else
        {
            velocity = Vector3.zero; // 그래플링 중 등일 때 중력 효과 제거
        }

        UpdateCameraRotation();
        CheckYMovementForDestruction();
    }



    private void UpdateMovement()
    {
        
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void UpdateCameraRotation()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        Quaternion yawRotation = Quaternion.AngleAxis(mouseX, Vector3.up);
        transform.rotation *= yawRotation;

        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -maxLookAngle, maxLookAngle);

        Quaternion pitchRotation = Quaternion.AngleAxis(verticalLookRotation, Vector3.right);
        cameraTransform.localRotation = initialCameraRotation * pitchRotation;
    }

    private void CheckYMovementForDestruction()
    {
         Debug.Log($"[CHECK] Y변화량: {Mathf.Abs(transform.position.y - lastYPosition):F2}, 준비됨: {SumCheckManager.Instance.isReadyToDestroy}");

        if (SumCheckManager.Instance.isReadyToDestroy &&
            Mathf.Abs(transform.position.y - lastYPosition) > 0.039f)
        {
            Debug.Log("[DESTROY] 조건 만족, 큐브 파괴 실행");
            DestroyManager.Instance.DestroyCubes(SumCheckManager.Instance.GetCubesToDestroy());
            SumCheckManager.Instance.ClearAfterDestruction();
        }

        lastYPosition = transform.position.y;
    }



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Cube"))
        {
            // Debug.Log("Hit cube");
            CubeController cube = hit.gameObject.GetComponent<CubeController>();

            if (cube != null && !cube.isSelected)
            {
                cube.isSelected = true; 
                cube.UpdateColor();
                SumCheckManager.Instance.ToggleSelection(cube);
            }
        }
    }
} 
