using UnityEngine;
using System.Collections;

public class SpawnOruga : MonoBehaviour
{
    public GameObject prefabOruga;
    public float tiempoDeEspera = 2.0f; // Tiempo que tarda en salir la siguiente tras morir la anterior

    void Start()
    {
        if (prefabOruga != null) StartCoroutine(SpawnCiclo());
    }

    IEnumerator SpawnCiclo()
    {
        while (true)
        {
            // Regla de oro: Solo si hay 0 orugas en el mapa
            if (OrugaAI.cantidadVivas == 0)
            {
                // Un pequeño margen aleatorio para que no siempre salga del mismo sitio
                // si hay varios spawners esperando
                yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));

                // Volvemos a comprobar por si otro spawner se adelantó en ese segundo
                if (OrugaAI.cantidadVivas == 0)
                {
                    Instantiate(prefabOruga, transform.position, Quaternion.identity);
                    Debug.Log("Nueva oruga en camino desde: " + gameObject.name);
                }
            }

            // Revisa cada segundo si el mapa está libre
            yield return new WaitForSeconds(1.0f);
        }
    }
}
