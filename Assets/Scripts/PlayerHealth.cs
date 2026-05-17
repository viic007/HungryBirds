using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class PlayerHealth : MonoBehaviour
{
    public float vidaActual = 100f;
    private float vidaMaxima = 100f;

    private bool yaEstaMuerto = false;

    [Header("Interfaz (UI)")]
    public Slider sliderBarraVida;

    void Start()
    {
        vidaActual = vidaMaxima;
        yaEstaMuerto = false;

        if (sliderBarraVida != null)
        {
            sliderBarraVida.minValue = 0;
            sliderBarraVida.maxValue = vidaMaxima;
            sliderBarraVida.value = vidaActual; 
        }
    }

    public void TakeDamage(float cantidad)
    {
        if (yaEstaMuerto) return;

        vidaActual -= cantidad;
        Debug.Log("¡Daño al jugador! Vida actual: " + vidaActual + "%");

        if (sliderBarraVida != null)
        {
            sliderBarraVida.value = vidaActual;
        }

        if (vidaActual <= 0)
        {
            MorderElPolvo();
        }
    }

    void MorderElPolvo()
    {
        yaEstaMuerto = true;
        Debug.Log("Has muerto");

        Time.timeScale = 0f;

        if (GameManager.instance != null)
        {
            Destroy(GameManager.instance.gameObject);
        }

        SceneManager.LoadScene("GameOver");
    }
}