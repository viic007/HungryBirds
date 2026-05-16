using UnityEngine;

public class OrugaAI : MonoBehaviour
{
    public static int cantidadVivas = 0;

    private Transform player;
    private Rigidbody2D rb;

    [Header("Ajustes de Movimiento")]
    public float speed = 3.0f; // Ahora un valor pequeño (2-5) es suficiente
    public int vida = 2;

    void Start()
    {
        cantidadVivas++;
        rb = GetComponent<Rigidbody2D>();

        // Buscamos al jugador
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;

        // Escala robusta
        transform.localScale = new Vector3(2f, 2f, 1f);

        // CONFIGURACIÓN DE RIGIDBODY POR CÓDIGO (Para evitar errores manuales)
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

    private void OnMouseDown()
    {
        vida--;
        GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("ResetColor", 0.1f);
        if (vida <= 0) Destroy(gameObject);
    }

    void ResetColor() => GetComponent<SpriteRenderer>().color = Color.white;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance != null) GameManager.instance.LoseLife();
            Destroy(gameObject);
        }
    }
}