using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Ajustes de Vida")]
    public float vidaMaxima = 100f;
    public float vidaActual;

    [Header("Interfaz de Usuario (slider)")]
    public Slider sliderVida;

    [Header("Efectos Visuales (Daño)")]
    public SpriteRenderer spriteDelJugador; 
    public Color colorDano = Color.red;     
    private Color colorOriginal;

    void Start()
    {
        // Al empezar CADA DÍA, la vida se reinicia al 100% automáticamente
        vidaActual = vidaMaxima;

        if (sliderVida != null)
        {
            sliderVida.maxValue = vidaMaxima;
            sliderVida.value = vidaActual;
        }

        if (spriteDelJugador != null)
        {
            colorOriginal = spriteDelJugador.color;
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

        if (spriteDelJugador != null)
        {
            StartCoroutine(EfectoParpadeo());
        }

        if (vidaActual <= 0)
        {
            vidaActual = 0;
            Morir();
        }
    }

    IEnumerator EfectoParpadeo()
    {
        spriteDelJugador.color = colorDano;
        yield return new WaitForSeconds(0.15f);
        spriteDelJugador.color = colorOriginal;
    }

    void Morir()
    {
        Debug.Log("Has muerto");
        // Aquí puedes decidir qué pasa si el jugador muere dentro del día
        // Por ejemplo, reiniciar el día actual:
        // UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}