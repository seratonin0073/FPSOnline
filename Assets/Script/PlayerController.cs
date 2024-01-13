using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private float 
        walksSeed = 2f, 
        sprintSpeed = 3f, 
        mouseSensetive = 150,
        jumpForce = 3f,
        smoothTime;
    private float verticalLookRotation;
    private bool isGrounded = false;
    private Vector3 smoothMove;
    private Vector3 moveAmount;
    private Rigidbody rb;
    private PhotonView pnView;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pnView = GetComponent<PhotonView>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        if(!pnView.IsMine)
        {
            Destroy(playerCamera);
        }
    }

    void Update()
    {
        if(!pnView.IsMine)
        {
            return;
        }
        Look();
        Movement();
    }

    private void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensetive * Time.deltaTime);
        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensetive * Time.deltaTime;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -80, 80);
        playerCamera.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    private void Movement()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0,
            Input.GetAxisRaw("Vertical")).normalized;
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir *
        (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walksSeed), ref smoothMove,
        smoothTime);
    }

    private void FixedUpdate()
    {
        if(!pnView.IsMine)
        {
            return;
        }
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * 
            Time.fixedDeltaTime);
    }
}
