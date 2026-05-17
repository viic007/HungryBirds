using UnityEngine;
using System.Collections;

public class UrracaAI : MonoBehaviour
{
    public float flySpeed = 5f;
    public Transform targetPlant;
    private Animator anim;

    [Header("Ajustes del Nivel 2")]
    public float danoPorcentaje = 15f;
    public int puntosRecompensa = 25;

    private bool fueAhuyentada = false;
    private BirdSpawner spawnerAsignado;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(CrowBehavior());
    }

    public void AsignarSpawner(BirdSpawner spawner)
    {
        spawnerAsignado = spawner;
    }

    IEnumerator CrowBehavior()
    {
        yield return new WaitForEndOfFrame();
        if (targetPlant != null)
        {
            Collider2D coll = targetPlant.GetComponent<Collider2D>();
            Vector3 destination = (coll != null) ? coll.bounds.center : targetPlant.position;

            yield return StartCoroutine(FlyTo(destination));

            if (anim != null) anim.SetBool("isLanded", true);

            yield return new WaitForSeconds(4f);

            if (!fueAhuyentada)
            {
                AtacarAlJugador();
            }

            DespegarYHuir();
        }
    }

    void AtacarAlJugador()
    {
        PlayerHealth jugadorVida = FindFirstObjectByType<PlayerHealth>();
        if (jugadorVida != null)
        {
            jugadorVida.TakeDamage(danoPorcentaje);
        }
    }

    public void RecibirDano()
    {
        if (fueAhuyentada) return;

        fueAhuyentada = true;

        if (GameManager.instance != null)
        {
            GameManager.instance.AddScore(puntosRecompensa);
        }

        StopAllCoroutines();
        DespegarYHuir();
    }

    void DespegarYHuir()
    {
        if (anim != null) anim.SetBool("isLanded", false);

        Vector2 escapePos = new Vector2(transform.position.x + 15f, transform.position.y + 5f);
        StartCoroutine(FlyTo(escapePos));

        StartCoroutine(EsperarParaMorir());
    }

    IEnumerator EsperarParaMorir()
    {
        yield return new WaitForSeconds(3f);

        if (spawnerAsignado != null)
        {
            spawnerAsignado.IniciarCuentaAtrasSiguienteUrraca();
        }

        Destroy(gameObject);
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