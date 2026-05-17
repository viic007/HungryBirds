using UnityEngine;
using UnityEngine.AI; //Necesario para usar NavMeshAgent

public class OrugaIQ : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform targetPlant;
    public int vida = 2;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Esto es para que la oruga no se "tumbe" en 2D
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        if (targetPlant != null)
        {
            // La oruga calcula sola el camino evitando obstáculos
            agent.SetDestination(targetPlant.position);
        }
    }

    public void RecibirDano(int dano)
    {
        vida -= dano;
        if (vida <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plant"))
        {
            Destroy(gameObject);
        }
    }
}
    
