using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab;
    public float minX = -12f, maxX = 12f, minY = -7f, maxY = 7f;

    private GameObject[] allPlants;
    private int lastSide = -1;

    void Start()
    {
        allPlants = GameObject.FindGameObjectsWithTag("Plant");

        if (allPlants.Length == 0)
        {
            Debug.LogError("¡Ojo! No se ha encontrado ninguna planta con el Tag 'Plant'.");
        }

        // Al empezar el nivel, llamamos a la función para que espere entre 3 y 5 segundos
        IniciarCuentaAtrasSiguienteUrraca();
    }

    public void IniciarCuentaAtrasSiguienteUrraca()
    {
        StartCoroutine(RutinaEsperaSiguienteUrraca());
    }

    IEnumerator RutinaEsperaSiguienteUrraca()
    {
        // Calculamos el tiempo aleatorio
        float tiempoAleatorio = Random.Range(2f, 4f);
        Debug.Log("Siguiente urraca en camino. Tiempo de respiro: " + tiempoAleatorio + " segundos.");

        yield return new WaitForSeconds(tiempoAleatorio);

        SpawnBird();
    }

    void SpawnBird()
    {
        if (allPlants == null || allPlants.Length == 0) return;

        int side;
        do { side = Random.Range(0, 4); } while (side == lastSide);
        lastSide = side;

        float px = 0, py = 0;
        switch (side)
        {
            case 0: px = minX; py = Random.Range(minY, maxY); break;
            case 1: px = maxX; py = Random.Range(minY, maxY); break;
            case 2: px = Random.Range(minX, maxX); py = maxY; break;
            case 3: px = Random.Range(minX, maxX); py = minY; break;
        }

        int index = Random.Range(0, allPlants.Length);
        GameObject target = allPlants[index];

        if (target == null) return;

        GameObject newBird = Instantiate(birdPrefab, new Vector3(px, py, 0), Quaternion.identity);

        UrracaAI ai = newBird.GetComponent<UrracaAI>();
        if (ai != null)
        {
            ai.targetPlant = target.transform;
            ai.AsignarSpawner(this);
        }
    }
}