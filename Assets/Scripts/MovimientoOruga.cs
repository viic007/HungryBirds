using UnityEngine;
using System.Collections.Generic;

public class MovimientoOruga : MonoBehaviour
{
    public float velocidad = 2f;
    private Transform objetivo;

    void Start()
    {
        BuscarAlJugador();
    }

    void Update()
    {
        if (objetivo != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, objetivo.position, velocidad * Time.deltaTime);

            if (objetivo.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (objetivo.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    void BuscarAlJugador()
    {
        // Buscamos todos los objetos que tengan el tag "Player"
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");

        if (jugador != null)
        {
            objetivo = jugador.transform; // Fijamos al jugador como nuestro objetivo
        }
        else
        {
            Debug.LogWarning("No se encontró al jugador. ¡Asegúrate de que tu personaje tiene puesto el Tag 'Player'!");
        }
    }
}