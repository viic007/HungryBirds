using UnityEngine;
using UnityEngine.InputSystem; 


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    

    public Rigidbody2D playerRb;
    private Vector2 moveInput;
    private Animator playerAnimator;

    private PlayerControls controls;

    private void Awake()
    {   
        controls = new PlayerControls();
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
       
        controls.Enable();
    }

    private void OnDisable()
    {
        
        controls.Disable();
    }

    private void Start()
    {  
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    public void Move(InputAction.CallbackContext cntx)
    {
        moveInput = cntx.ReadValue<Vector2>();   
    }
    
    private void Update()
    {
        if (playerAnimator != null)
        {
            playerAnimator.SetFloat("Horizontal", moveInput.x);
            playerAnimator.SetFloat("Vertical", moveInput.y);
            playerAnimator.SetFloat("Speed", moveInput.sqrMagnitude);
        }
    }

    private void FixedUpdate()
    {
        playerRb.linearVelocity = moveInput * speed;

    }
}