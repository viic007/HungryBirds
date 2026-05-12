using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public int vidas = 5;
    public static GameManager instance;
    public int currentDay = 1;
    public float dayDuration = 80f; // 80 segundos por día
    private float timer;
    private bool dayEnded = false;

    void Awake()
    {
        // Esto hace que el GameManager sea único y no se borre entre escenas
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        if (dayEnded) return;
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            EndDay();
        }
    }

    public void ResetTimer()
    {
        timer = dayDuration;
        dayEnded = false;
    }

    void EndDay()
    {
        dayEnded = true;
        Debug.Log("Día" + currentDay + " completado.");

        // Si estamos en el Día 1, vamos a la pantalla de "Día 2", etc.

        if (currentDay == 1)
        {
            currentDay = 2;
            SceneManager.LoadScene("TransicionDia2");
            
        }
        else if (currentDay == 2)
        {
            currentDay = 3;
            SceneManager.LoadScene("TransicionDia3");
        }
        else 
        {
            SceneManager.LoadScene("VictoriaFinal");
        }
    }

    public void LoseLife()
    {
        vidas--; // Resta una vida
        Debug.Log("¡Una oruga se ha comido una planta! Vidas restantes: " + vidas);

        if (vidas <= 0)
        {
            Debug.Log("Game Over");
            // Entrada de la escena de Menú o Game Over
        }
    }
}