using UnityEngine;
using System.Collections.Generic;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab;
    public float spawnRate = 4f;
    public float minX = -12f, maxX = 12f, minY = -7f, maxY = 7f;

    private List<GameObject> availablePlants = new List<GameObject>();
    private int lastSide = -1;

    void Start()
    {
        FillPlants();
        InvokeRepeating("SpawnBird", 2f, spawnRate);
    }

    void FillPlants()
    {
        GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");
        availablePlants.Clear();
        availablePlants.AddRange(plants);
        Debug.Log("Plantas encontradas: " + availablePlants.Count);
    }

    void SpawnBird()
    {
        if (availablePlants.Count == 0) FillPlants();
        if (availablePlants.Count == 0) return; // Por si no hay plantas con Tag "Plant"

        // Lado de aparición
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

        // Elegir planta sin repetir
        int index = Random.Range(0, availablePlants.Count);
        GameObject target = availablePlants[index];
        availablePlants.RemoveAt(index);

        // Crear pájaro
        GameObject newBird = Instantiate(birdPrefab, new Vector3(px, py, 0), Quaternion.identity);

        // Asignar objetivo
        CrowAI ai = newBird.GetComponent<CrowAI>();
        if (ai != null)
        {
            ai.targetPlant = target.transform;
        }
    }
}