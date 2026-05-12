using UnityEngine;
using System.Collections;

public class CrowAI : MonoBehaviour
{
    public float flySpeed = 4f;
    public Transform targetPlant; // Lo asigna el Spawner
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(CrowBehavior());
    }

    IEnumerator CrowBehavior()
    {
        // Esperamos un frame para recibir la planta del Spawner
        yield return new WaitForEndOfFrame();

        if (targetPlant != null)
        {
            // Intentamos obtener el centro del Collider
            Collider2D coll = targetPlant.GetComponent<Collider2D>();
            Vector3 destination;

            if (coll != null)
            {
                destination = coll.bounds.center; // El centro del cuadro verde
            }
            else
            {
                destination = targetPlant.position; // Si no hay collider, va a las flechas
            }

            // 1. Vuelo a la planta
            yield return StartCoroutine(FlyTo(destination));

            // 2. Aterrizaje
            if (anim != null) anim.SetBool("isLanded", true);
            yield return new WaitForSeconds(5f);

            // 3. Escape
            if (anim != null) anim.SetBool("isLanded", false);
            Vector2 escapePos = new Vector2(transform.position.x + 15f, transform.position.y);
            yield return StartCoroutine(FlyTo(escapePos));

            Destroy(gameObject);
        }
    }

    IEnumerator FlyTo(Vector2 destination)
    {
        while (Vector2.Distance(transform.position, destination) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, flySpeed * Time.deltaTime);

            // Girar sprite según dirección
            float scaleX = (destination.x > transform.position.x) ? -Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);

            yield return null;
        }
    }
}