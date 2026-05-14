using UnityEngine;
using System.Collections.Generic;

public class MovimientoOruga : MonoBehaviour
{
    public float velocidad = 2f;
    private Transform objetivo;

    void Start()
    {
        SeleccionarPlantaCentro();
    }

    void Update()
    {
        if (objetivo != null)
        {
            // Mover hacia la planta
            transform.position = Vector2.MoveTowards(transform.position, objetivo.position, velocidad * Time.deltaTime);

            if (Vector2.Distance(transform.position, objetivo.position) < 0.1f)
            {
                LlegamosALaPlanta();
            }
        }
    }

    void SeleccionarPlantaCentro()
    {
        // Buscamos todos los objetos que tengan el tag "PlantaCentro"
        GameObject[] plantas = GameObject.FindGameObjectsWithTag("PlantaCentro");

        if (plantas.Length > 0)
        {
            // Elegimos uno al azar de la lista
            int indiceAzar = Random.Range(0, plantas.Length);
            objetivo = plantas[indiceAzar].transform;
        }
        else
        {
            Debug.LogWarning("No se encontraron plantas con el tag 'PlantaCentro'");
        }
    }

    void LlegamosALaPlanta()
    {
        Debug.Log("¡La oruga ha llegado a su destino y está comiendo!");
        // Destroy(gameObject); 
    }
}