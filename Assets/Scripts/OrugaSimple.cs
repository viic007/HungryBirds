using UnityEngine;

public class OrugaSimple : MonoBehaviour
{
    public Transform targetPlant;
    public float velocidad = 1.5f;

    public float centroPasilloX = 0f;

    private bool enPasillo = false;

    void Update()
    {
        if (targetPlant == null) return;

        if (!enPasillo)
        {
            //Ir horizontalmente hacia el pasillo central
            Vector2 destinoPasillo = new Vector2(centroPasilloX, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, destinoPasillo, velocidad * Time.deltaTime);

            // Si ya llegó al centro del pasillo, cambia de fase
            if (Mathf.Abs(transform.position.x - centroPasilloX) < 0.1f)
            {
                enPasillo = true;
            }
        }
        else
        {
            // Bajar/Subir por el pasillo y luego entrar a la planta
            // Para que sea 100% justo, primero llega a la altura (Y) de la planta
            Vector2 destinoFinal = new Vector2(targetPlant.position.x, targetPlant.position.y);
            transform.position = Vector2.MoveTowards(transform.position, destinoFinal, velocidad * Time.deltaTime);
        }

        // Detección de llegada
        if (Vector2.Distance(transform.position, targetPlant.position) < 0.2f)
        {
            LlegarALaPlanta();
        }
    }

    void LlegarALaPlanta()
    {
        Debug.Log("La oruga se ha destruido porque cree que llegó a: " + targetPlant.name);
        if (GameManager.instance != null) GameManager.instance.LoseLife();
        Destroy(gameObject);
    }
}
