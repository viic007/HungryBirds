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
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (instance != this)
        {
          
            instance.scoreText = this.scoreText;
            instance.timerText = this.timerText;
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string nombre = scene.name.ToLower();

        if (!nombre.Contains("transicion") && !nombre.Contains("menu") && !nombre.Contains("gameover"))
        {
            timer = dayDuration;
            dayEnded = false;
            ActualizarTextoScore();
        }
    }

    void Start()
    {
        timer = dayDuration;
        dayEnded = false;
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

    public void AddScore(int puntosASumar)
    {
        score += puntosASumar;
        ActualizarTextoScore();
    }

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

        string escenaActual = SceneManager.GetActiveScene().name.ToLower();

        if (escenaActual.Contains("3") || escenaActual.Contains("win"))
        {
            SceneManager.LoadScene("Ending");
        }

        else if (escenaActual.Contains("2"))
        {
            currentDay = 3;
            SceneManager.LoadScene("Day3");
        }

        else if (escenaActual.Contains("1") || escenaActual.Contains("lvl1") || escenaActual.Contains("day1"))
        {
            currentDay = 2;
            SceneManager.LoadScene("Day2");
        }
        else
        {
            SceneManager.LoadScene("Ending");
        }
    }
}