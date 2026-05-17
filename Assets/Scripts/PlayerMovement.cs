using UnityEngine;
using UnityEngine.InputSystem; // A�adido para asegurar la compatibilidad

// Estas l�neas obligan a Unity a asegurarse de que el personaje tenga estos componentes
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    

    public Rigidbody2D playerRb;
    private Vector2 moveInput;
    private Animator playerAnimator;

    // Referencia al c�digo generado por el Input System
    private PlayerControls controls;

    private void Awake()
    {
        // 1. Inicializamos los controles
        controls = new PlayerControls();
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        // 2. Activamos TODOS los mapas de acci�n a la vez (m�s seguro que controls.Player.Enable())
        controls.Enable();
    }

    private void OnDisable()
    {
        // 3. Desactivamos todo al apagar
        controls.Disable();
    }

    private void Start()
    {
        // 4. Obtenemos las referencias
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    public void Move(InputAction.CallbackContext cntx)
    {
        moveInput = cntx.ReadValue<Vector2>();
        Debug.Log("caquita");
    }
    
    private void Update()
    {
      
        // 6. Imprimimos para verificar
       // Debug.Log("Direccion del Input: " + moveInput); 

        // 7. Pasamos los valores al Animator (con una comprobaci�n de seguridad)
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