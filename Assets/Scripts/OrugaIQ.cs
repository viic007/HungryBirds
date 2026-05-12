using UnityEngine;
using UnityEngine.AI; //Necesario para usar NavMeshAgent

public class OrugaIQ : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform targetPlant;

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

    private void OnMouseDown()
    {
        Destroy(gameObject); // Matar al hacer clic
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plant"))
        {
            GameManager.instance.LoseLife();
            Destroy(gameObject);
        }
    }
}
    
