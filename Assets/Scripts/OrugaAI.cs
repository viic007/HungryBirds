using UnityEngine;

public class OrugaAI : MonoBehaviour
{
    public static int cantidadVivas = 0;

    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [Header("Ajustes de Movimiento")]
    public float speed = 3.0f;
    public int vida = 2;

    [Header("Ajustes de Ataque")]
    public int danoAlJugador = 10;

    void Start()
    {
        cantidadVivas++;
        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
       
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    
        transform.localScale = new Vector3(2f, 2f, 1f);

        if (rb != null)
        {
            rb.gravityScale = 0;
            rb.freezeRotation = true;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            // Esto quita el deslizamiento
            rb.linearDamping = 20f;
        }
    }

    void FixedUpdate()
    {
        if (player == null || rb == null) return;

        // 1. Calculamos la distancia en cada eje
        float diffX = player.position.x - transform.position.x;
        float diffY = player.position.y - transform.position.y;

        Vector2 velocidadNueva = Vector2.zero;

        // 2. LÓGICA DE RECTAS (Prioriza el eje con mayor distancia)
        if (Mathf.Abs(diffX) > Mathf.Abs(diffY))
        {
            // Movimiento puro Horizontal
            velocidadNueva = new Vector2(Mathf.Sign(diffX) * speed, 0);
        }
        else
        {
            // Movimiento puro Vertical
            velocidadNueva = new Vector2(0, Mathf.Sign(diffY) * speed);
        }

        // 3. APLICAR VELOCIDAD DIRECTA (Cero resbalón)
        rb.linearVelocity = velocidadNueva;

        // 4. Orientación del sprite
        if (velocidadNueva.x != 0)
        {
            transform.localScale = new Vector3(velocidadNueva.x > 0 ? 2f : -2f, 2f, 1f);
        }
    }

    void OnDestroy()
    {
        cantidadVivas--;
        if (cantidadVivas < 0) cantidadVivas = 0;
    }

    void ResetColor()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth vidaJugador = other.GetComponent<PlayerHealth>();
            if (vidaJugador != null)
            {
                vidaJugador.TakeDamage(danoAlJugador);
            }

            else
            {
                Debug.LogWarning("El jugador no tiene un componente PlayerHealth.");
            }

            Destroy(gameObject);
        }
    }

    public void RecibirDano(int dano)
    {
        vida -= dano;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            Invoke("ResetColor", 0.15f);
        }

        if (vida <= 0)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddScore(10); 
            }
            Destroy(gameObject);
        }
    }
}