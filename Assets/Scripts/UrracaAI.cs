using UnityEngine;
using System.Collections;

public class UrracaAI : MonoBehaviour
{
    public float flySpeed = 5f;
    public Transform targetPlant;
    private Animator anim;

    [Header("Ajustes del Nivel 2")]
    public float danoPorcentaje = 15f;    // Resta 15% de vida
    public int puntosRecompensa = 25;     // Suma 25 puntos

    private bool fueAhuyentada = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(CrowBehavior());
    }

    IEnumerator CrowBehavior()
    {
        yield return new WaitForEndOfFrame();

        if (targetPlant != null)
        {
            Collider2D coll = targetPlant.GetComponent<Collider2D>();
            Vector3 destination;

            if (coll != null)
            {
                destination = coll.bounds.center;
            }
            else
            {
                destination = targetPlant.position; 
            }

            // 1. Vuelo a la planta
            yield return StartCoroutine(FlyTo(destination));

            // 2. Aterrizaje y Espera de 5 segundos
            if (anim != null) anim.SetBool("isLanded", true);

            yield return new WaitForSeconds(5f);

            if (!fueAhuyentada)
            {
                AtacarAlJugador();
            }
            DespegarYHuir();
        }
    }

    void AtacarAlJugador()
    {
        Debug.Log("¡La urraca se va volando y te ha picado! -15% de vida.");
        PlayerHealth jugadorVida = FindFirstObjectByType<PlayerHealth>();
        if (jugadorVida != null)
        {
            jugadorVida.TakeDamage(danoPorcentaje);
        }
    }

    void DespegarYHuir()
    {
        if (anim != null) anim.SetBool("isLanded", false);
        Vector2 escapePos = new Vector2(transform.position.x + 15f, transform.position.y + 5f); 
        StartCoroutine(FlyTo(escapePos));

        Destroy(gameObject, 3f);
    }

    public void RecibirDano()
    {
        if (fueAhuyentada) return;

        fueAhuyentada = true;
        Debug.Log("¡Urraca ahuyentada! +25 puntos.");

        if (GameManager.instance != null)
        {
            GameManager.instance.AddScore(puntosRecompensa);
        }

        StopAllCoroutines();
        DespegarYHuir();
    }

    IEnumerator FlyTo(Vector2 destination)
    {
        while (Vector2.Distance(transform.position, destination) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, flySpeed * Time.deltaTime);

            float scaleX = (destination.x > transform.position.x) ? -Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);

            yield return null;
        }
    }
}