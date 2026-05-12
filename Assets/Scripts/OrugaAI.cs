using UnityEngine;

public class OrugaAI : MonoBehaviour
{
    //Hace que la oruga se comporte como un teledirigido que se mueve hacia la planta más cercana. Si la oruga alcanza la planta, se destruye a sí misma. La oruga también tiene una función para recibir daño, y si su salud llega a cero, se destruye.
    public Transform targetPlant;
    public float speed = 0.5f;
    public int health = 1;

    void Update()
    {
        if (targetPlant != null)
        {
            Vector3 destino = targetPlant.position;
            Vector3 actual = transform.position;

            // Movimiento horizontal primero
            if (Mathf.Abs(actual.x - destino.x) > 0.05f)
            {
                float nuevaX = Mathf.MoveTowards(actual.x, destino.x, speed * Time.deltaTime);
                transform.position = new Vector3(nuevaX, actual.y, actual.z);

                // Girar sprite
                transform.localScale = new Vector3(nuevaX < actual.x ? -1 : 1, 1, 1);
            }
            // Si ya llegamos a la X, Movimiento vertical
            else if (Mathf.Abs(actual.y - destino.y) > 0.05f)
            {
                float nuevaY = Mathf.MoveTowards(actual.y, destino.y, speed * Time.deltaTime);
                transform.position = new Vector3(actual.x, nuevaY, actual.z);
            }
            // Ya estamos en el sitio
            else
            {
                LlegarALaPlanta();
            }
        }
    }

    void LlegarALaPlanta()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.LoseLife();
        }

        Destroy(gameObject);
    }

    // Choque de colisión con la planta
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plant"))
        {
            LlegarALaPlanta();
        }
    }
}
