using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Control del Tiempo y Días")]
    public int currentDay = 1;
    public float dayDuration = 80f;
    private float timer;
    private bool dayEnded = false;

    [Header("Sistema de Puntuación")]
    public int score = 0; 

    [Header("Componentes de la Interfaz (UI)")]
    public TextMeshProUGUI timerText; // Arrastra aquí el TextoTimer
    public TextMeshProUGUI scoreText;

    void Awake()
    {
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

        if (timerText != null)
        {
            float tiempoSeguro = Mathf.Max(0, timer);

            int minutos = Mathf.FloorToInt(tiempoSeguro / 60f);
            int segundos = Mathf.FloorToInt(tiempoSeguro % 60f);

            timerText.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }

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

    public void AddScore(int puntosASumar)
    {
        score += puntosASumar;
        ActualizarTextoScore();
    }

    // Refresca el texto de los puntos en la pantalla
    void ActualizarTextoScore()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puntos: " + score;
        }
    }

    void EndDay()
    {
        dayEnded = true;
        Debug.Log("Día " + currentDay + " completado.");

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
}