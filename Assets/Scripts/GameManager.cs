using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Control del Tiempo y Días")]
    public int currentDay = 1;
    public float dayDuration = 80f;
    private float timer;
    private bool dayEnded = false;

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