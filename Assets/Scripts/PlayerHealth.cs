using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Ajustes de Vida")]
    public float vidaMaxima = 100f;
    public float vidaActual;

    [Header("Interfaz de Usuario (slider)")]
    public Slider sliderVida;

    void Start()
    {
        // Al empezar CADA DÍA, la vida se reinicia al 100% automáticamente
        vidaActual = vidaMaxima;

        if (sliderVida != null)
        {
            sliderVida.maxValue = vidaMaxima;
            sliderVida.value = vidaActual;
        }
    }

    public void TakeDamage(float cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log("¡Daño al jugador! Vida actual: " + vidaActual + "%");

        if (sliderVida != null)
        {
            sliderVida.value = vidaActual;
        }

        if (vidaActual <= 0)
        {
            vidaActual = 0;
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log("Has muerto");
        // Aquí puedes decidir qué pasa si el jugador muere dentro del día
        // Por ejemplo, reiniciar el día actual:
        // UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}