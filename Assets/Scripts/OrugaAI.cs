using UnityEngine;

public class OrugaAI : MonoBehaviour
{
    // Variable estática: compartida por todas las orugas y spawners
    public static int cantidadVivas = 0;

    private Transform player;
    private Rigidbody2D rb;
    public float speed = 50.0f;
    public int vida = 2;

    void Start()
    {
        cantidadVivas++; // Suma al nacer
        rb = GetComponent<Rigidbody2D>();
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;

        // Ajuste de escala (puedes cambiar el 2f por el tamaño que prefieras)
        transform.localScale = new Vector3(2f, 2f, 1f);
    }

    void FixedUpdate()
    {
        if (player == null || rb == null) return;

        float diffX = player.position.x - transform.position.x;
        float diffY = player.position.y - transform.position.y;
        Vector2 movimiento = Vector2.zero;

        // Movimiento no diagonal
        if (Mathf.Abs(diffX) > Mathf.Abs(diffY))
            movimiento = new Vector2(Mathf.Sign(diffX), 0);
        else
            movimiento = new Vector2(0, Mathf.Sign(diffY));

        rb.AddForce(movimiento * speed);

        if (movimiento.x != 0)
            transform.localScale = new Vector3(movimiento.x > 0 ? 2f : -2f, 2f, 1f);
    }

    // Se ejecuta cuando la oruga se destruye
    void OnDestroy()
    {
        cantidadVivas--; // Resta al morir
        if (cantidadVivas < 0) cantidadVivas = 0; // Seguridad
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