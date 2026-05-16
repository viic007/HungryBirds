using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [Header("Sistema de Vida")]
    public float vidaMax = 100f;
    public float vidaActual;
    public Slider barraDeVida;

    private Rigidbody2D playerRb;
    private Vector2 moveInput;
    private Animator playerAnimator;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        playerAnimator.SetFloat("Horizontal", moveX);
        playerAnimator.SetFloat("Vertical", moveY);
        playerAnimator.SetFloat("Speed", moveInput.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + moveInput * speed * Time.fixedDeltaTime);
    }
}


public class PlayerHealth : MonoBehaviour
{
    [Header("Configuración de Vida")]
    public float vidaMax = 100f;
    private float vidaActual;

    [Header("Referencia a la UI")]
    public Slider barraDeVida; // Arrastra aquí tu Slider desde el Inspector

    void Start()
    {
        vidaActual = vidaMax;
        ActualizarInterfaz();
    }

    // Esta función la llamaremos cuando una oruga nos toque
    public void RecibirDano(float cantidad)
    {
        vidaActual -= cantidad;

        // Evitamos que la vida baje de 0
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMax);

        ActualizarInterfaz();

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void ActualizarInterfaz()
    {
        if (barraDeVida != null)
        {
            // El valor del Slider va de 0 a 1 (0% a 100%)
            barraDeVida.value = vidaActual / vidaMax;
        }
    }

    void Morir()
    {
        Debug.Log("Jane se ha quedado sin energía...");
        // Aquí podrías reiniciar el nivel o poner una animación
    }
}
