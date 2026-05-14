using UnityEngine;

public class OrugaSimple : MonoBehaviour
{
    public Transform targetPlant;
    public float velocidad = 1.5f;
    public float centroPasilloY = 7.88f; 
    private bool llegoAlPasillo = false;

    void Update()
    {
        if (targetPlant == null) return;

        Vector2 objetivoActual;

        // Si no está en el pasillo, su objetivo es la X del pasillo pero manteniendo su Y
        if (!llegoAlPasillo)
        {
            objetivoActual = new Vector2(centroPasilloY, transform.position.x);

            // Si ya está muy cerca de la X del pasillo, cambia de fase
            if (Mathf.Abs(transform.position.x - centroPasilloY) < 0.1f)
            {
                llegoAlPasillo = true;
            }
        }
        //  Una vez en el pasillo, va directa a la planta
        else
        {
            objetivoActual = targetPlant.position;
        }

        // MOVIMIENTO
        transform.position = Vector2.MoveTowards(transform.position, objetivoActual, velocidad * Time.deltaTime);

        // ROTACIÓN (Para que la cabeza mire a donde camina)
        ActualizarRotacion(objetivoActual);

        // DISTANCIA PARA COMER PLANTA
        if (Vector2.Distance(transform.position, targetPlant.position) < 0.1f)
        {
            LlegarALaPlanta();
        }
    }

    void ActualizarRotacion(Vector2 destino)
    {
        Vector2 direccion = (destino - (Vector2)transform.position).normalized;
        if (direccion != Vector2.zero)
        {
            float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
            // Si tus sprites miran a la derecha originalmente, usa 0. 
            // Si miran hacia arriba, resta 90.
            transform.rotation = Quaternion.Euler(0, 0, angulo);
        }
    }

    void LlegarALaPlanta()
    {
        if (GameManager.instance != null) GameManager.instance.LoseLife();
        Destroy(gameObject);
    }
}