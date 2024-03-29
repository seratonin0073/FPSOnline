using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPunCallbacks, IDamagable
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    private PlayerManager playerManager;

    [SerializeField] Item[] items;
    private int itemIndex;
    private int previtemIndex = -1;

    [SerializeField] Slider healthBar;
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

    public TMP_Text NicknameText;
    private Camera camera;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pnView = GetComponent<PhotonView>();
        Cursor.lockState = CursorLockMode.Locked;

        currentHealth = maxHealth;

        

        playerManager = PhotonView.Find((int)pnView.InstantiationData[0]).GetComponent<PlayerManager>();
    }

    private void Start()
    {
        camera = Camera.main;
        Debug.Log(camera);
        if (!pnView.IsMine)
        {

            Destroy(playerCamera.GetComponentInChildren<Camera>().gameObject);
        }
        else
        {
            EquipItem(0);
        }
        NicknameText.text = pnView.Owner.NickName;

        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

    }

    void Update()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
        NicknameText.transform.parent.LookAt(camera.transform);
        

        if (!pnView.IsMine)
        {
            return;
        }
        Look();
        Movement();
        SelectWeapon();
        UseItem();
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

    private void SelectWeapon()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if(Input.GetKeyDown((i+1).ToString()))
            {
                EquipItem(i);
                break;
            }
        }
    }

    private void EquipItem(int index)
    {
        if (index == previtemIndex) return;
        itemIndex = index;
        items[index].itemGameObject.SetActive(true);
        if (previtemIndex != -1)
        {
            items[previtemIndex].itemGameObject.SetActive(false);
        }

        previtemIndex = itemIndex;

        if(pnView.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("index", itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if(!pnView.IsMine && targetPlayer == pnView.Owner)
        {
            EquipItem((int)changedProps["index"]);
        }
    }

    private void UseItem()
    {
        if(Input.GetMouseButtonDown(0))
        {
            items[itemIndex].Use();
        }
    }

    public void TakeDamage(float damage)
    {
        pnView.RPC("RPC_Damage", RpcTarget.All, damage);
    }
    
    [PunRPC]
    void RPC_Damage(float damage)
    {
        if (!pnView.IsMine) return;
        currentHealth -= damage;
        Debug.Log("CurrentHP: " + currentHealth.ToString());
        healthBar.value = currentHealth;    

        if (currentHealth <= 0)
        {
            playerManager.Die();
        }
    }
}
